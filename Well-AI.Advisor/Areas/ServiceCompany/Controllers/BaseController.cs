using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using Well_AI.Advisor.Log.Audit;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [TypeFilter(typeof(FriendlyExceptionAttribute))]
    public class BaseController : Controller
    {
        private readonly WebAIAdvisorContext db;
        private readonly UserManager<WellIdentityUser> _userManager;
        public BaseController(UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
        {
            db = dbContext;
            _userManager = userManager;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log(filterContext.RouteData);
        }
        private void Log(RouteData routeData)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var controllerName = routeData.Values["controller"];
                var actionName = routeData.Values["action"];
                var message = String.Format("Location - Controller:{0} Action:{1}", controllerName, actionName);
                Guid companyId = Guid.Empty;
                if (WellAIAppContext.Current.Session.GetString("TenantId") != null)
                    companyId = Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId"));
                CustomAuditLogHandler customAuditHandler = new CustomAuditLogHandler(db, Guid.Parse(_userManager.GetUserId(HttpContext.User)), companyId);
                customAuditHandler.WriteAuditLog(message, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.User.Identity.Name);
            }
        }
    }
}