using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor
{
    public class OperatorFilterViewComponent : ViewComponent
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly TenantServiceDbContext _servicedb;
        private readonly WebAIAdvisorContext _db;

        public OperatorFilterViewComponent(SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager, TenantServiceDbContext servicedb, WebAIAdvisorContext db)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _servicedb = servicedb;
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var model = new OperatorFilterLayoutViewModel();

                var controller = ((string)RouteData.Values["controller"]).ToLower();

                if (controller != "servicedashboard" && controller != "indepthrigdatasrv" && controller != "activityviewsrv" &&
                   controller != "upcomingprojectssrv" && controller != "projectauctionssrv" && controller != "dispatch")
                {
                    return View("_OperatorFilter", model);
                }

                if (_signInManager.IsSignedIn(HttpContext.User))
                {
                    var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, _db);
                    var opers = await operrepo.GetSubscribedOperatorsByTenantId(tenantId);

                    var operators = opers.OrderBy(x => x.Name).ToList();

                    operators.Insert(0, new Model.ServiceCompany.Models.OperatingProviderProfile { CompanyId = DLL.Constants.NoSpecificWellFilterKey, Name = "All Operators" });

                    model.Operators = operators;

                    var operIdCookie = Request.Cookies["operfilterlayout"];
                    var operId = string.IsNullOrEmpty(operIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : operIdCookie.ToString();

                    if (string.IsNullOrEmpty(operId) || operId == DLL.Constants.NoSpecificWellFilterKey || opers.FirstOrDefault(x => x.CompanyId == operId) == null)
                        model.SelectedOperatorId = model.Operators.First().CompanyId;
                    else
                        model.SelectedOperatorId = operId;
                }

                return View("_OperatorFilter", await Task.FromResult(model));
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, null, Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatorFilterComponent GetNavigationItems", User.Identity.Name);
                return null;
            }
        }
    }
}
