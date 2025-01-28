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
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Finbuckle.MultiTenant;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class CorporateProfileController : Controller
    {
        private readonly ILogger<CorporateProfileController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _hostingEnvironment;
        public CorporateProfileController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, ILogger<CorporateProfileController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
            //: base(userManager, dbContext)
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
                        
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                CorporateProfile model = null;
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);


                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                model = await commonbus.GetCorporateProfileByTenant(ti.Id);

                string logofilesNull = model.LogoPath;
                if (model.LogoPath != null)
                    model.LogoPath = await GetUrlOfImage(model.LogoPath);
                AddCorporateProfile corporateProfile = new AddCorporateProfile
                {
                    Address1=model.Address1,
                    Address2=model.Address2,
                    City=model.City,
                    Country=model.Country,
                    ID=model.ID,
                    TenantId=model.TenantId,
                    LogoPath=model.LogoPath,
                    Name=model.Name,
                    Phone=model.Phone,
                    State=model.State,
                    Website=model.Website,
                    UserId=model.UserId,
                    Zip=model.Zip,
                    CServices=model.CServices,
                    logofilesNull= logofilesNull
                };
                var Statelist = (from Usa in db.USAStates
                                 select new USAState
                                 {
                                     StateId = Usa.StateId,
                                     Name = Usa.Name
                                 }).OrderBy(o => o.Name).ToList();
                ViewData["Statelist"] = Statelist;
                return View(corporateProfile);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Corporate Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(AddCorporateProfile input)
        {
            try
            {
                if (ModelState.IsValid || input.ID == null)
                {
                    var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                    var welluser = await _userManager.GetUserAsync(User);
                    var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                    if (input.logofiles != null)
                    {
                        string filename = await SaveFile(input.logofiles);
                        input.LogoPath = filename;
                    }
                    else
                    {
                        input.LogoPath = input.logofilesNull;
                    }
                    CorporateProfile corporateProfile = JsonConvert.DeserializeObject<CorporateProfile>(JsonConvert.SerializeObject(input));
                    var res = await commonbus.UpdateCorporateProfile(corporateProfile, welluser.Id, ti == null ? "00000000-0000-0000-0000-000000000000": ti.Id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Corporate Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<JsonResult> GetCorporateProfileAsync()
        {
            try
            {
                CorporateProfile model = null;
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                model = await commonbus.GetCorporateProfileByTenant(ti.Id);
                string logofilesNull = model.LogoPath;
                if (model.LogoPath != null)
                    model.LogoPath = await GetUrlOfImage(model.LogoPath);
                AddCorporateProfile corporateProfile = new AddCorporateProfile
                {
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    Country = model.Country,
                    ID = model.ID,
                    TenantId = model.TenantId,
                    LogoPath = model.LogoPath,
                    Name = model.Name,
                    Phone = model.Phone,
                    State = model.State,
                    Website = model.Website,
                    UserId = model.UserId,
                    Zip = model.Zip,
                    CServices = model.CServices,
                    logofilesNull = logofilesNull
                };
                return Json(corporateProfile);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Corporate GetCorporateProfileAsync", User.Identity.Name);
                return null;
            }
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            object result = null;
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;

                //if (WellAIAppContext.Current.Session.GetString("AccountType") == "2")
                //{
                //    //Image will save using Profile id for Dispatch type
                //    var profileId = WellAIAppContext.Current.Session.GetString("CorporateProfileId").ToString();
                //    var folderName = _configuration.GetSection("FolderName");
                //    var blobSection = _configuration.GetSection("AzureBlob");

           

                //    result = await AzureBlobStorage.UploadFileToBlobContainerWithFileName(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                //        blobSection["ContainerPrefixName"], profileId, file, folderName["CompanyProfile"], profileId);

                //    string AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //    System.Type type = result.GetType();
                //    Uri docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                //    return Path.GetFileName(docUri.OriginalString);
                //}
                //else
                //{
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
                //}
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV SaveFile", User.Identity.Name);
            }
            return "";
        }
        private async Task<string> GetUrlOfImage(string filename)
        {
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                //if (WellAIAppContext.Current.Session.GetString("AccountType") == "2")
                //{
                //    var profileId = WellAIAppContext.Current.Session.GetString("CorporateProfileId").ToString();
                //    var blobSection = _configuration.GetSection("AzureBlob");
                //    var folderName = _configuration.GetSection("FolderName");
                //    var items = await AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                //        blobSection["ContainerPrefixName"], profileId, folderName["CompanyProfile"], filename);
                //    return items;
                //}
                //else
                //{
                    var blobSection = _configuration.GetSection("AzureBlob");
                    var folderName = _configuration.GetSection("FolderName");
                    var items = await AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, folderName["CompanyProfile"], filename);
                    return items;
                //}                    
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "EditProfile Index", User.Identity.Name);
                return string.Empty;
            }
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "CorporateProfile",TenantId);
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

