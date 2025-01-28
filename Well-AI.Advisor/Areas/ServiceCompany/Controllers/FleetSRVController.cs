using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Well_AI.Advisor.Log.Error;
using Well_AI.Advisor.API.Samsara.Services.IServices;
using WellAI.Advisor.BLL;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class FleetSRVController : BaseController
    {
        private readonly IVechicleService _vechicleService;
        private readonly ISingleton _singleton;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        public FleetSRVController(IVechicleService vechicleService, ISingleton singleton, UserManager<WellIdentityUser> userManager,
           RoleManager<IdentityRole> roleManager, WebAIAdvisorContext dbContext)
        : base(userManager, dbContext)
        {
            _singleton = singleton;
            _userManager = userManager;
            _roleManager = roleManager;
            _vechicleService = vechicleService;
            db = dbContext;
       }
       public IActionResult Index()
        {
            return View();
        }
                public async Task<IActionResult> ServiceVehicle_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ServiceVehicleViewModel> result = new List<ServiceVehicleViewModel>();
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                result = await _singleton.serviceVehicleBusiness.GetServiceVehicles(tenantId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "FleetSRV ServiceVehicle_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        public async Task<IActionResult> Update()
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _vechicleService.GetVechicleAsyncByTenant(tenantId);
                var outpot = await _singleton.serviceVehicleBusiness.UpdateSamsaraData(result, userId, tenantId);
                return Json(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "FleetSRV Update", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
    }
}