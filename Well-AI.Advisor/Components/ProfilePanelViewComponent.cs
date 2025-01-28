using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Identity;
using System;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor
{
    public class ProfilePanelViewComponent : ViewComponent
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        public ProfilePanelViewComponent(SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            try
            {
                var controller = (string)RouteData.Values["controller"];
                var action = (string)RouteData.Values["action"];
                var area = (string)RouteData.Values["area"];

                HeaderViewModel model = new HeaderViewModel();
                model.Name = string.Empty;
                if (_signInManager.IsSignedIn(HttpContext.User))
                {
                    var userIdentity = (ClaimsIdentity)User.Identity;
                    ICommonRepository commonRepo = new CommonRepository(db, _roleManager, _userManager);
                    string result = commonRepo.GetUserBasicDetailByEmail(userIdentity.Name);
                    model.Name = result;
                }
                return View("_NavProfilePanel", model);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, null, Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProfilePaneViewComponent GetNavigationItems", User.Identity.Name);
                return null;
            }
        }
    }
}
