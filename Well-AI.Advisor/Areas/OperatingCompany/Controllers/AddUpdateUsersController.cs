using Finbuckle.MultiTenant;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class AddUpdateUsersController : BaseController
    {
        private readonly ILogger<AddUpdateUsersController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;

        public AddUpdateUsersController(
           UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           ILogger<AddUpdateUsersController> logger,
           WebAIAdvisorContext dbContext) : base(userManager, dbContext)
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
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                var reslt = await commonBusiness.GetUserList(tenantId);
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                for (int i = 0; i < reslt.Count; i++)
                {
                    if (reslt[i].WellOfficeUser)
                    {
                        var uwells = await AIBusiness.GetUserAssignedWells(reslt[i].UserID, tenantId);
                        var wellIds = uwells.Select(x => x.wellId).ToList();
                        var wellIdsStr = "";
                        foreach (var wellId in wellIds)
                        {
                            wellIdsStr += wellId + ";";
                        }
                        reslt[i].wells = uwells;
                        reslt[i].SelectedWells = wellIdsStr;
                    }
                }
                var roles = commonBusiness.GetRoles(tenantId);
                var user = _userManager.GetUserAsync(User).Result;
                var wells = await AIBusiness.GetWellMaster(Guid.Empty.ToString("D"), user);
                var riglist = (from rig in db.rig_register
                               where rig.TenantID.Equals(tenantId) && rig.isActive.Equals(true)
                               select new WellMasterDataViewModel
                               {
                                   rigID = rig.Rig_id,
                                   rigName = rig.Rig_Name
                               }).ToList();
                ViewData["roles"] = roles;
                ViewData["wells"] = riglist;
                return View(reslt);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers Index", User.Identity.Name);
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
            //var roleResult = rolePermissionBusiness.GetRoleByName(roleName);
            var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
            var rolesResult = roleResult.Result;
            if (roleResult != null)
            {
                List<string> roleIds = (from rl in rolesResult
                                        select rl.Id
                                        ).ToList();
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "ManageUsers", TenantId);
            }
            else
            {
                return false;
            }
        }

        public async Task<int> CheckUserSubscription()
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                CommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                int result = await commonBusiness.GetUserSubscriptionUserLeft(tenantId);
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers CheckUserSubscription", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return 0;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddUpdateUsers_Create([DataSourceRequest] DataSourceRequest request, UserViewModel input)
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
                var resultUserId = "";
                if (ModelState.IsValid && input != null)
                {
                    var user = new WellIdentityUser
                    {
                        UserName = input.Email,
                        FirstName = input.FirstName,
                        MiddleName = input.MiddleName,
                        LastName = input.LastName,
                        JobTitle = input.JobTitle,
                        Mobile = input.Mobile,
                        Address = input.Address,
                        City = input.City,
                        State = input.State,
                        Zip = input.Zip,
                        AdditionalNotes = input.AdditionalNotes,
                        Email = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        Primary = input.IsPrimary,
                        TenantId = tenantId,
                        WellUser = input.WellOfficeUser,
                        Field = input.Field,
                        EmailConfirmed = true
                    };
                    var pass = Utils.GenerateRandomPassword();
                    var result = await _userManager.CreateAsync(user, pass);
                    if (result.Succeeded)
                    {
                        user.EmailConfirmed = true;
                        result = await _userManager.UpdateAsync(user);
                    }
                    if (result.Succeeded)
                    {
                        try
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
                            if (input.WellOfficeUser && !string.IsNullOrEmpty(input.SelectedWells))
                            {
                                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                                var wellId = input.SelectedWells.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                var wellIds = new List<string>();
                                foreach (var Well in wellId)
                                {
                                    var RigList = (from wells in db.WellRegister
                                                   where wells.RigID == Well
                                                   select new
                                                   {
                                                       wells.well_id
                                                   }).ToList();
                                    foreach (var w in RigList)
                                    {
                                        wellIds.Add(Convert.ToString(w.well_id));
                                    }
                                }
                                await AIBusiness.AssignWellsToUser(user.Id, wellIds);
                            }
                            var tenantExistId = await commonBusiness.GetTenantIdByUserId(user.Id);
                            if (tenantExistId == null || tenantExistId == "")
                            {
                                user.TenantId = tenantId;
                                var result1 = await _userManager.UpdateAsync(user);

                                await commonBusiness.CreateTenantUser(user.Id, tenantId);
                            }
                            int accountType = 0;
                            var crmUserBasicDetail = new DLL.Entity.CrmUserBasicDetail
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
                                await commonBusiness.AddUserCountSubscription(tenantId);
                            }
                            resultUserId = user.Id;
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                            ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                            errorHandler.ErrorLog(ex.Message, "AddUpdateUsers page", ex.HResult.ToString());
                            _logger.LogInformation(ex.Message);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                if (resultUserId != "")
                {
                    input.IsActive = true;
                    input.UserID = resultUserId;
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers AddUpdateUsers_Create", User.Identity.Name);
                return LocalRedirect(@"/Dashboard/Error");
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddUpdateUser_Create([DataSourceRequest] DataSourceRequest request, UserViewModel input)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                var resultUserId = "";
                if (ModelState.IsValid && input != null)
                {
                    var user = new WellIdentityUser
                    {
                        UserName = input.Email,
                        FirstName = input.FirstName,
                        MiddleName = input.MiddleName,
                        LastName = input.LastName,
                        JobTitle = input.JobTitle,
                        Mobile = input.Mobile,
                        Address = input.Address,
                        City = input.City,
                        State = input.State,
                        Zip = input.Zip,
                        AdditionalNotes = input.AdditionalNotes,
                        Email = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        Primary = input.IsPrimary,
                        TenantId = tenantId,
                        WellUser = input.WellOfficeUser,
                        Field = input.Field,
                        EmailConfirmed = false,
                        IsUser = input.IsUser
                    };
                    var pass = Utils.GenerateRandomPassword();
                    var result = await _userManager.CreateAsync(user, pass);
                    if (result.Succeeded)
                    {
                        //user.EmailConfirmed = true;
                        result = await _userManager.UpdateAsync(user);
                    }
                    if (result.Succeeded)
                    {
                        try
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
                            if (input.WellOfficeUser && !string.IsNullOrEmpty(input.SelectedWells))
                            {
                                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                                var RigId = input.SelectedWells.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                await AIBusiness.AssignRigsToUser(user.Id, RigId);
                            }
                            var tenantExistId = await commonBusiness.GetTenantIdByUserId(user.Id);
                            if (tenantExistId == null || tenantExistId == "")
                            {
                                user.TenantId = tenantId;
                                var result1 = await _userManager.UpdateAsync(user);

                                await commonBusiness.CreateTenantUser(user.Id, tenantId);
                            }
                            int accountType = 0;
                            var crmUserBasicDetail = new DLL.Entity.CrmUserBasicDetail
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
                            if (status == true && input.IsUser == true)
                            {
                                EmailHandler emailHandler = new EmailHandler();
                                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                                var callbackUrl = "http://" + Request.Host.Value;
                                var flagStatus = await emailHandler.SendEmailAsync("Member", input.Email, "Welcome to Well AI Advisor", $"Dear Member, please remember your password " + pass + ".<br/>You can login with your email and provided password to <a href='" + callbackUrl + "'>Well AI Advisor site</a>.");
                                commonBusiness.UpdateUserPagesCompleteStatus(3, user.Id);
                                await commonBusiness.AddUserCountSubscription(tenantId);
                                if (flagStatus == true)
                                {
                                    user.EmailConfirmed = true;
                                    result = await _userManager.UpdateAsync(user);
                                }
                            }

                            resultUserId = user.Id;
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                            ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                            errorHandler.ErrorLog(ex.Message, "AddUpdateUsers page", ex.HResult.ToString());
                            _logger.LogInformation(ex.Message);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                if (resultUserId != "")
                {
                    input.IsActive = true;
                    input.UserID = resultUserId;
                }

                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers AddUpdateUsers_Create", User.Identity.Name);

                return LocalRedirect(@"/Dashboard/Error");
            }
        }
        /// <summary>
        /// Call from multiselect on edit from to setup initial roles of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AcceptVerbs("Get")]
        public async Task<JsonResult> SetSelectedRoles(string userId)
        {
            List<object> result = new List<object>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var user = await commonBusiness.GetUser(userId);

                foreach (var role in user.roles)
                {
                    result.Add(new { Id = role.Id, Name = role.Name });
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers SetSelectedRoles", User.Identity.Name);
            }
            return Json(result);
        }
        /// <summary>
        /// Call from multiselect on edit from to setup initial wells of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AcceptVerbs("Get")]
        public async Task<JsonResult> SetSelectedRigs(string userId)
        {
            try
            {
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var wells = await AIBusiness.GetUserAssignedRigs(userId, tenantId);
                List<object> result = new List<object>();
                foreach (var well in wells)
                {
                    result.Add(new { rigID = well.rigID, rigName = well.rigName });
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers SetSelectedRigs", User.Identity.Name);
                return null;
            }
        }
        public async Task<JsonResult> SetSelectedWells(string userId)
        {
            try
            {
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var wells = await AIBusiness.GetUserAssignedRigs(userId, tenantId);
                List<object> result = new List<object>();
                foreach (var well in wells)
                {
                    result.Add(new { wellId = well.wellId, wellName = well.wellName, rigID = well.rigID, rigName = well.rigName });
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers SetSelectedWells", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> AddUpdateUsers_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                var result = await commonBusiness.GetUserList(tenantId);
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].WellOfficeUser)
                    {
                        var wells = await AIBusiness.GetUserAssignedWells(result[i].UserID, result[i].UserTenantId);
                        var wellIds = wells.Select(x => x.wellId).ToList();
                        var wellIdsStr = "";
                        foreach (var wellId in wellIds)
                        {
                            wellIdsStr += wellId + ";";
                        }
                        result[i].wells = wells;
                        result[i].SelectedWells = wellIdsStr;
                    }
                }
                return Json(result.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers AddUpdateUsers_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddUpdateUsers_Update([DataSourceRequest] DataSourceRequest request, UserViewModel input)
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
                        Id = input.UserID,
                        UserName = input.Email,
                        FirstName = input.FirstName,
                        MiddleName = input.MiddleName,
                        LastName = input.LastName,
                        JobTitle = input.JobTitle,
                        Mobile = input.Mobile,
                        Address = input.Address,
                        City = input.City,
                        State = input.State,
                        Zip = input.Zip,
                        AdditionalNotes = input.AdditionalNotes,
                        Email = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        Primary = input.IsPrimary,
                        TenantId = tenantId,
                        WellUser = input.WellOfficeUser,
                        Field = input.Field,
                        IsUser = input.IsUser,
                        EmailConfirmed = GetUser.EmailConfirmed
                    };
                    var result = await commonBusiness.UpdateUser(user);
                    var newuser = commonBusiness.GetUserDetail(user.Id);
                    var allroles = commonBusiness.GetRoles(tenantId);
                    // get current roles of user
                    var userRoleNames = await commonBusiness.GetUserRoleNames(newuser);

                    //UpdateuserCrmcomapies
                    var crmUserBasicDetail = new DLL.Entity.CrmUserBasicDetail
                    {
                        UserId = user.Id,
                        Name = string.Format("{0} {1} {2}", input.FirstName, input.MiddleName, input.LastName),
                        ModifiedDate = DateTime.UtcNow
                    };
                    
                    var status = commonBusiness.UpdateUserBasicDetail(crmUserBasicDetail);

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
                    var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                    // save assigned wells only for well type users
                    if (input.WellOfficeUser && !string.IsNullOrEmpty(input.SelectedWells))
                    {
                        var RigId = input.SelectedWells.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        await AIBusiness.AssignRigsToUser(user.Id, RigId);
                    }
                    else
                    {
                        var RemoveSelectRigs = db.UserRigs.Where(x => x.UserId == input.UserID).ToList();
                        foreach (var selectrigs in RemoveSelectRigs)
                        {
                            db.UserRigs.Remove(selectrigs);
                            db.SaveChanges();
                        }
                    }

                    if (input.IsUser == true && GetUser.EmailConfirmed == false)
                    {
                        
                        var pass = Utils.GenerateRandomPassword();
                        var _user = await _userManager.FindByIdAsync(user.Id);
                        var code = await _userManager.GeneratePasswordResetTokenAsync(_user);
                        var Password = await _userManager.ResetPasswordAsync(_user, code, pass);
                        if (Password.Succeeded)
                        {
                            EmailHandler emailHandler = new EmailHandler();
                            var Bytes = Convert.FromBase64String(GetUser.PasswordHash);
                            var pwd = Encoding.UTF8.GetString(Bytes);
                            //var Password = pwd.Substring(0, pwd.Length);
                            //var Password = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(GetUser.PasswordHash));
                            var callbackUrl = "http://" + Request.Host.Value;
                            var flagStatus = await emailHandler.SendEmailAsync("Member", input.Email, "Welcome to Well AI Advisor", $"Dear Member, please remember your password " + pass + ".<br/>You can login with your email and provided password to <a href='" + callbackUrl + "'>Well AI Advisor site</a>.");
                            commonBusiness.UpdateUserPagesCompleteStatus(3, user.Id);
                            await commonBusiness.AddUserCountSubscription(tenantId);
                            if (flagStatus == true)
                            {
                                user.EmailConfirmed = true;
                                result = await commonBusiness.UpdateUser(user);
                            }
                        }
                    }

                    var welluser = JsonConvert.DeserializeObject<WellIdentityUser>(
                                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                    if (input.UserID == welluser.Id)
                    {
                        var newuserwell = await _userManager.FindByEmailAsync(input.Email);
                        WellAIAppContext.Current.Session.SetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity, JsonConvert.SerializeObject(newuserwell));
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers AddUpdateUsers_Update", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> EnableAndDisableUsers(string userId, bool Status)
        {
            try
            {
                var res = new UserViewModel();
                if (!string.IsNullOrEmpty(userId))
                {
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    if (Status == true)
                    { 
                         res = await commonBusiness.EnableUserDetails(userId);
                        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                        await commonBusiness.AddUserCountSubscription(tenantId);
                    }
                    else
                    {
                        res = await commonBusiness.RemoveUser(userId);
                        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                        await commonBusiness.RemoveUserCountSubscription(tenantId);
                    }
                    return Json(new { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddUpdateUsers AddUpdateUsers_Destroy", User.Identity.Name);
                return null;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}