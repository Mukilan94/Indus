using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    public class UploadController : BaseController
    {
        private readonly ILogger<UploadController> _logger;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        public UploadController(UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration,
           ILogger<UploadController> logger)
            : base(userManager, dbContext)
        {
            _userManager = userManager;
            db = dbContext;
            _logger = logger;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public IActionResult Index(int sid)
        {
            try
            {
                var model = new UploadsModel
                {
                    sid = sid
                };
                return View(model);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload Index", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> DrillPrograms()
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                var uwells = await AIBusiness.GetUserAssignedWells(userwell.WellUser.HasValue && userwell.WellUser.Value ? userwell.Id : null, userwell.TenantId);
                for (var i = 0; i < uwells.Count; i++)
                {
                    uwells[i].wellId += ":" + uwells[i].wellName;
                }
                ViewData["wells"] = uwells;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload DrillPrograms", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> MudPrograms()
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                var uwells = await AIBusiness.GetUserAssignedWells(userwell.WellUser.HasValue && userwell.WellUser.Value ? userwell.Id : null, userwell.TenantId);
                for (var i = 0; i < uwells.Count; i++)
                {
                    uwells[i].wellId += ":" + uwells[i].wellName;
                }
                ViewData["wells"] = uwells;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload MudPrograms", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> CementPrograms()
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                var uwells = await AIBusiness.GetUserAssignedWells(userwell.WellUser.HasValue && userwell.WellUser.Value ? userwell.Id : null, userwell.TenantId);
                for (var i = 0; i < uwells.Count; i++)
                {
                    uwells[i].wellId += ":" + uwells[i].wellName;
                }
                ViewData["wells"] = uwells;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload CementPrograms", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> DrillPermits()
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                var uwells = await AIBusiness.GetUserAssignedWells(userwell.WellUser.HasValue && userwell.WellUser.Value ? userwell.Id : null, userwell.TenantId);
                for (var i = 0; i < uwells.Count; i++)
                {
                    uwells[i].wellId += ":" + uwells[i].wellName;
                }
                ViewData["wells"] = uwells;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload DrillPermits", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> Msas()
        {
            try
            {
                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                var vendors = await commonBusiness.GetServiceCompanies();
                for (var i = 0; i < vendors.Count; i++)
                {
                    vendors[i].ID += ":" + vendors[i].Name;
                }
                ViewData["vendors"] = vendors;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload Msas", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> DrillPrograms_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var dpfiles = await commonBusiness.GetWellFilesFromTenant(tenantId, "Drill Programs");
                return Json(dpfiles.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DrillPrograms_Read Upload", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadDrillPrograms(UploadFileWellModelDrill input, IFormCollection form)
        {
            try
            {
                if (!string.IsNullOrEmpty(input.WellIdDrill))
                {
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var wellidname = input.WellIdDrill.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    var path = "Well/" + wellidname[1].Replace("/", ".").Replace("\\", ".") + "/Drill Programs";
                    foreach (var file in form.Files)
                    {
                        var result = await SaveFile(file, path.Replace("%20", " "), wellidname[0], null);
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Upload", action = "Index", sid = 0 }));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload UploadDrillPrograms", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadMudPrograms(UploadFileWellModelMud input, IFormCollection form)
        {
            try
            {
                if (!string.IsNullOrEmpty(input.WellIdMud))
                {
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var wellidname = input.WellIdMud.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    var path = "Well/" + wellidname[1].Replace("/", ".").Replace("\\", ".") + "/Mud Programs";
                    foreach (var file in form.Files)
                    {
                        var result = await SaveFile(file, path.Replace("%20", " "), wellidname[0], null);
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Upload", action = "Index", sid = 1 }));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload UploadMudPrograms", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> MudPrograms_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var dpfiles = await commonBusiness.GetWellFilesFromTenant(tenantId, "Mud Programs");
                return Json(dpfiles.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "MudPrograms_Read Upload", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadCementPrograms(UploadFileWellModelCement input, IFormCollection form)
        {
            try
            {
                if (!string.IsNullOrEmpty(input.WellIdCement))
                {
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var wellidname = input.WellIdCement.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    var path = "Well/" + wellidname[1].Replace("/", ".").Replace("\\", ".") + "/Cement Programs";
                    foreach (var file in form.Files)
                    {
                        var result = await SaveFile(file, path.Replace("%20", " "), wellidname[0], null);
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Upload", action = "Index", sid = 2 }));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload UploadCementPrograms", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> CementPrograms_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var dpfiles = await commonBusiness.GetWellFilesFromTenant(tenantId, "Cement Programs");
               return Json(dpfiles.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CementPrograms_Read Upload", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadDrillPermits(UploadFileWellModelPermits input, IFormCollection form)
        {
            try
            {
                if (!string.IsNullOrEmpty(input.WellIdPermits))
                {
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var wellidname = input.WellIdPermits.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    var path = "Well/" + wellidname[1].Replace("/", ".").Replace("\\", ".") + "/Drill Permits";
                    foreach (var file in form.Files)
                    {
                        var result = await SaveFile(file, path.Replace("%20", " "), wellidname[0], null);
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Upload", action = "Index", sid = 3 }));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload UploadDrillPermits", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> UploadsDrillPrograms_Destroy(string fileId, string well)
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                try
                {
                    bool deleted = false;
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var filename = await commonBusiness.RemoveWellFile(fileId);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                        var path = "Well/" + well.Replace("/", ".").Replace("\\", ".") + "/Drill Programs";
                        var blobSection = _configuration.GetSection("AzureBlob");
                        deleted = await AzureBlobStorage.Delete(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                            blobSection["ContainerPrefixName"], tenantId, path.Replace("%20", " "), filename);
                    }
                   return Json(new[] { deleted });
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "Uploads UploadsDrillPrograms_Destroy", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
            }
            return null;
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> CementDrillPrograms_Destroy(string fileId, string well)
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                try
                {
                    bool deleted = false;
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                   var filename = await commonBusiness.RemoveWellFile(fileId);
                   if (!string.IsNullOrEmpty(filename))
                    {
                        string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                        var path = "Well/" + well.Replace("/", ".").Replace("\\", ".") + "/Cement Programs";
                        var blobSection = _configuration.GetSection("AzureBlob");
                        deleted = await AzureBlobStorage.Delete(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenantId, path.Replace("%20", " "), filename);
                    }
                    return Json(new[] { deleted });
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "Uploads UploadsDrillPrograms_Destroy", User.Identity.Name);
                   _logger.LogInformation(ex.Message);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
            }
            return null;
        }
        public async Task<IActionResult> DrillPermits_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var dpfiles = await commonBusiness.GetWellFilesFromTenant(tenantId, "Drill Permits");
                return Json(dpfiles.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
               customErrorHandler.WriteError(ex, "DrillPermits_Read Upload", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadMsas(UploadFileVendorModel input, IFormCollection form)
        {
            try
            {
                if (!string.IsNullOrEmpty(input.VendorId))
                {
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var vendoridname = input.VendorId.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    var path = "Vendor/" + vendoridname[1].Replace("/", ".").Replace("\\", ".") + "/MSAs";
                    foreach (var file in form.Files)
                    {
                        var filename = db.WellFiles.Where(x => x.FileName == file.FileName && x.Category == path).Count();
                        if (filename > 0)
                        {
                            ModelState.AddModelError("Error", "This file already existing");
                        }
                        else
                        {
                            var fileId = await SaveFile(file, path.Replace("%20", " "), null, vendoridname[0]);
                            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                            var success = await commonBusiness.UpdateProviderMSALinkWellFile(tenantId, fileId, vendoridname[0], input.ExpireDate, input.ActiveStatus);
                        }
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Upload", action = "Index", sid = 4 }));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload UploadMsas", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> Msas_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var dpfiles = await commonBusiness.GetVendorFilesFromTenant(tenantId, "MSAs");
               return Json(dpfiles.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "MSAs_Read Upload", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> Msas_Update([DataSourceRequest] DataSourceRequest request, UploadsGridFileModel input)
        {
            try
            {
                if (input != null && ModelState.IsValid)
                {
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var success = await commonBusiness.UpdateProviderMSALinkWellFile(tenantId, input.FileId, input.VendorId, input.Expire, input.IsActive);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler =
                    new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Msas_Update Upload", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        protected async Task<string> SaveFile(IFormFile file, string pathToSave, string wellId, string vendorId)
        {
            string result = "";
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var blobSection = _configuration.GetSection("AzureBlob");
                var uploaded = await AzureBlobStorage.UploadFileToBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], tenantId, file, pathToSave);
                System.Type type = uploaded.GetType();
                var docUri = (Uri)type.GetProperty("uri").GetValue(uploaded, null);
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                result = Guid.NewGuid().ToString("D");
                await commonBusiness.CreateWellFile(new DLL.Entity.WellFile
                {
                    FileName = file.FileName,
                    TenantId = tenantId,
                    Category = pathToSave,
                    UserId = userId,
                    Date = DateTime.Now,
                    WellId = wellId,
                    VendorId = vendorId,
                    FileId = result
               });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload SaveFile", User.Identity.Name);
            }
            return result;
        }
        [HttpGet]
        public async Task<IActionResult> Download(string fileId)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var filebytes = new KeyValuePair<string, byte[]>();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = await commonBusiness.GetWellFileById(fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                var blobSection = _configuration.GetSection("AzureBlob");
                filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], tenantId, path);
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = msadocument.FileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload Download", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
    }
}