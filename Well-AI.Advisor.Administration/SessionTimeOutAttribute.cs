using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.Administration
{
    public class SessionTimeOutAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var keyexist = WellAIAppContext.Current.Session.Keys.Contains(WellAI.Advisor.DLL.Constants.SessionAdminNotExpireKey);

            if (!keyexist)
            {
                //filterContext.Result = new RedirectResult("~/Account/Login");
                filterContext.Result = new RedirectResult("../Account/Login");

            }
            //filterContext.Result = new RedirectResult("~/Account/Login");

            //var loginUrl = "~/Account/Login";


            base.OnActionExecuting(filterContext);
        }

    }
}
