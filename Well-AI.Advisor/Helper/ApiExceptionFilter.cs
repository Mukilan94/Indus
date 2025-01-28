using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Diagnostics;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;

namespace WellAI.Advisor.Helper
{
    public class FriendlyExceptionAttribute : IExceptionFilter
    {
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="dbContext"></param>
        public FriendlyExceptionAttribute(UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            db = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void OnException(ExceptionContext context)
        {
            string actionName = Convert.ToString(context.RouteData.Values["action"]);
            string controller = Convert.ToString(context.RouteData.Values["controller"]);
            Guid companyId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(WellAIAppContext.Current.Session.GetString("TenantId")))
                companyId = Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId"));

            CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(context.HttpContext.User)), companyId);
            customErrorHandler.WriteError(context.Exception, controller + " " + actionName, context.HttpContext.User.Identity.Name);

            var result = new ViewResult { ViewName = "Error" };
            var modelMetadata = new EmptyModelMetadataProvider();
            result.ViewData = new ViewDataDictionary(modelMetadata, context.ModelState);
            result.ViewData.Add("HandleException", context.Exception);
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
