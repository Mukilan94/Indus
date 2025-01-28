using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor
{
    public class GearViewComponent : ViewComponent
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;

        public GearViewComponent(SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                GearViewModel model = new GearViewModel();
                model.Enable = false;
                if (_signInManager.IsSignedIn(HttpContext.User) &&
                    (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")) || HaveAccessToComponent()))
                {
                    model.Enable = true;
                    model.ConfigUrl = Url.Action("Index", "Configuration");
                }
                return View("_Gear", await Task.FromResult(model));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, null, Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GearViewComponent InvokeAsync", User.Identity.Name);
                return null;
            }
        }

        private bool HaveAccessToComponent()
        {
            try
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
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, null, Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GearViewComponent HaveAccessToComponent", User.Identity.Name);
                return false;
            }
        }
    }
}
