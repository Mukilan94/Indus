using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.Identity;
using System.Collections.Generic;
using WellAI.Advisor.Model.OperatingCompany.Models;
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
using Microsoft.Extensions.Configuration;
using Finbuckle.MultiTenant;
using WellAI.Advisor.DLL.Repository;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class RigsController : BaseController
    {
        private readonly ILogger<RigsController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        protected readonly IConfiguration _configuration;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private TenantServiceDbContext _servicedb;
        private readonly WebAIAdvisorContext db;
        public RigsController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
                                      RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<RigsController> logger, IConfiguration configuration) : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
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
                               where rig.TenantID.Equals(tenantId.Result) /*&& rig.isActive.Equals(true)*//* && (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)*/
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
                customErrorHandler.WriteError(ex, "WellData Index", User.Identity.Name);
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
                               where rig.TenantID.Equals(tenantId.Result) /*&& rig.isActive.Equals(true)*//* && (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)*/
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
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellData Counts", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<IActionResult> GetRigMasterRead([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var result = await GetRigMaster();
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetRigMasterRead", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task<IEnumerable<WellAI.Advisor.Model.OperatingCompany.Models.RigModel>> GetRigMaster()
        {
            IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var result = new List<WellAI.Advisor.Model.OperatingCompany.Models.RigModel>();
            try
            {
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                result = await AIBusiness.GetRigMaster(tenantId.Result, RigId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetRigMaster", User.Identity.Name);
                _logger.LogInformation(ex.Message);
            }
            return result;
        }
        
       
        [AcceptVerbs("Get")]
        public async Task<IActionResult> RigCount_Check([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.DLL.Entity.ProductSubscriptionModel product) 
        {
            IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            var rigcount = (from rigc in db.Subscription
                            where rigc.TenantId.Equals(tenantId)
                            select rigc).ToList();
            var count = rigcount[0].SubscriptionCount;
            var Total = rigcount[0].CurrentCount;

            return await Task.FromResult(Json(rigcount));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> RigData_Create([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.OperatingCompany.Models.RigModel input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            bool IsActive = true;
         
            
            try
            {
                Rig_register rig = new Rig_register();
                if (ModelState.IsValid)
                {
                    rig.Rig_id = Guid.NewGuid().ToString();
                    rig.Rig_Name = input.Rig_Name;
                    rig.Latitude = input.Latitude.Value;
                    rig.Longitude = input.Longitude.Value;
                    rig.TenantID = tenantId;
                    rig.isActive = IsActive;
                };
                db.rig_register.Add(rig);
                await db.SaveChangesAsync();
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "RigData_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "RigData_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }


        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> RigData_Update([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.OperatingCompany.Models.RigModel input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            bool IsActive = true;
            try
            {
                Rig_register rig = new Rig_register();
                if (ModelState.IsValid)
                {
                    rig.Rig_id = input.Rig_id;
                    rig.Rig_Name = input.Rig_Name;
                    rig.Latitude = input.Latitude.Value;
                    rig.Longitude = input.Longitude.Value;
                    rig.TenantID = input.TenantID;
                    rig.isActive = IsActive;
                };
                db.rig_register.Update(rig);
                await db.SaveChangesAsync();
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "RigData_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "RigData_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> RigData_Destroy([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.OperatingCompany.Models.RigModel input, string RigId)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            try
            {
                //Phase-II Changes
                var RigList = db.rig_register.Where(x => x.Rig_id == RigId).FirstOrDefault();
                if (!string.IsNullOrEmpty(RigId))
                {
                    if (RigList.isActive == true)
                    {
                        var DepthPermissionRig = db.RigsDepth_Permissions.Where(x => x.DepthPermission == true && x.OprTenantId == tenantId && x.RigId == RigList.Rig_id).ToList();
                        bool permission = false;
                        var VendorsList = new List<string>();
                        foreach (var depth in DepthPermissionRig)
                        {
                            permission = depth.DepthPermission;
                            var Vendor = depth.SerTenantId;
                            VendorsList.Add(Vendor);
                        }
                        if (permission != true)
                        {
                            RigList.Rig_id = RigList.Rig_id;
                            RigList.Rig_Name = RigList.Rig_Name;
                            RigList.Latitude = RigList.Latitude;
                            RigList.Longitude = RigList.Longitude;
                            RigList.TenantID = RigList.TenantID;
                            RigList.isActive = false;
                        }
                        else
                        {
                            var VenderName = new List<string>();
                            foreach (var vendor in VendorsList)
                            {
                                var vendorProfile = db.CorporateProfile.Where(x => x.TenantId == vendor).Select(y => y.Name).FirstOrDefault();
                                VenderName.Add(vendorProfile);
                            }

                            string ven = string.Join(",", VenderName);

                            return Json(new { DepthPermission = permission, Vendor = ven, RigName = RigList.Rig_Name });
                        }
                    }
                    else
                    {
                        RigList.Rig_id = RigList.Rig_id;
                        RigList.Rig_Name = RigList.Rig_Name;
                        RigList.Latitude = RigList.Latitude;
                        RigList.Longitude = RigList.Longitude;
                        RigList.TenantID = RigList.TenantID;
                        RigList.isActive = true;


                        var ServiceCompanies = db.CrmCompanies.Select(x => x.TenantId).ToList();
                        var Companies = db.CorporateProfile.Where(x => ServiceCompanies.Contains(x.TenantId)).ToList();

                        //foreach (var service in Companies)
                        //{
                        //    var dbprefix = "serv";
                        //    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                        //                           dbprefix + "db_" + service.TenantId.ToString());

                        //    var ti = new TenantInfo(service.TenantId, service.TenantId, service.TenantId, connString, null);
                        //    var SerContext = new TenantServiceDbContext(ti);
                        //    _servicedb = SerContext;
                        //    var Servicerepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                        //    var subOperators = await Servicerepo.GetSubsriptionOperators();
                        //    var subsctiptionsRigs = subOperators.Where(x => x.CompanyId == tenantId).FirstOrDefault();

                        //    string[] rigId = subsctiptionsRigs.RigId.Split(",", StringSplitOptions.RemoveEmptyEntries);

                        //    foreach (var rig in rigId)
                        //    {
                        //        if (input.Rig_id == rig)
                        //        {
                        //            MessageQueue messageQueue = new MessageQueue { };
                        //        }
                        //    }
                        //}
                    }
                };
                db.rig_register.Update(RigList);
                await db.SaveChangesAsync();
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "RigData_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "RigData_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
    }
}
