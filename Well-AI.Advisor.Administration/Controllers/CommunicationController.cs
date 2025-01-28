using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Communication;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.BLL.IBusiness;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class CommunicationController : BaseController
    {
        private readonly ILogger<CommunicationController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private IWebHostEnvironment _hostingEnvironment;
        private TenantOperatingDbContext _tdbContext;
        private readonly IConfiguration _configuration;
        //Karthik
        private readonly ICommunication communication;
        public CommunicationController(
           UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext,
           ILogger<CommunicationController> logger,
           IWebHostEnvironment hostingEnvironment,
           TenantOperatingDbContext tdbContext, ICommunication communication, IConfiguration configuration) : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _hostingEnvironment = hostingEnvironment;
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
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var model = await GetCommunicationModel();
                return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication Index", User.Identity.Name);
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "Connections",TenantId);
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
                    customErrorHandler.WriteError(ex, "Communication DownloadCorpLogo", User.Identity.Name);
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
        private async Task<CommunicationViewModel> GetCommunicationModel()
        {
            var communicationViewModel = new CommunicationViewModel();
            var model = new List<CommunicationModel>();
            //Karthik
            var chatRoomListModel = new List<ChatRoomModel>();
            var chatRoomFromListModel = new List<ChatRoomModel>();
            var chatRoomToListModel = new List<ChatRoomModel>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                var clientContacts = await operrepo.GetClientContactsList(userId);
                //Karthik
                string companyName = string.Empty;
                string phone = string.Empty;
                string email = string.Empty;
                //Contact List
                if (clientContacts != null && clientContacts.Count() > 0)
                {
                    foreach (var item in clientContacts)
                    {
                        //Karthik
                        //string companyName = string.Empty;
                        string logopath = string.Empty;
                        string companyWebsite = string.Empty;
                        var userData = await commonBusiness.GetUser(item.ContactId);
                        if (userData != null)
                        {
                            //TU-Karthik-05/29/2020-CorporateProfile empty Handling
                            var corporateProfile = (from u in _userManager.Users.Where(e => e.Id.Equals(item.ContactId))
                                                    join c in db.CorporateProfile on u.TenantId equals c.TenantId into corp
                                                    from cp in corp.DefaultIfEmpty()
                                                    select new CorporateProfile
                                                    {
                                                        Name = cp.Name ?? String.Empty,
                                                        LogoPath = cp.LogoPath ?? String.Empty,
                                                        Website = cp.Website ?? String.Empty,
                                                        CServices = cp.CServices ?? String.Empty,
                                                        TenantId = cp.TenantId
                                                    }
                                                    ).FirstOrDefault();
                            //TU-Karthik-corporateProfile changed to corporateProfile.Name
                            if (corporateProfile.Name != null || corporateProfile.Name != "")
                            {
                                companyName = corporateProfile.Name;
                                logopath = corporateProfile.LogoPath;
                                companyWebsite = corporateProfile.Website;
                                if (!string.IsNullOrEmpty(logopath))
                                {
                                    logopath = await GetUrlOfImage(corporateProfile.LogoPath, corporateProfile.TenantId);
                                }
                            }
                            phone = userData.PhoneNumber;
                           email = userData.Email;
                            List<CompanyServicesModel> companyServiceList = new List<CompanyServicesModel>();
                            if (corporateProfile.CServices != null)
                            {
                                companyServiceList = JsonConvert.DeserializeObject<List<CompanyServicesModel>>(corporateProfile.CServices);
                            }
                            if (userData.ProfileImageName != null)
                                userData.ProfileImageName = await GetUrlOfImage(userData.ProfileImageName, userData.UserTenantId);
                            model.Add(new CommunicationModel
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
                }
                communicationViewModel.CommunicationModel = model;
                communicationViewModel.ChatRoomModel = chatRoomListModel;
            }
            catch (Exception ex)
            {
                communicationViewModel.CommunicationModel = model;
                communicationViewModel.ChatRoomModel = chatRoomListModel;
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication GetCommunicationModel", User.Identity.Name);
            }
            return communicationViewModel;
        }
        private async Task<CommunicationViewModel> GetCommunicationChatRoomModel()
        {
            var communicationViewModel = new CommunicationViewModel();
            var model = new List<CommunicationModel>();
            //Karthik
            var chatRoomListModel = new List<ChatRoomModel>();
            var chatRoomFromListModel = new List<ChatRoomModel>();
            var chatRoomToListModel = new List<ChatRoomModel>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
               var clientContacts = await operrepo.GetClientContactsList(userId);
                //Karthik
                string companyName = string.Empty;
                string phone = string.Empty;
                string email = string.Empty;
                //Chat Conversation List
                //Chat Rooms and messages
                chatRoomFromListModel = (from tw in db.TwilioChatUserMappings.Where(tw => tw.useridentity.Equals(userId))
                                         join wu in db.WellIdentityUser on tw.receivername equals wu.Id into twu
                                         from cu in twu.DefaultIfEmpty()
                                         select new ChatRoomModel
                                         {
                                             UserId = cu.Id,
                                             UserRoomId = tw.channelsid,
                                             UserRoomName = cu.FirstName + ' ' + cu.LastName,
                                             LastChatDate = "",
                                             LastChatMessage = ".......",
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
                                           LastChatDate = "",
                                           LastChatMessage = ".......",
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
                communicationViewModel.ChatRoomModel = chatRoomListModel;
            }
            catch (Exception ex)
            {
                communicationViewModel.CommunicationModel = model;
                communicationViewModel.ChatRoomModel = chatRoomListModel;
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication GetCommunicationChatRoomModel", User.Identity.Name);
            }
            return communicationViewModel;
        }
        public async Task<IActionResult> Contacts_Read([DataSourceRequest] DataSourceRequest request,string Tenantid)
        {
            try
            {
                List<string> userList = new List<string>();
                var userId = _userManager.GetUserId(User);
                userList.Add(userId);
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                var clientContacts = await operrepo.GetClientContactsList(userId);
                if (clientContacts != null && clientContacts.Count() > 0)
                {
                    userList.AddRange(clientContacts.Select(e => e.ContactId));
                }
                var a=_userManager.Users.ToList();
                var allUser = (from u in _userManager.Users.Where(e => !userList.Contains(e.Id) && (e.TenantId == Tenantid || Tenantid == null))
                               join c in db.CorporateProfile on u.TenantId equals c.ID into corp
                               from cp in corp.DefaultIfEmpty()
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
                return Json(allUser.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication Contacts_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveClientContact([FromBody] List<ClientContact> clients)
        {
            CommunicationViewModel model = new CommunicationViewModel();
            try
            {
                if (clients != null && clients.Count > 0)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
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
                customErrorHandler.WriteError(ex, "Communication SaveClientContact", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClientContact(int clientContactId)
        {
            CommunicationViewModel model = new CommunicationViewModel();
            try
            {
                if (clientContactId != 0)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var result = await operrepo.UpdateClientContacts(clientContactId);
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
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication UpdateClientContact", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
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
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication GetChannelForFromAndToUser", User.Identity.Name);
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
                customErrorHandler.WriteError(ex, "Communication AddUserChannelInTwilioMappings", User.Identity.Name);
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
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication UpdateUserChannelInvitationStatus", User.Identity.Name);
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateLeaveUserChannelStatus(string fromUser, string toUser, string channelId)
        {
            try
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
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication UpdateLeaveUserChannelStatus", User.Identity.Name);
                return null;
            }
        }

        //Karthik
        private IEnumerable<CompanyServicesModel> GetCompanyServices(string tenantId)
        {
            var CompanyServicesModel = new CompanyServicesModel();
            List<CompanyServicesModel> companyServiceList = new List<CompanyServicesModel>();
            try
            {
                var model = new List<CompanyServicesModel>();
                var corporateProfile = (from u in _userManager.Users.Where(e => e.TenantId.Equals(tenantId))
                                        join c in db.CorporateProfile on u.TenantId equals c.TenantId into corp
                                        from cp in corp.DefaultIfEmpty()
                                        select new CorporateProfile
                                        {
                                            Name = cp.Name ?? String.Empty,
                                            LogoPath = cp.LogoPath ?? String.Empty,
                                            Website = cp.Website ?? String.Empty,
                                            CServices = cp.CServices ?? String.Empty
                                        }
                                       ).FirstOrDefault();
                if (corporateProfile.CServices != null)
                {
                    companyServiceList = JsonConvert.DeserializeObject<List<CompanyServicesModel>>(corporateProfile.CServices); ;
                }
                return companyServiceList;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication GetCompanyServices", User.Identity.Name);
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
                customErrorHandler.WriteError(ex, "Communication CompanyServices", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
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

        [HttpPost]
        //Phase II Changes - 03/11/2021
        //messageType added for differentiate Chat/Video Call
        public async Task<IActionResult> SaveMessageNotification(string toUser, string message, int type,string messageType)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    MessageQueue messageQueue = new MessageQueue
                    {
                        From_id = _userManager.GetUserId(User),
                        To_id = toUser,
                        TaskName=message,
                        JobName= messageType,
                        Type = type,
                    };
                    NotificationHandler notificationHandler = new NotificationHandler(db);
                    notificationHandler.AddNotification(messageQueue);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication SaveMessageNotification", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return await Task.FromResult(Json(true));
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
                customErrorHandler.WriteError(ex, "Communication UpdateStatusNotification", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(true);
        }
        public async Task<IActionResult> RefreshChatList()
        {
            try
            {
                var model = await GetCommunicationChatRoomModel();//Getting Chat Room last chat history
                return PartialView("_ChatRoom", model.ChatRoomModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication RefreshChatList", User.Identity.Name);
                return null;
            }
        }
       public IActionResult ManageCall(string phone)
        {
            try
            {
                var model = new ManageCallModel
                {
                    Phone = phone
                };

                return View("_ManageCall", model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication ManageCall", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult UpComingServices(string tenantId)
        {
            List<WellAI.Advisor.Model.ServiceCompany.Models.ProjectViewSRVModel> result = new List<WellAI.Advisor.Model.ServiceCompany.Models.ProjectViewSRVModel>();
            try
            {            
                var operId = WellAIAppContext.Current.Session.GetString("TenantId");
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
               result = projectBusiness.GetUpComingProjectsSRV_Chat(tenantId, operId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UpcomingProject UpcomingProject_Read", User.Identity.Name);               
            }
            return PartialView("_UpComingServices", result);

        }

        [HttpPost]
        //Phase II Changes - 03/11/2021
        //messageType added for differentiate Chat/Video Call
        public JsonResult CloseRoom(string identity,string roomId)
        {
            try
            {
                var result = communication.CloseRoom(identity,roomId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication {", User.Identity.Name);
                return Json("False");
            }
            return Json("true");
        }

    }
}