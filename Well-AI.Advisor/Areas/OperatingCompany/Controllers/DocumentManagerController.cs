using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.Identity;
using Microsoft.Extensions.Configuration;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Finbuckle.MultiTenant;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using WellAI.Advisor.Areas.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Well_AI.Advisor.Log.Error;
using System.Net;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Telerik.Web.PDF;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using Well_AI.Advisor.Log.Audit;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class DocumentManagerController : Controller
    {
        private readonly ILogger<DocumentManagerController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly WebAIAdvisorContext db;
        private TenantOperatingDbContext _tdbContext;
        private TenantServiceDbContext _ServContext;

        public IActionResult Index(int sid)
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                //checking invalid user
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var model = new UploadsModel
                {
                    sid = sid
                };
                return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "DocumentManager",TenantId);
            }
            else
            {
                return false;
            }
        }

        public DocumentManagerController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, TenantOperatingDbContext tdbContext, ILogger<DocumentManagerController> logger, IConfiguration configuration)
        //: base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            db = dbContext;
            _tdbContext = tdbContext;
        }
        /// <summary>
        /// Call new folder dialog
        /// </summary>
        /// <param name="path">Opened path of folders</param>
        /// <param name="selected">Selected folder but not opened or empty if file is selected on nothing is seleced</param>
        /// <returns></returns>
        public IActionResult NewFolderContent(string path, string selected)
        {
            try
            {
                string parpath = "";
                parpath = string.IsNullOrEmpty(selected) ? path : selected;
                if (parpath == null)
                    parpath = "/";
                var parentPath = WebUtility.UrlDecode(parpath);
                var output = new DocumentManagerNewFolderModel
                {
                    ParentPath = parpath,
                };
                return View("_NewFolder", output);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager NewFolderContent", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> CreateFolderAndReload([DataSourceRequest] DataSourceRequest request, DocumentManagerNewFolderModel input, IFormCollection form)
        {
            if (string.IsNullOrEmpty(input.NewFolderName) || string.IsNullOrEmpty(input.ParentPath))
                return RedirectToAction("Index");
            try
            {
                await CreateNewFolder(input.ParentPath + "/" + input.NewFolderName);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager CreateFolderAndReload", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl); ;
            }
        }
        public async Task<ActionResult> Create(string target, FileManagerEntry entry)
        {
            try
            {
                FileManagerEntry newEntry = null;
                if (String.IsNullOrEmpty(entry.Path))
                {
                    await CreateNewFolder(target + "/" + entry.Name);
                }
                else
                {
                }
                return Json(newEntry);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager Create", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }
        }
        private async Task CreateNewFolder(string target)
        {
            var ti = HttpContext.GetMultiTenantContext().TenantInfo;
            if (ti != null)
            {
                var blobSection = _configuration.GetSection("AzureBlob");
                await AzureBlobStorage.CreateNewFolderInContainerForTenant(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, target.TrimStart('/'));
            }
        }
        /// <summary>
        /// Uploads a file to a given path.
        /// </summary>
        /// <param name="path">The path to which the file should be uploaded.</param>
        /// <param name="file">The file which should be uploaded.</param>
        /// <returns>A <see cref="JsonResult"/> containing the uploaded file's size and name.</returns>
        /// <exception cref="HttpException">Forbidden</exception>
        [AcceptVerbs("POST")]
        public async Task<ActionResult> Upload(string path, IFormFile file, FileManagerEntry entry)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    return null;
                var result = await SaveFile(file, path.Replace("%20", " "));

                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager Upload", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }

        }
        protected async Task<object> SaveFile(IFormFile file, string pathToSave)
        {
            object result = null;
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");

                    result = await AzureBlobStorage.UploadFileToBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, file, pathToSave);




                    System.Type type = result.GetType();
                    var docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                    var userId = _userManager.GetUserId(User);
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    await commonBusiness.CreateWellFile(new DLL.Entity.WellFile
                    {
                        FileName = file.FileName,
                        TenantId = ti.Id,
                        Category = pathToSave,
                        UserId = userId,
                        FileId = Guid.NewGuid().ToString("D")
                    });
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager SaveFile", User.Identity.Name);
            }
            return result;
        }
        private string GetPathOfFolder(string[] segments, string prefix, string tenantId)
        {
            var result = "";
            for (var i = 0; i < segments.Length; i++)
            {
                var curpath = segments[i].Replace("/", "");
                if (curpath != "" && curpath != prefix + tenantId && curpath != WellAI.Advisor.DLL.Constants.AzureReadmeFile)
                {
                    result += curpath + "/";
                }
            }
            return result.TrimEnd('/');
        }
        public async Task<JsonResult> Read(string target)
        {
            List<FileManagerEntry> files = new List<FileManagerEntry>();
            List<FileManagerEntry> dirs = new List<FileManagerEntry>();
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                var blobSection = _configuration.GetSection("AzureBlob");
                if (target == null)
                {
                    if (ti != null)
                    {
                        var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                        var folders = await commonBusiness.GetWellFileFolders();
                        var folderForAccount = folders.Where(x => x.AccountType == 0).ToList();
                        await AzureBlobStorage.EnsureAndCheckFolderTreeInContainerForTenant(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, folderForAccount);
                    }
                    target = "";
                }
                else
                    target = target.Replace("%20", " ");
                if (ti != null)
                {
                    var items = await AzureBlobStorage.GetFilesBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, target);
                    foreach (var item in items)
                    {
                        //Phase II Changes - get Date and time from Azure
                        DateTimeOffset createdDate = new DateTimeOffset();
                        DateTimeOffset lastModified = new DateTimeOffset();

                        if (item.Key != "") // files from root of container
                        {
                            var itemKey = item.Key.Replace("%20", " ");
                            var foundpath = "";
                            for (var i = item.Value.Count - 1; i >= 0; i--)
                            {
                                CloudBlockBlob blobItem = (CloudBlockBlob)item.Value[i];

                                createdDate = (DateTimeOffset)blobItem.Properties.Created;
                              
                                lastModified = (DateTimeOffset)blobItem.Properties.LastModified;
                              

                                var paths = item.Value[i].Uri.Segments;
                                if (paths[paths.Length - 1] == WellAI.Advisor.DLL.Constants.AzureReadmeFile)
                                {
                                    var curpath = paths[paths.Length - 2].Replace("/", "");
                                    if (curpath == itemKey)
                                    {
                                        foundpath = GetPathOfFolder(item.Value[i].Uri.Segments, blobSection["ContainerPrefixName"], ti.Id);
                                        break;
                                    }
                                }
                            }
                            if (foundpath == "" && target != itemKey)
                                foundpath = target + "/" + itemKey;
                            if (target != itemKey)
                            {
                                dirs.Add(new FileManagerEntry
                                {
                                    Name = itemKey,
                                    IsDirectory = true,
                                    Path = foundpath,
                                    Extension = "",
                                    Created = createdDate.DateTime,
                                    CreatedUtc = createdDate.UtcDateTime,
                                    HasDirectories = false,
                                    Modified = lastModified.DateTime,
                                    ModifiedUtc = lastModified.UtcDateTime,
                                    Size = 0
                                });
                            }
                            else
                            {
                                foreach (var file in item.Value)
                                    if (!file.Uri.AbsoluteUri.EndsWith(WellAI.Advisor.DLL.Constants.AzureReadmeFile))
                                    {
                                        var cloudFile = file as CloudBlockBlob;
                                        if (cloudFile != null)
                                        {
                                            var splits = cloudFile.Name.Split(".");
                                            var ext = splits[splits.Length - 1];
                                            var extindex = cloudFile.Name.LastIndexOf("." + ext);
                                            var name = cloudFile.Name.Substring(0, extindex).Replace("//", "/");
                                          //  var DateTime = cloudFile.Properties.LastModified.Value.DateTime;
                                            files.Add(new FileManagerEntry
                                            {
                                                Name = name.Replace(target + "/", "").Replace("%20", " "),
                                                Extension = "." + ext,
                                                Size = cloudFile.Properties.Length,
                                                Created = cloudFile.Properties.Created.Value.DateTime.ToLocalTime(),
                                                Modified = cloudFile.Properties.LastModified.Value.DateTime.ToLocalTime(),
                                                Path = name + "." + ext
                                            });
                                        }
                                    }
                            }
                        }
                    }
                }

            }
            catch (DirectoryNotFoundException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager Read", User.Identity.Name);
            }
            var result = files.Concat(dirs).Select(VirtualizePath);
            return Json(result.ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> Download(string path)
        {
            try
            {
                var filebytes = new KeyValuePair<string, byte[]>();
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, path);
                }
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                var split = path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = split[split.Length - 1],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager Download", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<ActionResult> Destroy(FileManagerEntry entry)
        {
            //Get the file name and Tenant Id, Service Tenant Id
            //MSAs/WellOperatingFile.xlsx
            try
            {
                var blobSection = _configuration.GetSection("AzureBlob");
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                if (!string.IsNullOrEmpty(entry.Path))
                {
                    var path = entry.Path.Replace("%20", " ");
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    if (entry.IsDirectory)
                    {
                        var filenames = await AzureBlobStorage.DeleteFolder(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                            blobSection["ContainerPrefixName"], tenantId, path);
                        await commonBusiness.RemoveWellFilesByName(filenames, tenantId);
                    }
                    else
                    {
                        var deleted = await AzureBlobStorage.DeleteFileByPath(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                            blobSection["ContainerPrefixName"], tenantId, path);
                        await commonBusiness.RemoveWellFileByName(path, entry.Name + entry.Extension, tenantId);
                    }
                    return Json(new object[0]);
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager Destroy", User.Identity.Name);
                return null;
            }
        }
        protected virtual FileManagerEntry VirtualizePath(FileManagerEntry entry)
        {
            var ti = HttpContext.GetMultiTenantContext().TenantInfo;
            if (ti != null)
            {
                var blobSection = _configuration.GetSection("AzureBlob");
                entry.Path = entry.Path.Replace(blobSection["ContainerPrefixName"] + ti.Id, "").Replace(@"\", "/").TrimStart('/');
                return entry;
            }
            return entry;
        }
        /// <summary>
        /// Updates an entry with a given entry.
        /// </summary>
        /// <param name="path">The path to the parent folder in which the folder should be created.</param>
        /// <param name="entry">The entry.</param>
        /// <returns>An empty <see cref="ContentResult"/>.</returns>
        public async Task<ActionResult> Update(string target, FileManagerEntry entry)
        {
            try
            {
                var newfolderName = entry.Name;
                var oldFolderPath = entry.Path;
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    if (entry.IsDirectory)
                    {
                        await AzureBlobStorage.RenameFolderInContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, oldFolderPath.Replace("%20", " "), newfolderName.Replace("%20", " "));
                    }
                    else
                    {
                    }
                }
                return Json(entry);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager Update", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public virtual string GetFileName(IFormFile file)
        {
            try
            {
                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                return Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager GetFileName", User.Identity.Name);
                return null;
            }
        }
        private void CopyFolder(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            foreach (var file in Directory.EnumerateFiles(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(file));
                System.IO.File.Copy(file, dest);
            }
            foreach (var folder in Directory.EnumerateDirectories(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(folder));
                CopyFolder(folder, dest);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Uploads
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager DrillPrograms", User.Identity.Name);
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager MudPrograms", User.Identity.Name);
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager CementPrograms", User.Identity.Name);
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager DrillPermits", User.Identity.Name);
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager Msas", User.Identity.Name);
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
                    var path = "Well/" + wellidname[1].Replace("/", ".").Replace("\\", ".").Replace("%20", " ") + "/Drill Programs";
                    foreach (var file in form.Files)
                    {
                        var filename = db.WellFiles.Where(x => x.FileName == file.FileName && x.Category == path).Count();
                        if (filename > 0)
                        {
                            ModelState.AddModelError("Error", "This file already exists");
                        }
                        else
                        {
                            var result = await SaveFile(file, path, wellidname[0], null);
                        }
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DocumentManager", action = "Index", sid = 0 }));
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
                        var filename = db.WellFiles.Where(x => x.FileName == file.FileName && x.Category == path).Count();
                        if (filename > 0)
                        {
                            ModelState.AddModelError("Error", "This file already exists");
                        }
                        else
                        {
                            var result = await SaveFile(file, path.Replace("%20", " "), wellidname[0], null);
                        }
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DocumentManager", action = "Index", sid = 1 }));
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
                        var filename = db.WellFiles.Where(x => x.FileName == file.FileName && x.Category == path).Count();
                        if (filename > 0)
                        {
                            ModelState.AddModelError("Error", "This file already exists");
                        }
                        else
                        {
                            var result = await SaveFile(file, path.Replace("%20", " "), wellidname[0], null);
                        }
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DocumentManager", action = "Index", sid = 2 }));
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
                        var filename = db.WellFiles.Where(x => x.FileName == file.FileName && x.Category == path).Count();
                        if (filename > 0)
                        {
                            ModelState.AddModelError("Error", "This file already exists");
                        }
                        else
                        {
                            var result = await SaveFile(file, path.Replace("%20", " "), wellidname[0], null);
                        }
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DocumentManager", action = "Index", sid = 3 }));
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

        public async Task<IActionResult> MudPrograms_Destroy(string fileId, string well)
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
                        var path = "Well/" + well.Replace("/", ".").Replace("\\", ".") + "/Mud Programs";
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
        public async Task<IActionResult> DrillPermit_Destroy(string fileId, string well)
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
                        var path = "Well/" + well.Replace("/", ".").Replace("\\", ".") + "/Drill Permits";
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
        public async Task<IActionResult> MSAs_Destroy(string fileId, string well)
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
                        var path = "Well/" + well.Replace("/", ".").Replace("\\", ".") + "/MSAs";
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
        [HttpGet]
        public IActionResult GetMSAfile_Validation(string Vendor, string FileName)
        {
            try
            {
                var VendorDetails = Vendor.Split(":", StringSplitOptions.RemoveEmptyEntries);
                var CategoryName = "Vendor/" + VendorDetails[1].Replace("/", ".").Replace("\\", ".") + "/MSAs";
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                List<string> TenantIds = new List<string>();
                TenantIds.Add(tenantId);
                TenantIds.Add(VendorDetails[0]);

                int WellFiledetails1 = db.WellFiles.Where(x => TenantIds.Contains(x.VendorId)).Count();
                //int WellFiledetails1 = db.WellFiles.Where(x => x.Category == CategoryName).Count();
                if (WellFiledetails1 > 0)
                {
                    return Json(new { Status1 = "True" });
                }
                int WellFiledetails = db.WellFiles.Where(x => x.FileName == FileName && x.Category == CategoryName).Count();
                return Json(new { Status = WellFiledetails == 0 ? "False" : "True" });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload GetMSAfile_Validation", User.Identity.Name);
                _logger.LogInformation(ex.Message);
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
                    var pathToFolder = "Vendor/" + vendoridname[1].Replace("/", ".").Replace("\\", ".") + "/Pricing Agreements";
                    var blobSection = _configuration.GetSection("AzureBlob");
                    await AzureBlobStorage.CreateNewFolderInContainerForTenant(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                           blobSection["ContainerPrefixName"], tenantId, pathToFolder);

                    //List<string> TenantIds = new List<string>();
                    //TenantIds.Add(tenantId);
                    //TenantIds.Add(vendoridname[0]);
                    //var isExits = db.WellFiles.Where(x => TenantIds.Contains(x.VendorId)).ToList();

                    var isExits = db.WellFiles.Where(x => x.VendorId == vendoridname[0] && x.Category == path).ToList();

                    if (isExits.Count > 0)
                    {
                        var FileId = isExits.Select(x => x.FileId).ToList();
                        foreach (var id in FileId)
                        {
                            var Getpath = db.WellFiles.Where(x => x.FileId == id).FirstOrDefault();
                            var MSA = db.ProviderMSALinks.Where(X => X.FileId == id).FirstOrDefault();
                            if (MSA != null)
                            {
                                var deleted = await AzureBlobStorage.DeleteFileByPath(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                                    blobSection["ContainerPrefixName"], tenantId, Getpath.Category + "/" + Getpath.FileName);

                                db.ProviderMSALinks.Remove(MSA);
                                db.SaveChanges();
                            }
                        }
                        db.WellFiles.RemoveRange(isExits);
                        db.SaveChanges();

                        var ServiceTenantId = db.CorporateProfile.Where(x => x.ID == vendoridname[0]).FirstOrDefault();

                        var tId = Guid.Parse(ServiceTenantId.TenantId);
                        var dbprefix = "serv";
                        var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                               dbprefix + "db_" + tId.ToString("N"));

                        var ti = new TenantInfo(ServiceTenantId.TenantId, ServiceTenantId.TenantId, ServiceTenantId.TenantId, connString, null);
                        var ServContext = new TenantServiceDbContext(ti);
                        _ServContext = ServContext;

                        var Vendor = _ServContext.OperatingDirectory.Where(x => x.CompanyId == tenantId).FirstOrDefault();
                        Vendor.MSA = null;
                        _tdbContext.SaveChanges();

                        var ServiceProvider = _tdbContext.ProvidersDirectory.Where(x => x.CompanyId == ServiceTenantId.TenantId).FirstOrDefault();
                        ServiceProvider.MSA = null;
                        _tdbContext.SaveChanges();
                    }

                    foreach (var file in form.Files)
                    {
                        var filename = db.WellFiles.Where(x => x.FileName == file.FileName && x.Category == path).Count();
                        if (filename > 0)
                        {
                            ModelState.AddModelError("Error", "This file already exists");

                            ViewBag.Error = "True";

                        }
                        else
                        {
                            var fileId = await SaveFile(file, path.Replace("%20", " "), null, vendoridname[0]);
                            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                            var success = await commonBusiness.UpdateProviderMSALinkWellFile(tenantId, fileId, vendoridname[0], input.ExpireDate, input.ActiveStatus);

                            var IsServiceMSA = db.WellFiles.Where(x => x.VendorId == tenantId).ToList();

                            if (IsServiceMSA != null)
                            {
                                //var FileId = isExits.Select(x => x.FileId).ToList();
                                foreach (var msa in IsServiceMSA)
                                {
                                    msa.FileName = file.FileName;
                                    msa.Date = DateTime.Now;
                                    db.SaveChanges();

                                    var ProMSA = db.ProviderMSALinks.Where(X => X.FileId == msa.FileId).FirstOrDefault();
                                    if (ProMSA != null)
                                    {
                                        ProMSA.IsActive = false;
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DocumentManager", action = "Index", sid = 4 }));
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

                var VendorDetails = db.CorporateProfile.Where(x => x.ID == vendorId).FirstOrDefault();
                var oprDetails = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();

                var dbprefix = "serv";
                var servguid = new Guid(VendorDetails.TenantId);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);

                var servDBContext = new TenantServiceDbContext(ti);
                var servdb = new ServiceTenantRepository(servDBContext, HttpContext, _userManager, db);

                var Entity = servDBContext.OperatingDirectory.Where(x => x.CompanyId == tenantId).FirstOrDefault();
                MessageQueue messageQueue = new MessageQueue { From_id = userId, To_id = VendorDetails.UserId, EntityId = Entity.ID, Type = Convert.ToInt32(7), IsActive = 1, TaskName = "MSA Document:" + " " + file.FileName + ", Uploaded By :" + oprDetails.Name, JobName = "MSA Document Upload", CreatedDate = DateTime.Now };
                db.MessageQueues.Add(messageQueue);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload SaveFile", User.Identity.Name);
            }
            return result;
        }


        [HttpGet]
        public IActionResult GetPdfFileForDocumentContext(int? pageNumber, string Filepath, string FileName)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                string[] category = Filepath.Split("/", StringSplitOptions.RemoveEmptyEntries);
                var msadocument = db.WellFiles.FirstOrDefault(x => x.Category == category[0] && x.FileName == category[1]);
                var path = msadocument.Category + "/" + msadocument.FileName;
                var blobSection = _configuration.GetSection("AzureBlob");
                var filebyte = AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                         blobSection["ContainerPrefixName"], msadocument.TenantId, msadocument.Category, msadocument.FileName);

                JsonResult jsonResult;
                WebClient client = new WebClient();
                byte[] arr = client.DownloadData(filebyte.Result);
                FixedDocument doc = FixedDocument.Load(arr);
                if (pageNumber == null)
                {
                    jsonResult = Json(doc.ToJson());
                }
                else
                {
                    jsonResult = Json(doc.GetPage((int)pageNumber));
                }

                return jsonResult;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentMergeSRVController GetPdfFile", User.Identity.Name);
                return null;
            }
        }


        /// <summary>
        /// Well AI Phase II changes - //Open or View Operating Company Pdf Document files in PdfViewer - 01/28/2021
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fileId"></param>
        /// <param name="TenId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = await commonBusiness.GetWellFileById(fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                var blobSection = _configuration.GetSection("AzureBlob");
                var filebyte = AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                         blobSection["ContainerPrefixName"], msadocument.TenantId, msadocument.Category, msadocument.FileName);

                JsonResult jsonResult;
                WebClient client = new WebClient();
                byte[] arr = client.DownloadData(filebyte.Result);
                FixedDocument doc = FixedDocument.Load(arr);
                if (pageNumber == null)
                {
                    jsonResult = Json(doc.ToJson());
                }
                else
                {
                    jsonResult = Json(doc.GetPage((int)pageNumber));
                }

                return jsonResult;
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManager GetPdfFile", User.Identity.Name);
                return null;
            }
        }


        [HttpGet]
        public async Task<IActionResult> DownloadUploadFile(string fileId,string TenId)
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
                customErrorHandler.WriteError(ex, "Upload DownloadUploadFile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        #endregion
        //Phase II Changes - 03/03/2021
        /// <summary>
        /// Check for File Exists
        /// </summary>
        /// <param name="azureFilePath"></param>
        /// <returns></returns>
        public async Task<JsonResult> IsFileExistsInCloud(string filePath)
        {
            bool fileExistFlag = true;
            CustomAuditLogHandler customAuditHandler = new CustomAuditLogHandler(db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
            try
            {
                customAuditHandler.WriteAuditLog("Check File exist in Cloud", "",User.Identity.Name);
                var blobSection = _configuration.GetSection("AzureBlob");
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                if (!string.IsNullOrEmpty(filePath))
                {
                    var path = filePath.Replace("%20", " ");
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                    fileExistFlag = await AzureBlobStorage.IsFileExist(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                                blobSection["ContainerPrefixName"], tenantId, path);
                }
                return Json(fileExistFlag);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "IsFileExistsInCloud", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json(fileExistFlag);
            }
        }
    }
}