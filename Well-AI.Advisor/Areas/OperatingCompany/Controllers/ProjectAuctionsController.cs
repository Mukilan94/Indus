using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WellAI.Advisor.DLL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.BLL.IBusiness;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;   
using Finbuckle.MultiTenant;
using WellAI.Advisor.Areas.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL;
using WellAI.Advisor.Model.Common;
using Well_AI.Advisor.API.Models;
using WellAI.Advisor.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Extensions;
using System.Globalization;
using System.Net;
using Telerik.Web.PDF;
using WellAI.Advisor.DLL.Repository;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class ProjectAuctionsController : BaseController
    {
        private readonly ILogger<ProjectAuctionsController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private readonly ISingleton _singleton;
        private TenantServiceDbContext _servicedb;
        private IHubContext<NotificationHub> _hubContext { get; set; }
        public ProjectAuctionsController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           ISingleton singleton,
            TenantServiceDbContext ServiceDb,
           WebAIAdvisorContext dbContext, ILogger<ProjectAuctionsController> logger, IConfiguration configuration, IHubContext<NotificationHub> hubContext)
            : base(userManager, dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _logger = logger;
            _singleton = singleton;
            _hubContext = hubContext;
            _configuration = configuration;
            _servicedb = ServiceDb;
        }
        public IActionResult Index()
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
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var data = new AuctionBidsModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                data = auctionProposalBusiness.AuctionDashboardOperatorStatus(userwell, RigId);
               return View(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> Counts()
        {
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var data = new AuctionBidsModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                data = auctionProposalBusiness.AuctionDashboardOperatorStatus(userwell, RigId);
                return await Task.FromResult(Json(data));
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction Counts", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        #nullable enable
        public JsonResult GetSRVTenantName(string? ServiceCategoryId)
        {
            var result = new List<CorporateProfile>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var companies = (commonBusiness.GetServiceCompaniesByCategories(ServiceCategoryId)).Result;
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                {
                    string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                    result = companies.Select(x => new CorporateProfile { TenantId = x.TenantId, Name = x.Name }).ToList();
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV GetVehicleByTenantid", User.Identity.Name);
            }
            return Json(result);
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "ViewDashboard", TenantId);
            }
            else
            {
                return false;
            }
        }

        public IActionResult GetBidsDataGridAction([DataSourceRequest] DataSourceRequest request, string valueFrom, string valueTo)
        {
            try
            {
                var data = GetBidsData(valueFrom, valueTo);
                if (data != null) {
                    return Json(data.ToDataSourceResult(request));
                }
                return new JsonResult(request);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetBidsDataGridAction", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }
        }
        public List<AuctionProposalViewModel> GetBidsData(string valueFrom, string valueTo)
        {
            IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
            List<AuctionProposalViewModel> result;
            var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
            var wellIdCookie = Request.Cookies["wellfilterlayout"];
            var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
            result = auctionProposalBusiness.GetAuctionsListByTenantid(userwell, RigId);
            return result;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddUpdateAuctionsProposal([DataSourceRequest] DataSourceRequest request, AddAuctionProposalViewModel auctionProposal, IFormCollection form)
        {
            AuctionProposalViewModel auctionProposalViewModel = new AuctionProposalViewModel();
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                auctionProposal.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (string.IsNullOrEmpty(auctionProposal.ProposalId))
                {
                    auctionProposal.AuctionStart = DateTime.ParseExact(form["AuctionStart"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    //auctionProposal.ProjectStartDate = DateTime.ParseExact(form["ProjectStartDate"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    auctionProposal.ProjectStartDate = Convert.ToDateTime(form["ProjectStartDate"].ToString());//, "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
                }
                else
                {
                    auctionProposal.AuctionStart = DateTime.ParseExact(form["AuctionStart"].ToString(), "M/d/yyyy", CultureInfo.InvariantCulture);
                    //auctionProposal.ProjectStartDate = DateTime.ParseExact(form["ProjectStartDate"].ToString(), "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
                    auctionProposal.ProjectStartDate = Convert.ToDateTime(form["ProjectStartDate"].ToString());//, "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
                }
                //auctionProposal.AuctionEnd = DateTime.ParseExact(form["AuctionEnd"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                auctionProposal.AuctionEnd = Convert.ToDateTime(form["AuctionEnd"].ToString());//, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                auctionProposal.TenantID = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                bool result = true;
                if (!string.IsNullOrEmpty(auctionProposal.ProposalId))
                {
                    result = await auctionProposalBusiness.UpdateAuctionProposal(auctionProposal);
                    if (auctionProposal.files != null)
                    {
                        GetFileInfo(auctionProposal.files, auctionProposal.ProposalId, auctionProposal.TenantID, auctionProposal.AuctionNumber);
                    }
                    var welluser = JsonConvert.DeserializeObject<WellIdentityUser>(
                             WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    //DWOP - Task data from DrillPlanDetails
                    var Jobname = db.DrillPlanDetails.Where(x => x.TaskId == auctionProposal.JobId).Select(y => y.TaskName).FirstOrDefault();
                    if (auctionProposal.SRVTenantId != null)
                    {
                        var user = await commonBusiness.GetUserList(auctionProposal.SRVTenantId);
                        for (int i = 0; i < user.Count; i++)
                        {
                            
                                MessageQueue messageQueue = new MessageQueue { From_id = auctionProposal.AuthorId, To_id = user[i].UserID, Type = 5, EntityId = auctionProposal.ProposalId, IsActive = 1, RigId = auctionProposal.RigId, JobName = "Updated Bid", TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + auctionProposal.JobName, CreatedDate = DateTime.Now };
                                await commonBusiness.AddNotifications(messageQueue);
                        }
                    }
                    else { 
                        //Phase II Changes - 03/16/2021 
                        var CrmCompanidetails = db.CrmCompanies.Where(y => y.TenantId != null && y.UserId != null).Select(x => new { x.TenantId, x.UserId }).ToList();
                        string Oprtenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                        foreach (var CrmCompanies in CrmCompanidetails)
                        {
                            var dbprefix = "serv";
                            var servguid = new Guid(CrmCompanies.TenantId);
                            var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                                   dbprefix + "db_" + servguid.ToString("N"));
                            var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                            var OperDBContext = new TenantServiceDbContext(ti);
                            _servicedb = OperDBContext;
                            var ServiceTenatDb = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                            var Result = await ServiceTenatDb.GetProductSubscribedRigs(auctionProposal.RigId, Oprtenantid);

                            if (Result > 0)
                            {
                                MessageQueue messageQueue = new MessageQueue { From_id = welluser.Id, To_id = CrmCompanies.UserId, Type = 3, IsActive = 1, EntityId = auctionProposal.ProposalId, JobName = "Updated Bid", CreatedDate = DateTime.Now, TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + Jobname, RigId = auctionProposal.RigId };
                                await commonBusiness.AddNotifications(messageQueue);
                                await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
                            }
                        }
                    }
                   

                    return RedirectToAction("AddUpdateAuctionsProposalDetail", new { id = auctionProposal.ProposalId });
                }
                else
                {
                    var count = db.AuctionProposals.Count() + 1;
                    auctionProposal.AuctionNumber = "AUC" + count.ToString("00000");
                    auctionProposal.ProposalId = Guid.NewGuid().ToString();
                    result = await auctionProposalBusiness.AddAuctionProposal(auctionProposal);
                }
                if (result == false)
                {
                    ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                }
                else
                {
                    if (auctionProposal.files != null)
                    {
                        GetFileInfo(auctionProposal.files, auctionProposal.ProposalId, auctionProposal.TenantID, auctionProposal.AuctionNumber);
                    }
                    auctionProposalViewModel = await auctionProposalBusiness.GetNewAddedAuctionProposalByProposalId(auctionProposal.ProposalId);
                }
                  await SendNotification(auctionProposal.ProposalId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction AddUpdateAuctionProposal", User.Identity.Name);
            }
            return Json(new[] { auctionProposalViewModel }.ToDataSourceResult(request, ModelState));
        }
        //#nullable enable
        private async Task SendNotification(string proposalId, string? BidStatus = null)
        {
            try
            {
                BidStatus = BidStatus == null ? NotificationTypeEnum.BidRequest.GetAttributeOfType<System.ComponentModel.DataAnnotations.DisplayAttribute>().Name : BidStatus;
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                
                AuctionProposalViewModel auctionProposal = new AuctionProposalViewModel();
                auctionProposal = await auctionProposalBusiness.GetAuctionProposalByProposalId(proposalId);
                if (auctionProposal != null)
                {
                    if (!auctionProposal.IsPrivate ?? false)
                    {
                        if (auctionProposal.SRVTenantId != null)
                        {
                            var user = await commonBusiness.GetUserList(auctionProposal.SRVTenantId);
                            for (int i = 0; i < user.Count; i++)
                            {
                               
                                    MessageQueue messageQueue = new MessageQueue { From_id = auctionProposal.AuthorId, To_id = user[i].UserID, Type = 5, EntityId = auctionProposal.ProposalId, IsActive = 1, RigId = auctionProposal.RigId, JobName = BidStatus, TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + auctionProposal.JobName, CreatedDate = DateTime.Now };
                                    await commonBusiness.AddNotifications(messageQueue);
                            }
                        }
                        else
                        {
                            //Phase II Changes - 03/11/2021 
                            var CrmCompanidetails = db.CrmCompanies.Where(y => y.TenantId != null && y.UserId != null).Select(x => new { x.TenantId, x.UserId }).ToList();
                            string Oprtenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                            foreach (var CrmCompanies in CrmCompanidetails)
                            {
                                var dbprefix = "serv";
                                var servguid = new Guid(CrmCompanies.TenantId);
                                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                                       dbprefix + "db_" + servguid.ToString("N"));
                                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                                var OperDBContext = new TenantServiceDbContext(ti);
                                _servicedb = OperDBContext;
                                var ServiceTenatDb = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                                var Result = await ServiceTenatDb.GetProductSubscribedRigs(auctionProposal.RigId, Oprtenantid);

                                if (Result > 0)
                                {
                                    MessageQueue messageQueue = new MessageQueue { From_id = auctionProposal.AuthorId, To_id = CrmCompanies.UserId, Type = 5, EntityId = auctionProposal.ProposalId, IsActive = 1, RigId = auctionProposal.RigId, TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + auctionProposal.JobName, JobName = BidStatus, CreatedDate = DateTime.Now };
                                    await commonBusiness.AddNotifications(messageQueue);
                                }
                            }                            
                        }
                    }
                    else
                    {
                        var user = await commonBusiness.GetUserList(auctionProposal.SRVTenantId);
                        for (int i = 0; i < user.Count; i++)
                        {
                           
                                MessageQueue messageQueue = new MessageQueue { From_id = auctionProposal.AuthorId, To_id = user[i].UserID, Type = 5, EntityId = auctionProposal.ProposalId, IsActive = 1, RigId = auctionProposal.RigId, JobName = BidStatus, TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + auctionProposal.JobName, CreatedDate = DateTime.Now };
                                await commonBusiness.AddNotifications(messageQueue);
                        }
                    }
                }
                else
                {
                    if (auctionProposal != null)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = auctionProposal.AuthorId, To_id = auctionProposal.AuthorId, Type = 5, EntityId = auctionProposal.ProposalId, IsActive = 1, RigId = auctionProposal.RigId, JobName = NotificationTypeEnum.BidRequest.GetAttributeOfType<System.ComponentModel.DataAnnotations.DisplayAttribute>().Name, TaskName = auctionProposal.RigName + ":" + auctionProposal.WellName + "-" + auctionProposal.JobName, CreatedDate = DateTime.Now };
                        await commonBusiness.AddNotifications(messageQueue);
                    }
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

        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddNewRequestAuctionsProposal(AddAuctionProposalViewModel auctionProposal, IFormCollection form)
        {
            AuctionProposalViewModel auctionProposalViewModel = new AuctionProposalViewModel();
            try
            {
                auctionProposal.WellId = form["WellId"].ToString();
                auctionProposal.JobId = form["JobId"].ToString();
                auctionProposal.Body = form["Body"].ToString();
                auctionProposal.ServiceCategoryId = form["ServiceCategoryId"].ToString();
                auctionProposal.ProjectDuration = Convert.ToDouble(form["ProjectDuration"]);
                auctionProposal.AuctionStart = Convert.ToDateTime(form["AuctionStart"]);
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                auctionProposal.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                auctionProposal.RigId = db.WellRegister.Where(x => x.well_id == auctionProposal.WellId).FirstOrDefault().RigID;
                auctionProposal.TenantID = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                bool result = true;
                if (!string.IsNullOrEmpty(auctionProposal.ProposalId))
                {
                    result = await auctionProposalBusiness.UpdateAuctionProposal(auctionProposal);
                    if (auctionProposal.files != null)
                    {
                        GetFileInfo(auctionProposal.files, auctionProposal.ProposalId, auctionProposal.TenantID, auctionProposal.AuctionNumber);
                    }
                    return RedirectToAction("AddUpdateAuctionsProposalDetail", new { id = auctionProposal.ProposalId });
                }
                else
                {
                    var count = db.AuctionProposals.Count() + 1;
                    auctionProposal.AuctionNumber = "AUC" + count.ToString("00000");
                    auctionProposal.ProposalId = Guid.NewGuid().ToString();
                    result = await auctionProposalBusiness.AddAuctionProposal(auctionProposal);
                }
                if (result == false)
                {
                    ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                }
                else
                {
                    if (auctionProposal.files != null)
                    {
                        GetFileInfo(auctionProposal.files, auctionProposal.ProposalId, auctionProposal.TenantID, auctionProposal.AuctionNumber);
                    }
                    auctionProposalViewModel = await auctionProposalBusiness.GetNewAddedAuctionProposalByProposalId(auctionProposal.ProposalId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction AddUpdateAuctionProposal", User.Identity.Name);
            }
            return RedirectToAction("AddUpdateAuctionsProposalDetail", new { id = auctionProposal.ProposalId });
        }
        private void GetFileInfo(IEnumerable<IFormFile> files, string ProposalID, string TenantID, string auctionNumber)
        {
            try
            {
                List<AuctionProposalOperatorAttachments> fileInfo = new List<AuctionProposalOperatorAttachments>();
                foreach (var file in files)
                {
                    var folderName = _configuration.GetSection("FolderName");
                    var result = Task.Run(async () => await SaveFile(file, folderName["Auctions"] + "/" + auctionNumber, ProposalID, TenantID)).Result;
                    fileInfo.Add(result);
                }
                db.AuctionProposalOperatorAttachments.AddRange(fileInfo);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetFileInfo", User.Identity.Name);
            }
        }
        protected async Task<AuctionProposalOperatorAttachments> SaveFile(IFormFile file, string blobblob, string ProposalID, string TenantID)
        {
            AuctionProposalOperatorAttachments proposalAttachments = new AuctionProposalOperatorAttachments();
            try
            {
                object? result = null;
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    result = await AzureBlobStorage.UploadFileToBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, file, blobblob);
                    System.Type type = result.GetType();
                    #nullable disable
                    Uri docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                    var userId = _userManager.GetUserId(User);
                    proposalAttachments = new AuctionProposalOperatorAttachments()
                    {
                        AttachmentId = Guid.NewGuid().ToString(),
                        DateUploaded = DateTime.Now,
                        FileName = file.FileName,
                        FilePatch = blobblob,
                        ProposalID = ProposalID,
                        TenantID = TenantID
                    };
                    return proposalAttachments;
                }
                return proposalAttachments;
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction SaveFile", User.Identity.Name);
                return proposalAttachments;
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddUpdateAuctionsProposalDetail(string id)
        {
            try
           {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var RigCookies = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(RigCookies) ? DLL.Constants.NoSpecificWellFilterKey : RigCookies.ToString();
                ViewBag.Wells = auctionProposalBusiness.GetWellForAuctionProposal(userwell)
                                    .Select(x => new
                                    {
                                        WellId = x.WellId,
                                        Name = x.Name
                                    });
                ViewBag.Rigs = auctionProposalBusiness.GetRigForAuctionProposal(userwell, RigId)
                                    .Select(x => new
                                    {
                                        RigId = x.RigId,
                                        RigName = x.RigName
                                    });
                var result = await auctionProposalBusiness.GetAuctionProposalByProposalId(id);
                AddAuctionProposalViewModel model = new AddAuctionProposalViewModel();
               AddAuctionProposalViewModel obj2 = JsonConvert.DeserializeObject<AddAuctionProposalViewModel>(JsonConvert.SerializeObject(result));
                model = obj2;
                if(model.SRVTenantId != null)
                {
                    model.VendorName = db.CorporateProfile.Where(x => x.TenantId == model.SRVTenantId).Select(y => y.Name).FirstOrDefault();
                }
                return View("AddUpdateAuctionsProposalDetail", model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction AddUpdateAuctionsProposalDetail", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetAuctionProposalOperatorAttachmentByProposalId([DataSourceRequest] DataSourceRequest request, string proposalId)
        {
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var data = await auctionProposalBusiness.GetAuctionProposalOperatorAttachmentByProposalId(proposalId);
                return Json(data.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetAuctionProposalOperatorAttachmentByProposalId", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }
        }

        [HttpGet]
        public IActionResult GetOpePdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = db.AuctionProposalOperatorAttachments.FirstOrDefault(x => x.AttachmentId == fileId);
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
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetOpePdfFile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json("");
            }
        }

         [HttpGet]
        public IActionResult GetPdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = db.AuctionProposalAttachments.Find(fileId);
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
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetPdfFile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json("");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Download(string fileId, string TenId)
        {
            var filebytes = new KeyValuePair<string, byte[]>();
            try
            {
                var result = db.AuctionProposalAttachments.Find(fileId);
                var blobSection = _configuration.GetSection("AzureBlob");
                filebytes = await AzureBlobStorage.DownloadFilesFromBlobBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], result.TenantID, result.FilePatch, result.FileName);
                if (filebytes.Key == "" || filebytes.Value.Length == 0)
                    return File(filebytes.Value, filebytes.Key);
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = result.FileName.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction Download", User.Identity.Name);
            }
            return File(filebytes.Value, filebytes.Key);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadOper(string fileId, string TenId)
        {
            var filebytes = new KeyValuePair<string, byte[]>();
            try
            {
                var result = db.AuctionProposalOperatorAttachments.Find(fileId);
                var blobSection = _configuration.GetSection("AzureBlob");
                filebytes = await AzureBlobStorage.DownloadFilesFromBlobBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], result.TenantID, result.FilePatch, result.FileName);
                if (filebytes.Key == "" || filebytes.Value.Length == 0)
                    return File(filebytes.Value, filebytes.Key);
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = result.FileName.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction DownloadOper", User.Identity.Name);
            }
            return File(filebytes.Value, filebytes.Key);
        }
        public JsonResult GetWelllistddl(string text, string RigId)
        {
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var result = auctionProposalBusiness.GetWellForAuctionProposal(userwell)
                                .Select(x => new
                                {
                                    WellId = x.WellId,
                                    Name = x.Name,
                                    RigId = x.RigId
                                }).ToList();
                var WellList = result.Where(x => x.RigId == RigId).ToList();
                return Json(WellList);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetWelllistddl", User.Identity.Name);
                return Json("");
            }
        }
        public JsonResult GetRiglistddl(string text)
        {
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var UserRig = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var TenantId = UserRig.TenantId;
                var RigCookies = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(RigCookies) ? DLL.Constants.NoSpecificWellFilterKey : RigCookies.ToString();
                var result = auctionProposalBusiness.GetRigForAuctionProposal(UserRig, RigId)
                                .Select(x => new
                                {
                                    RigId = x.RigId,
                                    RigName = x.RigName
                                }).ToList();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetRiglistddl", User.Identity.Name);
                return Json("");
            }
        }
        public JsonResult GetTaskByWellTypeIdForJob(string text, string ServiceCategoryId, string wellId)
        {
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                if (!string.IsNullOrEmpty(ServiceCategoryId))
                {

                    var PlanWellsTaks = (from PlanHeader in db.DrillPlanHeader
                                             join PlanWells in db.DrillPlanWells on PlanHeader.DrillPlanId equals PlanWells.DrillPlanId
                                             join PlanWellsDetails in db.DrillPlanDetails on PlanWells.DrillPlanWellsId equals PlanWellsDetails.DrillPlanWellsId
                                             where PlanWells.Wellid == wellId && PlanHeader.TenantId == userwell.TenantId
                                             select PlanWellsDetails).ToList();

                    var SerTask = (from task in PlanWellsTaks
                                   where task.CategoryId == ServiceCategoryId
                                   select new
                                   {
                                       TaskId = task.TaskId,
                                       Name = task.TaskName
                                   }).ToList();


                    //var SerTask = (from category in db.CategoryTasks
                    //               join task in db.Tasks on category.TaskId equals task.TaskId
                    //               where category.ServiceCategoryId == ServiceCategoryId
                    //               select new
                    //               {
                    //                   TaskId = task.TaskId,
                    //                   Name = task.Name
                    //               }).ToList();

                    if (!string.IsNullOrEmpty(text))
                    {
                        SerTask = SerTask.Where(p => p.Name.Contains(text)).ToList();
                    }

                    return Json(SerTask);
                }
                return Json("");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetTaskByWellTypeIdForJob", User.Identity.Name);

                return Json("");
            }
        }
        public JsonResult GetServiceCategoryList(string text, string wellId)
        {
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                //var result = auctionProposalBusiness.GetServiceCategorys().Result;

                var PlanWellsCategory = (from PlanHeader in db.DrillPlanHeader
                                         join PlanWells in db.DrillPlanWells on PlanHeader.DrillPlanId equals PlanWells.DrillPlanId
                                         join PlanWellsDetails in db.DrillPlanDetails on PlanWells.DrillPlanWellsId equals PlanWellsDetails.DrillPlanWellsId
                                         where PlanWells.Wellid == wellId && PlanHeader.TenantId == userwell.TenantId
                                         select PlanWellsDetails).ToList();

                var Categoties = PlanWellsCategory.Select(x => x.CategoryId).ToList().Distinct();

                var ServiceCategory = (from Ct in Categoties
                                       join ServiceCategories in db.serviceCategories on Ct equals ServiceCategories.ServiceCategoryId
                                       select new ServiceCategory
                                       {
                                           ServiceCategoryId = Ct,
                                           Name = ServiceCategories.Name
                                       }).OrderBy(a => a.Name).ToList();
             
                if (!string.IsNullOrEmpty(text))
                {
                    ServiceCategory = ServiceCategory.Where(x => x.Name.Contains(text)).ToList();
                }

                return Json(ServiceCategory);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction GetServiceCategoryList", User.Identity.Name);
                return Json("");
            }
        }
        public IActionResult AuctionBidderDetailContent(string id, string title)
        {
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                ViewBag.AuctionBidStatus = auctionProposalBusiness.GetAuctionsBidStatus();
                AuctionBidderDetailsViewModel output = auctionProposalBusiness.GetAuctionsBidderDetailsByBidId(id);
                if (output != null)
                {
                    output.AuctionProposalAttachments = auctionProposalBusiness.GetAuctionProposalAttachmentsByBidId(output.BidId);
                    output.Title = title;
                    if (output.BidStatusId == 5)
                    {
                        output.BidStatusName = "Rejected";
                    }
                }
                return View("_AuctionBidderDetail", output);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction AuctionBidderDetailContent", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> AuctionsBidAAcceptedRejectedCounter([DataSourceRequest] DataSourceRequest request, AuctionBidderDetailsViewModel input, IFormCollection form)
        {
            try
            {
                string bidStatus = string.Empty;
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                input.BidStatusName = form["Accept"];
                input.OperTenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                input.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (input.BidStatusName == null)
                {
                    input.BidStatusName = "Reject";
                    bidStatus = "Bid Rejected";
                }
                var projectCode = auctionProposalBusiness.AuctionsBidAcceptedRejectedCounter(input);
                MessageToQueue message = await _singleton.senderAndReceiverBusiness.GetSendOnAuctionsBidAcceptedRejectedForService(input.AuthorId, input.OperTenantId, input.ProposalId, projectCode, input.BidStatusName, input.BidId);
                EmailHandler emailHandler = new EmailHandler();
                emailHandler.SendMessageToQueue(message);
                if (input.BidStatusName == "Accept")
                {
                    bidStatus = "Bid Accepted";
                    var blobSection = _configuration.GetSection("AzureBlob");
                    var folderName = _configuration.GetSection("FolderName");
                    await AzureBlobStorage.CreateNewFolderInContainerForTenant(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                            blobSection["ContainerPrefixName"], input.OperTenantId, folderName["Projects"] + "/" + projectCode);
                }
                await SendNotification(input.ProposalId, bidStatus);
                return RedirectToAction("AddUpdateAuctionsProposalDetail", new { id = input.ProposalId });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuction AuctionsBidAAcceptedRejectedCounter", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
    }
}