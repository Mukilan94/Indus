using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.Identity;
using Kendo.Mvc.Extensions;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class ConfigurationController : BaseController
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        public ConfigurationController(
            UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext,
           ILogger<ConfigurationController> logger) : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
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
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var gear = GetGearPermissions();
                return View(gear);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Configuration Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private GearViewModel GetGearPermissions()
        {
            var gear = new GearViewModel
            {
                Corporateprofile = "none",
                Addupdateusers = "none",
                BillingInvoiceHistory = "none",
                ManagePermissions = "none",
                ManageRoles = "none",
                PaymentMethods = "none",
                Productsubscription = "none",
                ManageAPI = "none",
                ChecklistTemplate = "none"
            };
            if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
            {
                gear.Corporateprofile = "block";
                gear.Addupdateusers = "block";
                gear.BillingInvoiceHistory = "block";
                gear.ManagePermissions = "block";
                gear.ManageRoles = "block";
                gear.PaymentMethods = "block";
                gear.Productsubscription = "block";
                gear.ManageAPI = "block";
                gear.ChecklistTemplate = "block";
            }
            else
            {
                var userIdentity = (ClaimsIdentity)User.Identity;
                var claims = userIdentity.Claims;
                var roleClaimType = userIdentity.RoleClaimType;
                var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                string roleName = roles.FirstOrDefault().Value;
                IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                var roleResult = rolePermissionBusiness.GetRoleByName(roleName);
                bool service = WellAIAppContext.Current.Session.GetString("AccountType") == "1";
                gear.Corporateprofile = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "CorporateProfile") ? "block" : "none";
                gear.Addupdateusers = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "ManageUsers") ? "block" : "none";
                gear.BillingInvoiceHistory = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "BillingHistorySRVNew") ? "block" : "none";
                gear.ManagePermissions = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "ManagePermissions") ? "block" : "none";
                gear.ManageRoles = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "ManageRoles") ? "block" : "none";
                gear.PaymentMethods = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "PaymentMethods") ? "block" : "none";
                gear.Productsubscription = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "BillingHistory") ? "block" : "none";
                gear.ManageAPI = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "Configuration") ? "block" : "none";

                //DWOP
                gear.ChecklistTemplate = rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, service ? "ViewDashboard" : "ChecklistTemplate") ? "block" : "none";
            }
            return gear;
        }
        private bool GetComponentsBasedOnRole()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            string roleName = roles.FirstOrDefault().Value;
            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRoleByName(roleName);
            return rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, "Configuration");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}