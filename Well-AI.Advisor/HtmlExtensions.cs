using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WellAI.Advisor
{
    public static class HtmlExtensions
    {
        
        private static readonly HtmlContentBuilder _emptyBuilder = new HtmlContentBuilder();

        public static IHtmlContent BuildBreadcrumbNavigation(this IHtmlHelper helper,string AccountType)
        {
            if (!helper.ViewContext.RouteData.Values.ContainsKey("controller"))
            {
                return _emptyBuilder;
            }

            string controllerName = helper.ViewContext.RouteData.Values["controller"].ToString();
            string actionName = helper.ViewContext.RouteData.Values["action"].ToString();

            var viewbagTitle0 = helper.ViewContext.ViewBag.Title0;
            var viewbagTitle = helper.ViewContext.ViewBag.Title;
            string title0 = viewbagTitle0 == null ? "" :viewbagTitle0.ToString();
            string title = viewbagTitle.ToString();
            string HomeIcon = "/";
            if (AccountType == "0" || AccountType == "3")
            {
                HomeIcon = "/OperatingDashboard/AdvisorWithDispatch";
            }
            else if (AccountType == "1" || AccountType == "4")
            {
                HomeIcon = "/ServiceDashboard/AdvisorWithDispatch";
            }

            var breadcrumb = new HtmlContentBuilder()
                                    .AppendHtml("<ol class='k-breadcrumb-container'><li class='k-breadcrumb-item k-breadcrumb-root-item'>")
                                    .AppendHtml("<a href='" + HomeIcon + "' class='k-breadcrumb-root-link  k-breadcrumb-icon-link'><span class='k-icon k-i-home '></span></a><span class='k-breadcrumb-delimiter-icon k-icon k-i-arrow-chevron-right' aria-hidden='true'></span><span class='k-breadcrumb-delimiter-icon k-hidden k-icon k-i-arrow-chevron-right' aria-hidden='true'></span>")
                                    .AppendHtml("</li><li class='k-breadcrumb-item'>");

            if (AccountType == "0" || AccountType == "3")
                if (title0 == "Settings")
                {
                    breadcrumb.AppendHtml(helper.ActionLink(title0 == "" ? title : title0, "Index", "Configuration", null, new { @class = "k-breadcrumb-link" }));
                }
                else if (controllerName == "OperatingDashboard")
                {
                    breadcrumb.AppendHtml(helper.ActionLink(title0 == "" ? title : title0, "AdvisorWithDispatch", "OperatingDashboard", null, new { @class = "k-breadcrumb-link" }));
                }
                else
                {
                    breadcrumb.AppendHtml(helper.ActionLink(title0 == "" ? title : title0, "Index", controllerName, null, new { @class = "k-breadcrumb-link" }));
                }
            else if (AccountType == "1" || AccountType == "4")
                if (title0 == "Settings")
                {
                    breadcrumb.AppendHtml(helper.ActionLink(title0 == "" ? title : title0, "Index", "Configuration", null, new { @class = "k-breadcrumb-link" }));
                }
                else if (controllerName == "ServiceDashboard")
                {
                    breadcrumb.AppendHtml(helper.ActionLink(title0 == "" ? title : title0, "AdvisorWithDispatch", "ServiceDashboard", null, new { @class = "k-breadcrumb-link" }));
                }
                else
                {
                    breadcrumb.AppendHtml(helper.ActionLink(title0 == "" ? title : title0, "Index", controllerName, null, new { @class = "k-breadcrumb-link" }));
                }
            else
            {
                if (title0 == "Settings")
                {
                    breadcrumb.AppendHtml(helper.ActionLink(title0 == "" ? title : title0, "Index", "Configuration", null, new { @class = "k-breadcrumb-link" }));
                }
                else
                {
                    breadcrumb.AppendHtml(helper.ActionLink(title0 == "" ? title : title0, "Index", controllerName, null, new { @class = "k-breadcrumb-link" }));
                }
            }
            breadcrumb.AppendHtml("<span class='k-breadcrumb-delimiter-icon k-icon k-i-arrow-chevron-right' aria-hidden='true'></span></li>");

            if (title0 != "")
            {
                breadcrumb.AppendHtml("<li class='k-breadcrumb-item  k-breadcrumb-last-item'>")
                          .AppendHtml(helper.ActionLink(title, actionName, controllerName, null,
                          new Dictionary<string, object> { { "class", "k-breadcrumb-link k-state-disabled" }, { "aria-curren", "page" } }))
                          .AppendHtml("</li>");
            }
            return breadcrumb.AppendHtml("</ol>");
        }
    }
}
