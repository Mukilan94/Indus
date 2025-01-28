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
using System.Security.Claims;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using WellAI.Advisor.Hubs;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class WellDataController : BaseController
    {
        private readonly ILogger<WellDataController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        private IHubContext<NotificationHub> _hubContext { get; set; }

        public WellDataController(SignInManager<WellIdentityUser> signInManager, ILogger<WellDataController> logger,
            WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, IHubContext<NotificationHub> hubContext, IConfiguration configuration)
            : base(userManager, db)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _hubContext = hubContext;
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
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                   WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var wellTypes = (from tp in db.WellType
                                 select new WellTypeModel { wellTypeId = tp.welltype_id, wellTypeName = tp.welltype_name }
                                 ).ToList();
                var riglist = (from rig in db.rig_register
                               where rig.TenantID.Equals(tndid)/* && rig.isActive.Equals(true)*/ /*&& (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)*/
                               select new RigList
                               {
                                   Rig_Id = rig.Rig_id,
                                   Rig_Name = rig.Rig_Name
                               }).ToList();
                var padlist = (from pad in db.pad_register
                               where pad.TenantID.Equals(tndid) && pad.isActive.Equals(true)
                               select new PadList
                               {
                                   Pad_Id = pad.Pad_id,
                                   Pad_Name = pad.Pad_Name
                               }).ToList();
                var batchlist = (from batch in db.BatchDillingType_Register
                                 select new BatchDillingType
                                 {
                                     BatchDrillingType_Id = batch.BatchDrillingType_Id,
                                     BatchDrillingType = batch.BatchDrillingType
                                 }).ToList();
                var BasinNames = (from basin in db.BasinTypes
                                  select new BasinTypeModel
                                  {
                                      Basin_ID = basin.Basin_ID,
                                      BasinType_name = basin.BasinType_name
                                  }).ToList();
                ViewData["BasinType_Names"] = BasinNames;
                ViewData["riglist"] = riglist;
                ViewData["batchlist"] = batchlist;
                ViewData["padlist"] = padlist;
                ViewData["wellTypes"] = wellTypes;
                List<WellMasterDataViewModel> AIWellDataModelList = new List<WellMasterDataViewModel>();
                var WellList = db.WellRegister.Where(wel => wel.customer_id.Equals(tndid) && (wel.RigID == RigId && !checkwellFilter || checkwellFilter)).ToList();
                var DrillPlanCount = (from planHeader in db.DrillPlanHeader
                                      join PlanWels in db.DrillPlanWells on planHeader.DrillPlanId equals PlanWels.DrillPlanId into w
                                      from PlanWels in w.DefaultIfEmpty()
                                      where planHeader.TenantId == tndid && (PlanWels.RigId == RigId && !checkwellFilter || checkwellFilter)
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
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                   WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var wellTypes = (from tp in db.WellType
                                 select new WellTypeModel { wellTypeId = tp.welltype_id, wellTypeName = tp.welltype_name }
                                 ).ToList();
                var riglist = (from rig in db.rig_register
                               where rig.TenantID.Equals(tndid)/* && rig.isActive.Equals(true)*/ /*&& (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)*/
                               select new RigList
                               {
                                   Rig_Id = rig.Rig_id,
                                   Rig_Name = rig.Rig_Name
                               }).ToList();
                var padlist = (from pad in db.pad_register
                               where pad.TenantID.Equals(tndid) && pad.isActive.Equals(true)
                               select new PadList
                               {
                                   Pad_Id = pad.Pad_id,
                                   Pad_Name = pad.Pad_Name
                               }).ToList();
                var batchlist = (from batch in db.BatchDillingType_Register
                                 select new BatchDillingType
                                 {
                                     BatchDrillingType_Id = batch.BatchDrillingType_Id,
                                     BatchDrillingType = batch.BatchDrillingType
                                 }).ToList();
                var BasinNames = (from basin in db.BasinTypes
                                  select new BasinTypeModel
                                  {
                                      Basin_ID = basin.Basin_ID,
                                      BasinType_name = basin.BasinType_name
                                  }).ToList();
                ViewData["BasinType_Names"] = BasinNames;
                ViewData["riglist"] = riglist;
                ViewData["batchlist"] = batchlist;
                ViewData["padlist"] = padlist;
                ViewData["wellTypes"] = wellTypes;
                List<WellMasterDataViewModel> AIWellDataModelList = new List<WellMasterDataViewModel>();
                var WellList = db.WellRegister.Where(wel => wel.customer_id.Equals(tndid) && (wel.RigID == RigId && !checkwellFilter || checkwellFilter)).ToList();
                var WellCounts = new WellsCountModel
                {
                    RigCounts = riglist.Count(),
                    PadCounts = padlist.Count(),
                    WellCounts = WellList.Count(),
                    DrillingPlanCounts = db.DrillingPlan.Where(x => x.TenantId == tndid).Count()
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

        public async Task<IActionResult> GetWellMasterRead([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var result = await GetWellMaster();
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellMasterRead", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Well Register data for Dropdown List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<WellMasterDataViewModel>> GetWellsList()
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                               WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));

                var tndid = userwell.TenantId;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<WellMasterDataViewModel> AIWellDataModelList = new List<WellMasterDataViewModel>();
                var groupList = GetRIGAIGroup();
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                List<WellMasterDataViewModel> wellMasterResult = new List<WellMasterDataViewModel>();
                wellMasterResult = await commonBusiness.GetWellRegister(tndid, RigId, checkwellFilter);
                return wellMasterResult;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellsList", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return new List<WellMasterDataViewModel>();
            }

        }

        public async Task<ActionResult> GetWellData([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                               WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<WellMasterDataViewModel> AIWellDataModelList = new List<WellMasterDataViewModel>();
                var groupList = GetRIGAIGroup();
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;

                //DWOP
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                List<WellMasterDataViewModel> wellMasterResult = new List<WellMasterDataViewModel>();
                wellMasterResult = await commonBusiness.GetWellRegister(tndid, RigId, checkwellFilter);

               // var wellMaster = (from wel in db.WellRegister
               //                   join rig in db.rig_register on wel.RigID equals rig.Rig_id into t1
               //                   from rigresult in t1.DefaultIfEmpty()
               //                   join pad in db.pad_register on wel.PadID equals pad.Pad_id into t2
               //                   from padresult in t2.DefaultIfEmpty()
               //                   join batch in db.BatchDillingType_Register on wel.BatchDrillingTypeID equals batch.BatchDrillingType_Id into t3
               //                   from batchresult in t3.DefaultIfEmpty()
               //                   join typ in db.WellType on wel.welltype_id equals typ.welltype_id into tj
               //                   from subresult in tj.DefaultIfEmpty()
               //                   where wel.customer_id.Equals(tndid) && (rigresult.Rig_id == RigId && !checkwellFilter || checkwellFilter)
               //                   join bas in db.BasinTypes on wel.Basin equals bas.Basin_ID into t4
               //                   from basinresult in t4.DefaultIfEmpty()
               //                   join chklist in db.ChecklistTemplate on wel.ChecklistTemplateId equals chklist.CheckListTemplateId into t5
               //                   from chklistresult in t5.DefaultIfEmpty()
               //                   select new WellMasterDataViewModel
               //                   {
               //                       wellId = wel.well_id,
               //                       wellName = wel.wellname,
               //                       wellType = subresult.welltype_name ?? String.Empty,
               //                       wellTypeId = new WellTypeModel { wellTypeId = subresult.welltype_id, wellTypeName = subresult.welltype_name },
               //                       county = wel.County,
               //                       complete_well_drill = wel.Conplete_well_drill == 1 ? true : false,
               //                       batch_drill_casing = wel.Batch_drill_casing == 1 ? true : false,
               //                       batch_drill_horizontal = wel.Batch_drill_horizontal == 1 ? true : false,
               //                       casing_string = wel.Casing_string == 1 ? true : false,
               //                       numAPI = wel.NumAPI,
               //                       numAFE = wel.NumAFE,
               //                       rigName = rigresult.Rig_Name ?? String.Empty,
               //                       padName = padresult.Pad_Name ?? String.Empty,
               //                       state = wel.State,
               //                       batchFlag = wel.BatchFlag == 1 ? true : false,
               //                       batchDrillingTypeId = wel.BatchDrillingTypeID ?? String.Empty,
               //                       casingString = wel.CasingString,
               //                       padID = wel.PadID,
               //                       rigID = wel.RigID,
               //                       latitude = wel.Latitude,
               //                       longitude = wel.Longitude,
               //                       Prediction = wel.Prediction,
               //                       OldPredictionForUpdate = wel.Prediction,
               //                       chartColor = wel.ChartColor,
               //                       fieldName = wel.FieldName,
               //                       basin = basinresult.BasinType_name ?? String.Empty,                                     
               //                       Basin_ID = new BasinTypeModel { Basin_ID = basinresult.Basin_ID, BasinType_name = basinresult.BasinType_name },
               //                       ChecklistTemplateName = chklistresult.TemplateName,
               //                       ChecklistTemplateId = wel.ChecklistTemplateId
               //                   }).ToList();

               //var wellMasterResult = (from wel in wellMaster
               //                         join gp in groupList on wel.wellId equals gp.wellId into gj
               //                         from subresult1 in gj.DefaultIfEmpty()
               //                         orderby wel.wellName
               //                         select new WellMasterDataViewModel
               //                         {
               //                             wellId = wel.wellId,
               //                             wellName = wel.wellName,
               //                             wellType = wel.wellType,
               //                             taskCount = subresult1?.taskCount ?? String.Empty,
               //                             minSchdDate = subresult1?.minSchdDate ?? String.Empty,
               //                             maxSchdDate = subresult1?.maxSchdDate ?? String.Empty,
               //                             wellTypeId = wel.wellTypeId,
               //                             county = wel.county,
               //                             complete_well_drill = wel.complete_well_drill,
               //                             batch_drill_casing = wel.batch_drill_casing,
               //                             batch_drill_horizontal = wel.batch_drill_horizontal,
               //                             casing_string = wel.casing_string,
               //                             numAPI = wel.numAPI,
               //                             numAFE = wel.numAFE,
               //                             rigName = wel.rigName,
               //                             padName = wel.padName,
               //                             state = wel.state,
               //                             batchFlag = wel.batchFlag,
               //                             batchDrillingTypeId = wel.batchDrillingTypeId,
               //                             casingString = wel.casingString,
               //                             padID = wel.padID,
               //                             rigID = wel.rigID,
               //                             latitude = wel.latitude,
               //                             longitude = wel.longitude,
               //                             fieldName = wel.fieldName,
               //                             basin = wel.basin,
               //                             Basin_ID = wel.Basin_ID,
               //                             Prediction = wel.Prediction,
               //                             OldPredictionForUpdate = wel.Prediction,
               //                             chartColor = wel.chartColor,
               //                             RigRelease = wel.RigRelease == null ? DateTime.Now : wel.RigRelease,
               //                             SpudWell = wel.SpudWell == null ? DateTime.Now : wel.SpudWell,
               //                             Lastboptest = wel.Lastboptest == null ? DateTime.Now : wel.Lastboptest,
               //                             NextbopTest = wel.NextbopTest == null ? DateTime.Now : wel.NextbopTest,
               //                             PlannedTd = wel.PlannedTd,
               //                             ChecklistTemplateName = wel.ChecklistTemplateName,
               //                             ChecklistTemplateId = wel.ChecklistTemplateId
               //                         }
               //                        ).ToList();

                return Json(wellMasterResult.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellData", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

        private IList<WellMasterGroupDataViewModel> GetRIGAIGroup()
        {
            try
            {
                List<AIWellDataModel> AIWellDataModelList = new List<AIWellDataModel>();
                var associatedList = db.AIAssociatedTasks.AsNoTracking();
                var predictiveList = db.AIPredictiveTasks.AsNoTracking();
                var exemptionlist = db.AIExemptionTasks.AsNoTracking();
                var associatedtasks = (from ai in associatedList
                                       join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                       from subresult in tj.DefaultIfEmpty()
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                       from subresult1 in wj.DefaultIfEmpty()
                                       where ai.customer_id != null
                                       select new AIWellDataModel
                                       {
                                           actionDate = ai.ActionDate,
                                           adt = ai.ADT,
                                           customerId = ai.customer_id,
                                           dependency = ai.dependency,
                                           dependencyFlag = ai.dependency_flag,
                                           depth = ai.depth,
                                           duration = ai.duration,
                                           eFlag = ai.Eflag,
                                           leadTime = ai.leadtime,
                                           scheduleDate = ai.ScheduleDate,
                                           startTime = ai.StartTime,
                                           taskName = ai.taskname,
                                           taskStatus = ai.taskstatus,
                                           time = ai.time,
                                           wellTaskId = ai.welltask_id,
                                           wellTypeId = ai.welltype_id,
                                           wellId = ai.well_id,
                                           dependencyTask = subresult.taskname ?? String.Empty,
                                           wellName = subresult1.wellname ?? String.Empty,
                                       }).ToList();
                var predictiveTasks = (from ai in predictiveList
                                       join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                       from subresult in tj.DefaultIfEmpty()
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                       from subresult1 in wj.DefaultIfEmpty()
                                       select new AIWellDataModel
                                       {
                                           actionDate = ai.ActionDate,
                                           adt = ai.ADT,
                                           customerId = ai.customer_id,
                                           dependency = ai.dependency,
                                           dependencyFlag = ai.dependency_flag,
                                           depth = ai.depth,
                                           duration = ai.duration,
                                           eFlag = ai.Eflag,
                                           leadTime = ai.leadtime,
                                           scheduleDate = ai.ScheduleDate,
                                           startTime = ai.StartTime,
                                           taskName = ai.taskname,
                                           taskStatus = ai.taskstatus,
                                           time = ai.time,
                                           wellTaskId = ai.welltask_id,
                                           wellTypeId = ai.welltype_id,
                                           wellId = ai.well_id,
                                           dependencyTask = subresult.taskname ?? String.Empty,
                                           wellName = subresult1.wellname ?? String.Empty,
                                       }).ToList();
                var exemptionTasks = (from ai in exemptionlist
                                      join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                      from subresult in tj.DefaultIfEmpty()
                                      join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                      from subresult1 in wj.DefaultIfEmpty()
                                      select new AIWellDataModel
                                      {
                                          actionDate = ai.ActionDate,
                                          adt = ai.ADT,
                                          customerId = ai.customer_id,
                                          dependency = ai.dependency,
                                          dependencyFlag = ai.dependency_flag,
                                          depth = ai.depth,
                                          duration = ai.duration,
                                          eFlag = ai.Eflag,
                                          leadTime = ai.leadtime,
                                          scheduleDate = ai.ScheduleDate,
                                          startTime = ai.StartTime,
                                          taskName = ai.taskname,
                                          taskStatus = ai.taskstatus,
                                          time = ai.time,
                                          wellTaskId = ai.welltask_id,
                                          wellTypeId = ai.welltype_id,
                                          wellId = ai.well_id,
                                          dependencyTask = subresult.taskname ?? String.Empty,
                                          wellName = subresult1.wellname ?? String.Empty,
                                      }).ToList();
                var welldata = associatedtasks.Union(predictiveTasks).Union(exemptionTasks);
                var groupdata = (from wel in welldata
                                 group wel by wel.wellId into g
                                 select new WellMasterGroupDataViewModel
                                 {
                                     wellId = g.Key,
                                     taskCount = g.Count().ToString(),
                                     minSchdDate = g.Min(c => c.scheduleDate).ToString(),
                                     maxSchdDate = g.Max(c => c.scheduleDate).ToString()
                                 }).ToList();
                return groupdata.ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellData GetRIGAIGroup", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

        //Well Data for Service Company
        private async Task<IEnumerable<WellMasterDataViewModel>> GetWellMaster()
        {
            IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
            var result = new List<WellMasterDataViewModel>();
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                result = await AIBusiness.GetWellMaster(wellId, userwell);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler =
                    new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)),
                                            Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellMaster", User.Identity.Name);
                _logger.LogInformation(ex.Message);
            }
            return result;
        }

        public ActionResult GetWellRIGData(string wellId)
        {
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                IEnumerable<AIWellDataModel> aIWellData;
                aIWellData = AIBusiness.GetRIGAIResult(wellId);
                return View("IndexWellAI", aIWellData);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellRIGData", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> WellData_Create([DataSourceRequest] DataSourceRequest request, WellMasterDataViewModel input, IFormCollection form)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            byte BatchFlag;
            try
            {
                WellRegister well = new WellRegister();
                input.chartColor = form["chartColor2"].ToString();
                ModelState.Remove("chartColor");
                //if (ModelState.IsValid)
                //{
                    if (input.wellId == null)
                    {
                        if (input.batchFlag)
                            BatchFlag = 1;
                        else
                            BatchFlag = 0;
                        well.well_id = Guid.NewGuid().ToString();
                        well.wellname = input.wellName;
                        well.welltype_id = input.wellTypeId.wellTypeId;
                        well.County = input.county;
                        well.customer_id = tenantId;
                        well.Conplete_well_drill = input.complete_well_drill == true ? Convert.ToByte(1) : Convert.ToByte(0);
                        well.Batch_drill_casing = input.batch_drill_casing == true ? Convert.ToByte(1) : Convert.ToByte(0);
                        well.Batch_drill_horizontal = input.batch_drill_horizontal == true ? Convert.ToByte(1) : Convert.ToByte(0);
                        well.Casing_string = input.casing_string == true ? Convert.ToByte(1) : Convert.ToByte(0);
                        well.NumAPI = input.numAPI;
                        well.NumAFE = input.numAFE;
                        well.RigID = input.rigID;
                        well.PadID = input.padID;
                        well.BatchFlag = BatchFlag;
                        well.CasingString = input.casingString;
                        well.Latitude = input.latitude;
                        well.Longitude = input.longitude;
                        well.FieldName = input.fieldName;
                        well.BatchDrillingTypeID = input.batchDrillingTypeId;
                        well.State = input.state;
                        well.Basin = input.Basin_ID.Basin_ID;                      
                        well.Prediction = true;
                        well.ChecklistTemplateId = input.ChecklistTemplateId;
                        well.Router_WellId = input.Router_WellId;

                    if (input.Prediction == true)
                        {
                            well.StartDate = DateTime.Now.Date;
                        }
                        else
                        {
                            well.StartDate = null;
                        }
                        //well.Prediction = input.Prediction;
                        if (!string.IsNullOrEmpty(input.chartColor))
                            well.ChartColor = input.chartColor;
                    }
                    db.WellRegister.Add(well);
                    await db.SaveChangesAsync();
                    if (input.Prediction)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = "Well-AI Operator", To_id = userId, Type = 4, IsActive = 1, EntityId = well.well_id, JobName = "New Prediction", TaskName = input.rigName + ":" + input.wellName, CreatedDate = DateTime.Now };
                        await commonBusiness.AddNotifications(messageQueue);
                        await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
                    }

                    //Phase II Changes - 05/21/2021
                    //05/24/2021
                    await commonBusiness.CreateWellChecklist(tenantId, well.well_id);
                //};

                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellData_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellData_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> WellDataDestroy(string wellId)
        {
            try
            {
                if (!string.IsNullOrEmpty(wellId))
                {
                    var itemToDelete = (from wl in db.WellRegister
                                        where wl.well_id == wellId
                                        select wl).SingleOrDefault();
                    if (itemToDelete != null)
                    {
                        db.WellRegister.Remove(itemToDelete);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellDataDestroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(null);
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> WellData_Update([DataSourceRequest] DataSourceRequest request, WellMasterDataViewModel input, IFormCollection form)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            byte BatchFlag;
            try
            {
                ModelState.Remove("chartColor");
                var well = db.WellRegister.Where(x => x.well_id == input.wellId).FirstOrDefault();
                if (well != null)
                {
                    input.chartColor = form["chartColor2"].ToString();
                    if (input.batchFlag)
                    {
                        BatchFlag = 1;
                    }
                    else
                    {
                        BatchFlag = 0;
                    }
                    well.wellname = input.wellName;
                    well.welltype_id = input.wellTypeId.wellTypeId;
                    well.County = input.county;
                    well.customer_id = tenantId;
                    well.Conplete_well_drill = input.complete_well_drill == true ? Convert.ToByte(1) : Convert.ToByte(0);
                    well.Batch_drill_casing = input.batch_drill_casing == true ? Convert.ToByte(1) : Convert.ToByte(0);
                    well.Batch_drill_horizontal = input.batch_drill_horizontal == true ? Convert.ToByte(1) : Convert.ToByte(0);
                    well.Casing_string = input.casing_string == true ? Convert.ToByte(1) : Convert.ToByte(0);
                    well.NumAPI = input.numAPI;
                    well.NumAFE = input.numAFE;
                    well.RigID = input.rigID;
                    well.PadID = input.padID;
                    well.BatchFlag = BatchFlag;
                    well.CasingString = input.casingString;
                    well.Latitude = input.latitude;
                    well.Longitude = input.longitude;
                    well.FieldName = input.fieldName;
                    well.BatchDrillingTypeID = input.batchDrillingTypeId;
                    well.State = input.state;
                    well.Basin = input.Basin_ID.Basin_ID;                    
                    well.Prediction = true;
                    well.ChecklistTemplateId = input.ChecklistTemplateId;
                    well.Router_WellId = input.Router_WellId;
                    if (input.Prediction == true /*&& input.OldPredictionForUpdate != input.Prediction*/)
                    {
                        well.StartDate = DateTime.Now.Date;
                    }
                    else
                    {
                        well.StartDate = null;
                    }
                    if (!string.IsNullOrEmpty(input.chartColor))
                        well.ChartColor = input.chartColor;
                    input.chartColor = well.ChartColor;
                };
                await db.SaveChangesAsync();
                string prdStatus = input.Prediction ? "Started" : "Stopped";
                MessageQueue messageQueue = new MessageQueue { From_id = "Well-AI Operator", To_id = userId, Type = 1, IsActive = 1, EntityId = well.well_id, JobName = "Prediction(Start/Stop)", TaskName = well.wellname + " " + prdStatus, CreatedDate = DateTime.Now };
                await commonBusiness.AddNotifications(messageQueue);
                await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellData_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellData_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        //DWOP
        [HttpGet]
        public async Task<JsonResult> GetWellDetailsByApiNumberAsync(string text, string filterType)
        {
            List<Result> WellData = new List<Result>();

            if (text != null)
            {
                text = text.ToUpper();
            }
            
            if (filterType.Equals("null")==true)
            {
                filterType = "API";
            }
            try
            {
                WellApiData ResResult = new WellApiData();
                var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
                Result data = new Result();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();

                    string Url = "";
                   if (filterType == "Name")
                    {
                        Url = GetUrl + "search_wells/name/";
                    }
                    else if(filterType == "API")
                    {
                        Url = GetUrl + "search_wells/api_number/";
                    }
                   
                    response = await client.GetAsync(Url + text).ConfigureAwait(true);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        ResResult = JsonConvert.DeserializeObject<WellApiData>(result);
                    }
                }

                if (ResResult.results != null)
                {
                    //WellApiData ResResult = new WellApiData();
                    //ResResult = JsonConvert.DeserializeObject<WellApiData>(JsonResult);
                    var wellResult = ResResult.results;

                    if(filterType=="API")
                    {
                        if (wellResult.Count > 0)
                        {
                            wellResult = wellResult.Where(ap => ap.api_number.Contains(text)).ToList();
                        }
                    }
                    else if(filterType=="Name")
                    {

                        if (wellResult.Count > 0)
                        {
                            wellResult = wellResult.Where(ap => ap.name.Contains(text)).ToList();
                        }
                    }

                    return Json(wellResult);
                }

                else if (ResResult.message != null && text != null)
                {
                    TempData["Error"] = ResResult.message;
                    return Json(WellData);
                }
                else
                {
                    return Json(WellData);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellDetailsByApiNumberAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json(WellData);
            }
        }

        //DWOP
        /// <summary>
        /// Get Template List for a Well Design and Operator Tenant
        /// </summary>
        /// <param name="wellDesign"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetChecklistTemplateForDesignAsync(string wellDesign)
        {

            List<ChecklistTemplate> templates = new List<ChecklistTemplate>();
            try
            {
                if (wellDesign != null)
                {
                    var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    List<ChecklistTemplateModel> templateList = new List<ChecklistTemplateModel>();
                    templateList = await commonBusiness.GetChecklistTemplateList(wellDesign, TenantId);
                    if (templateList != null)
                    {
                        templates = (from t in templateList
                                    select new ChecklistTemplate
                                    {
                                        ChecklistTemplateId = t.TemplateId,
                                        ChecklistTemplateName = t.TemplateName
                                    }).ToList();
                    }
                    
                    return Json(templates);
                }
                else
                {
                    return Json(templates);
                }                
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetChecklistTemplateForDesignAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json(templates);
            }
        }
    }
}