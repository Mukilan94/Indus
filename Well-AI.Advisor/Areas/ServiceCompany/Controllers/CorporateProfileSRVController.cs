using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Finbuckle.MultiTenant;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.Model.OperatingCompany.Models;
using System.Collections.Generic;
using WellAI.Advisor.DLL.Entity;
using Newtonsoft.Json;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class CorporateProfileSRVController : BaseController
    {
        private readonly ILogger<CorporateProfileSRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _hostingEnvironment;
        public CorporateProfileSRVController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, ILogger<CorporateProfileSRVController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                //checking invalid user//
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                ServiceCorporateProfile model = null;
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
               var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                model = await commonbus.GetServiceCorporateProfileByTenant(tenantId);
                string filename = Path.GetFileName(model.LogoPath);
                if (filename != null && filename != "")
                    model.LogoPath = await GetUrlOfImage(filename);
                var resultCompany = commonbus.GetCompanyDetailByTenant(tenantId);
                if(resultCompany != null)
                {
                    model.ServiceCategories = resultCompany.Category;
                }
                var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var categories = await auctionProposalBusiness.GetServiceCategorys();
                var StateList= (from USA in db.USAStates
                                    select new USAState {
                                        StateId = USA.StateId,
                                        Name = USA.Name
                                    }).OrderBy(o => o.Name).ToList();
                ViewData["StateList"] = StateList;
                ViewData["categories"] = categories;
                return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CorporateProfileSRV Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task<string> GetUrlOfImage(string filename)
        {
            var ti = HttpContext.GetMultiTenantContext().TenantInfo;
            var blobSection = _configuration.GetSection("AzureBlob");
            var folderName = _configuration.GetSection("FolderName");
            var items = await AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                blobSection["ContainerPrefixName"], ti.Id, folderName["CompanyUserProfile"], filename);
            return items;
        }
        [HttpPost]
        public async Task<ActionResult> Update(ServiceCorporateProfile input)
        {
            try
            {
                if (ModelState.IsValid || input.ID == null)
                {
                    var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                    var welluser = await _userManager.GetUserAsync(User);
                    var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    if (input.logofiles != null)
                    {
                        string result = Task.Run(async () => await SaveFile(input.logofiles)).Result;
                        input.LogoPath = result;
                    }
                    else
                    {
                        input.LogoPath = null;
                    }
                    var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                    var categories = await auctionProposalBusiness.GetServiceCategorys();
                    if (input.ServiceCategories != null)
                    {
                        List<Model.ServiceCompany.Models.CompanyServicesModel> cServices = new List<Model.ServiceCompany.Models.CompanyServicesModel>();
                        string[] serviceCategoryList = input.ServiceCategories.Split(";");
                        if (serviceCategoryList.Length > 0)
                        {
                            var serviceResults = categories.Where(x => serviceCategoryList.Contains(x.ServiceCategoryId));
                            cServices = (from cs in serviceResults
                                              select new Model.ServiceCompany.Models.CompanyServicesModel
                                              {
                                                  ServiceName = cs.Name
                                              }
                                              ).ToList();

                            if (serviceCategoryList.Length > 0)
                            {
                                input.CServices = JsonConvert.SerializeObject(cServices);
                            }
                        }
                                                
                    }
                    var res = await commonbus.UpdateServiceCorporateProfile(input, welluser.Id, tenantId);
                    var res2 = commonbus.UpdateCompanyCategories(input.ServiceCategories, tenantId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV Update", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<JsonResult> SetSelectedCategories()
        {
            var result = new List<object>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var categories = await auctionProposalBusiness.GetServiceCategorys();
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var company = commonBusiness.GetCompanyDetailByTenant(tenantId);
                if (!string.IsNullOrEmpty(company.Category))
                {
                    var userCats = company.Category.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (var category in categories)
                    {
                        if (userCats.Any(x => x == category.ServiceCategoryId.ToString()))
                        {
                            result.Add(new { ServiceCategoryId = category.ServiceCategoryId, Name = category.Name });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CorporateProfileSRV OnGetSetSelectedCategories", User.Identity.Name);
            }

            return Json(result);
        }
        private string GetFileInfo(IFormFile files)
        {
            string result = Task.Run(async () => await SaveFile(files)).Result;
            return result;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            object result = null;
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var folderName = _configuration.GetSection("FolderName");
                    var blobSection = _configuration.GetSection("AzureBlob");
                    result = await AzureBlobStorage.UploadFileToBlobContainerWithFileName(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, file, folderName["CompanyProfile"], ti.Id);
                    string AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    System.Type type = result.GetType();
                    Uri docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                    return Path.GetFileName(docUri.OriginalString);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV SaveFile", User.Identity.Name);
            }
            return "";
        }
        private async Task<string> UploadCorporateLogo(string logoPath, string id)
        {
            var result = "";
            try
            {
                if (!string.IsNullOrEmpty(logoPath) && logoPath.Length > 0)
                {
                    var split = logoPath.Split(new[] { ',' });
                    var filebytes = Convert.FromBase64String(split[1]);
                    string logoFolder = _configuration.GetValue<string>("Documents:LogoPath").Replace('/', '\\');
                    if (!Directory.Exists(logoFolder))
                        Directory.CreateDirectory(logoFolder);
                    var ext = ".png";
                    if (split[0].StartsWith("data:image/jpg;base64", StringComparison.InvariantCulture))
                        ext = ".jpg";
                    else if (split[0].StartsWith("data:image/jpeg;base64", StringComparison.InvariantCulture))
                        ext = ".jpeg";
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath + logoFolder + id + ext);
                    using (var filestream = new FileStream(Path.Combine(filePath), FileMode.Create))
                    {
                        await filestream.WriteAsync(filebytes, 0, filebytes.Length);
                        filestream.Flush();
                        filestream.Close();
                    }
                    result = logoFolder + id + ext;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV UploadCorporateLogo", User.Identity.Name);
            }
            return result;
        }
        private async Task<string> DownloadCorporateLogo(string logoPath)
        {
            var result = "";
            if (!string.IsNullOrEmpty(logoPath) && logoPath.Length > 0)
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath + logoPath);
                byte[] temp = null;
                try
                {
                    using (var filestream = new FileStream(Path.Combine(filePath), FileMode.Open))
                    {
                        temp = new byte[filestream.Length];
                        await filestream.ReadAsync(temp, 0, (int)filestream.Length);
                        filestream.Flush();
                        filestream.Close();
                    }
                    if (temp != null && temp.Length > 0)
                    {
                        var extIndex = logoPath.LastIndexOf(".");
                        var ext = logoPath.Substring(extIndex + 1, logoPath.Length - extIndex - 1);
                        result = "data:image/" + ext + ";base64," + Convert.ToBase64String(temp);
                    }
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "CorporateProfileSRV DownloadCorpLogo", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                }
            }
            return result;
        }

        private bool GetComponentsBasedOnRole()
        {
            var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            List<string> rolesName = (from rl in roles
                                      select rl.Value
                                 ).ToList();

            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
            var rolesResult = roleResult.Result;
            if (roleResult != null)
            {
                List<string> roleIds = (from rl in rolesResult
                                        select rl.Id
                                        ).ToList();
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "CorporateProfile", TenantId);
            }
            else
            {
                return false;
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
    }

    
}