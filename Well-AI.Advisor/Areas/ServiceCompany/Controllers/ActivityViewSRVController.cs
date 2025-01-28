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
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.BLL.IBusiness;
using Finbuckle.MultiTenant;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Helper;
using Microsoft.AspNetCore.Http;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using WellAI.Advisor.DLL.Repository;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class ActivityViewSRVController : BaseController
    {
        private readonly ILogger<ActivityViewSRVController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantServiceDbContext _servicedb;
        public ActivityViewSRVController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
                                         RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager,
                                         ILogger<ActivityViewSRVController> logger, TenantServiceDbContext servicedb)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _servicedb = servicedb;
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
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActivityViewSRV Index", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }


        private bool GetComponentsBasedOnRole()
        {
            string TenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "ViewActivityView", TenantId);//, TenantId
            }
            else
            {
                return false;
            }
        }




        public async Task<IEnumerable<ActivityViewModel>> Activities()
        {
            List<ActivityViewModel> data = null;
            try
            {
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                {
                    string TenantID = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                    var operIdcooki = Request.Cookies["operfilterlayout"];
                    var  operId = string.IsNullOrEmpty(operIdcooki) ? DLL.Constants.NoSpecificWellFilterKey : operIdcooki.ToString();

                    var rigIdCookie = Request.Cookies["rigIdCookie"];
                    var wellIdCookie = Request.Cookies["wellIdCookie"];

                    var rigId = string.IsNullOrEmpty(rigIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : rigIdCookie.ToString();
                    var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();

                    //Activity with Rig and Well Filter 
                    //data = auctionProposalBusiness.GetProjectActivityService(TenantID, operId);
                    data = auctionProposalBusiness.GetProjectActivityServiceWithRigAndWellId(TenantID, operId, rigId, wellId);
                    var serviceTenant = new ServiceTenantRepository(_servicedb, HttpContext, _userManager,db);
                    var tasks = await serviceTenant.GetActivityTasks(operId);
                    data.AddRange(tasks);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActivityViewSRV Activities", User.Identity.Name);
            }
            return data;
        }
        public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var res = await Activities();
                return Json(res.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActivityViewSRV Read", User.Identity.Name);
                return null;
            }
        }
        public async Task<JsonResult> UpcomingRead([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var res = await Activities();
                var UpcomingActivity = res.Where(x => x.ProjectStatusName == "Upcoming").ToList();
                return Json(UpcomingActivity.ToDataSourceResult(request, ModelState)); ;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActivityViewSRV UpcomingRead", User.Identity.Name);
                return null;
            }
        }
        public async Task<JsonResult> Destroy([DataSourceRequest] DataSourceRequest request, ActivityViewModel task)
        {
            try
            {
                if (ModelState.IsValid && task.ActivityIsTask)
                {
                    var serviceTenant = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    await serviceTenant.DeleteActivityTask(task.ProjectId);
                }
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActivityViewSRV Destroy", User.Identity.Name);
                return Json(ex);
            }
        }
        public async Task<JsonResult> Create([DataSourceRequest] DataSourceRequest request, ActivityViewModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var serviceTenant = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var id = await serviceTenant.UpdateActivityTask(task);
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
                customErrorHandler.WriteError(ex, "ActivityViewSRV Create", User.Identity.Name);
                return Json(ex);
            }
        }
        public async Task<JsonResult> Update([DataSourceRequest] DataSourceRequest request, ActivityViewModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var serviceTenant = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var id = await serviceTenant.UpdateActivityTask(task);
                    if (string.IsNullOrEmpty(task.ProjectId))
                        task.ProjectId = id;
                }
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActivityViewSRV Update", User.Identity.Name);
                return Json(ex);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}