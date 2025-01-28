using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using WellAI.Advisor.DLL.Entity;
using Microsoft.Extensions.Configuration;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL;
using WellAI.Advisor.Model.Common;
using Newtonsoft.Json;
using WellAI.Advisor.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Extensions;
using WellAI.Advisor.DLL.Repository;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class ProjectAuctionsSRVController : BaseController
    {
        private readonly ILogger<ProjectAuctionsSRVController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private readonly ISingleton _singleton;
       private IHubContext<NotificationHub> _hubContext { get; set; }
        public ProjectAuctionsSRVController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           ISingleton singleton,
           WebAIAdvisorContext dbContext, ILogger<ProjectAuctionsSRVController> logger, IConfiguration configuration, IHubContext<NotificationHub> hubContext)
        : base(userManager, dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _logger = logger;
            _singleton = singleton;
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
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var data = new WellAI.Advisor.Model.ServiceCompany.Models.AuctionBidsModel();
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                {
                    var tenantid = HttpContext.GetMultiTenantContext().TenantInfo;
                 var operId = Request.Cookies["operfilterlayout"].ToString();

                    //Phase II changes - 02/16/2021
                    var dbprefix = "serv";
                    var servguid = new Guid(tenantid.Id);
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + servguid.ToString("N"));
                    var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                    //var newDbName = "wellai_" + dbprefix + "db_" + servguid.ToString("N");                   
                    //var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);
                    var servDBContext = new TenantServiceDbContext(ti);
                    var servrepo = new ServiceTenantRepository(servDBContext, HttpContext, _userManager, db);

                  //  data = await _singleton.auctionProposalBusiness.AuctionDashboardServiceStatus(tenantid.Id, "", servrepo);

                     data = await _singleton.auctionProposalBusiness.AuctionDashboardServiceStatus(tenantid.Id, operId, servrepo);
                }
                return View(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuctionSRV Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> Counts()
        {
            var data = new WellAI.Advisor.Model.ServiceCompany.Models.AuctionBidsModel();
            if (WellAIAppContext.Current.Session.GetString("AccountType1") == "2")//Dispatch
            {
                data.ProjectsStartedLastMonthCount = 0;
                //data.ProjectsStartedLastMonthDate = null;
                data.ProjectsStartedThisMonthCount = 0;
                data.ProjectsStartedThisMonthValue = 0;

                return Json(data);
            }
            if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
            {
                var tenantid = HttpContext.GetMultiTenantContext().TenantInfo;
               var operId = Request.Cookies["operfilterlayout"].ToString();

                //Phase II changes - 02/16/2021
                var dbprefix = "serv";
                var servguid = new Guid(tenantid.Id);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var servDBContext = new TenantServiceDbContext(ti);
                var servrepo = new ServiceTenantRepository(servDBContext, HttpContext, _userManager, db);
               // data = await _singleton.auctionProposalBusiness.AuctionDashboardServiceStatus(tenantid.Id, "", servrepo);

             data = await _singleton.auctionProposalBusiness.AuctionDashboardServiceStatus(tenantid.Id, operId, servrepo);
            }
            return Json(data);
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "ProjectAuctions", TenantId);
            }
            else
            {
                return false;
            }
        }

        public IActionResult GetBidsDataGridAction([DataSourceRequest] DataSourceRequest request, string valueFrom, string valueTo)
        {
            List<AuctionBidViewModel> data = new List<AuctionBidViewModel>();
            try
            {
                var operId = Request.Cookies["operfilterlayout"].ToString();
                string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;

                //Phase II Changes - 02/16/2021
                var dbprefix = "serv";
                var servguid = new Guid(tenantid);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var servDBContext = new TenantServiceDbContext(ti);
                var servrepo = new ServiceTenantRepository(servDBContext, HttpContext, _userManager, db);                

                data = _singleton.auctionProposalBusiness.GetAuctionsProposalListForSRV(tenantid, operId, servrepo); //GetBidsData(valueFrom, valueTo);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuctionSRV GetBidsDataGridAction", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(data.ToDataSourceResult(request));
        }
        //Phase-II Changes
        public List<AuctionBidViewModel> GetBidsData(string valueFrom, string valueTo)
        {
            List<AuctionBidViewModel> result = null;
            try
            {
                double from = 0;
                double to = double.MaxValue;
                if (!string.IsNullOrEmpty(valueFrom))
                {
                    from = Convert.ToDouble(valueFrom);
                }
                if (!string.IsNullOrEmpty(valueTo))
                {
                    to = Convert.ToDouble(valueTo);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuctionSRV GetBidsData", User.Identity.Name);
            }
            return result;
        }
        public IActionResult GetAuctionBidsAmountHistory([DataSourceRequest] DataSourceRequest request,string Bidsid)
        {
            List<AuctionBidAmountHistoryViewModel> result = new List<AuctionBidAmountHistoryViewModel>();
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                result = _singleton.auctionProposalBusiness.GetAuctionBidAmountHistoryByAuctionBidId(Bidsid, tenantId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuctionSRV GetAuctionBidsAmountHistory", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        private List<AuctionBidAmountHistoryViewModel> GetAuctionBidsAmountHistoryAsync(string bidsId)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            List<AuctionBidAmountHistoryViewModel> result = null;
            result = _singleton.auctionProposalBusiness.GetAuctionBidAmountHistoryByAuctionBidId(bidsId, tenantId);
            return result;
        }
        public IActionResult AuctionBidContent(string id)
        {
            AuctionProposalBidDeatilsViewModel auctionBidViewModel = new AuctionProposalBidDeatilsViewModel();
            try
            {
                string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                auctionBidViewModel = _singleton.auctionProposalBusiness.GetAuctionsProposalForSRVByProposalId(id,tenantid); // Bid proposal details & status
                if (auctionBidViewModel != null)
                {
                    auctionBidViewModel.auctionBidAmountHistoryViewModels = GetAuctionBidsAmountHistoryAsync(auctionBidViewModel.BidID); // Bid history
                    auctionBidViewModel.auctionProposalAttachments = _singleton.auctionProposalBusiness.GetAuctionProposalAttachmentsByBidId(auctionBidViewModel.BidID);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuctionSRV AuctionBidContent", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return View("_Acutionbiddetails", auctionBidViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddAcuctionBidsBySRV(AuctionProposalBidDeatilsViewModel input)
        {
            try
            {
                
                input.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                input.TenantID = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                if (input.files != null)
                {
                    GetFileInfo(input.files, input.ProposalId, input.TenantID, input.AuctionNumber);
                }
                var GetBidId=await _singleton.auctionProposalBusiness.AddAucuctionBidsBySRV(input);
                EmailHandler emailHandler = new EmailHandler();
                MessageToQueue message =  _singleton.senderAndReceiverBusiness.GetSendOnAuctionRequestforOperator(input.AuthorId,input.TenantID,input.ProposalId,input.BidAmount,input.BidSummary).Result;
                emailHandler.SendMessageToQueue(message);
                var result1 = db.AuctionBids.Find(input.BidID);
                if(result1==null)
                {
                    var welluser = JsonConvert.DeserializeObject<WellIdentityUser>(
                                       WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var ProposalDetail = db.AuctionProposals.Where(x => x.ProposalId == input.ProposalId).FirstOrDefault();
                    MessageQueue messageQueue = new MessageQueue { From_id = input.AuthorId, To_id = ProposalDetail.AuthorId, Type = 3, IsActive = 1, EntityId = input.ProposalId, JobName = NotificationTypeEnum.Bids.GetAttributeOfType<System.ComponentModel.DataAnnotations.DisplayAttribute>().Name, TaskName = input.RigName+":"+input.WellName+"-"+input.JobName, CreatedDate = DateTime.Now };
                    await commonBusiness.AddNotifications(messageQueue);
                    await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
                }
                return RedirectToAction("AuctionBidContent", new { id = input.ProposalId });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuctionSRV AddAcuctionBidsBySRV", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private void GetFileInfo(IEnumerable<IFormFile> files, string ProposalID, string TenantID, string auctionNumber)
        {
            try
            {
                List<AuctionProposalAttachments> fileInfo = new List<AuctionProposalAttachments>();
                foreach (var file in files)
                {
                    var folderName = _configuration.GetSection("FolderName");
                    var result = Task.Run(async () => await SaveFile(file, folderName["Auctions"] + "/" + auctionNumber, ProposalID, TenantID)).Result;
                    fileInfo.Add(result);
                }
                db.AuctionProposalAttachments.AddRange(fileInfo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuctionSRV GetFileInfo", User.Identity.Name);
            }
        }

        [HttpGet]
        public async Task<FileResult> Download(string fileId, String TenId)
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
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV Download", User.Identity.Name);
                return null;
            }
        }
        protected async Task<AuctionProposalAttachments> SaveFile(IFormFile file, string pathToSave, string ProposalID, string TenantID)
        {
            object result = null;
            AuctionProposalAttachments proposalAttachments = new AuctionProposalAttachments();
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    result = await AzureBlobStorage.UploadFileToBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, file, pathToSave);
                    System.Type type = result.GetType();
                    Uri docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                    var userId = _userManager.GetUserId(User);
                    proposalAttachments = new AuctionProposalAttachments()
                    {
                        AttachmentId = Guid.NewGuid().ToString(),
                        DateUploaded = DateTime.Now,
                        FileName = file.FileName,
                        FilePatch = pathToSave,
                        ProposalID = ProposalID,
                        TenantID = TenantID
                    };
                    return proposalAttachments;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProjectAuctionSRV SaveFile", User.Identity.Name);
            }
            return proposalAttachments;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
    
    

}