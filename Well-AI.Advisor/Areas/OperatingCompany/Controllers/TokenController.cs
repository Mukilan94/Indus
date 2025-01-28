using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Conversations.V1.Service;
using Well_AI.Advisor.Communication;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class TokenController : BaseController
    {
        private readonly ICommunication communication;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        public TokenController(ICommunication communication,
            SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager,
            WebAIAdvisorContext dbContext)
        : base(userManager, dbContext)
        {
            this.communication = communication;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
        }

        public ActionResult GenerateCallToken()
        {
            try
            {
                string role = "customer";
                var userIdentity = (ClaimsIdentity)User.Identity;
                var token = communication.GenerateCallToken(role);
               

                return Json(new { role, token });
                //return new JsonResult(token);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GenerateCallToken", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public ActionResult GenerateVideoToken()
        {
            try
            {
                var userIdentity = (ClaimsIdentity)User.Identity;
                var result = communication.GenerateVideoToken(userIdentity.Name);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GenerateVideoToken", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public ActionResult GenerateChatToken()
        {
            try
            {
                var userIdentity = (ClaimsIdentity)User.Identity;
                ICommonRepository commonRepo = new CommonRepository(db, _roleManager, _userManager);
                string userName = commonRepo.GetUserBasicDetailByEmail(userIdentity.Name);
                var result = communication.GenerateChatToken(userName);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GenerateChatToken", User.Identity.Name);
               string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<ActionResult> GenerateUserChatToken(string userName)
        {
            try
            {

                //string accountSid = "AC1820ebda6a9235f7001e441ea8e21ff8";
                //string authToken = "890aeeb62b58893a7c03e7f50ad4f712";

                //TwilioClient.Init(accountSid, authToken);

                //ConversationResource.Delete("IS33046f11e9de4e45808afe9bbb230a88", "CH44617963b9b74a3eaaefad4f91053d0a");
                //ServiceResource.Delete(pathSid: "IS083cc84d962948e88aedea2805138cf2");



                var twiliochannel = new TwilioChat(db, _roleManager, _userManager);
                ICommonRepository commonRepo = new CommonRepository(db, _roleManager, _userManager);
                WellIdentityUser user = new WellIdentityUser();
                user = await commonRepo.GetUserByEmail(userName);
                if (user != null)
                {
                    var useridentity = user.Id;//twiliochannel.GetUniqueIdentifier(userName);
                    var result = await communication.GenerateChatToken(useridentity);
                    return new JsonResult(result);
                }
                else
                {
                    var adminuser = await commonRepo.GetAdminUserByEmail(userName);
                    if (adminuser != null)
                    {
                        var useridentity = Convert.ToString(adminuser.Id);//twiliochannel.GetUniqueIdentifier(userName);
                        var result = await communication.GenerateChatToken(useridentity);
                        return new JsonResult(result);
                    }
                    else
                    {
                        return new JsonResult("");
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GenerateUserChatToken", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpGet]
        public ActionResult GenerateRoomName()
        {
            try
            {
                var roomName = Guid.NewGuid().ToString();
                return new JsonResult(roomName);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GenerateRoomName", User.Identity.Name);
                return null;
            }
        }
        [HttpGet]
        public ActionResult GenerateChannelName()
        {
            try
            {
                var channelName = Guid.NewGuid().ToString();
                return new JsonResult(channelName);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GenerateChannelName", User.Identity.Name);
                return null;
            }
        }
    }
}