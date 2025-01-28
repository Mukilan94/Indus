using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.DLL.Repository;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class BillingInvoiceHistorySRVController : BaseController
    {
        private readonly ILogger<BillingInvoiceHistorySRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantServiceDbContext _servicedb;
        public BillingInvoiceHistorySRVController(TenantServiceDbContext servicedb,UserManager<WellIdentityUser> userManager,
                                                  SignInManager<WellIdentityUser> signInManager,RoleManager<IdentityRole> roleManager,
                                                  WebAIAdvisorContext dbContext,ILogger<BillingInvoiceHistorySRVController> logger)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                customErrorHandler.WriteError(ex, "BillingInvoiceHistorySRV Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> BillingHistory_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<BillingHistory> model = new List<BillingHistory>();
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var servoppe = new ServiceTenantRepository(_servicedb, HttpContext, _userManager,db);
                //model = await operrepo.GetBillingHistoryInvoices();
                model = await servoppe.GetBillingHistoryInvoices(tenantId);
                if (model == null)
                    model = new List<BillingHistory>();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "BillingInvoiceHistorySRV BillingHistory_Read", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(model.ToDataSourceResult(request, ModelState));
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "BillingHistory",TenantId);
            }
            else
            {
                return false;
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}