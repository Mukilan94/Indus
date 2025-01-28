using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using Newtonsoft.Json;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class ManageRolesController : BaseController
    {
        private readonly ILogger<ManageRolesController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        public ManageRolesController(SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<ManageRolesController> logger,
            WebAIAdvisorContext dbContext)
            : base(userManager, dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            db = dbContext;
            _signInManager = signInManager;
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
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var roles = await GetRoles();
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                var permissions = await rolePermissionBusiness.GetAllPermissions(WellAIAppContext.Current.Session.GetString("TenantId"));
                var dt = Utils.ToDataTable(roles, permissions);
                return View(dt);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageRoles Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private bool GetComponentsBasedOnRole()
        {
            try
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
                    return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "ManageRoles", TenantId);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageRoles GetComponentsBasedOnRole", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return false;
            }

        }

    public async Task<List<UserRole>> GetRoles()
        {
            var roles = new List<UserRole>();
            try
            {
                string tenantId = Guid.Empty.ToString();
                if (!string.IsNullOrWhiteSpace(WellAIAppContext.Current.Session.GetString("TenantId")))
                    tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                var permissions = await rolePermissionBusiness.GetAllPermissions(tenantId);
                var rolePermissions = await rolePermissionBusiness.GetRoles(tenantId);
                
                foreach (var rolePermission in rolePermissions)
                {
                    var role = new UserRole { Id = rolePermission.Id, Title = rolePermission.RoleName };
                    role.Permissions = new List<UserAction>();
                    foreach (var permission in permissions)
                    {
                        var newaction = new UserAction { Id = permission.RolePermissionId, Title = permission.RolePermissionName };
                        var permissionExist = rolePermission.RolePermissions
                            .FirstOrDefault(x => x.PermissionId == permission.RolePermissionId && x.PermissionName == permission.RolePermissionName);
                        newaction.IsActive = permissionExist != null && permissionExist.IsPermitted;
                        role.Permissions.Add(newaction);
                    }
                    roles.Add(role);
                }
                return roles;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageRoles GetRoles", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return roles;
            }
           
        }
        public async Task<ActionResult> ManageRoles_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                string tenantId = Guid.Empty.ToString();
                if (!string.IsNullOrWhiteSpace(WellAIAppContext.Current.Session.GetString("TenantId")))
                    tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var roles = await GetRoles();
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                var permissions = await rolePermissionBusiness.GetAllPermissions(tenantId);
                var dt = Utils.ToDataTable(roles, permissions);
                return Json(dt.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageRoles ManageRoles_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManageRoles_CreateAsync([DataSourceRequest] DataSourceRequest request, string data)
        {
            try
            {
                var userIdentityName = ((ClaimsIdentity)User.Identity).Name;
                var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                var tocreate = input.Where(x => x["Id"] == null || x["Id"] == "").ToList();
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                foreach (var newItem in tocreate)
                {
                    var roleperms = new List<RolePermissions>();
                    foreach (var permstatus in newItem)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                        {
                            var roleperm = new RolePermissions
                            {
                                RolePermissionName = permstatus.Key,
                                IsActive = permstatus.Value == "true"
                            };
                            roleperms.Add(roleperm);
                        }
                    }
                    await rolePermissionBusiness.CreateRolePermissions(newItem["Title"], roleperms, userIdentityName, WellAIAppContext.Current.Session.GetString("TenantId"));
                }
                var permissions = await rolePermissionBusiness.GetAllPermissions(WellAIAppContext.Current.Session.GetString("TenantId"));
                var roles = await GetRoles();
                var dt = Utils.ToDataTable(roles, permissions);
                return Json(dt);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageRoles ManageRoles_CreateAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
            
        }
        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManageRoles_UpdateAsync([DataSourceRequest] DataSourceRequest request, string data)
        {
            try
            {
                var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                var toedit = input.Where(x => x["Id"] != null || x["Id"] != "").ToList();
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                foreach (var editItem in toedit)
                {
                    var roleperms = new List<RolePermissions>();
                    foreach (var permstatus in editItem)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                        {
                            var roleperm = new RolePermissions
                            {
                                RolePermissionName = permstatus.Key,
                                IsActive = permstatus.Value == "true"
                            };
                            roleperms.Add(roleperm);
                        }
                    }
                    await rolePermissionBusiness.UpdateRolePermissions(editItem["Id"], editItem["Title"], roleperms, WellAIAppContext.Current.Session.GetString("TenantId"));
                }
                var dt = Utils.ToDataTable(new List<UserRole>(), new List<RolePermissions>());
                return Json(dt);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageRoles ManageRoles_UpdateAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManageRoles_Destroy(string roleId)
        {
            try
            {
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                await rolePermissionBusiness.DeleteRoles(roleId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageRoles_Destroy", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new object());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}