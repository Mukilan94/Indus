using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using Well_AI.Advisor.Log.Error;
using Well_AI.Advisor.Communication;
using Finbuckle.MultiTenant;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Hubs;
using Microsoft.AspNetCore.SignalR;
using WellAI.Advisor.BLL.IBusiness;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class CommunicationSRVController : BaseController
    {
        private readonly ILogger<CommunicationSRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private IWebHostEnvironment _hostingEnvironment;
        private TenantServiceDbContext _tdbContext;
        private readonly IConfiguration _configuration;
        private IHubContext<NotificationHub> _hubContext { get; set; }
        //Karthik
        private readonly ICommunication communication;
        public CommunicationSRVController(
             UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext,
           ILogger<CommunicationSRVController> logger,
           IWebHostEnvironment hostingEnvironment,
           TenantServiceDbContext tdbContext, ICommunication communication, IConfiguration configuration,   IHubContext<NotificationHub> hubContext )
            : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _hostingEnvironment = hostingEnvironment;
            _hubContext = hubContext;
            _tdbContext = tdbContext;
            this.communication = communication;
            this._configuration = configuration;
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
                var model = await GetCommunicationModel();
                return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV Index", User.Identity.Name);
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "Connections", TenantId);
            }
            else
            {
                return false;
            }
        }

        public IActionResult ChatHistory_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ChatHistory> result = null;
            try
            {
                result = ChatHistory();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV ChatHistory_Read", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        public IEnumerable<ChatHistory> ChatHistory()
        {
            var data = new ChatHistory[]
            {
                new ChatHistory{ Id=1, UserName="Hannah Bair", JobTitle="Petro Mechanical Services", Unread=1, CreateDate=new System.DateTime(2020,2,11,11, 30, 0)},
                new ChatHistory{ Id=2, UserName="Adam Benzo", JobTitle="Tdj Oilfield", CreateDate=new System.DateTime(2020,2,12,10, 30, 0)},
                new ChatHistory{ Id=3, UserName="Hannah Bair", JobTitle="Petro Mechanical Services", Unread=1, CreateDate=new System.DateTime(2020,2,10,9, 30, 0)},
                new ChatHistory{ Id=4, UserName="Hannah Bair", JobTitle="Petro Mechanical Services", Unread=1, CreateDate=new System.DateTime(2020,2,9,10, 30, 0)},
                new ChatHistory{ Id=5, UserName="Hannah Bair", JobTitle="Petro Mechanical Services", Unread=1, CreateDate=new System.DateTime(2020,2,8,17, 30, 0)},
                new ChatHistory{ Id=6, UserName="Hannah Bair", JobTitle="Petro Mechanical Services", Unread=1, CreateDate=new System.DateTime(2020,2,8,14, 30, 0)},
                new ChatHistory{ Id=7, UserName="Hannah Bair", JobTitle="Petro Mechanical Services", Unread=1, CreateDate=new System.DateTime(2020,2,8,11, 30, 0)},
                new ChatHistory{ Id=8, UserName="Hannah Bair", JobTitle="Petro Mechanical Services", Unread=1, CreateDate=new System.DateTime(2020,2,8,9, 30, 0)},
                new ChatHistory{ Id=9, UserName="Hannah Bair", JobTitle="Petro Mechanical Services", Unread=1, CreateDate=new System.DateTime(2020,2,6,11, 30, 0)},
                new ChatHistory{ Id=10, CreateDate=new System.DateTime(2020,2,2,10, 30, 0)},
                new ChatHistory{ Id=11, CreateDate=new System.DateTime(2020,2,1,11, 30, 0)},
                new ChatHistory{ Id=12, CreateDate=new System.DateTime(2020,1,29,16, 30, 0)},
                new ChatHistory{ Id=13, CreateDate=new System.DateTime(2020,1,29,11, 30, 0)},
                new ChatHistory{ Id=14, CreateDate=new System.DateTime(2020,1,27,10, 30, 0)},
                new ChatHistory{ Id=15, CreateDate=new System.DateTime(2020,1,24,11, 30, 0)},
                new ChatHistory{ Id=16, CreateDate=new System.DateTime(2020,1,22,10, 30, 0)},
            };
            return data;
        }
        public IActionResult Communication_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(ChatHistory());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "CommunicationSRv DownloadCorpLogo SRV", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                }

                if (temp != null && temp.Length > 0)
                {
                    var extIndex = logoPath.LastIndexOf(".");
                    var ext = logoPath.Substring(extIndex + 1, logoPath.Length - extIndex - 1);
                    result = "data:image/" + ext + ";base64," + Convert.ToBase64String(temp);
                }
            }
            return result;
        }
        private async Task<CommunicationSRVViewModel> GetCommunicationModel()
        {
            var communicationSRVViewModel = new CommunicationSRVViewModel();            
            var model = new List<CommunicationSRVModel>();
            //Karthik
            var chatRoomListModel = new List<ChatRoomModel>();
            var chatRoomFromListModel = new List<ChatRoomModel>();
            var chatRoomToListModel = new List<ChatRoomModel>();
            try
            {                
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId =await _userManager.GetUserAsync(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId.Id);
                var operrepo = new ServiceTenantRepository(_tdbContext, HttpContext, _userManager,db);
                var clientContacts = await operrepo.GetClientContactsList(userId.Id);
                //Karthik
                string companyName = string.Empty;
                string phone = string.Empty;
                string email = string.Empty;
                if (clientContacts != null && clientContacts.Count() > 0)
                {
                    foreach (var item in clientContacts)
                    {
                        companyName = string.Empty;
                        string logopath = string.Empty;
                        string companyWebsite = string.Empty;
                        var userData = await commonBusiness.GetUser(item.ContactId);
                        var corporateProfile = (from u in _userManager.Users.Where(e => e.Id.Equals(userData.UserID))
                                                join c in db.CorporateProfile on u.TenantId equals c.TenantId //into corp
                                                select c).FirstOrDefault();
                        if (corporateProfile != null)
                        {
                            companyName = corporateProfile.Name;
                            logopath = corporateProfile.LogoPath;
                            companyWebsite = corporateProfile.Website;
                            if (!string.IsNullOrEmpty(logopath))
                            {
                                logopath = await GetUrlOfImage(corporateProfile.LogoPath, corporateProfile.TenantId);
                            }
                        }
                        if (userData.ProfileImageName != null)
                            userData.ProfileImageName = await GetUrlOfImage(userData.ProfileImageName, userData.UserTenantId);
                        model.Add(new CommunicationSRVModel
                        {
                            UserName = string.Format("{0} {1}", userData.FirstName, userData.LastName),
                            UserLogoPath = userData.ProfileImageName,
                            Email = userData.Email,
                            Phone = userData.PhoneNumber,
                            CompanyName = companyName,
                            LogoPath = logopath,
                            CompanyWebsite = companyWebsite,
                            ClientContactId = item.ClientContactId,
                            TenantId = item.TenantId,
                            ClientUserId = userData.UserID
                        });
                    }
                }
                communicationSRVViewModel.CommunicationModel = model;
                communicationSRVViewModel.ChatRoomModel = chatRoomListModel;
            }
            catch (Exception ex)
            {
                communicationSRVViewModel.CommunicationModel = model;
                communicationSRVViewModel.ChatRoomModel = chatRoomListModel;
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV GetCommunicationModel", User.Identity.Name);
            }
            return communicationSRVViewModel;
        }
        private async Task<CommunicationSRVViewModel> GetCommunicationChatRoomModel()
        {
            var communicationSRVViewModel = new CommunicationSRVViewModel();
            var model = new List<CommunicationSRVModel>();
            //Karthik
            var chatRoomListModel = new List<ChatRoomModel>();
            var chatRoomFromListModel = new List<ChatRoomModel>();
            var chatRoomToListModel = new List<ChatRoomModel>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                var operrepo = new ServiceTenantRepository(_tdbContext, HttpContext, _userManager, db);
                var clientContacts = await operrepo.GetClientContactsList(userId);
                //Karthik
                string companyName = string.Empty;
                string phone = string.Empty;
                string email = string.Empty;
                //Chat Rooms and messages
                chatRoomFromListModel = (from tw in db.TwilioChatUserMappings.Where(tw => tw.useridentity.Equals(userId))
                                         join wu in db.WellIdentityUser on tw.receivername equals wu.Id into twu
                                         from cu in twu.DefaultIfEmpty()
                                         select new ChatRoomModel
                                         {
                                             UserId = cu.Id,
                                             UserRoomId = tw.channelsid,
                                             UserRoomName = cu.FirstName + ' ' + cu.LastName,
                                             LastChatDate = ".......",
                                             LastChatMessage = "",
                                             Phone = cu.PhoneNumber,
                                             Email = cu.Email,
                                             CompanyName = companyName,
                                             UserName = string.Format("{0} {1}", cu.FirstName, cu.LastName)
                                         }
                                        ).ToList();
                chatRoomToListModel = (from tw in db.TwilioChatUserMappings.Where(tw => tw.receivername.Equals(userId))
                                       join wu in db.WellIdentityUser on tw.sendername equals wu.Id into twu
                                       from cu in twu.DefaultIfEmpty()
                                       select new ChatRoomModel
                                       {
                                           UserId = cu.Id,
                                           UserRoomId = tw.channelsid,
                                           UserRoomName = cu.FirstName + ' ' + cu.LastName,
                                           LastChatDate = ".......",
                                           LastChatMessage = "",
                                           Phone = cu.PhoneNumber,
                                           Email = cu.Email,
                                           CompanyName = companyName,
                                           UserName = string.Format("{0} {1}", cu.FirstName, cu.LastName)
                                       }
                                      ).ToList();
                chatRoomListModel = chatRoomFromListModel.Union(chatRoomToListModel).ToList();
                foreach (var item in chatRoomListModel)
                {
                    //Communication comn = new Communication();
                    var chat = communication.GetTwilioMessagForChannel(userId, item.UserRoomId);
                    var chatDate = chat["chatDate"];
                    var chatMessage = chat["chatMessage"];
                    item.LastChatDate = chatDate;
                    item.LastChatMessage = chatMessage;
                }
                communicationSRVViewModel.CommunicationModel = model;
                communicationSRVViewModel.ChatRoomModel = chatRoomListModel;
            }
            catch (Exception ex)
            {
                communicationSRVViewModel.CommunicationModel = model;
                communicationSRVViewModel.ChatRoomModel = chatRoomListModel;
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV GetCommunicationModel", User.Identity.Name);
            }
            return communicationSRVViewModel;
        }
        public async Task<IActionResult> Contacts_Read([DataSourceRequest] DataSourceRequest request,string Tenantid)
        {
            List<ClientContactModel> allUser = new List<ClientContactModel>();
            try
            {
                List<string> userList = new List<string>();
                var userId = _userManager.GetUserId(User);
                userList.Add(userId);
                var operrepo = new ServiceTenantRepository(_tdbContext, HttpContext, _userManager,db);
                var clientContacts = await operrepo.GetClientContactsList(userId);
                if (clientContacts != null && clientContacts.Count() > 0)
                {
                    userList.AddRange(clientContacts.Select(e => e.ContactId));
                }
                allUser = (from u in _userManager.Users.Where(e => !userList.Contains(e.Id) && (e.TenantId == Tenantid || Tenantid == null))
                           join c in db.CorporateProfile on u.TenantId equals c.ID into corp
                           from cp in corp.DefaultIfEmpty()
                           where u.FirstName != "NULL"
                           select new ClientContactModel
                           {
                               UserId = userId,
                               TenantId = u.TenantId,
                               ContactId = u.Id,
                               UserName = string.Format("{0} {1}", u.FirstName, u.LastName),
                               Email = u.Email,
                               Phone = u.PhoneNumber,
                               CompanyName = cp.Name
                           }).ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRv Contacts_Read", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(allUser.ToDataSourceResult(request));
        }
        [HttpPost]
        public async Task<IActionResult> SaveClientContact([FromBody] List<ClientContact> clients)
        {
            CommunicationSRVViewModel model = new CommunicationSRVViewModel();
            try
            {
                if (clients != null && clients.Count > 0)
                {
                    var operrepo = new ServiceTenantRepository(_tdbContext, HttpContext, _userManager,db);
                    var result = await operrepo.CreateClientContacts(clients);
                    if (result)
                    {
                        model = await GetCommunicationModel();
                        return PartialView("_AddContacts", model.CommunicationModel);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV Save client", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
       [HttpPost]
        public async Task<IActionResult> UpdateClientContact(int clientContactId)
        {
            CommunicationSRVViewModel model = new CommunicationSRVViewModel();
            try
            {
                if (clientContactId != 0)
                {
                    var operrepo = new ServiceTenantRepository(_tdbContext, HttpContext, _userManager,db);
                    var result = await operrepo.UpdateClientContacts(clientContactId);
                    if (result)
                    {
                        model = await GetCommunicationModel();
                        return PartialView("_AddContacts", model.CommunicationModel);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV UpdateClientContact", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return PartialView("_AddContacts", model.CommunicationModel);
        }
        public string GetChannelForFromAndToUser(string fromUser, string toUser)
        {
            try
            {
                var twiliochannel = new TwilioChat(db, _roleManager, _userManager);
                var result = twiliochannel.GetChannelForFromAndToUser(fromUser, toUser);
                //return Json("{channelid:'" + result + "'}");
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV GetChannelForFromAndToUser", User.Identity.Name);
                //return Json("{channelid:''}");
                return "";
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddUserChannelInTwilioMappings(string fromUser, string toUser, string channelId, string userIdentity, string channelUniqueName)
        {
            try
            {
                if (channelId != "")
                {
                    var twiliochannel = new TwilioChat(db, _roleManager, _userManager);
                    var result = await twiliochannel.AddTwilioUserChannel(fromUser, toUser, channelId, userIdentity, channelUniqueName);
                    return Json(result.ToString());
                }
                else
                {
                    return Json("");
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV AddUserChannelInTwilioMappings", User.Identity.Name);
                return Json("");
            }
        }
        [HttpPost]
        public async Task<JsonResult> UpdateUserChannelInvitationStatus(string fromUser, string toUser, string channelId)
        {
            try
            {
                if (channelId != "")
                {
                    var twiliochannel = new TwilioChat(db, _roleManager, _userManager);
                    var result = await twiliochannel.UpdateTwilioUserChannel(fromUser, toUser, channelId);
                    return Json(result.ToString());
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV UpdateUserChannelInvitationStatus", User.Identity.Name);
                return Json("");
            }
        }
       private async Task<string> GetUrlOfImage(string filename, string tenantId)
        {
            try
            {
                var blobSection = _configuration.GetSection("AzureBlob");
                var folderName = _configuration.GetSection("FolderName");
                var items = await AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], tenantId, folderName["CompanyUserProfile"], filename);
                return items;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "EditProfile Index", User.Identity.Name);
                return string.Empty;
            }
        }
        private IEnumerable<CompanyServicesSRVModel> GetCompanyServices(string tenantId)
        {
            var CompanyServicesModel = new CompanyServicesSRVModel();
            List<CompanyServicesSRVModel> companyServiceList = new List<CompanyServicesSRVModel>();
            try
            {
                var model = new List<CompanyServicesSRVModel>();
                var corporateProfile = (from u in _userManager.Users.Where(e => e.TenantId.Equals(tenantId))
                                        join c in db.CorporateProfile on u.TenantId equals c.TenantId into corp
                                        from cp in corp.DefaultIfEmpty()
                                        select new ServiceCorporateProfile
                                        {
                                            Name = cp.Name ?? String.Empty,
                                            LogoPath = cp.LogoPath ?? String.Empty,
                                            Website = cp.Website ?? String.Empty,
                                            CServices = cp.CServices ?? String.Empty
                                        }
                                       ).FirstOrDefault();
                if (corporateProfile.CServices != null)
                {
                    companyServiceList = JsonConvert.DeserializeObject<List<CompanyServicesSRVModel>>(corporateProfile.CServices); ;
               }
                return companyServiceList;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
               customErrorHandler.WriteError(ex, "CommunicationSRV GetCompanyServices", User.Identity.Name);
                return companyServiceList;
            }
        }
        [HttpGet]
        public IActionResult CompanyServices(string tenantId)
        {
            try
            {
                var model = GetCompanyServices(tenantId);
                return PartialView("_CompanyServices", model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV CompanyServices", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<JsonResult> UpdateLeaveUserChannelStatus(string fromUser, string toUser, string channelId)
        {
            if (channelId != "")
            {
                var twiliochannel = new TwilioChat(db, _roleManager, _userManager);
                var result = await twiliochannel.UpdateTwilioUserChannelLeaveStatus(fromUser, toUser, channelId);
                return Json(result.ToString());
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        //Phase II Changes - 03/11/2021
        //messageType added for differentiate Chat/Video Call
        public async Task<IActionResult> SaveMessageNotification(string toUser, string message, int type, string messageType)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //Check weather invited user is online or not
                    MessageQueue messageQueue = new MessageQueue { From_id = _userManager.GetUserId(User), To_id = toUser, Type = Convert.ToInt32(type), IsActive = 1, TaskName=message,JobName= messageType, CreatedDate = DateTime.Now };
                    await commonBusiness.AddNotifications(messageQueue);
                    await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV SaveMessageNotification", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(true);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatusNotification()
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                await commonBusiness.UpdateNotifications(_userManager.GetUserId(User));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV UpdateStatusNotification", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(true);
        }
        public async Task<IActionResult> RefreshChatList()
        {
           var model = await GetCommunicationChatRoomModel();
            return PartialView("_ChatRoom", model.ChatRoomModel);
        }
        public IActionResult UpComingServices(string tenantId)
        {
            List<WellAI.Advisor.Model.OperatingCompany.Models.ProjectViewModel> result = new List<WellAI.Advisor.Model.OperatingCompany.Models.ProjectViewModel>();
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var serviceTenantId = HttpContext.GetMultiTenantContext().TenantInfo;
                result = projectBusiness.GetUpComingProjects_Chat(tenantId, serviceTenantId.Id);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProjectSRV UpcomingProject_Read", User.Identity.Name);
            }
            return PartialView("_UpComingServices", result);
        }
    }
}