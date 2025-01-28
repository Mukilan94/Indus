using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using Kendo.Mvc.Extensions;
using System.Data;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class ManagePermissionsSRVController : BaseController
    {
        private readonly ILogger<ManagePermissionsSRVController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
       private readonly WebAIAdvisorContext db;
        public ManagePermissionsSRVController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, ILogger<ManagePermissionsSRVController> logger)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var permissions = await GetPermissions();
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                var components = await rolePermissionBusiness.GetAllPermittedComponentsSRV();
                var dt = ServiceUtils.ToDataTable(permissions, components);
                return View(dt);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManagePermissionSRV Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/DashboardSRV/Error";
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "ManagePermissions", TenantId);
            }
            else
            {
                return false;
            }
        }


        public async Task<List<UserPermission>> GetPermissions()
        {
            var permissions = new List<UserPermission>();
            try
            {
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                var components = await rolePermissionBusiness.GetAllPermittedComponentsSRV();
                string tenantId = Guid.Empty.ToString();
                if (!string.IsNullOrWhiteSpace(WellAIAppContext.Current.Session.GetString("TenantId")))
                    tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var comppermissions = rolePermissionBusiness.GetSRVRolePermissions(tenantId);
                foreach (var compPermission in comppermissions)
                {
                    var permission = new UserPermission { Id = compPermission.PermissionId, Title = compPermission.PermissionName };
                    permission.Components = new List<UserAction>();
                    foreach (var component in components)
                    {
                        var newaction = new UserAction { Id = component.ComponentId, Title = component.ComponentName };
                        var componentExist = compPermission.RolePermissionComponent
                            .FirstOrDefault(x => x.ComponentId == component.ComponentId && x.ComponentName == component.ComponentName);
                        newaction.IsActive = componentExist != null && componentExist.IsPermitted;
                        permission.Components.Add(newaction);
                    }
                    permissions.Add(permission);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManagePermissionSRV GetPermissions", User.Identity.Name);
            }
            return permissions.ToList();
        }
        public async Task<ActionResult> ManagePermissions_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataTable dt = new DataTable();
            try
            {
                var permissions = await GetPermissions();
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                var components = await rolePermissionBusiness.GetAllPermittedComponentsSRV();
                dt = ServiceUtils.ToDataTable(permissions, components);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManagePermissionSRV GetPermissions", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(dt.ToDataSourceResult(request));
        }
        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManagePermissions_UpdateAsync([DataSourceRequest] DataSourceRequest request, string data)
        {
            try
            {
                var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                var toedit = input.Where(x => x["Id"] != null && x["Id"] != "").ToList();
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                foreach (var editItem in toedit)
                {
                    var permcomps = new List<RolePermissionComponentSRVModel>();

                    foreach (var permstatus in editItem)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                        {
                            var permcomp = new RolePermissionComponentSRVModel
                            {
                                ComponentName = permstatus.Key,
                                IsPermitted = permstatus.Value == "true"
                            };
                            permcomps.Add(permcomp);
                        }
                    }
                    await rolePermissionBusiness.UpdatePermissionSRVComponents(Convert.ToInt32(editItem["Id"]), editItem["Title"], permcomps);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManagePermissionSRV Update", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new object());
        }
        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManagePermissions_CreateAsync([DataSourceRequest] DataSourceRequest request, string data)
        {
            try
            {
                var userIdentityName = ((ClaimsIdentity)User.Identity).Name;
               var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                var tocreate = input.Where(x => x["Id"] == null || x["Id"] == "").ToList();
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                foreach (var newItem in tocreate)
                {
                    var permcomps = new List<RolePermissionComponentSRVModel>();
                    foreach (var permstatus in newItem)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                        {
                            var permcomp = new RolePermissionComponentSRVModel
                            {
                                ComponentName = permstatus.Key,
                                IsPermitted = permstatus.Value == "true"
                            };
                            permcomps.Add(permcomp);
                        }
                    }
                    string tenatID = Guid.Empty.ToString();
                    if (!string.IsNullOrWhiteSpace(WellAIAppContext.Current.Session.GetString("TenantId")))
                        tenatID = WellAIAppContext.Current.Session.GetString("TenantId");
                    await rolePermissionBusiness.CreatePermissionSRVComponents(newItem["Title"], permcomps, userIdentityName, tenatID);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManagePermissionSRV Create", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new object());
        }
        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManagePermissions_Destroy(int permissionId)
        {
            try
            {
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                await rolePermissionBusiness.DeletePermissions(permissionId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManagePermissions_Destroy", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
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