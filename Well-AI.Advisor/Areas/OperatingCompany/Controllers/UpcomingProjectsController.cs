using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using WellAI.Advisor.Helper;
using WellAI.Advisor.BLL.IBusiness;
using Microsoft.AspNetCore.Http;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Routing;
using Kendo.Mvc.Extensions;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.DLL.Entity;
using Well_AI.Advisor.Log.Error;
using Newtonsoft.Json;
using Microsoft.OpenApi.Extensions;
using WellAI.Advisor.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Net;
using Telerik.Web.PDF;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class UpcomingProjectsController : BaseController
    {
        private readonly ILogger<UpcomingProjectsController> _logger;
        RoleManager<IdentityRole> _roleManager;
        private readonly ISingleton singleton;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private IHubContext<NotificationHub> _hubContext { get; set; }
        public UpcomingProjectsController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           ISingleton singleton,
           WebAIAdvisorContext dbContext, ILogger<UpcomingProjectsController> logger, IConfiguration configuration, IHubContext<NotificationHub> hubContext)
           : base(userManager, dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.singleton = singleton;
            _signInManager = signInManager;
            db = dbContext;
            _logger = logger;
            _configuration = configuration;
            _hubContext = hubContext;
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
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var result = await commonBusiness.GetTechnicianName(tenantId);
                ViewData["TechName"] = result;
                ProjectDashboardOperViewModel projectDashboard = new ProjectDashboardOperViewModel();
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var welluser = JsonConvert.DeserializeObject<WellIdentityUser>(
                                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                projectDashboard = await projectBusiness.ProjectDashboardOperTenantId(tenantId, welluser, RigId);
                return View(projectDashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjects", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> Counts()
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ProjectDashboardOperViewModel projectDashboard = new ProjectDashboardOperViewModel();
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var welluser = JsonConvert.DeserializeObject<WellIdentityUser>(
                                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                projectDashboard = await projectBusiness.ProjectDashboardOperTenantId(tenantId, welluser, RigId);
                return Json(projectDashboard);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjects Count", User.Identity.Name);
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
            var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
            var rolesResult = roleResult.Result;
            if (roleResult != null)
            {
                List<string> roleIds = (from rl in rolesResult
                                        select rl.Id
                                        ).ToList();
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "ViewDashboard",TenantId);
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUpcommingProjectDetails(ProjectViewModel input, IFormCollection form)
        {
            try
            {
                string statusName = form["btnSubmit"];
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
               input.ProjectStatusName = statusName;
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
               ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                AuctionProposalViewModel auctionProposal = new AuctionProposalViewModel();
                auctionProposal = await auctionProposalBusiness.GetAuctionProposalByProposalId(input.ProposalId);
                var result = await projectBusiness.UpdateUpComingProjectsDetailsForOperator_V1(input , auctionProposal.RigId);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                return RedirectToAction("Index", new RouteValueDictionary(
                           new { controller = "UpcomingProjects", action = "UpcomingProject_Read", Id = tenantId }));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpdateUpcommingProjectDetails", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUpcommingProjectCheckList(ProjectViewModel input)
        {
            try
            {
               string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var ids = string.IsNullOrEmpty(input.CheckIds) ? new string[] { } : input.CheckIds.Split(';', StringSplitOptions.RemoveEmptyEntries);
                var checkIds = ids.Length > 0 ? ids.ToList() : new List<string>();
                var updateResult = await projectBusiness.UpdateWellCheckStatusListForProject(tenantId, input.WellId, checkIds);
                if (string.IsNullOrEmpty(updateResult))
                    return RedirectToAction("CheckList", new RouteValueDictionary(
                               new { controller = "UpcomingProjects", action = "CheckList", Id = input.ProjectId }));
                else
                    throw new Exception(updateResult);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpdateUpcommingProjectDetails", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> UpcomingProject_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var welluser = JsonConvert.DeserializeObject<WellIdentityUser>(
                                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var result = await commonBusiness.GetUpCommingProjectsForOperator(welluser, RigId);
                return Json(result.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProject_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> ProjectDetails(string id)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var result = await projectBusiness.GetUpCommingProjectsDetailsByTenantIdForOperator(tenantId, id);
                return View(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectDetails", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> CheckList(string id)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var result = await projectBusiness.GetUpCommingProjectsDetailsByTenantIdForOperator(tenantId, id);
                return View(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectDetails", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetProjectProposalAttachments([DataSourceRequest] DataSourceRequest request, string proposalId, string projectId)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var result = await projectBusiness.GetProjectProposalAttachments(tenantId, proposalId, projectId);
                return Json(result.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetProjectProposalAttachments", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetAssignedTechnicianByProjectId([DataSourceRequest] DataSourceRequest request, string projectId)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var result = await projectBusiness.GetAssignedTechnicianByProjectId(projectId);
                return Json(result.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetProjectProposalAttachments", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public JsonResult GetTechnicianByTenantid()
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var result = projectBusiness.GetTechnicianByTenantid(tenantId);
                return Json(result.Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcominProjects GetTechnicianByTenantid", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
       }
        public JsonResult GetVehicleByTenantid()
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var result = singleton.serviceVehicleBusiness.GetServiceVehiclesList(tenantId);
                return Json(result.Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcominProjects GetVehicleByTenantid", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [HttpGet]
        public async Task<FileResult> Download(string AttachmentId)
        {
            try
            {
                var result = db.ProjectAttachments.Find(AttachmentId);
                var filebytes = new KeyValuePair<string, byte[]>();
                string path = $"{result.FilePatch}/{result.FileName}";
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
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcominProjects Download", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

        [HttpGet]
        public IActionResult GetPdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = db.ProjectAttachments.Find(fileId);
                var path = msadocument.FilePatch + "/" + msadocument.FileName;
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
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcominProjects GetPdfFile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }



        public async Task<IActionResult> AddMoreAttachment_Create([DataSourceRequest] DataSourceRequest request, AddProjectAttachmentViewModel input)
        {
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                input.TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                input.ProposalId = db.Projects.Where(x => x.ID == input.ProjectId).Select(y => y.ProposalID).FirstOrDefault();
                if (input.files != null)
                {
                    await GetFileInfo(input.files, input.ProjectCode, input.ProjectId, input.ProposalId, input.TenantId, input.Note);
                }
                return Json(new[] { true });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddMoreAttachment_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
       }
        private Task<bool> GetFileInfo(IEnumerable<IFormFile> files,string ProjectCode, string ProjectId,string ProposalID, string TenantID, string notes)
        {
            List<ProjectAttachment> fileInfo = new List<ProjectAttachment>();
            foreach (var file in files)
            {
                var folderName = _configuration.GetSection("FolderName");
                var result = Task.Run(async () => await SaveFile(file, folderName["Projects"] + "/" + ProjectCode, ProjectId, ProposalID, TenantID, notes)).Result;
                fileInfo.Add(result);
            }
            db.ProjectAttachments.AddRange(fileInfo);
            db.SaveChanges();
            return Task.FromResult(true);
        }
        protected async Task<ProjectAttachment> SaveFile(IFormFile file, string pathToSave, string ProjectId, string ProposalID, string TenantID, string notes)
        {
            object result = null;
            ProjectAttachment projectAttachment = new ProjectAttachment();
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var blobSection = _configuration.GetSection("AzureBlob");
                result = await AzureBlobStorage.UploadFileToBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], tenantId, file, pathToSave);
                string AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                System.Type type = result.GetType();
                Uri docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
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
                return projectAttachment;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SaveFile", User.Identity.Name);
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "RemoveTechUserIdFromProject", User.Identity.Name);
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddTechnicianByProjectId_Create([DataSourceRequest] DataSourceRequest request, TechnicianViewModel input)
        {
            TechnicianViewModel output = new TechnicianViewModel();
            if (ModelState.IsValid && input != null)
            {
                try
                {
                    IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    output = await projectBusiness.AddTechnicianOnProject(input);
                    output.TechWorkingStatus = "Assigned";
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "AddTechnicianByProjectId_Create", User.Identity.Name);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
            }
            return Json(new[] { output }.ToDataSourceResult(request, ModelState));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> CheckList_Read([DataSourceRequest] DataSourceRequest request,string wellId)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var wellCheckListDetail = await projectBusiness.EnsureAndGetWellCheckListForProject(tenantId, wellId);
                //TimeSpan? value = wellCheckListDetail[0].Time;
                return Json(wellCheckListDetail.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjects CheckList_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> UploadedFiles_Read(string wellId, string category, [DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var dpfiles = await commonBusiness.GetWellFilesFromTenantAndWell(tenantId, wellId, category.Replace("%20", " "));
                return Json(dpfiles.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjects UploadedFiles_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> ProjectNotes_Read([DataSourceRequest] DataSourceRequest request, string projectId)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var result = await projectBusiness.GetProjectNotes(projectId);
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjects ProjectNotes_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProjectNotes_Create(string projectId, [DataSourceRequest] DataSourceRequest request, ProjectNote input)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                input.ProjectId = projectId;
                input.ID = Guid.NewGuid().ToString("D");
                input.Created = DateTime.UtcNow;
                input.Author = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await projectBusiness.CreateNewProjectNote(input);
                return Json(new[] { result }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjects ProjectNotes_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
       private async Task SendNotification(string ProposalId, string BidStatus = null)
        {
            try
            {
                BidStatus = BidStatus == null ? NotificationTypeEnum.ServiceRequest.GetAttributeOfType<System.ComponentModel.DataAnnotations.DisplayAttribute>().Name : BidStatus;
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                AuctionProposalViewModel auctionProposal = new AuctionProposalViewModel();
                auctionProposal = await auctionProposalBusiness.GetAuctionProposalByProposalId(ProposalId);
                if (auctionProposal != null)
                {
                    if (!auctionProposal.IsPrivate ?? false)
                    {
                        var user = commonBusiness.GetCategorywiseCompanyDetail(auctionProposal.ServiceCategoryId);
                        foreach (var item in user)
                        {
                            MessageQueue messageQueue = new MessageQueue { From_id = auctionProposal.AuthorId, To_id = item.UserId, Type = 6, EntityId = auctionProposal.ProposalId, IsActive = 1, RigId = auctionProposal.RigId, TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + auctionProposal.JobName, JobName = BidStatus, CreatedDate = DateTime.Now };
                            await commonBusiness.AddNotifications(messageQueue);
                        }
                    }
                    else
                    {
                        var user = await commonBusiness.GetUserList(auctionProposal.SRVTenantId);
                        for (int i = 0; i < user.Count; i++)
                        {
                            bool value = await commonBusiness.NotificationExists(auctionProposal.ProposalId, user[i].UserID);
                            if (!value)
                            {
                                MessageQueue messageQueue = new MessageQueue { From_id = auctionProposal.AuthorId, To_id = user[i].UserID, Type = 6, EntityId = auctionProposal.ProposalId, IsActive = 1, RigId = auctionProposal.RigId, JobName = BidStatus, TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + auctionProposal.JobName, CreatedDate = DateTime.Now };
                                await commonBusiness.AddNotifications(messageQueue);
                            }
                        }
                    }
                }
                else
                {
                    MessageQueue messageQueue = new MessageQueue { From_id = auctionProposal.AuthorId, To_id = auctionProposal.AuthorId, Type = 6, EntityId = auctionProposal.ProposalId, IsActive = 1, RigId = auctionProposal.RigId, JobName = NotificationTypeEnum.BidRequest.GetAttributeOfType<System.ComponentModel.DataAnnotations.DisplayAttribute>().Name, TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + auctionProposal.JobName, CreatedDate = DateTime.Now };
                    await commonBusiness.AddNotifications(messageQueue);
                }
                await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction New Proposal Notification", User.Identity.Name);
            }
        }
    }
}