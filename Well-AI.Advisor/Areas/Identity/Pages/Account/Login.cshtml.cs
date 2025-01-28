using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using System.Text;
using WellAI.Advisor.DLL;
using WellAI.Advisor.Model.Identity;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Finbuckle.MultiTenant;
using WellAI.Advisor.Tenant;
using Finbuckle.MultiTenant.Stores;
using System.Security.Claims;
using WellAI.Advisor.Helper;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.DLL.Entity;
using Microsoft.OpenApi.Extensions;
using WellAI.Advisor.BLL.IBusiness;
using Microsoft.AspNetCore.SignalR;
using WellAI.Advisor.Hubs;
using Well_AI_Advisior.API.Authorize.Net;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly WebAIAdvisorContext db;
        private readonly IMultiTenantStore _store;
        RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private IHubContext<NotificationHub> _hubContext { get; set; }

        public LoginModel(SignInManager<WellIdentityUser> signInManager,
            ILogger<LoginModel> logger,
            WebAIAdvisorContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager,
            IConfiguration configuration,
            IMultiTenantStore store, IHubContext<NotificationHub> hubContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.roleManager = roleManager;
            _logger = logger;
            _configuration = configuration;
            db = dbContext;
            _store = store;
            _hubContext = hubContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }



        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            try
            {
                returnUrl = returnUrl ?? Url.Content("~/");

                if (ModelState.IsValid)
                {
                    var r = _store as MultiTenantStoreWrapper<TenantConfigurationStore>;

                    var id = Guid.NewGuid().ToString("D");

                    var res = await r.Store.TryUpdateAsync(new TenantInfo(id, id, id, "", null));

                    ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                    ISubscriptionBusiness subscriptionBusiness = new SubscriptionBusiness(db, roleManager, _userManager);

                    var userExisting = await commonBusiness.GetUserByEmail(Input.Email);
                    if (userExisting == null)
                    {
                        ModelState.AddModelError(string.Empty, "User is not existing in system, please check your email address.");
                        _logger.LogInformation("User is not existing in system, please check your email address.");
                        return Page();
                    }
                    else
                    {
                        var userExist = db.UserSessions.Where(x => x.UserId == userExisting.Id).FirstOrDefault();
                        //if (userExist != null)
                        //{

                        //    DateTime LoggedTime = userExist.SessionTimeStamp;
                        //    DateTime CurTime = DateTime.Now.ToUniversalTime();

                        //    TimeSpan ts = CurTime - LoggedTime;
                        //    ////Temporary Comment
                        //    //if (ts.TotalHours >= 24)
                        //    if (ts.TotalMinutes >= 5)
                        //    {
                        //        var res1 = await commonBusiness.DeleteUsersession(userExisting.Email);

                        //    }
                        //    else
                        //    {
                        //        WellAIAdvisiorApiAuthorizeContext _db = new WellAIAdvisiorApiAuthorizeContext();
                        //        var msg = _db.Configuration.Where(x => x.ConstantName == "ConcurrentLoginWarningMessage").FirstOrDefault();
                        //        //string msg = "This user is already Logged in another device or User Session may not be closed properly. Please try after sometime.";
                        //        ModelState.AddModelError(string.Empty, msg.Value.ToString());
                        //        _logger.LogInformation(msg.Value.ToString());
                        //        return Page();
                        //    }
                        //}
                    }
                    var detail = commonBusiness.GetUserBasicDetail(userExisting.Id);
                    WellAIAppContext.Current.Session.SetString("AccountType1", detail.AccountType.ToString());

                    if (Convert.ToBoolean(detail.IsMaster))
                    {
                        if (detail.AccountType == 1)
                        {
                            if (detail.RegisterPagesCompleteStatus == 1)
                            {
                                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, type = detail.AccountType.ToString() });
                            }
                            else if (detail.RegisterPagesCompleteStatus == 2)
                            {
                                return RedirectToPage("./ForgotPasswordConfirmation");
                            }
                            else if (detail.RegisterPagesCompleteStatus == 3)
                            {
                                return RedirectToPage("Company", new { userId = userExisting.Id });
                            }
                            else if (detail.RegisterPagesCompleteStatus == 4)
                            {
                                return RedirectToPage("UploadDocument", new { userId = userExisting.Id });
                            }
                            else if (detail.RegisterPagesCompleteStatus == 5)
                            {
                                return RedirectToPage("Subscription", new { userId = userExisting.Id });
                            }
                            else if (detail.RegisterPagesCompleteStatus == 6)
                            {
                                return RedirectToPage("PaymentMethod", new { userId = userExisting.Id });
                            }
                            else if (detail.RegisterPagesCompleteStatus == 7)
                            {
                                return RedirectToPage("Subscribe", new { userId = userExisting.Id });
                            }
                        }
                        else
                        {
                            if (detail.RegisterPagesCompleteStatus == 1)
                            {
                                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, type = detail.AccountType.ToString() });
                            }
                            else if (detail.RegisterPagesCompleteStatus == 2)
                            {
                                return RedirectToPage("./ForgotPasswordConfirmation");
                            }
                        }
                    }


                    //Phase II Changes - 03/16/2021
                    //int IsUserSession = db.UserSessions.Where(x => x.UserName == Input.Email).Count();
                    //if(IsUserSession > 0)
                    //{
                    //    ModelState.AddModelError(string.Empty, "User Already Logged in, Please try after some time");
                    //    return Page();
                    //}

                    UserSessions LoginUser = new UserSessions();
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        //getting user detail with email
                        var user = await _userManager.FindByEmailAsync(Input.Email);

                        if (!Convert.ToBoolean(detail.IsMaster))
                        {
                            var roles = await commonBusiness.GetRoleNames(user);
                            if (roles == null || roles.Count == 0)
                            {
                                ModelState.AddModelError(string.Empty, "Invalid account, please check with support team.");
                                return Page();
                            }
                        }

                        //Phase II Changes - 03/09/2021 Redirect to page if Subscription is Deactivated                         
                        if (detail.AccountType == 2) //Account type 2 Dispatch only{
                        {

                            WellAIAppContext.Current.Session.SetString("CorporateProfileId", detail.CorporateProfileId.ToString());

                            //GetProductSubscriptionForProfile for Dispatch only 
                            //user.TenantId = "00000000-0000-0000-0000-000000000000";
                            //var productSubscription = await subscriptionBusiness.GetProductSubscriptionForProfile(userExisting.TenantId);//pass profile id
                            var productSubscription = await commonBusiness.GetProductSubscription(userExisting.TenantId);
                            if (productSubscription.ID.ToString() != "00000000-0000-0000-0000-000000000000" || detail.AccountType == 2)
                            {
                                if (productSubscription.IsEnable == false)
                                {
                                    ModelState.AddModelError(string.Empty, "Subscription was canceled, please check with support team.");
                                    return Page();
                                }
                            }
                        }
                        else 
                        {
                            var productSubscription = await commonBusiness.GetProductSubscription(userExisting.TenantId);
                            if (productSubscription.ID.ToString() != "00000000-0000-0000-0000-000000000000" || detail.AccountType == 1)
                            {
                                if (productSubscription.IsEnable == false)
                                {
                                    ModelState.AddModelError(string.Empty, "Subscription was canceled, please check with support team.");
                                    return Page();
                                }
                            }
                        }

                        WellAIAppContext.Current.Session.Set("Email", Encoding.UTF8.GetBytes(user.Email));
                        WellAIAppContext.Current.Session.Set(Constants.SessionNotExpireKey, Encoding.UTF8.GetBytes(user.Id));
                        WellAIAppContext.Current.Session.SetString(Constants.SessionUserWellIdentity, JsonConvert.SerializeObject(user));
                        if (user.ProfileImageName != null && user.ProfileImageName != "")
                        {
                            var profileImageName = await GetUrlOfImage(user.ProfileImageName, user.TenantId);
                            WellAIAppContext.Current.Session.SetString("ProfileImageName", profileImageName);
                        }
                        else
                        {
                            var profileImageName = "/img/nophotouser1.png";
                            WellAIAppContext.Current.Session.SetString("ProfileImageName", profileImageName);
                        }

                        //Set companyid into context
                        WellAIAppContext.Current.Session.SetString("TenantId", user.TenantId);
                        WellAIAppContext.Current.Session.SetString("AccountType", detail.AccountType.ToString());

                        WellAIAppContext.Current.Session.SetString("IsMaster", detail.IsMaster.ToString());

                        // Set AdminSupportUser , AdminSupportPhone and AdminSupportUserId
                        var twilioCls = new TwilioChat(db, roleManager, _userManager);
                        string AdminSupportUser = twilioCls.GetAdminSupportUser();
                        WellAIAppContext.Current.Session.SetString("AdminSupportUser", AdminSupportUser.ToString());

                        string AdminSupportPhone = twilioCls.GetAdminSupportPhone();

                        WellAIAppContext.Current.Session.SetString("AdminSupportPhone", AdminSupportPhone.ToString());
                        var userData = await commonBusiness.GetAdminUserByEmail(AdminSupportUser);

                        string AdminSupportUserId = Convert.ToString(userData.Id);
                        WellAIAppContext.Current.Session.SetString("AdminSupportUserId", AdminSupportUserId.ToString());

                        string AdminSupportUserName = "Well-AI Support";
                        WellAIAppContext.Current.Session.SetString("AdminSupportUserName", AdminSupportUserName.ToString());
                        //Check for role statuse operator user or service provider user...
                        //If logged in successfull then need to add status of user online

                        commonBusiness.UpdateUserStatus(user.Id, true);

                        //List<MessageQueue> userNotificationModel = commonBusiness.GetUserNotificationDetails(user.Id);

                        var callCount = await commonBusiness.GetUserNotificationCount(user.Id, "c"); //userNotificationModel.Where(x => x.Type == 0).Count();
                        var messageCount = await commonBusiness.GetUserNotificationCount(user.Id, "m"); //userNotificationModel.Where(x => x.Type == 0).Count();//userNotificationModel.Where(x => x.Type != 0).Count();

                        //count only
                        var callCount1 = db.MessageQueues.Where(x => x.Type == 0 && x.To_id == user.Id && x.IsActive == 1).Count();
                        var msgCount = db.MessageQueues.Where(x => x.Type != 0 && x.To_id == user.Id && x.IsActive == 1).Count();
                        WellAIAppContext.Current.Session.SetString("CallCount", callCount1.ToString());
                        WellAIAppContext.Current.Session.SetString("MessageCount", msgCount.ToString());


                        //WellAIAppContext.Current.Session.SetString("CallCount", callCount.ToString());
                        //WellAIAppContext.Current.Session.SetString("MessageCount", messageCount.ToString());


                        WellAIAppContext.Current.Session.SetString("UserId", user.Id.ToString());

                        var model = await commonBusiness.GetCorporateProfileByTenant(user.TenantId);
                        var companyName = string.IsNullOrEmpty(model.Name) ? detail.Name : model.Name;

                        WellAIAppContext.Current.Session.SetString("CompanyName", companyName);
                        WellAIAppContext.Current.Session.SetString("LoginUserName", user.Email);


                        if (detail.AccountType == 0) //Account type 0 operator company
                        {
                            //if (user.Email == "operatordisonlyuser@yopmail.com")
                            //{
                            //    WellAIAppContext.Current.Session.SetString("subscriptiontype", "Dispatch");
                            //    returnUrl = "/OperatingDashboard/AdvisorWithDispatch";
                            //    return LocalRedirect(returnUrl);
                            //}
                            //else
                            //{
                            //    WellAIAppContext.Current.Session.SetString("subscriptiontype", "AdvisorAndDispatch");
                            //    returnUrl = "/OperatingDashboard/AdvisorWithDispatch";
                            //    return LocalRedirect(returnUrl);
                            //}
                            WellAIAppContext.Current.Session.SetString("subscriptiontype", "Adivisor");
                            //subscriptionModel = "Advisor";
                            returnUrl = "/OperatingDashboard/AdvisorWithDispatch";
                            return LocalRedirect(returnUrl);
                        }
                        else if (detail.AccountType == 1) //Account type 1 service company
                        {

                            //if (user.Email == "servicedispatchuser01@yopmail.com")
                            //{
                            //    WellAIAppContext.Current.Session.SetString("subscriptiontype", "Dispatch");
                            //    returnUrl = "/ServiceDashboard/AdvisorWithDispatch";
                            //    return LocalRedirect(returnUrl);
                            //}
                            //else if (user.Email == "servicedispatchuser02@yopmail.com")
                            //{
                                WellAIAppContext.Current.Session.SetString("subscriptiontype", "Adivisor");
                                //subscriptionModel = "Advisor";
                                returnUrl = "/ServiceDashboard/AdvisorWithDispatch";
                                return LocalRedirect(returnUrl);
                            //}
                            //else
                            //{
                            //    WellAIAppContext.Current.Session.SetString("subscriptiontype", "AdvisorAndDispatch");
                            //    returnUrl = "/ServiceDashboard/AdvisorWithDispatch";
                            //    return LocalRedirect(returnUrl);
                            //}
                        }
                        else if(detail.AccountType == 2) //Account type 2 Dispatch only
                        {
                            WellAIAppContext.Current.Session.SetString("subscriptiontype", "Dispatch");
                            returnUrl = "/ServiceDashboard/AdvisorWithDispatch";
                            return LocalRedirect(returnUrl);
                        }
                        else if (detail.AccountType == 4) //Account type 4 Service and Dispatch
                        {
                            WellAIAppContext.Current.Session.SetString("subscriptiontype", "AdvisorAndDispatch");
                            returnUrl = "/ServiceDashboard/AdvisorWithDispatch";
                            return LocalRedirect(returnUrl);
                        }
                        else if (detail.AccountType == 3) //Account type 3 Operator and Dispatch
                        {
                            WellAIAppContext.Current.Session.SetString("subscriptiontype", "AdvisorAndDispatch");
                            returnUrl = "/OperatingDashboard/AdvisorWithDispatch";
                            return LocalRedirect(returnUrl);
                        }

                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid email and password.");
                        _logger.LogInformation("User login failed.");
                        return Page();
                    }
                }
                // If we got this far, something failed, redisplay form
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "Login - Post", Input.Email);
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                return Page();
            }
        }

        private async Task<string> GetUrlOfImage(string filename, string tenantId)
        {
            try
            {
                if (tenantId != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    var folderName = _configuration.GetSection("FolderName");

                    var items = await AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenantId, folderName["CompanyUserProfile"], filename);
                    return items;
                }
                else
                {
                    return "/img/nophotouser1.png";
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Login user profile Image", User.Identity.Name);

                return string.Empty;
            }
        }


        public IActionResult OnGetUpdateNotificationStatus(int messageQueueId)
        {
            var userId = WellAIAppContext.Current.Session.GetString(DLL.Constants.SessionNotExpireKey);
            ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
            commonBusiness.UpdateNotificationStatus(messageQueueId);
            List<MessageQueue> userNotificationModel = commonBusiness.GetUserNotificationDetails(userId);

            //count only
            var callCount = db.MessageQueues.Where(x => x.Type == 0 && x.To_id == userId && x.IsActive == 1).Count();
            var msgCount = db.MessageQueues.Where(x => x.Type != 0 && x.To_id == userId && x.IsActive == 1).Count();
            WellAIAppContext.Current.Session.SetString("CallCount", callCount.ToString());
            WellAIAppContext.Current.Session.SetString("MessageCount", msgCount.ToString());

            //WellAIAppContext.Current.Session.SetString("CallCount", userNotificationModel.Where(x => x.Type == 0).Count().ToString());
            //WellAIAppContext.Current.Session.SetString("MessageCount", userNotificationModel.Where(x => x.Type != 0).Count().ToString());


            return new JsonResult(new { callValue = userNotificationModel.Where(x => x.Type == 0).Count().ToString(), messageValue = userNotificationModel.Where(x => x.Type != 0).Count().ToString() });
        }

        public IActionResult OnGetRefreshView(int notificationType)
        {
            var userId = WellAIAppContext.Current.Session.GetString(DLL.Constants.SessionNotExpireKey);
            ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
            List<MessageQueue> userNotificationModel = commonBusiness.GetUserNotificationDetails(userId);
            // count only
            var callCount = db.MessageQueues.Where(x => x.Type == 0 && x.To_id == userId && x.IsActive == 1).Count();
            var msgCount = db.MessageQueues.Where(x => x.Type != 0 && x.To_id == userId && x.IsActive == 1).Count();
            WellAIAppContext.Current.Session.SetString("CallCount", callCount.ToString());
            WellAIAppContext.Current.Session.SetString("MessageCount", msgCount.ToString());

            //WellAIAppContext.Current.Session.SetString("CallCount", userNotificationModel.Where(x => x.Type == 0).Count().ToString());
            //WellAIAppContext.Current.Session.SetString("MessageCount", userNotificationModel.Where(x => x.Type != 0).Count().ToString());

            foreach (var item in userNotificationModel)
            {
                // Well-AI Phase II Changes 02/01/2021 - Type 7 for MSA Document review 
                if (item.Type != 5 && item.Type != 3 && item.Type != 6 && item.Type != 7 && item.Type != 8 && item.Type != 9 && item.Type != 10)
                {
                    var notificationStatus = (NotificationTypeEnum)item.Type;

                    item.JobName = notificationStatus.GetAttributeOfType<DisplayAttribute>().Name;
                }
            }
            if (notificationType == 0)
            {
                return Partial("~/Views/Shared/_CallNotification.cshtml", userNotificationModel);
            }
            else
            {
                return Partial("~/Views/Shared/_MessageNotification.cshtml", userNotificationModel);
            }
        }

        public IActionResult OnGetScrollView(int notificationType, int skip, int take)
        {
            var userId = WellAIAppContext.Current.Session.GetString(DLL.Constants.SessionNotExpireKey);
            ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
            List<MessageQueue> userNotificationModel = new List<MessageQueue>();

            userNotificationModel = commonBusiness.GetUserNotificationDetailsScroll(userId, skip, take);

            return Partial("~/Views/Shared/_MessageNotification.cshtml", userNotificationModel);
        }

        public async Task<IActionResult> OnGetNotificationCount()
        {
            var userId = WellAIAppContext.Current.Session.GetString(DLL.Constants.SessionNotExpireKey);
            ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
            List<MessageQueue> userNotificationModel = commonBusiness.GetUserNotificationDetails(userId);
            var callCount = db.MessageQueues.Where(x => x.Type == 0 && x.To_id == userId && x.IsActive == 1).Count();
            var msgCount = db.MessageQueues.Where(x => x.Type != 0 && x.To_id == userId && x.IsActive == 1).Count();
            WellAIAppContext.Current.Session.SetString("CallCount", callCount.ToString());
            WellAIAppContext.Current.Session.SetString("MessageCount", msgCount.ToString());
            return await Task.FromResult(new JsonResult(new { callValue = callCount.ToString(), messageValue = msgCount.ToString() }));
        }

        public async Task<IActionResult> OnGetClosingNotificationBids()
        {
            await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
            return new JsonResult(new { message = "success" });
        }

        //Phase II Changes - 03/16/2021
        //public async Task<IActionResult> OnGetRefreshUserSession()
        //{
        //    int Result = 0;
        //    var Email = WellAIAppContext.Current.Session.GetString("Email");
        //    ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
        //    //var RefershTime =  _configuration.GetSection("UserSessionRefrsh")["RefreshTime"];
        //    if (Email != null)
        //    {
        //        // await commonBusiness.DeleteUsersession(Email);
        //        var UserDetails = await _userManager.FindByEmailAsync(Email);
        //        UserSessions User = new UserSessions();
        //        if (ModelState.IsValid)
        //        {
        //            User.SessionId = HttpContext.Session.Id;
        //            User.UserId = UserDetails.Id;
        //            User.UserName = UserDetails.UserName;
        //            User.SessionTimeStamp = DateTime.Now;
        //        }
        //        if (UserDetails != null)
        //        {
        //            //Temporary Comment
        //            //Result = await commonBusiness.SaveUsersession(User);
        //        }
        //    }
        //    return new JsonResult(new { });
        //}
        public async Task<IActionResult> OnGetRefreshUserSession()
        {
            int Result = 0;
            var Email = WellAIAppContext.Current.Session.GetString("Email");
            ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
            //var RefershTime =  _configuration.GetSection("UserSessionRefrsh")["RefreshTime"];
            if (Email != null)
            {
                // await commonBusiness.DeleteUsersession(Email);
                var UserDetails = await _userManager.FindByEmailAsync(Email);
                UserSessions User = new UserSessions();
                if (ModelState.IsValid)
                {
                    User.SessionId = HttpContext.Session.Id;
                    User.UserId = UserDetails.Id;
                    User.UserName = UserDetails.Email;
                    User.SessionTimeStamp = DateTime.Now.ToUniversalTime();

                }
                if (UserDetails != null)
                {
                    var UserSessionExits = db.UserSessions.Where(x => x.UserName == User.UserName).FirstOrDefault();
                    if (UserSessionExits != null)
                    {
                        //Update session
                        Result = await commonBusiness.UpdateUsersession(User, UserDetails.Email);
                    }
                    else
                    {
                        //Insert session
                        Result = await commonBusiness.SaveUsersession(User);
                    }


                }
            }
            return new JsonResult(new { });
        }
    }

}
