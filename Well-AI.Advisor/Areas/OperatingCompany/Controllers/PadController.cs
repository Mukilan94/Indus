using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.Identity;
using System.Collections.Generic;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using System;
using WellAI.Advisor.Areas.Identity;
using System.Linq;
using WellAI.Advisor.DLL.Entity;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Routing;
using WellAI.Advisor.Helper;
using System.Security.Claims;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class PadController : BaseController
    {
        private readonly ILogger<PadController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        public PadController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
                                      RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<PadController> logger) : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
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
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                var riglist = (from rig in db.rig_register
                               where rig.TenantID.Equals(tenantId.Result) /*&& rig.isActive.Equals(true) *//*&& (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)*/
                               select rig).ToList();
                var padlist = (from pad in db.pad_register
                               where pad.TenantID.Equals(tenantId.Result) && pad.isActive.Equals(true)
                               select pad).ToList();
               
                var WellList = db.WellRegister.Where(wel => wel.customer_id.Equals(tenantId.Result) && (wel.RigID == RigId && !checkwellFilter || checkwellFilter)).ToList();
                var DrillPlanCount = (from planHeader in db.DrillPlanHeader
                                      join PlanWels in db.DrillPlanWells on planHeader.DrillPlanId equals PlanWels.DrillPlanId into w
                                      from PlanWels in w.DefaultIfEmpty()
                                      where planHeader.TenantId == tenantId.Result && (PlanWels.RigId == RigId && !checkwellFilter || checkwellFilter)
                                      select planHeader).Distinct().Count();

                var WellCounts = new WellsCountModel
                {
                    RigCounts = riglist.Count(),
                    PadCounts = padlist.Count(),
                    WellCounts = WellList.Count(),
                    DrillingPlanCounts = DrillPlanCount
                };
                return View(WellCounts);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Pad Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
         }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Counts()
        {
            try
            {
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                var riglist = (from rig in db.rig_register
                               where rig.TenantID.Equals(tenantId.Result) /*&& rig.isActive.Equals(true) *//*&& (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)*/
                               select rig).ToList();
                var padlist = (from pad in db.pad_register
                               where pad.TenantID.Equals(tenantId.Result) && pad.isActive.Equals(true)
                               select pad).ToList();
                var WellList = db.WellRegister.Where(wel => wel.customer_id.Equals(tenantId.Result) && (wel.RigID == RigId && !checkwellFilter || checkwellFilter)).ToList();
                var WellCounts = new WellsCountModel
                {
                    RigCounts = riglist.Count(),
                    PadCounts = padlist.Count(),
                    WellCounts = WellList.Count(),
                    DrillingPlanCounts = db.DrillingPlan.Where(x => x.TenantId == tenantId.Result).Count()
                };
                return await Task.FromResult(Json(WellCounts));
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Pad Counts", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<IActionResult> GetPadMasterRead([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var result = await GetPadMaster();
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetPadMasterRead", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task<IEnumerable<WellAI.Advisor.Model.OperatingCompany.Models.PadModel>> GetPadMaster()
        {
            IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var result = new List<WellAI.Advisor.Model.OperatingCompany.Models.PadModel>();
            try
            {
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                result = await AIBusiness.GetPadMaster(tenantId.Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetPadMaster", User.Identity.Name);
                _logger.LogInformation(ex.Message);
            }

            return result;
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> PadData_Create([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.OperatingCompany.Models.PadModel input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            bool IsActive = true;
            try
            {
                Pad_register pad = new Pad_register();
                if (ModelState.IsValid)
                {
                    pad.Pad_id = Guid.NewGuid().ToString();
                    pad.Pad_Name = input.Pad_Name;
                    pad.Latitude = input.Latitude.Value;
                    pad.Longitude = input.Longitude.Value;
                    pad.TenantID = tenantId;
                    pad.isActive = IsActive;
                };
                db.pad_register.Add(pad);
                await db.SaveChangesAsync();
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PadData_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PadData_Create", User.Identity.Name);
               _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> PadData_Update([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.OperatingCompany.Models.PadModel input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            bool IsActive = true;
            try
            {
                Pad_register Pad = new Pad_register();
                if (ModelState.IsValid)
                {
                    Pad.Pad_id = input.Pad_id;
                    Pad.Pad_Name = input.Pad_Name;
                    Pad.Latitude = input.Latitude.Value;
                    Pad.Longitude = input.Longitude.Value;
                    Pad.TenantID = input.TenantID;
                    Pad.isActive = IsActive;
                };
                db.pad_register.Update(Pad);
                await db.SaveChangesAsync();
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PadData_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PadData_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> PadData_Destroy([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.OperatingCompany.Models.PadModel input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            try
            {
                Pad_register Pad = new Pad_register();
                if (ModelState.IsValid)
                {
                    Pad.Pad_id = input.Pad_id;
                    Pad.Pad_Name = input.Pad_Name;
                    Pad.Latitude = input.Latitude.Value;
                    Pad.Longitude = input.Longitude.Value;
                    Pad.TenantID = input.TenantID;
                    Pad.isActive = false;
                };
                db.pad_register.Update(Pad);
                await db.SaveChangesAsync();
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PadData_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PadData_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
    }
}
