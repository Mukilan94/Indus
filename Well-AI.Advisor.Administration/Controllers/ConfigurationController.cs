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
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;

namespace Well_AI.Advisor.Administration.Controllers
{
    public class ConfigurationController : BaseController
    {
        //Phase II - Clear Warning
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
        RoleManager<IdentityRole> _roleManager;
        public ConfigurationController(UserManager<WellIdentityUser> userManager,
           RoleManager<IdentityRole> roleManager,
            ISingletonAdministration _singleton, WebAIAdvisorContext db, UserManager<StaffWellIdentityUser> staffuserManager) : base( _singleton,db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _staffUserManager = staffuserManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        #region Configuration


        public ActionResult Configuration_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var serviceCategory = _singleton.configurationBusiness.GetConfigurations().Result;

                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Configuration Configuration_Read", User.Identity.Name);
                
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Configuration_Create([DataSourceRequest] DataSourceRequest request, ConfigurationViewModel input)
        {
            try
            {
                ModelState.Remove("Password");
                var result = await _singleton.configurationBusiness.AddConfiguration(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Configuration Configuration_Create", User.Identity.Name);
                
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Configuration_Update([DataSourceRequest] DataSourceRequest request, ConfigurationViewModel input)
        {
            try
            {
                ModelState.Remove("Password");

                var result = await _singleton.configurationBusiness.UpdateConfiguration(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Configuration Configuration_Update", User.Identity.Name);
                
                return null;
            }
        }
        #endregion
    }
}