using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    //[SessionTimeOut]
    public class WellPredictionController : BaseController
    {
        //Phase II - Clear Warning
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<StaffWellIdentityUser> _userManager;
        public WellPredictionController(ISingletonAdministration _singleton, RoleManager<IdentityRole> roleManager,
            UserManager<StaffWellIdentityUser> userManager, WebAIAdvisorContext db) : base(_singleton,db)
        {
            this.db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetWellData([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var wellData = _singleton.wellPredictionBusiness.GetWellDatas(request.Page, request.PageSize).Result;
                return Json(wellData.Item1.ToDataSourceResult(request, ModelState));              
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Index", User.Identity.Name);
                
                return null;
            }
        }
    }
}