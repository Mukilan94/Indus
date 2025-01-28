using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.API.Samsara.Services.IServices;
using WellAI.Advisor.BLL;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Finbuckle.MultiTenant;
using Well_AI.Advisor.Log.Error;
using System;
using WellAI.Advisor.Helper;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.DLL.Data;
using System.Security.Claims;
using Newtonsoft.Json;
using WellAI.Advisor.BLL.IBusiness;
using Microsoft.AspNetCore.Authorization;
using WellAI.Advisor.Model.ServiceCompany.Models;
using System.Linq;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class TechnicianTrackerController : BaseController
    {
        private readonly ILogger<TechnicianTrackerController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly IVechicleService _vechicleService;
        private readonly ISingleton singleton; 
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext _context;
        public TechnicianTrackerController(SignInManager<WellIdentityUser> signInManager, ILogger<TechnicianTrackerController> logger,
                                           ISingleton singleton, IVechicleService vechicleService, WebAIAdvisorContext context, UserManager<WellIdentityUser> userManager,
                                          RoleManager<IdentityRole> roleManager)
            : base(userManager, context)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.singleton = singleton;
            _vechicleService = vechicleService;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                return View();
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TechnicianTracker Indesx", User.Identity.Name);
                return null;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Project(string id, string tId)
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                ViewBag.TechnicianId = tId;
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var result = await singleton.projectBusiness.GetUpCommingProjectsDetailsByTenantIdForOperator(tenantId, id);
                var activeTechnician = await singleton.serviceVehicleBusiness.GetActiveTechnicianByProjectId(userwell, id);
                TechnicianTracker technicianTracker = new TechnicianTracker
                {
                    ActualEndDate = result.ActualEndDate,
                    ActualStartDate = result.ActualStartDate,
                    DateAwared = result.DateAwared,
                    Description = result.Description,
                    ExpectedEndDate = result.ExpectedEndDate,
                    ExpectedStartDate = result.ExpectedStartDate,
                    ProjectCode = result.ProjectCode,
                    Title = result.Title,
                    Job = result.Job,
                    OperatorCompanyName = result.OperatorCompanyName,
                    OperatorMobile = result.OperatorMobile,
                    OperatorUserName = result.OperatorUserName,
                    ProjectStatusName = result.ProjectStatusName,
                    WellId = result.WellId,
                    WellName = result.WellName,
                    ProjectId = result.ProjectId,
                    RigName = result.RigName,
                    ServiceVehicleViewModels = activeTechnician
                };
                return View("Techniciantracker", technicianTracker);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TechnicianTracker Project", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> GetActiveTechnicianAndProjectByTenantId([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var result = await singleton.serviceVehicleBusiness.GetActiveTechnicianAndProjectByOprTenantId(userwell, wellId);
                return Json(result.ToDataSourceResult(request));
            }
            catch (System.Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TechnicianTrackerByTenantId", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetLatLngOfVehicle(string Id)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var result = await _vechicleService.GetMostRecentVechicleLocationByVechicleIdAsync(Id, tenantId);
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetLatLngOfVehicle", User.Identity.Name);
                _logger.LogInformation(ex.Message); 
                return null;
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult GetDrillPlan(String wellId, String tenantId)
        {
            try
            {
                tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                return Json((from dr in _context.DrillPlanHeader
                        join dw in _context.DrillPlanWells on dr.DrillPlanId equals dw.DrillPlanId
                        where dr.TenantId == tenantId && dw.Wellid == wellId
                        select dw).FirstOrDefault());
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetLatLngOfVehicle", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
    }
}