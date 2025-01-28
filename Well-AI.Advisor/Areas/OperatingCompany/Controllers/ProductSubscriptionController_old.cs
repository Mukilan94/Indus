using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class ProductSubscription_oldController : BaseController
    {
        private readonly ILogger<ProductSubscriptionController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private TenantOperatingDbContext _tdbContext;
        public ProductSubscription_oldController(
            UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext,
           TenantOperatingDbContext tdbContext,
           ILogger<ProductSubscriptionController> logger) : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _tdbContext = tdbContext;
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
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ViewBag.CustomerSubscriptions = (from sub in db.Subscription
                                                 join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
                                                 where sub.TenantId == tenantId && sub.IsPaid==true
                                                 select new CustomerSubscriptions
                                                 {
                                                     SubscriptionName = pcg.Name,
                                                     SubscriptionStart = sub.SubStartdate,
                                                     SubscriptionEnd = sub.SubEndDate,
                                                     SubscriptionTotalAmount=sub.PackageAmount,
                                                     SubscriptionUsersCount=sub.SubscriptionCount,
                                                     IsEnableSubscription=sub.IsEnable==true?"Active":"Deactive",
                                                     PackageOrder=pcg.PackageOrder
                                                 }
                                                 ).FirstOrDefault();
                var result = await db.SubscriptionPackage.Where(x => x.IsActive == true && x.AccountType == 2).OrderBy(x=>x.PackageOrder).ToListAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscription Index", User.Identity.Name);
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "BillingHistory",TenantId);
            }
            else
            {
                return false;
            }
        }
    }
}
