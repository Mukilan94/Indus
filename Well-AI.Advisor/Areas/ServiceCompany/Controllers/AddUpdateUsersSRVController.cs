using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Collections.Generic;
using Well_AI.Advisor.Log.Error;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class AddUpdateUsersSRVController : BaseController
    {
        private readonly ILogger<AddUpdateUsersSRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        public AddUpdateUsersSRVController(
           UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           ILogger<AddUpdateUsersSRVController> logger,
           WebAIAdvisorContext dbContext)
        : base(userManager, dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            db = dbContext;
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
                        //string returnUrl = @"/Identity/Account/Login";
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }

                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                //var result = await commonBusiness.GetUserSRVList(tenantId);
                var roles = commonBusiness.GetRoles(tenantId);
                ViewData["roles"] = roles;
                return View();
           
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Add/Upate Users Index SRV", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/ServiceDashboard/Error";
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "ManageUsers", TenantId);
            }
            else
            {
                return false;
            }
        }
        [AcceptVerbs("Get")]
        public async Task<IActionResult> UserCount_Check([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.DLL.Entity.ProductSubscriptionModel product)
        {   
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            //var usercount = (from user in db.Subscription
            //                where user.TenantId.Equals(tenantId)
            //                select user); 

            var usercount = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                           

            var usertotalcount = (from u in _userManager.Users
                             join tu in db.TenantUsers on u.Id equals tu.UserId
                             where tu.TenantId == tenantId
                             select u).ToList().Count();

         

            var UserCounts = new UserCountModel
            {
                Usercount = usercount.SubscriptionCount,
                UserTotalCount=usertotalcount
            };
            return await Task.FromResult(Json(UserCounts));
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddUpdateUsers_Create([DataSourceRequest] DataSourceRequest request, UserViewSRVModel input)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                if (input.IsPrimary)
                {
                    var tenantUsers = db.WellIdentityUser.Where(x => x.TenantId == input.UserTenantId && x.Primary == true).ToList();
                    if (tenantUsers.Count > 0)
                    {
                        ModelState.AddModelError("IsPrimary", "Primary Contact Already exits");
                        return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                    }
                }
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                if (ModelState.IsValid && input != null)
                {
                    var user = new WellIdentityUser
                    {
                        UserName = input.Email,
                        FirstName = input.FirstName,
                        MiddleName = input.MiddleName,
                        LastName = input.LastName,
                        JobTitle = input.JobTitle,
                        Primary = input.IsPrimary,
                        Address = input.Address,
                        City = input.City,
                        AdditionalNotes = input.AdditionalNotes,
                        Email = input.Email,
                        Mobile=input.Mobile,
                        PhoneNumber = input.PhoneNumber,
                        TenantId = tenantId,
                        WellUser=true,
                        EmailConfirmed = true
                    };
                    var pass = ServiceUtils.GenerateRandomPassword();
                    var result = await _userManager.CreateAsync(user, pass);
                    if (result.Succeeded)
                    {
                        var allroles = commonBusiness.GetRoles(tenantId);
                        // add new roles
                        if (!string.IsNullOrEmpty(input.SelectedRoles))
                        {
                            var roles = input.SelectedRoles.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < roles.Length; i++)
                            {
                                var roleName = allroles.FirstOrDefault(x => x.Id == roles[i]);
                                await commonBusiness.AddUserRole(user, roleName.Name);
                            }
                        }
                        //Karthik-Changed validation to check against user.Id (new user) from userId (old user)
                        var tenantExistId = await commonBusiness.GetTenantIdByUserId(user.Id);
                        if (tenantExistId == null || tenantExistId == "")
                        {
                            user.TenantId = tenantId;
                            var result1 = await _userManager.UpdateAsync(user);
                            await commonBusiness.CreateTenantUser(user.Id, tenantId);
                        }
                        int accountType = 1;
                        var crmUserBasicDetail = new CrmUserBasicDetail
                        {
                            UserId = user.Id,
                            Name = string.Format("{0} {1} {2}", input.FirstName, input.MiddleName, input.LastName),
                            AccountType = accountType,
                            IsActive = true,
                            IsMaster = false,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow
                        };
                        var status = commonBusiness.CreateUserBasicDetail(crmUserBasicDetail);
                        if (status == true)
                        {
                            EmailHandler emailHandler = new EmailHandler();
                            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            //var callbackUrl = "http://" + Request.Host.Value;
                            //var flagStatus = await emailHandler.SendEmailAsync("Member", input.Email, "Welcome to Well AI Advisor", $"Dear Member, please remember your password " + pass + ".<br/>You can login with your email and provided password to <a href='" + callbackUrl + "'>Well AI Advisor site</a>.");
                            var callbackUrl = "http://" + Request.Host.Value + "/Identity/Account/ConfirmEmail?userId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id)) + "&code=" + pass; //Url.Page("Account/ResetPassword", pageHandler: null, values: new { area = "Identity", code },
                            var flagStatus = await emailHandler.SendEmailAsync("Member", input.Email, "Welcome to Well AI Advisor", $"Dear Member, please remember your password " + pass + " ,please click on  <a href='" + callbackUrl + "'><b>Link</b></a> to confirm you email address, thanks");

                            commonBusiness.UpdateUserPagesCompleteStatus(3, user.Id);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Add/Upate Users Crete SRV", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
        }
        /// <summary>
        /// Call from multiselect on edit from to setup initial roles of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AcceptVerbs("Get")]
        public async Task<JsonResult> SetSelected(string userId)
        {
            List<object> result = new List<object>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var user = await commonBusiness.GetUserSRV(userId);
                foreach (var role in user.roles)
                {
                    result.Add(new { Id = role.Id, Name = role.Name });
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Add/UpateUsersSRV SetSelected", User.Identity.Name);
            }
            return Json(result);
        }
        public async Task<IActionResult> AddUpdateUsers_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<UserViewSRVModel> result = new List<UserViewSRVModel>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                result = await commonBusiness.GetUserSRVList(tenantId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Add/UpateUsersSRV AddUpdateUsers_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
     //   [AcceptVerbs("Post")]
       [HttpPost]
        public async Task<IActionResult> UpdateUsers_Update([DataSourceRequest] DataSourceRequest request, UserViewSRVModel input)
        {
            try
            {
                if (input != null && ModelState.IsValid)
                {
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    if (input.IsPrimary)
                    {
                        var tenantUsers = db.WellIdentityUser.Where(x => x.TenantId == input.UserTenantId && x.Primary == true).ToList();
                        if (tenantUsers.Count > 0)
                        {
                            ModelState.AddModelError("IsPrimary", "Primary Contact Already exits");
                            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    var GetUser = await _userManager.FindByEmailAsync(input.Email);
                    var userId = _userManager.GetUserId(User);
                    var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                    var user = new WellIdentityUser
                    {
                        Id = Convert.ToString(input.UserID),
                        UserName = input.Email,
                        FirstName = input.FirstName,
                        MiddleName = input.MiddleName,
                        LastName = input.LastName,
                        JobTitle = input.JobTitle,
                        Primary = input.IsPrimary,
                        Address = input.Address,
                        City = input.City,
                        AdditionalNotes = input.AdditionalNotes,
                        Email = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        TenantId = tenantId,
                        State=input.State,
                        Zip=input.Zip,
                        Mobile=input.Mobile,
                        EmailConfirmed = GetUser.EmailConfirmed
                    };
                    var result = await commonBusiness.UpdateUser(user);
                    var newuser = commonBusiness.GetUserDetail(user.Id);
                    var allroles = commonBusiness.GetRoles(tenantId);
                    //Update CrmUserDetails

                    var crmUserBasicDetail = new CrmUserBasicDetail
                    {
                        UserId = user.Id,
                        Name = string.Format("{0} {1} {2}", input.FirstName, input.MiddleName, input.LastName),
                        ModifiedDate = DateTime.UtcNow
                    };
                    var status = commonBusiness.UpdateUserBasicDetail(crmUserBasicDetail);

                    // get current roles of user
                    var userRoleNames = await commonBusiness.GetUserRoleNames(newuser);
                    // remove all roles of user
                    await commonBusiness.RemoveAllUserRoles(newuser, userRoleNames);
                    // add new roles
                    if (!string.IsNullOrEmpty(input.SelectedRoles))
                    {
                        var roles = input.SelectedRoles.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < roles.Length; i++)
                        {
                            var roleName = allroles.FirstOrDefault(x => x.Id == roles[i]);
                            await commonBusiness.AddUserRole(newuser, roleName.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Add/UpateUsersSRV Update", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> EnableAndDisableUsers(string userId, bool Status)
        {
            try
            {
                var res = new WellAI.Advisor.Model.ServiceCompany.Models.UserViewSRVModel();

                if (!string.IsNullOrEmpty(userId))
                {

                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    if (Status == true)
                    {
                        res = await commonBusiness.EnableUserDetailsSRV(userId);
                    }
                    else
                    {
                       res = await commonBusiness.RemoveSRVUser(userId);      
                    }
                    
                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers_Destroy", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}