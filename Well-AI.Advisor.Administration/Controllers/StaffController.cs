using System;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Common;

namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    //[SessionTimeOut]
    public class StaffController : BaseController
    {
        //Phase II - Clear Warning
        private new readonly WebAIAdvisorContext db;        
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<StaffWellIdentityUser> _userManager;
        public StaffController(ISingletonAdministration _singleton, WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, 
            UserManager<StaffWellIdentityUser> userManager) :base(_singleton, userManager, db)
        {
            this.db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                status = "active";
            }
            ViewBag.Status = status.ToLower();
            return View();
        }

        #region Staff


        public ActionResult Staff_Read([DataSourceRequest] DataSourceRequest request,string status)
        {
            try
            {
                
                bool IsActive = true;
                if (status.ToLower() == "deactivate")
                {
                    IsActive = false;
                }
                var serviceCategory = _singleton.staffBusiness.GetStaffs(IsActive).Result;

                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Staff Staff_Read", User.Identity.Name);
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Staff_Create([DataSourceRequest] DataSourceRequest request, RegisterStaffViewModel input)
        {
            try
            {
                ModelState.Remove("Password");
                var pass = Utils.GenerateRandomPassword();
                input.Password = pass;
                EmailHandler emailHandler = new EmailHandler();
                var callbackUrl = "http://" + Request.Host.Value;

                var result = await _singleton.staffBusiness.AddStaff(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                }
                else
                {
                    MessageToQueue message = new MessageToQueue
                    {
                        FromEmail = null,
                        FromName = "Well AI",
                        MsgBody = $"Dear Member, please remember your password " + pass + ".<br/>You can login with your email and provided password to <a href='" + callbackUrl + "'>Well AI Advisor site</a>.",
                        MsgSubject = "Welcome to Well AI Advisor",
                        ToEmail = input.Email,
                        ToName = input.FullName
                    };
                    emailHandler.SendMessageToQueue(message);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Staff Staff_Create", User.Identity.Name);
                
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Staff_Update([DataSourceRequest] DataSourceRequest request, RegisterStaffViewModel input)
        {
            try
            {
                ModelState.Remove("Password");
                if (ModelState.IsValid)
                {
                    var result = await _singleton.staffBusiness.UpdateStaff(input);
                    if (result.ErrorMessage != null)
                    {
                        ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    }
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Staff Staff_Update", User.Identity.Name);
                
                return null;
            }
        }
        #endregion

        #region Staff ForgotPassword

        public IActionResult ForgotPassword(string username)
        {
            try
            {
                StaffWellIdentityUser user = userManager.FindByNameAsync(username).Result;

                if (user == null)
                {
                    ViewBag.Message = "Error while resetting your password!";
                    return View("Error");
                }

                var token = userManager.GeneratePasswordResetTokenAsync(user).Result;

                var resetLink = Url.Action("ResetPassword",
                                "Account", new { token = token },
                                 protocol: HttpContext.Request.Scheme);

                // code to email the above link
                EmailHandler emailHandler = new EmailHandler();
                MessageToQueue message = new MessageToQueue
                {
                    FromEmail = null,
                    FromName = "Well AI",
                    MsgBody = $"Dear Customer, please click on  <a href='{resetLink}'>link</a> to reset password, thanks",
                    MsgSubject = "Reset Password",
                    ToEmail = user.Email,
                    ToName = "Customer"
                };
                ViewBag.Message = "Password reset link has been sent to your email address!";
                return Json(resetLink);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Staff ForgotPassword", User.Identity.Name);
                
                return null;
            }
        }
        #endregion
    }
}