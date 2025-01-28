using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class CompanyController : BaseController
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        public CompanyController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager, 
                                 RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<CompanyController> logger)
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
                var data = new CompanyModel[]
                {
                new CompanyModel{ name="AieryBakel" , phone="5554449999",mobilephone="5554449999",Website="www.AieryBakel.com"},
                new CompanyModel{ name="Constituent", phone="5554447564",mobilephone="5554447564",Website="www.Constituent.com"},
                new CompanyModel{ name="RMVEnterprise" , phone="5234449559",mobilephone="5234449559",Website="www.RMVEnterprise.com"},
                new CompanyModel{ name="QFreedom"    , phone="5754489999",mobilephone="5754489999",Website="www.QFreedom.com"},
                new CompanyModel{ name="Prudence"   , phone="5563449999",mobilephone="5563449999",Website="www.Prudence.com"},
                new CompanyModel{ name="Justicemate"    , phone="5554449999",mobilephone="5554449999",Website="www.Justicemate.com"},
                new CompanyModel{ name="Lemayert"   , phone="5464449259",mobilephone="5464449259",Website="www.Lemayert.com"}
            };
                return View(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Company Index", User.Identity.Name);
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
