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
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Microsoft.AspNetCore.Routing;
using Kendo.Mvc.Extensions;
using WellAI.Advisor.DLL.Repository;
using System.Net;
using Telerik.Web.PDF;
using WellAI.Advisor.DLL.Entity;
using Well_AI.Advisor.Log.Audit;
using System.Globalization;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class DocumentManagerSRVController : BaseController
    {
        private TenantOperatingDbContext _tdbContext;
        private readonly ILogger<DocumentManagerSRVController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly WebAIAdvisorContext db;
        private readonly TenantServiceDbContext _servicedb;
        public IActionResult Index()
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
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRV Index", User.Identity.Name);
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "DataManager", TenantId);
            }
            else
            {
                return false;
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
                customErrorHandler.WriteError(ex, "DocumentManagerSRV CreateFolderAndReload", User.Identity.Name);
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
        public DocumentManagerSRVController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, TenantServiceDbContext servicedb, ILogger<DocumentManagerSRVController> logger, IConfiguration configuration)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            db = dbContext;
            _servicedb = servicedb;
        }
        /// <summary>
        /// Call new folder dialog
        /// </summary>
        /// <param name="path">Opened path of folders</param>
        /// <param name="selected">Selected folder but not opened or empty if file is selected on nothing is seleced</param>
        /// <returns></returns>
        public async Task<IActionResult> NewFolderContent(string path, string selected)
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
                return View("_NewFolder", await Task.FromResult(output));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRV NewFolderContent", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
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
        public async Task<ActionResult> Upload(FileManagerEntry newEntry, IFormFile file, string FileName)
        {
            try
            {
                var path = newEntry.Path;
                if (string.IsNullOrEmpty(path))
                    return null;
                var result = await SaveFile(file, path.Replace("%20", " "));
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRV Upload", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
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
                        FileId = Guid.NewGuid().ToString("D"),
                        Date = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRV SaveFile", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return result;
        }
        /// Phase II Changes - 03/02/2021 - Definition from Operating Company
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
                        var folderForAccount = folders.Where(x => x.AccountType == 1).ToList();
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
                        if (item.Key != "") // files from root of container
                        {
                            //Phase II Changes - get Date and time from Azure
                            DateTimeOffset lastModified = new DateTimeOffset();
                            DateTimeOffset createdDate = new DateTimeOffset();

                            var itemKey = item.Key.Replace("%20", " ");
                            var foundpath = "";
                            for (var i = item.Value.Count - 1; i >= 0; i--)
                            {
                                CloudBlockBlob blobItem = (CloudBlockBlob)item.Value[i];

                                createdDate = (DateTimeOffset)blobItem.Properties.Created;
                                lastModified = (DateTimeOffset)blobItem.Properties.LastModified;
                                createdDate = (DateTimeOffset)blobItem.Properties.Created;

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
                                //Phase II Changes - added lastModified
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
                                            if (splits.Length == 2)
                                            {
                                                var ext = splits[splits.Length - 1];
                                                var extindex = cloudFile.Name.LastIndexOf("." + ext);
                                                var name = cloudFile.Name.Substring(0, extindex).Replace("//", "/");
                                                files.Add(new FileManagerEntry
                                                {
                                                    Name = name.Replace(target + "/", "").Replace("%20", " "),
                                                    Extension = "." + ext,
                                                    Size = cloudFile.Properties.Length,
                                                   // Created = cloudFile.Propertie.Value.DateTime.ToLocalTime(),
                                                    //Modified = cloudFile.Properties.Value.DateTime.ToLocalTime(),
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

            }
            catch (DirectoryNotFoundException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRV Read", User.Identity.Name);
            }
            var result = files.Concat(dirs).Select(VirtualizePath);
            return Json(result.ToArray());
        }
        [HttpGet]
        public async Task<FileResult> Download(string path)
        {
            var filebytes = new KeyValuePair<string, byte[]>();
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, path);
                }
                if (filebytes.Key == "" || filebytes.Value.Length == 0)
                    return null;
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRV Download", User.Identity.Name);
            }
            return File(filebytes.Value, filebytes.Key);
        }
        /// <summary>
        /// Phase II Changes - 03/01/2021 - Destroy Azure File,Folder function definition from Operating Company
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public async Task<ActionResult> Destroy(FileManagerEntry entry, bool IsDirectory, string FileName)
        {
            var blobSection = _configuration.GetSection("AzureBlob");
            string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            if (!string.IsNullOrEmpty(entry.Path))
            {
                var path = entry.Path.Replace("%20", " ");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                if (IsDirectory)
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
                return Json(new { status = true});
            }
            throw new Exception("File Not Found");
        }
        protected virtual void DeleteFile(string path)
        {
            try
            {
                var physicalPath = path;
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRVController DeleteFile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
            }
        }
        protected virtual void DeleteDirectory(string path)
        {
            var physicalPath = path;
            if (Directory.Exists(physicalPath))
            {
                Directory.Delete(physicalPath, true);
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
        /// <exception cref="HttpException">Forbidden</exception>
        public ActionResult Update(string target, FileManagerEntry entry)
        {
            try
            {
                FileManagerEntry newEntry;
                newEntry = RenameEntry(entry);
                return Json(newEntry);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRVController Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        protected FileManagerEntry RenameEntry(FileManagerEntry entry)
        {
            var path = entry.Path;
            var physicalPath = path;
            FileManagerEntry newEntry = new FileManagerEntry();
            return newEntry;
        }
        public virtual string GetFileName(IFormFile file)
        {
            try
            {
                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                return Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRVController GetFileName", User.Identity.Name);
                _logger.LogInformation(ex.Message);
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
        public async Task<IActionResult> Msas()
        {
            string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
            var operators = await operrepo.GetProviderDirectoriesByTenantId(tenantId);
            for (var i = 0; i < operators.Count; i++)
            {
                operators[i].CompanyId += ":" + operators[i].Name;
            }
            ViewData["operators"] = operators;
            return View();
        }

        [HttpGet]
        public IActionResult GetMSAfile_Validation(string Vendor, string FileName)
        {
            try
            {
                var VendorDetails = Vendor.Split(":", StringSplitOptions.RemoveEmptyEntries);
                var CategoryName = "Vendor/" + VendorDetails[1].Replace("/", ".").Replace("\\", ".") + "/MSAs";
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var Vendorid = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();
                List<string> TenantIds = new List<string>();
                TenantIds.Add(Vendorid.ID);
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
        public async Task<IActionResult> UploadMsas(UploadFileOperatorModel input, IFormCollection form)
        {
            try
            {
                if (!string.IsNullOrEmpty(input.OperatorId))
                {
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var operidname = input.OperatorId.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var model = await commonBusiness.GetCorporateProfileByTenant(tenantId);
                    var path = "Vendor/" + operidname[1].Replace("/", ".").Replace("\\", ".") + "/MSAs";
                    var blobSection = _configuration.GetSection("AzureBlob");           
                    var isExits = db.WellFiles.Where(x => x.VendorId == operidname[0] && x.Category == path).ToList();
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

                        var tId = Guid.Parse(operidname[0]);
                        var dbprefix = "oper";
                        var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                               dbprefix + "db_" + tId.ToString("N"));

                        var ti = new TenantInfo(operidname[0], operidname[0], operidname[0], connString, null);
                        var operContext = new TenantOperatingDbContext(ti);
                        _tdbContext = operContext;

                        var Vendor = _tdbContext.ProvidersDirectory.Where(x => x.CompanyId == tenantId).FirstOrDefault();
                        Vendor.MSA = null;
                        Vendor.Preferred = Convert.ToByte(1);
                        _tdbContext.SaveChanges();

                        var ServiceProvider = _servicedb.OperatingDirectory.Where(x => x.CompanyId == operidname[0]).FirstOrDefault();
                        ServiceProvider.MSA = null;
                        _servicedb.SaveChanges();
                    }


                    foreach (var file in form.Files)
                    {
                        var filename = db.WellFiles.Where(x => x.FileName == file.FileName && x.Category == path).Count();
                        if (filename > 0)
                        {
                            ModelState.AddModelError("Error", "This file already exist");
                        }
                        else
                        {
                            var fileId = await SaveFile(file, path.Replace("%20", " "), null, operidname[0]);
                            var success = await commonBusiness.UpdateProviderMSALinkWellFileService(operidname[0], fileId, tenantId, input.ExpireDate, input.ActiveStatus);
                            var Vendorid = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();

                            var IsOperatorMSA = db.WellFiles.Where(x => x.VendorId == Vendorid.ID).ToList();

                            if (IsOperatorMSA != null)
                            {
                                //var FileId = isExits.Select(x => x.FileId).ToList();
                                foreach (var msa in IsOperatorMSA)
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
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DocumentManagerSRV", action = "Index" }));
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
        public async Task<IActionResult> MSAs_Destroy(string fileId, string well)
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                try
                {
                    bool deleted = false;
                    bool isFileApproved = false;
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var category = "Vendor/" + well.Replace("/", ".").Replace("\\", ".") + "/MSAs";
                    var WellFileExisting = db.WellFiles.Where(x => x.FileId == fileId && x.Category == category).FirstOrDefault();
                    if (!string.IsNullOrEmpty(WellFileExisting.FileId))
                    {
                        var file = db.ProviderMSALinks.Where(x => x.FileId == fileId).FirstOrDefault();
                        isFileApproved = Convert.ToBoolean(file.IsApproved) ? true : false;
                        //await commonBusiness.RemoveWellFile(fileId);
                    }
                    //Phase II Changes 02/08/2021
                    if (isFileApproved)
                    {
                        return Json(new {FileApproved = isFileApproved, VendorName = well, FileName = WellFileExisting.FileName });
                    }
                    else
                    {
                        //Phase II Changes 02/08/2021- remove from ProviderLinkMSA table
                        var filename = await commonBusiness.RemoveWellFile(fileId);
                        if (!string.IsNullOrEmpty(filename))
                        {
                            string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                            var path = "Well/" + well.Replace("/", ".").Replace("\\", ".") + "/MSAs";
                            var blobSection = _configuration.GetSection("AzureBlob");
                            deleted = await AzureBlobStorage.Delete(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                            blobSection["ContainerPrefixName"], tenantId, path.Replace("%20", " "), filename);
                        }
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
        public async Task<IActionResult> Msas_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var dpfiles = await commonBusiness.GetVendorFilesFromServiceTenant(tenantId, "MSAs");
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
                if (input != null /*&& ModelState.IsValid*/)
                {
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var success = await commonBusiness.UpdateProviderMSALinkWellFileServiceEdit(input.FileId, tenantId, input.Expire, input.IsActive);
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

                var OperDetails = db.CorporateProfile.Where(x => x.TenantId == vendorId).FirstOrDefault();
                var VendorDetails = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();

                var dbprefix = "oper";
                var servguid = new Guid(OperDetails.TenantId);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);

                var OperDBContext = new TenantOperatingDbContext(ti);
                var servdb = new OperatingTenantRepository(OperDBContext, HttpContext, _userManager);

                var Entity = OperDBContext.ProvidersDirectory.Where(x => x.CompanyId == tenantId).FirstOrDefault();
                MessageQueue messageQueue = new MessageQueue { From_id = userId, To_id = OperDetails.UserId, EntityId = Entity.ID, Type = Convert.ToInt32(7), IsActive = 1, TaskName = "MSA Document  :" + " " + file.FileName + ",Uploaded by :" + VendorDetails.Name, JobName = "MSA Document Upload", CreatedDate = DateTime.Now };
                db.MessageQueues.Add(messageQueue);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload SaveFile", User.Identity.Name);
            }
            return result;
        }

        /// <summary>
        /// Well AI Phase II changes - //Open or View Operating Company Pdf Document files in PdfViewer - 01/28/2021
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fileId"></param>
        /// <param name="TenId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = db.WellFiles.FirstOrDefault(x => x.FileId == fileId);
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

        //Phase II Changes - 03/02/2021
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

        //Phase II Changes - 03/03/2021
        /// <summary>
        /// Check for File Exists
        /// </summary>
        /// <param name="azureFilePath"></param>
        /// <returns></returns>
        public async Task<JsonResult> IsFileExistsInCloud(FileManagerEntry entry,string filePath)
        {
            bool fileExistFlag = true;
            CustomAuditLogHandler customAuditHandler = new CustomAuditLogHandler(db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
            try
            {
                customAuditHandler.WriteAuditLog("Check File exist in Cloud", "", User.Identity.Name);
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



        public async Task<JsonResult> MSAFileExits(FileManagerEntry entry,bool IsDirectory, string FileName)
        {
            bool fileExist = true;
            try
            {

                bool isFileApproved = true;
                var blobSection = _configuration.GetSection("AzureBlob");
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var path = entry.Path.Replace("%20", " ");

                if (!string.IsNullOrEmpty(entry.Path))
                {
                    if (IsDirectory)
                    {
                        var items = await AzureBlobStorage.GetFilesBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                            blobSection["ContainerPrefixName"], tenantId, path);

                        if (items != null)
                        {
                            foreach (var file in items.Values)
                            {
                                foreach (var item in file)
                                {
                                    var cloudFile = item as CloudBlockBlob;
                                    if (cloudFile != null)
                                    {
                                        //var ExitsFileDetails = cloudFile.Name.Split("/", StringSplitOptions.RemoveEmptyEntries).SkipLast(1);
                                        string[] temp = cloudFile.Name.Split('/');
                                        string last = "/" + temp.Last();
                                        string ExitsFileDetails = cloudFile.Name.Replace(last, "");
                                        var FileName1 = temp.Last();
                                        var FilePath = cloudFile.Name;
                                        if (!string.IsNullOrEmpty(FilePath))
                                        {
                                            //var category = "Vendor/" + FileDetails[1].Replace("/", ".").Replace("\\", ".") + "/MSAs";
                                            var WellFileExisting = db.WellFiles.Where(x => x.FileName == FileName1 && x.Category == ExitsFileDetails).FirstOrDefault();
                                            if (WellFileExisting != null)
                                            {
                                                var files = db.ProviderMSALinks.Where(x => x.FileId == WellFileExisting.FileId).FirstOrDefault();
                                                isFileApproved = Convert.ToBoolean(files.IsApproved) ? true : false;
                                                var VendorName = db.CorporateProfile.Where(x => x.TenantId == WellFileExisting.VendorId).Select(y => y.Name).FirstOrDefault();
                                                if (isFileApproved)
                                                {
                                                    // await commonBusiness.RemoveWellFile(WellFileExisting.FileId);
                                                    return Json(new { IsApproved = isFileApproved, VendorName = VendorName, directory = true, Folder = FileName });
                                                    //}
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] temp = entry.Path.Split('/');
                        string last = "/" + temp.Last();
                        string category = entry.Path.Replace(last, "");
                        FileName = temp.Last();
                        ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                        fileExist = await AzureBlobStorage.IsFileExist(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                                    blobSection["ContainerPrefixName"], tenantId, path);

                        if (fileExist)
                        {
                            // var category = "Vendor/" + FileDetails[1].Replace("/", ".").Replace("\\", ".") + "/MSAs";
                            var WellFileExisting = db.WellFiles.Where(x => x.FileName == FileName && x.Category == category).FirstOrDefault();
                            if (WellFileExisting != null)
                            {
                                var file = db.ProviderMSALinks.Where(x => x.FileId == WellFileExisting.FileId).FirstOrDefault();
                                if (file != null)
                                {
                                    isFileApproved = Convert.ToBoolean(file.IsApproved) ? true : false;
                                }
                                else
                                {
                                    isFileApproved = false;
                                }
                                // await commonBusiness.RemoveWellFile(WellFileExisting.FileId);
                                var VendorName = db.CorporateProfile.Where(x => x.TenantId == WellFileExisting.VendorId).Select(y => y.Name).FirstOrDefault();

                                return Json(new { IsApproved = isFileApproved, FileName = WellFileExisting.FileName, VendorName = VendorName });

                            }

                        }
                    }
                }
                return Json(new { IsApproved = false });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "MSAFileExits", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json(new { });
            }
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResult> UploadInsurance(UploadFileOperatorModel input, IFormCollection form)
        {
            try
            {
                var blobSection = _configuration.GetSection("AzureBlob");
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                string OperTenantId = form["Operators"];
                DateTime ExpireDate = DateTime.ParseExact(form["ExpireDate"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(form["ExpireDate"]);
                var isExits = db.WellFiles.Where(x => x.TenantId == tenantId && x.Category == "Insurance" && x.VendorId == OperTenantId).ToList();
                if (isExits.Count > 0)
                {
                    var FileId = isExits.Select(x => x.FileId).ToList();
                    foreach (var ids in FileId)
                    {
                        var Getpath = db.WellFiles.Where(x => x.FileId == ids).FirstOrDefault();

                        var deleted = await AzureBlobStorage.DeleteFileByPath(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                            blobSection["ContainerPrefixName"], tenantId, "Insurance" + "/" + Getpath.FileName);

                    }
                    db.WellFiles.RemoveRange(isExits);
                    db.SaveChanges();
                }

                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                foreach (var file in form.Files)
                {
                    var fileId = await SaveFile(file, "Insurance", OperTenantId);
                    var insuranceRes = await commonBusiness.UpdateProviderInsuranceLink(tenantId, OperTenantId, Convert.ToString(fileId), ExpireDate);

                    var tId = Guid.Parse(OperTenantId);
                    var dbprefix = "oper";
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + tId.ToString("N"));

                    var ti1 = new TenantInfo(OperTenantId, OperTenantId, OperTenantId, connString, null);
                    var operContext = new TenantOperatingDbContext(ti1);
                    _tdbContext = operContext;
                    var provider = _tdbContext.ProvidersDirectory.Where(x => x.CompanyId == tenantId).FirstOrDefault();
                    provider.Insurance = Convert.ToString(fileId);
                    provider.InsuranceExpire = ExpireDate;
                    _tdbContext.SaveChanges();

                    var OperatingDirectoryData = _servicedb.OperatingDirectory.Where(x => x.CompanyId == OperTenantId).FirstOrDefault();
                    OperatingDirectoryData.Insurance = Convert.ToString(fileId);
                    OperatingDirectoryData.InsuranceExpire = ExpireDate;
                    _servicedb.SaveChanges();
                }

                //var id = _operdb.ProvidersDirectory.Where(x => x.CompanyId == CompanyId).FirstOrDefault(); ;

                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DocumentManagerSRV", action = "Index" }));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Upload", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }


        protected async Task<object> SaveFile(IFormFile file, string pathToSave, string OperTenantId)
        {
            object result = null;
            var FileId = "";
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    result = await AzureBlobStorage.UploadFileToBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, file, pathToSave);
                    System.Type type = result.GetType();
                    var docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                    var userId = _userManager.GetUserId(User);
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var Savefile = (new DLL.Entity.WellFile
                    {
                        FileName = file.FileName,
                        TenantId = Convert.ToString(tenantId),
                        Category = pathToSave,
                        UserId = userId,
                        FileId = Guid.NewGuid().ToString("D"),
                        Date = DateTime.Now,
                        VendorId = OperTenantId
                    });

                    db.WellFiles.Add(Savefile);
                    db.SaveChanges();

                    FileId = Savefile.FileId;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory SaveFile", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }

            return FileId;
        }

        public IActionResult Insurance_ReadForGrid([DataSourceRequest] DataSourceRequest request, string CompanyId)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var docs = db.WellFiles.Where(x => x.TenantId == tenantId && x.Category == "Insurance").ToList();
                var result = new List<UploadsGridFileModel>();

                foreach (var doc in docs)
                {
                    var vendor = db.CorporateProfile.FirstOrDefault(x => x.TenantId == doc.VendorId);
                    var providerInsuranceLink = db.ProviderInsuranceLinks.FirstOrDefault(x => x.FileId == doc.FileId && x.ServiceTenantId == tenantId);

                    if (vendor != null)
                    {
                        var newItem = new UploadsGridFileModel
                        {
                            FileId = doc.FileId,
                            FileName = doc.FileName,
                            Date = (DateTime?)doc.Date.Value,
                            WellName = vendor.Name
                        };
                        //result.Add(newItem);

                        if (providerInsuranceLink != null)
                        {
                            newItem.Expire = providerInsuranceLink.Expire.Value;
                            newItem.VendorId = vendor.ID;

                            result.Add(newItem);
                        }
                    }
                }
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Insurance_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return null;
            }
        }


        [HttpGet]
        public IActionResult GetInsurancefile_Validation(string Vendor, string FileName)
        {
            try
            {
                //var VendorDetails = Vendor.Split(":", StringSplitOptions.RemoveEmptyEntries);
                var CategoryName = "Insurance";
                if (Vendor != null)
                {
                    int WellFiledetails1 = db.WellFiles.Where(x => x.Category == CategoryName && x.VendorId == Vendor).Count();
                    if (WellFiledetails1 > 0)
                    {
                        return Json(new { Status1 = "True" });
                    }
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
        public async Task<IActionResult> Insurance_Destroy(string fileId)
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                try
                {
                    bool deleted = false;
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var category = "Insurance";
                    var WellFileExisting = db.WellFiles.Where(x => x.FileId == fileId && x.Category == category).FirstOrDefault();

                    //Phase II Changes 02/08/2021- remove from ProviderLinkMSA table
                    var filename = await commonBusiness.RemoveWellFile(fileId);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                        var path = "Insurance";
                        var blobSection = _configuration.GetSection("AzureBlob");
                        deleted = await AzureBlobStorage.DeleteFileByPath(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenantId, path + "/" + filename);
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

        public async Task<IActionResult> Insurance()
        {

            return View();
        }


        public async Task<JsonResult> GetOperators(string text)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var operators = await operrepo.GetProviderDirectoriesByTenantId(tenantId);
                return Json(operators);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DocumentManagerSRV GetOperators", User.Identity.Name);
                return Json("");
            }

        }

    }
}