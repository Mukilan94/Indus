using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class SalesController : BaseController
    {
        private readonly ILogger<SalesController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        public SalesController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager, 
                               RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<SalesController> logger)
            : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
                //checking invalid user//
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
            var data = new SalesModel[]
            {
                new SalesModel{ Custname="AieryBakel"   , IsQualified=true, IsConverted=true,  Date=new DateTime(2020,03,18,11,30, 0),Planneddateofdelivery=DateTime.Now.AddDays(100),Channel="Advertisement"},
                new SalesModel{ Custname="Constituent"  , IsQualified=true, IsConverted=false, Date=new DateTime(2020,03,11,11,30, 0),Planneddateofdelivery=DateTime.Now.AddDays(100),Channel="BIDDING"},
                new SalesModel{ Custname="RMVEnterprise", IsQualified=true, IsConverted=true,  Date=new DateTime(2020,02,10,11,30, 0),Planneddateofdelivery=DateTime.Now.AddDays(100),Channel="Advertisement"},
                new SalesModel{ Custname="QFreedom"     , IsQualified=false, IsConverted=false,Date=new DateTime(2020,01,13,16,30, 0),Planneddateofdelivery=DateTime.Now.AddDays(100),Channel="Advertisement"},
                new SalesModel{ Custname="Prudence"     , IsQualified=true, IsConverted=false, Date=new DateTime(2019,12,12,14,19, 0),Planneddateofdelivery=DateTime.Now.AddDays(100),Channel="Advertisement"},
                new SalesModel{ Custname="Justicemate"  , IsQualified=true, IsConverted=false, Date=new DateTime(2019,09,12,11,30, 0),Planneddateofdelivery=DateTime.Now.AddDays(100),Channel="PAID"},
                new SalesModel{ Custname="Lemayert"     , IsQualified=false, IsConverted=false,Date=new DateTime(2019,11,18,11,20, 0),Planneddateofdelivery=DateTime.Now.AddDays(100),Channel="Advertisement"}
                };
                return View (data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Sales Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult Error()
        {
            return View();
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
            return rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, "ViewDashboard");
        }
    }
}
