using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Tenant;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private readonly IMultiTenantStore _store;

        public ConfirmEmailModel(UserManager<WellIdentityUser> userManager,
            SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            WebAIAdvisorContext dbContext,
            IConfiguration configuration,
            IMultiTenantStore store)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
            _configuration = configuration;
            _store = store;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Code { get; set; }

            public string UserId { get; set; }
        }

        public async Task<IActionResult> OnPost(string userId)
        {
            var tUserId = Encoding.UTF8.GetString(Convert.FromBase64String(userId));
            var user = await _userManager.FindByIdAsync(tUserId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var commonBusiness = new CommonBusiness(db, roleManager, _userManager);

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var detail = commonBusiness.GetUserBasicDetail(user.Id);

                var tenantid = Guid.NewGuid();
              
                WellAIAppContext.Current.Session.SetString("TenantId", tenantid.ToString());
                var tenantExistId = await commonBusiness.GetTenantIdByUserId(user.Id);
                if (string.IsNullOrEmpty(tenantExistId))
                {
                    user.TenantId = tenantid.ToString("D");
                    var result1 = await _userManager.UpdateAsync(user);
                    //CorporateProfile corporateProfile = new CorporateProfile()
                    //{
                    //    ID = Guid.NewGuid().ToString(),
                    //    UserId = user.Id,
                    //    Name = detail.Company,
                    //    Phone = user.PhoneNumber,
                    //    TenantId = user.TenantId
                    //};
                    //db.CorporateProfile.Add(corporateProfile);
                    //db.SaveChanges();
                    await commonBusiness.CreateTenantUser(tUserId, tenantid.ToString("D"));

                    var blobSection = _configuration.GetSection("AzureBlob");

                    await AzureBlobStorage.EnsureBlobContainerForTenant(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenantid.ToString("D"));

                    var folders = await commonBusiness.GetWellFileFolders();
                    var folderForAccount = folders.Where(x => x.AccountType == detail.AccountType).ToList();

                    await AzureBlobStorage.EnsureFolderTreeInContainerForTenant(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenantid.ToString("D"), folderForAccount);

                    var dbprefix = "oper";
                    if (detail.AccountType == 1)
                        dbprefix = "serv";

                    //Create Tenant DB only if not Dispatch type
                    if (detail.AccountType != 2)
                    {
                        var newDbName = "wellai_" + dbprefix + "db_" + tenantid.ToString("N");
                        var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", newDbName);

                        var repo = new TenantRepository(_configuration.GetConnectionString("WebAIAdvisorContextConnection"));
                        var resnewdb = await repo.CreateTenantDB(newDbName);

                        commonBusiness.CreateWellTenant(tenantid.ToString("D"), connString, tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"));
                        
                        var ti = new TenantInfo(tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"), connString, null);

                        if (detail.AccountType == 0)
                        {
                            using (var appcontext = new TenantOperatingDbContext(ti))
                            {
                                appcontext.Database.EnsureCreated();
                            }
                        }
                        else
                        {
                            using (var appcontext = new TenantServiceDbContext(ti))
                            {
                                appcontext.Database.EnsureCreated();
                            }
                        }
                    }
                    else
                    {
                        var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "");

                        var repo = new TenantRepository(_configuration.GetConnectionString("WebAIAdvisorContextConnection"));
                        //var resnewdb = await repo.CreateTenantDB(newDbName);
                         commonBusiness.CreateWellTenant(tenantid.ToString("D"), connString, tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"));
                        var ti = new TenantInfo(tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"), connString, null);
                        await commonBusiness.CreateTenantRoles(tenantid.ToString("D"));
                    }
                                        
                    var r = _store as MultiTenantStoreWrapper<TenantConfigurationStore>;

                    var id = Guid.NewGuid().ToString("D");

                    var res = await r.Store.TryUpdateAsync(new TenantInfo(id, id, id, "", null));

                    UpdateTenentId(tenantid.ToString(), detail.CorporateProfileId.ToString());
                }

                //if (detail.AccountType == 1)
                //{
                //    commonBusiness.UpdateUserPagesCompleteStatus(3, user.Id);
                //    return new JsonResult("/Identity/Account/Company?userId=" + tUserId);
                //}
                //else
                //{
                    commonBusiness.UpdateUserPagesCompleteStatus(8, user.Id);
                   return new JsonResult("/Identity/Account/Login");
                //}
            }
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }


        public async Task<bool> UpdateTenentId(string  _tenantId, string CorporateProfileId)
        {
            try
            {
                // var _tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                var result = db.Subscription.Where(x => x.CorporateProfileId == CorporateProfileId);
                if(result!=null)
                {

                    foreach (var record in result)
                    {
                        record.TenantId = _tenantId;
                      
                    }
                   
                 db.SaveChanges();

                }

                var result2 = db.CorporateProfile.Where(x => x.ID == CorporateProfileId);
                if (result2 != null)
                {

                    foreach (var record2 in result2)
                    {
                        record2.TenantId = _tenantId;

                    }

                    db.SaveChanges();

                }

                //update tenantid to subscriotion table
                //ProductSubscriptionModel productSubscriptionModel = new ProductSubscriptionModel();
                //productSubscriptionModel.TenantId = _tenantId;

                //db.Subscription.Add(productSubscriptionModel);
                //db.Entry(productSubscriptionModel).State = EntityState.Modified;
                //db.SaveChanges();

                ////update tenantid to corporateProfile table
                //CorporateProfile corporateProfile = new CorporateProfile();
                //corporateProfile.TenantId = _tenantId;

                //db.CorporateProfile.Add(corporateProfile);
                //db.Entry(corporateProfile).State = EntityState.Modified;
                //db.SaveChanges();

            }
            catch (Exception ex)
            {

                return false;
            }


            return true;
        }


        public IActionResult OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            Input = new InputModel
            {
                UserId = userId,
                Code = code
            };

            return Page();
        }
    }
}
