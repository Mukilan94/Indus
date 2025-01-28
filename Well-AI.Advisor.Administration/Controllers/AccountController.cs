using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Tenant;
using WellAI.Advisor.DLL;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.Model.Identity;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.DLL.Entity;


namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrappe
    public class AccountController : BaseController
    {
        private readonly IMultiTenantStore _store;

        private readonly UserManager<StaffWellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<StaffWellIdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public static string email { get; set; }


        //Phase II Changes - 03/17/2021
        private new readonly WebAIAdvisorContext db;

        public AccountController(UserManager<StaffWellIdentityUser> userManager, SignInManager<StaffWellIdentityUser> signInManager, ILogger<AccountController> logger,
            IMultiTenantStore store, WebAIAdvisorContext dbContext, RoleManager<IdentityRole> roleManager) : base(userManager, signInManager,dbContext)
        {
            _store = store;
            db = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _signInManager = signInManager;
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            //Phase II Changes - 03/16/2021
            await DeleteUserSession();
            return RedirectToAction("login", "account");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            try
            {
                if (signInManager.IsSignedIn(User))
                {

                    return Redirect("/Customer/Operator");
                }
                await signInManager.SignOutAsync();
                return View();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Account Login", User.Identity.Name);
                
                return null;
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (signInManager.IsSignedIn(User))
                {
                    return Redirect("/Customer/Operator");
                }

                var userExist = db.StaffUserSessions.Where(x => x.UserName == model.Email).FirstOrDefault();
                //if (userExist != null)
                //{
                //    DateTime LoggedTime = userExist.SessionTimeStamp;
                //    DateTime CurTime = DateTime.Now.ToUniversalTime();

                //    TimeSpan ts = CurTime - LoggedTime;
                //    if (ts.TotalMinutes >= 5)
                //    {
                //        var res1 = await DeleteUserSessionInTable(userExist.UserName);
                //    }
                //    else
                //    {
                //        string msg = "This user is already Logged in another device or User Session may not be closed properly. Please try after sometime.";
                //        ModelState.AddModelError(string.Empty, msg);
                //        _logger.LogInformation(msg);
                //        return View(model);
                //    }

                //}
                //Phase II Changes - 03/16/2021
                //int IsUserSession = db.StaffUserSessions.Where(x => x.UserName == model.Email).Count();
                //if (IsUserSession > 0)
                //{
                //    ModelState.AddModelError(string.Empty, "User Already Logged in, Please try after some time");
                //}
                //else
                //{
                var IsActiveUser = userManager.FindByEmailAsync(model.Email).Result;
              
                if (IsActiveUser == null || !IsActiveUser.IsActive)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }  
                else
                if (ModelState.IsValid)
                {

                    var result = await signInManager.PasswordSignInAsync(
                        model.Email, model.Password, model.RememberMe, false);
                    WellAI.Advisor.DLL.Data.StaffUserSessions User = new WellAI.Advisor.DLL.Data.StaffUserSessions();
                    if (result.Succeeded)
                    {
                        User.SessionId = HttpContext.Session.Id;
                        User.UserId = IsActiveUser.Id;
                        User.UserName = IsActiveUser.Email;
                        User.SessionTimeStamp = DateTime.Now.ToUniversalTime(); ;

                        email = model.Email;
                        var r = _store as MultiTenantStoreWrapper<TenantConfigurationStore>;
                        var id = Guid.NewGuid().ToString("D");
                        var res = await r.Store.TryUpdateAsync(new TenantInfo(id, id, id, "", null));

                        var user = await userManager.FindByEmailAsync(model.Email);
                        await userManager.RemoveClaimAsync(user, new Claim("FullName", user.FullName));
                        await userManager.AddClaimAsync(user, new Claim("FullName", user.FullName));
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            //Phase II Changes - 03/10/2021
                            WellAIAppContext.Current.Session.SetString("CompanyId", user.Id.ToString());
                            WellAIAppContext.Current.Session.Set(Constants.SessionAdminNotExpireKey, Encoding.UTF8.GetBytes(user.Id));
                            WellAIAppContext.Current.Session.SetString(Constants.SessionUserWellIdentity, JsonConvert.SerializeObject(user));
                            WellAIAppContext.Current.Session.SetString(Constants.SessionAdminUser, user.Email.ToString());
                            return Redirect("/Customer/Operator");
                        }
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

                //}

                return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Account Login model", User.Identity.Name);
                
                return null;
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token)
        {
            try
            {
                signInManager.SignOutAsync();
                ResetPasswordViewModel obj = new ResetPasswordViewModel
                {
                    Token = token
                };
                return View(obj);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Account ResetPassword", User.Identity.Name);
                
                return null;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordViewModel obj)
        {
            try
            {
                StaffWellIdentityUser user = userManager.
                         FindByNameAsync(obj.Email).Result;

                var result = userManager.ResetPasswordAsync(user, obj.Token, obj.Password).Result;
                if (result.Succeeded)
                {
                    TempData["Message"] = "Password reset successful!";
                    return View("login");
                }
                else
                {
                    ViewBag.Message = "Error while resetting the password!";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Account ResetPassword Obj", User.Identity.Name);
                
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterStaffViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Copy data from RegisterViewModel to IdentityUser
                    var user = new StaffWellIdentityUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        FullName = model.FullName
                    };

                    // Store user data in AspNetUsers database table
                    var pass = Utils.GenerateRandomPassword();

                    var result = await userManager.CreateAsync(user, pass);

                    // If user is successfully created, sign-in the user using
                    // SignInManager and redirect to index action of HomeController 
                    if (result.Succeeded)
                    {
                        return RedirectToAction("login", "account");
                    }

                    // If there are any errors, add them to the ModelState object
                    // which will be displayed by the validation summary tag helper
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Account Register", User.Identity.Name);
                
                return null;
            }
        }

        //Phase II Changes - 03/16/2021
        [HttpPost]
        public async Task<IActionResult> RefreshUserSession()
       {
            try
            {
                var email = WellAIAppContext.Current.Session.GetString(Constants.SessionAdminUser);
                if (email != null)
                {
                    var sessionDetails = db.StaffUserSessions.Where(x => x.UserName == email).FirstOrDefault();
                    //Phase-II Changes
                    //StaffUserSessions session = new StaffUserSessions();
                    if (sessionDetails != null)
                    {
                        if (ModelState.IsValid)
                        {  
                            //Phase-II Changes
                            //update existing session
                            //sessionDetails = new StaffUserSessions();
                            //sessionDetails.SessionId = HttpContext.Session.Id;
                            sessionDetails.UserId = sessionDetails.UserId;
                            sessionDetails.UserName = sessionDetails.UserName;
                            sessionDetails.SessionTimeStamp = DateTime.Now.ToUniversalTime();
                            db.StaffUserSessions.Update(sessionDetails);
                            await db.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        //add new session
                        //  StaffUserSessions session = new StaffUserSessions();
                        WellAI.Advisor.DLL.Data.StaffUserSessions session = new WellAI.Advisor.DLL.Data.StaffUserSessions();
                        session.SessionId = HttpContext.Session.Id;
                        session.UserId = Encoding.UTF8.GetString(WellAIAppContext.Current.Session.Get(Constants.SessionAdminNotExpireKey));
                        session.UserName = email;
                        session.SessionTimeStamp = DateTime.Now.ToUniversalTime();

                        db.StaffUserSessions.Add(session);
                        await db.SaveChangesAsync();
                    }
                }
                //var currentTenantId = HttpContext.Session.GetString("AdminSessionCurrentTenantId");
                //WellAIAppContext.Current.Session.SetString("AdminSessionCurrentTenantId", currentTenantId.ToString());

                return new JsonResult(new { });
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Account RefreshUserSession", User.Identity.Name);
                
                return null;
            }
        }
        /// <summary>
        /// Phase II Changes - Delete user Session - Prevent Same user login 
        /// </summary>
        private async Task<int> DeleteUserSession()
        {
            var email = WellAIAppContext.Current.Session.GetString(Constants.SessionAdminUser);
            try
            {
                if (email != null)
                {
                    var sessionDetails = db.StaffUserSessions.Where(x => x.UserName == email).AsNoTracking();
                    WellAI.Advisor.DLL.Data.StaffUserSessions session = new WellAI.Advisor.DLL.Data.StaffUserSessions();
                    //StaffUserSessions session = new StaffUserSessions();
                    if (sessionDetails != null)
                    {

                        foreach (var usrSession in sessionDetails)
                        {
                            if (usrSession.SessionId != null)
                            {
                                db.StaffUserSessions.Remove(usrSession);
                            }
                            
                        }
                        await db.SaveChangesAsync();
                    }                    
                }
                return 1;

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Account RefreshUserSession", User.Identity.Name);
                
                return 0;
            }
        }

        
        public IActionResult OnPassWordChange()
        {

            return PartialView("_PassWordChanges");
        }
        [HttpPost]
        public async Task<bool> ChangePassword([FromBody] Password password)
        {
            try
            {
                //Get User details
                var user = await _userManager.GetUserAsync(User);
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, password.CurrentPassWord, password.NewPassWord);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                 customErrorHandler.WriteError(ex, "Change Password", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        [HttpGet]
        public async Task<bool> IsValidPassword(string passWord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                       //assign email id
                        string userName = email;   //email get login model
                        var result = await _signInManager.PasswordSignInAsync(userName, passWord, false, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                        customErrorHandler.WriteError(ex, "Change Password", User.Identity.Name);
                        _logger.LogInformation(ex.Message);
                        ModelState.AddModelError(string.Empty, "IsValidPassword");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Change Password", User.Identity.Name);
                 _logger.LogInformation(ex.Message);
                 return false;
            }
        }
        public async Task<int> DeleteUserSessionInTable(string Email)
        {
            try
            {
                int Result = 0;
                var UserSessionExits = db.StaffUserSessions.Where(x => x.UserName == Email).ToList();
                foreach (var user in UserSessionExits)
                {
                    db.StaffUserSessions.Remove(user);
                    Result = await db.SaveChangesAsync();
                }

                return Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Account DeleteUserSessionInTable", User.Identity.Name);

                return 0;
            }
        }
    }
}