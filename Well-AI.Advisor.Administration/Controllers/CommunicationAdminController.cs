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
using WellAI.Advisor.Model.Administration;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.DLL.Entity;

using AuthorizeNet.Api.Contracts.V1;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Well_AI_Advisior.API.Authorize.Net;
using Well_AI_Advisior.API.Authorize.Net.Model;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;
using WellAI.Advisor;
using WellAI.Advisor.BLL.Administration;
using Microsoft.AspNetCore.SignalR;
using Well_AI.Advisor.Administration.Hubs;

namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    //[SessionTimeOut]
    public class CommunicationAdminController : BaseController
    {
        //Phase II - Clear Warning      
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
        RoleManager<IdentityRole> _roleManager;
        
        private IWebHostEnvironment _hostingEnvironment;
        private TenantOperatingDbContext _tdbContext;
        private new readonly IConfiguration _configuration;
        private readonly ICommunication communication;
        private new readonly WebAIAdvisorContext db;
        private IHubContext<NotificationHub> _hubContext { get; set; }

        
        public CommunicationAdminController(IConfiguration configuration,TenantOperatingDbContext tdbContext, UserManager<WellIdentityUser> userManager, ICommunication communication, WebAIAdvisorContext db, ISingletonAdministration _singleton, 
            IHubContext<NotificationHub> hubContext, UserManager<StaffWellIdentityUser> staffUserManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment) : base(_singleton,db)
        {
            this.db = db;
            this.communication = communication;
            _userManager = userManager;
            _tdbContext = tdbContext;
            _hubContext = hubContext;
            _staffUserManager = staffUserManager;
            _roleManager = roleManager;
            _hostingEnvironment = environment;
            _configuration = configuration;
        }
        //Phase II - Clear Warning
        public IActionResult Index()
        {
            try
            {
               var model = new CommunicationAdminViewModel();
               return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "CommunicationAdmin Index", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
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
                    CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                    customErrorHandler.WriteError(ex, "Communication DownloadCorpLogo", User.Identity.Name);

                    
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

        private async Task<CommunicationAdminViewModel> GetCommunicationModel()
        {
            var communicationViewModel = new CommunicationAdminViewModel();

            try
            {
                var model = new List<CommunicationAdminModel>();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //Karthik
                var chatRoomListModel = new List<ChatRoomModel>();
                var chatRoomFromListModel = new List<ChatRoomModel>();
                var chatRoomToListModel = new List<ChatRoomModel>();

                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                var clientContacts = await operrepo.GetClientContactsList("957dcba6-6438-4a4d-8424-42669a1983c0");

                //Karthik
                string companyName = string.Empty;
                string phone = string.Empty;
                string email = string.Empty;


                if (clientContacts != null && clientContacts.Count() > 0)
                {
                    foreach (var item in clientContacts)
                    {
                        //Karthik
                        
                        string logopath = string.Empty;
                        string companyWebsite = string.Empty;

                         
                        var userData = await commonBusiness.GetUser(item.ContactId);

                        if (userData != null)
                        {
                            //TU-Karthik-05/29/2020-CorporateProfile empty Handling
                            var corporateProfile = (from u in _userManager.Users.Where(e => e.Id.Equals(item.ContactId))
                                                    join c in db.CorporateProfile on u.TenantId equals c.TenantId into corp
                                                    from cp in corp.DefaultIfEmpty()
                                                    select new CorporateProfileAdmin
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


                            ;
                            List<CompanyServicesAdminModel> companyServiceList = new List<CompanyServicesAdminModel>();
                            
                            if (corporateProfile.CServices != null)
                            {
                                companyServiceList = JsonConvert.DeserializeObject<List<CompanyServicesAdminModel>>(corporateProfile.CServices); ;

                            }

                            if (userData.ProfileImageName != null)
                                userData.ProfileImageName = await GetUrlOfImage(userData.ProfileImageName, userData.UserTenantId);

                            model.Add(new CommunicationAdminModel
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

                    

                }

                communicationViewModel.CommunicationModel = model;
                communicationViewModel.ChatRoomModel = chatRoomListModel;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication GetCommunicationModel", User.Identity.Name);
            }
            return communicationViewModel;
        }
        public async Task<IActionResult> Contacts_Read([DataSourceRequest] DataSourceRequest request, string Tenantid)
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
                var a = _userManager.Users.ToList();
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication Contacts_Read", User.Identity.Name);

                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveClientContact([FromBody] List<ClientContact> clients)
        {
            CommunicationAdminViewModel model = new CommunicationAdminViewModel();
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication SaveClientContact", User.Identity.Name);

                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            //Phase II - Clear warning
            //return PartialView("_AddContacts", model.CommunicationModel);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateClientContact(int clientContactId)
        {
            CommunicationAdminViewModel model = new CommunicationAdminViewModel();
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
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
                    //add participant to channel
                    communication.ConversantionParticipantAdd(channelId, toUser);

                    try
                    {
                        communication.ConversantionParticipantAdd(channelId, fromUser);
                    }
                    catch
                    {                       
                    }
                    

                    return Json(result.ToString());
                }
                else
                {
                    return Json("");
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
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
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication UpdateUserChannelInvitationStatus", User.Identity.Name);

                return Json("");
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication UpdateLeaveUserChannelStatus", User.Identity.Name);

                return Json("");
            }
        }

        //Karthik
        private IEnumerable<CompanyServicesAdminModel> GetCompanyServices(string tenantId)
        {
            var CompanyServicesModel = new CompanyServicesAdminModel();
            List<CompanyServicesAdminModel> companyServiceList = new List<CompanyServicesAdminModel>();
            try
            {
                var model = new List<CompanyServicesAdminModel>();
                var corporateProfile = (from u in _userManager.Users.Where(e => e.TenantId.Equals(tenantId))
                                        join c in db.CorporateProfile on u.TenantId equals c.TenantId into corp
                                        from cp in corp.DefaultIfEmpty()
                                        select new CorporateProfileAdmin
                                        {
                                            Name = cp.Name ?? String.Empty,
                                            LogoPath = cp.LogoPath ?? String.Empty,
                                            Website = cp.Website ?? String.Empty,
                                            CServices = cp.CServices ?? String.Empty
                                        }
                                       ).FirstOrDefault();

                
                if (corporateProfile.CServices != null)
                {
                    companyServiceList = JsonConvert.DeserializeObject<List<CompanyServicesAdminModel>>(corporateProfile.CServices); ;

                }
                return companyServiceList;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication Admin GetCompanyServices", User.Identity.Name);

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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "EditProfile Index", User.Identity.Name);

                return string.Empty;
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
              
                MessageQueue messageQueue = new MessageQueue
                {
                    From_id = _userManager.GetUserId(User),
                    To_id = toUser,
                    TaskName = message,
                    JobName = messageType,
                    Type = type,
                    
                };
               
                NotificationHandler notificationHandler = new NotificationHandler(db);
                notificationHandler.AddNotification(messageQueue);
                await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
                
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication SaveMessageNotification", User.Identity.Name);
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
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
                var model = await GetCommunicationModel();
                return PartialView("_ChatRoom", model.ChatRoomModel);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication ManageCall", User.Identity.Name);

                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
    }
}
