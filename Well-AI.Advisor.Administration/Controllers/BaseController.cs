using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using Well_AI.Advisor.Log.Audit;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.DLL;
using System.Text;

namespace Well_AI.Advisor.Administration.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ISingletonAdministration _singleton;
        protected readonly IConfiguration _configuration;
        protected readonly UserManager<StaffWellIdentityUser> userManager;
        protected readonly SignInManager<StaffWellIdentityUser> signInManager;
        protected WebAIAdvisorContext db;

        public BaseController(UserManager<WellAI.Advisor.Model.Identity.WellIdentityUser> userManager)
        {

        }
        public BaseController(UserManager<StaffWellIdentityUser> userManager, SignInManager<StaffWellIdentityUser> signInManager, WebAIAdvisorContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }
        public BaseController(IConfiguration configuration, ISingletonAdministration singleton, WebAIAdvisorContext db)
        {
            _configuration = configuration;
            _singleton = singleton;
            this.db = db;

        }
        public BaseController(ISingletonAdministration singleton, WebAIAdvisorContext db)
        {
            _singleton = singleton;
            this.db = db;
        }

        public BaseController(ISingletonAdministration singleton, SignInManager<StaffWellIdentityUser> signInManager, WebAIAdvisorContext db)
        {
            _singleton = singleton;
            this.signInManager = signInManager;
            this.db = db;
        }
        public BaseController(ISingletonAdministration singleton, UserManager<StaffWellIdentityUser> userManager, WebAIAdvisorContext db)
        {
            _singleton = singleton;
            this.userManager = userManager;
            this.db = db;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {

            var CompanyId = WellAIAppContext.Current.Session.Get("CompanyId");

            if (db != null)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var controllerName = routeData.Values["controller"];
                    var actionName = routeData.Values["action"];
                    var message = String.Format("Location - Controller:{0} Action:{1}", controllerName, actionName);
                    Guid companyId = Guid.Empty;
                    if (CompanyId != null)
                        companyId = Guid.Parse(Encoding.UTF8.GetString(WellAIAppContext.Current.Session.Get(Constants.SessionAdminNotExpireKey)));
                    AdminCustomAuditLogHandler customErrorHandler = new AdminCustomAuditLogHandler(db, /*Guid.Parse(userManager.GetUserId(HttpContext.User))*/companyId, companyId);
                    customErrorHandler.WriteAuditLog(message, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.User.Identity.Name);
                }
            }
        }

    }
}