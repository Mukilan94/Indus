using Finbuckle.MultiTenant;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using Well_AI.Advisor.API.Samsara.Services;
using Well_AI.Advisor.API.Samsara.Services.IServices;
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.BLL;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using System.Net;
using Telerik.Web.PDF;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class UpcomingProjectsSRVController : BaseController
    {
        private readonly ILogger<UpcomingProjectsSRVController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISingleton singleton;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        public UpcomingProjectsSRVController(UserManager<WellIdentityUser> userManager,
                                            SignInManager<WellIdentityUser> signInManager,RoleManager<IdentityRole> roleManager,
                                            ISingleton singleton,WebAIAdvisorContext dbContext, ILogger<UpcomingProjectsSRVController> logger, IConfiguration configuration)
            : base(userManager, dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.singleton = singleton;
            _signInManager = signInManager;
            db = dbContext;
            _logger = logger;
            _configuration = configuration;
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
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                ProjectDashboardSerViewModel projectDashboard = new ProjectDashboardSerViewModel();
                var userId = _userManager.GetUserId(User);
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                {
                    string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                    var operId = Request.Cookies["operfilterlayout"].ToString();
                    IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                  //  projectDashboard = await projectBusiness.ProjectDashboardSerTenantId(tenantId, operId);
                    projectDashboard = await projectBusiness.ProjectDashboardSerTenantId(tenantId, operId);

                }
                return View(projectDashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        //Phase-II Changes
        public async Task<IActionResult> Counts()
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                ProjectDashboardSerViewModel projectDashboard = new ProjectDashboardSerViewModel();
                var userId = _userManager.GetUserId(User);
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                {
                    string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                    var operId = Request.Cookies["operfilterlayout"].ToString();
                    IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                    projectDashboard = await projectBusiness.ProjectDashboardSerTenantId(tenantId, operId);
                }
                return Json(projectDashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV Counts", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
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
            //var roleResult = rolePermissionBusiness.GetRoleByName(roleName);
            var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
            var rolesResult = roleResult.Result;
            if (roleResult != null)
            {
                List<string> roleIds = (from rl in rolesResult
                                        select rl.Id
                                        ).ToList();

                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "UpcomingProjects",TenantId);//,TenantId
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUpcommingProjectDetails(ProjectViewSRVModel input, IFormCollection form)
        {
            try
            {
                string statusName = form["btnSubmit"];
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                input.ProjectStatusName = statusName;
                var result = await projectBusiness.UpdateUpCommingProjectsDetails(input);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                return RedirectToAction("Index", new RouteValueDictionary(
                           new { controller = "UpcomingProjectsSRV", action = "UpcomingProject_Read", Id = tenantId }));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV UpdateUpcommingProjectDetails", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> UpcomingProject_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ProjectViewSRVModel> result = new List<ProjectViewSRVModel>();
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var operId = Request.Cookies["operfilterlayout"].ToString();
                result = await projectBusiness.GetUpCommingProjectsSRV(tenantId, operId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV UpcomingProject_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        public async Task<IActionResult> ProjectDetails(string id)
        {
            ProjectViewSRVModel result = new ProjectViewSRVModel();
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                result = await projectBusiness.GetUpCommingProjectsDetailsByTenantIdForSRV(tenantId, id);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV ProjectDetails", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return View(result);
        }
        public async Task<IActionResult> GetProjectProposalAttachments([DataSourceRequest] DataSourceRequest request, string proposalId, string projectId, string operTenantId)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var result = await projectBusiness.GetProjectProposalAttachments(operTenantId, proposalId, projectId);
                return Json(result.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV GetProjectProposalAttachments", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetAssignedTechnicianByProjectId([DataSourceRequest] DataSourceRequest request, string projectId)
        {
            List<TechnicianViewModel> result = new List<TechnicianViewModel>();
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                result = await projectBusiness.GetAssignedTechnicianByProjectId(projectId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV GetProjectProposalAttachments", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        public JsonResult GetTechnicianByTenantid()
        {
            List<TechnicianViewModel> result = new List<TechnicianViewModel>();
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _roleManager, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                result = projectBusiness.GetTechnicianByTenantid(tenantId).Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV GetTechnicianByTenantid", User.Identity.Name);
            }
            return Json(result);
        }
        public JsonResult GetVehicleByTenantid()
        {
            List<ServiceVehicleViewModel> result = new List<ServiceVehicleViewModel>();
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _roleManager, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                result = singleton.serviceVehicleBusiness.GetServiceVehiclesList(tenantId).Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV GetVehicleByTenantid", User.Identity.Name);
            }
            return Json(result);
        }

        /// <summary>
        /// Well AI Phase II changes - //Open or View Operating Company Pdf Document files in PdfViewer - 01/28/2021
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fileId"></param>
        /// <param name="TenId"></param>
        /// <returns></returns>
        [HttpGet]
        //Phase-II Changes
        public IActionResult GetPdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = db.ProjectAttachments.Find(fileId);
                var blobSection = _configuration.GetSection("AzureBlob");
                var filebyte = AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                         blobSection["ContainerPrefixName"], msadocument.TenantID, msadocument.FilePatch, msadocument.FileName);

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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV GetPdfFile", User.Identity.Name);
                return null;
            }
        }
        //Phase-II Changes
        [HttpGet]
        public async Task<FileResult> Download(string fileId,String TenId)
        {
            try
            {
                var result = db.ProjectAttachments.Find(fileId);
                var filebytes = new KeyValuePair<string, byte[]>();
                var blobSection = _configuration.GetSection("AzureBlob");
                filebytes = await AzureBlobStorage.DownloadFilesFromBlobBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], result.TenantID, result.FilePatch, result.FileName);
                //if (filebytes.Key == "" || filebytes.Value.Length == 0)
                //    return null;

                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = result.FileName.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV Download", User.Identity.Name);
                return null;
            }
        }
        //Phase-II Changes
        public async Task<IActionResult> AddMoreAttachment_Create([DataSourceRequest] DataSourceRequest request, AddProjectAttachmentViewModel input)
        {
            try
            {
                input.TenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                if (input.files != null)
                {
                    await GetFileInfo(input.files, input.ProposalId, input.ProjectId, input.ProjectCode, input.OperatorTenantId, input.Note);
                }
                return Json(new[] { true });
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV Download", User.Identity.Name);
                return null;
            }
        }
        private Task<bool> GetFileInfo(IEnumerable<IFormFile> files, string ProposalID, string ProjectId, string ProjectCode, string TenantID, string notes)
        {
            List<ProjectAttachment> fileInfo = new List<ProjectAttachment>();
            foreach (var file in files)
            {
                var folderName = _configuration.GetSection("FolderName");
                var result = Task.Run(async () => await SaveFile(file, folderName["Projects"] + "/" + ProjectCode, ProjectId, ProposalID, TenantID, notes)).Result;
                fileInfo.Add(result);
            }
            try
            {
                db.ProjectAttachments.AddRange(fileInfo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV GetFileInfo", User.Identity.Name);
            }
            return Task.FromResult(true);
        }
       protected async Task<ProjectAttachment> SaveFile(IFormFile file, string pathToSave, string ProjectId, string ProposalID, string TenantID, string notes)
        {
            object result = null;
            ProjectAttachment projectAttachment = new ProjectAttachment();
            try
            {
                var blobSection = _configuration.GetSection("AzureBlob");
                result = await AzureBlobStorage.UploadFileToBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], TenantID, file, pathToSave);
                string AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                System.Type type = result.GetType();
                Uri docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                var userId = _userManager.GetUserId(User);
                projectAttachment = new ProjectAttachment()
                {
                    AttachmentId = Guid.NewGuid().ToString(),
                    DateUploaded = DateTime.Now,
                    FileName = file.FileName,
                    FilePatch = pathToSave,
                    ProposalID = ProposalID,
                    TenantID = TenantID,
                    AuthorId = AuthorId,
                    ProjectId = ProjectId,
                    Note = notes
                };
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV SaveFile", User.Identity.Name);
            }
            return projectAttachment;
       }
        public IActionResult RemoveTechUserIdFromProject(string Id)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _roleManager, _userManager);
                var result = projectBusiness.RemoveTechUserIdFromProject(Id);
                return Json(new[] { result });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV RemoveTechUserIdFromProject", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        //AddTechnicianByProjectId_Create
        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddTechnicianByProjectId_Create([DataSourceRequest] DataSourceRequest request, TechnicianViewModel input)
        {
            TechnicianViewModel output = new TechnicianViewModel();
            try
            {
                if (ModelState.IsValid && input != null)
                {
                    try
                    {
                        IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                        string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                        output = await projectBusiness.AddTechnicianOnProject(input);                        
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                        customErrorHandler.WriteError(ex, "UpcomingProjectSRV AddTechnicianByProjectId_Create", User.Identity.Name);
                        _logger.LogInformation(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV AddTechnicianByProjectId_Create", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new[] { output }.ToDataSourceResult(request, ModelState));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
         }
    }
}