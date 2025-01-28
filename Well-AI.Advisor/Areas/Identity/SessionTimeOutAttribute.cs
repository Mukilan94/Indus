using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using WellAI.Advisor.DLL.Data;

namespace WellAI.Advisor.Areas.Identity
{
    public class SessionTimeOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var keyexist = WellAIAppContext.Current.Session.Keys.Contains(WellAI.Advisor.DLL.Constants.SessionNotExpireKey);

            if (!keyexist)
            {
                filterContext.Result = new RedirectResult("~/Identity/Account/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
