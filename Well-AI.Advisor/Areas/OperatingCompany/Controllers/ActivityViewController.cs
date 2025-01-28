using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using WellAI.Advisor.DLL.Repository;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class ActivityViewController : BaseController
    {
        private readonly ILogger<ActivityViewController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private TenantOperatingDbContext _tdbContext;
        public ActivityViewController(WebAIAdvisorContext dbContext, TenantOperatingDbContext tdbContext, SignInManager<WellIdentityUser> signInManager,
                                      RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<ActivityViewController> logger)
            : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _tdbContext = tdbContext;
        }
        public IActionResult Index()
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
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Activity Index", User.Identity.Name);
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "ViewActivityView", TenantId);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ActivityViewModel>> GetActivities()
        {
            var result = new List<ActivityViewModel>();
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var rigIdCookie = Request.Cookies["wellfilterlayout"];
                var wellIdCookie = Request.Cookies["wellIdCookie"];
                var serviceProviderCookie = Request.Cookies["serviceProviderCookie"];
                var rigId = string.IsNullOrEmpty(rigIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : rigIdCookie.ToString();
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var serviceProviderId = string.IsNullOrEmpty(serviceProviderCookie) ? DLL.Constants.NoSpecificWellFilterKey : serviceProviderCookie.ToString();

                //result = await auctionProposalBusiness.GetProjectActivityOperator(tenantId, userwell, wellId);
                result = await auctionProposalBusiness.GetProjectActivityOperatorForServiceAndAdmin(tenantId, userwell, rigId, wellId, serviceProviderId);
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                //var tasks = await operrepo.GetActivityTasks(wellId);
                var tasks = await operrepo.GetActivityTasks(tenantId);
                result.AddRange(tasks);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Activity GetActivities", User.Identity.Name);
            }
            return result;
        }
        public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var res = await GetActivities();
                return Json(res.ToDataSourceResult(request, ModelState)); ;
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Activity Read", User.Identity.Name);
                return null;
            }
        }
        public async Task<JsonResult> Destroy([DataSourceRequest] DataSourceRequest request, ActivityViewModel task)
        {
            try
            {
                if (ModelState.IsValid && task.ActivityIsTask)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    await operrepo.DeleteActivityTask(task.ProjectId);
                }
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Activity Destroy", User.Identity.Name);
                return null;
            }
        }
        public async Task<JsonResult> Create([DataSourceRequest] DataSourceRequest request, ActivityViewModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var id = await operrepo.UpdateActivityTask(task, tenantId);
                    if (string.IsNullOrEmpty(task.ProjectId))
                    {
                        task.ProjectId = id;
                        task.ActivityIsTask = true;
                    }
                }
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Activity Create", User.Identity.Name);
                return null;
            }
        }
        public async Task<JsonResult> Update([DataSourceRequest] DataSourceRequest request, ActivityViewModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var id = await operrepo.UpdateActivityTask(task, tenantId);
                    if (string.IsNullOrEmpty(task.ProjectId))
                        task.ProjectId = id;
                }
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Activity Update", User.Identity.Name);
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