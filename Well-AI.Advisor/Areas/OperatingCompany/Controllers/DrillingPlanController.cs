using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    public class DrillingPlanController : Controller
    {

        private readonly WebAIAdvisorContext db;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public DrillingPlanController(WebAIAdvisorContext _db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, IConfiguration configuration)
        {
            this.db = _db;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                 WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;

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
                List<WellMasterDataViewModel> AIWellDataModelList = new List<WellMasterDataViewModel>();
                var WellList = db.WellRegister.Where(wel => wel.customer_id.Equals(tndid)).ToList();

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


                ViewBag.WellList = WellList;

                ViewBag.Stage = db.Stages.ToList();

                return View(WellCounts);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellData Index", User.Identity.Name);
                //_logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> DrillingPlan_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                 WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                List<DrillingPlanList> PlanData = new List<DrillingPlanList>();
                List<DrillingPlanModel> DrillPlan = new List<DrillingPlanModel>();
                List<DrillPlanWellViewModel> planwellsList = new List<DrillPlanWellViewModel>();
                List<DrillPlanHeader> DrillingPlanHeaderData = new List<DrillPlanHeader>();

                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //Rig Filter
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;

                if (RigId != "00000000-0000-0000-0000-000000000000")
                {
                    var Plan = (from dplan in db.DrillPlanHeader
                                join dpw in db.DrillPlanWells on dplan.DrillPlanId equals dpw.DrillPlanId into planWells
                                from wells in planWells.DefaultIfEmpty()
                                where dplan.TenantId == tndid && (wells.RigId == RigId && !checkwellFilter || checkwellFilter)
                                select new DrillingPlanList
                                {
                                    DrillingPlanId = dplan.DrillPlanId,
                                    DrillingPlanName = dplan.DrillPlanName,
                                    PlanStartDate = dplan.PlanStartDate,
                                    PlanCompletedDate = dplan.PlanCompleteDate,
                                    WellId = wells.Wellid,
                                    WellName = db.WellRegister.Where(x => x.well_id == wells.Wellid).Select(y => y.wellname).FirstOrDefault(),
                                    RigId = wells.RigId,
                                }).ToList();

                    var DrillPlanData = (from d in Plan
                                         group d by d.DrillingPlanId into drill
                                         select new DrillingPlanList
                                         {
                                             DrillingPlanId = drill.Key,
                                             DrillingPlanName = drill.Select(x => x.DrillingPlanName).FirstOrDefault(),
                                             WellName = string.Join(",", drill.Select(x => x.WellName).ToList()),
                                             PlanCompletedDate = drill.Select(x => x.PlanCompletedDate).FirstOrDefault(),
                                             PlanStartDate = drill.Select(x => x.PlanStartDate).FirstOrDefault(),
                                             WellCounts = drill.Select(x => x.WellId).ToList().Count()
                                         }).ToList();

                    return Json(DrillPlanData.ToDataSourceResult(request));
                }
                else
                {
                    DrillingPlanHeaderData = db.DrillPlanHeader.Where(x => x.TenantId == tndid).ToList();
                    foreach (var details in DrillingPlanHeaderData)
                    {
                        List<wellmodel> WellIdlist = await commonBusiness.GetDrillPlanWells(details.DrillPlanId, tndid);

                        DrillingPlanList tasks = new DrillingPlanList();
                        tasks = new DrillingPlanList
                        {
                            DrillingPlanId = details.DrillPlanId,
                            DrillingPlanName = details.DrillPlanName,
                            PlanStartDate = details.PlanStartDate,
                            PlanCompletedDate = (DateTime?)details.PlanCompleteDate,
                            WellName = WellIdlist == null ? "" : string.Join(",", WellIdlist.Select(x => x.wellName).ToList()),
                            WellCounts = WellIdlist.Count()
                        };

                        PlanData.Add(tasks);
                    }
                }



                return Json(PlanData.ToDataSourceResult(request));

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DrillingPlan_Read Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        //public async Task<IActionResult> DrillingPlan_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    try
        //    {
        //        List<DrillingPlanList> PlanData = new List<DrillingPlanList>();
        //        List<DrillingPlanModel> DrillPlan = new List<DrillingPlanModel>();
        //        var DrillingData = db.DrillingPlan.ToList();

        //        foreach (var details in DrillingData)
        //        {
        //            List<string> WellIdlist = new List<string>();
        //            List<string> WellNames = new List<string>();

        //            DrillingPlanList tasks = new DrillingPlanList();

        //            DrillPlanTasks PlanTasks = JsonConvert.DeserializeObject<DrillPlanTasks>(details.PlanDetails);

        //            if(PlanTasks != null)
        //            {
        //                foreach(var Well in PlanTasks.PlanDetails)
        //                {
        //                    WellIdlist.Add(Well.WellId);
        //                }
        //            }

        //            if (WellIdlist.Count > 0)
        //            {
        //                foreach (var WellId in WellIdlist)
        //                {
        //                   var WellName = db.WellRegister.Where(x => x.well_id == WellId).Select(y => y.wellname).FirstOrDefault();
        //                    WellNames.Add(WellName);
        //                }
        //            }

        //            tasks = new DrillingPlanList
        //            {
        //                DrillingPlanId = details.DrillingPlanId,
        //                DrillingPlanName = details.DrillingPlanName,
        //                PlanStartDate = details.PlanStartDate,
        //                PlanCompletedDate = details.PlanCompletedDate,
        //                WellName = string.Join(",", WellNames),
        //                WellCounts = WellNames.Count(),
        //                WellNames = WellNames
        //            };

        //            PlanData.Add(tasks);
        //        }


        //        return Json(PlanData.ToDataSourceResult(request));

        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "DrillingPlan_Read Index", User.Identity.Name);
        //        string returnUrl = @"/Dashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}


        public async Task<IActionResult> DrillPlanDetailsAsync(string DrillingPlanId)
        {
            try
            {
                DrillingPlanList PlanData = new DrillingPlanList();
                List<DrillingPlanModel> DrillPlan = new List<DrillingPlanModel>();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var WellList = db.WellRegister.Where(wel => wel.customer_id.Equals(tndid)).ToList();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                List<wellmodel> WellIdata = new List<wellmodel>();
                List<string> WellIdlist = new List<string>();
                List<string> WellNames = new List<string>();
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;

                DrillPlanTasks PlanTasks = new DrillPlanTasks();

                if (!string.IsNullOrEmpty(DrillingPlanId))
                {
                    if (RigId != "00000000-0000-0000-0000-000000000000")
                    {
                        var Plan = (from dplan in db.DrillPlanHeader
                                    join dpw in db.DrillPlanWells on dplan.DrillPlanId equals dpw.DrillPlanId into planWells
                                    from wells in planWells.DefaultIfEmpty()
                                    where dplan.TenantId == tndid && dplan.DrillPlanId == DrillingPlanId && (wells.RigId == RigId && !checkwellFilter || checkwellFilter)
                                    select new DrillingPlanList
                                    {
                                        DrillingPlanId = dplan.DrillPlanId,
                                        DrillingPlanName = dplan.DrillPlanName,
                                        PlanStartDate = dplan.PlanStartDate,
                                        PlanCompletedDate = dplan.PlanCompleteDate,
                                        WellId = wells.Wellid,
                                        WellName = db.WellRegister.Where(x => x.well_id == wells.Wellid).Select(y => y.wellname).FirstOrDefault(),
                                        RigId = wells.RigId,
                                    }).ToList();

                        var DrillPlanData = (from d in Plan
                                             group d by d.DrillingPlanId into drill
                                             select new DrillingPlanList
                                             {
                                                 DrillingPlanId = drill.Key,
                                                 DrillingPlanName = drill.Select(x => x.DrillingPlanName).FirstOrDefault(),
                                                 WellName = string.Join(",", drill.Select(x => x.WellName).ToList()),
                                                 PlanCompletedDate = drill.Select(x => x.PlanCompletedDate).FirstOrDefault(),
                                                 PlanStartDate = drill.Select(x => x.PlanStartDate).FirstOrDefault(),
                                                 WellCounts = drill.Select(x => x.WellId).ToList().Count(),
                                                 WellIds = drill.Select(x => new wellmodel { wellId = x.WellId, wellName = x.WellName }).ToList(),
                                                 WellIdList = string.Join(",", drill.Select(x => x.WellId).ToList())
                                             }).FirstOrDefault();

                        return View(DrillPlanData);
                    }
                    else
                    {
                        var DrillinplanData = db.DrillPlanHeader.Where(x => x.DrillPlanId == DrillingPlanId).FirstOrDefault();
                        WellIdata = await commonBusiness.GetDrillPlanWells(DrillingPlanId, tndid);

                        PlanData = new DrillingPlanList
                        {
                            DrillingPlanId = DrillinplanData.DrillPlanId,
                            DrillingPlanName = DrillinplanData.DrillPlanName,
                            PlanStartDate = DrillinplanData.PlanStartDate,
                            PlanCompletedDate = (DateTime?)DrillinplanData.PlanCompleteDate,
                            WellName = WellIdata == null ? "" : string.Join(",", WellIdata.Select(x => x.wellName).ToList().Distinct()),
                            WellIds = WellIdata.Distinct().ToList(),
                            WellIdList = string.Join(";", WellIdata.Select(x => x.wellId))
                        };
                    }

                }

                return View(PlanData);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DrillPlanDetails  ", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> DrillingPlanDetail_Read([DataSourceRequest] DataSourceRequest request, string wellId,string drillPlanId, string StageId)
        {
            try
            {
                List<PlannedTasksModel> PlannedTasks = new List<PlannedTasksModel>();
                List<ChecklistTaskTemplateModel> checklistTemplateTaks = new List<ChecklistTaskTemplateModel>();
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                List<Stagelist> stages = new List<Stagelist>();

                if (!string.IsNullOrEmpty(wellId))
                {
                    var wellData = db.WellRegister.FirstOrDefault(x => x.well_id == wellId);

                    PlannedTasks = await commonBusiness.GetPlanDetailsTasks(wellId, drillPlanId,TenantId);

                    if(PlannedTasks.Count == 0)
                    {
                        checklistTemplateTaks = await commonBusiness.ChecklistTemplateFordrillplan(wellData.welltype_id, wellId);
                        if (checklistTemplateTaks.Count > 0)
                        {
                            float accumDays = 0;
                            foreach (var checklist in checklistTemplateTaks)
                            {
                                string serviceDuration = checklist.ServiceDuration == null ? "00:00:00" : Convert.ToString(checklist.ServiceDuration);
                                var Days = serviceDuration.Split(":")[0];
                                var hours = serviceDuration.Split(":")[1];
                                var minitus = serviceDuration.Split(":")[2];

                                float duration = await commonBusiness.CalculateHours(Convert.ToInt32(Days), Convert.ToInt32(hours), Convert.ToInt32(minitus));

                                WellAI.Advisor.Model.Administration.TaskModel Tasks = new WellAI.Advisor.Model.Administration.TaskModel();

                                Tasks.CategoryName = checklist.CategoryName;
                                Tasks.StageType = checklist.StageType;
                                Tasks.IsBiddable = checklist.IsBiddable;
                                Tasks.ServiceCategoryId = checklist.ServiceCategoryId;

                                PlannedTasksModel PlannedTask = new PlannedTasksModel
                                {
                                    TaskId = checklist.TaskId,
                                    TaskName = checklist.Name,
                                    StageType = checklist.StageType,
                                    StageTypeName = checklist.StageTypeName,
                                    Category = checklist.ServiceCategoryId,
                                    CategoryName = checklist.CategoryName,
                                    IsBiddable = checklist.IsBiddable,
                                    ServiceDuration = checklist.ServiceDuration,
                                    ServiceDurationDays = Days ?? "00",
                                    ServiceDurationHours = hours ?? "00",
                                    ServiceDurationMinutes = minitus ?? "00",
                                    Description = checklist.Description,
                                    Dependency = checklist.SeletedDependency,
                                    Day = checklist.Day,
                                    Depth = checklist.Depth,
                                    IsSpecialServices = Convert.ToByte(checklist.IsSpecialServices),
                                    LeadTime = checklist.LeadTime,
                                    IsActive = (bool)checklist.IsActive,
                                    OperationDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                                    OperationHours = (decimal)Math.Round(duration, 2, MidpointRounding.AwayFromZero),
                                    AccumulatedDays = (decimal?)accumDays,// (int?)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                                    ServiceCategoryId = checklist.ServiceCategoryId,
                                    ScheduleTime = Convert.ToString(checklist.ScheduleTime),
                                    SeletedDependency = checklist.SeletedDependency != null ? checklist.SeletedDependency.Replace(";", ",") : null,
                                    Serviceoperator = null,
                                    Vendor = null,
                                    VendorName = null,
                                    commands = null,
                                    EmployeeId = null,
                                    EmployeeName = null,
                                    IsRowModified = false,
                                    IsPreSpud = (bool)checklist.IsPreSpud,
                                    IsBenchMark = (bool)checklist.IsBenchMark
                                };

                                PlannedTasks.Add(PlannedTask);
                            }
                        }

                    }        

                    if (!string.IsNullOrEmpty(StageId))
                    {
                        PlannedTasks =  PlannedTasks.Where(x => x.StageType == StageId).ToList();
                    }

                    return Json(PlannedTasks.ToDataSourceResult(request));

                }

                return Json(PlannedTasks.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DrillingPlanDetail_Read ", User.Identity.Name);
                //_logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> DrillingPlanTabContentAsync(string wellid, string DrillingPlanId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                DrillPlanWellViewModel PlanWellData = new DrillPlanWellViewModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                PlanWellData = await commonBusiness.DrillingPlanTabContent(wellid, DrillingPlanId, tndid);
                
                //ViewBag.Stage = db.Stages.ToList();
                var employees = (from user in db.WellIdentityUser
                                 where user.IsUser == false
                                 select new Employeelist
                                 {
                                     EmployeeId = user.Id,
                                     EmployeeName = user.FirstName ?? ""
                                 }).OrderBy(e => e.EmployeeName).ToList();
               
                ViewData["employeeList"] = employees;
                ViewData["defaultemployee"] = employees.FirstOrDefault();

                //ViewBag.Stage = db.Stages.ToList();
                var stages = db.Stages.OrderBy(x => x.Name).ToList();
                var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var categories = await auctionProposalBusiness.GetServiceCategorys();
                stages.Insert(0, new Stage { Id = DLL.Constants.NoSpecificWellFilterKey, Name = "Select Stage" });
                categories.Insert(0, new ServiceCategory { ServiceCategoryId = DLL.Constants.NoSpecificWellFilterKey, Name = "Select Category" });


                var stagesList = (from st in stages
                                  select new Stagelist
                                  {
                                      stage = st.Id,
                                      StageName = st.Name
                                  }
                                 ).ToList();

                var categoriesList = (from ct in categories
                                      select new Categorylist
                                      {
                                          Category = ct.ServiceCategoryId,
                                          CategoryName = ct.Name
                                      }
                                ).ToList();

                ViewData["stagesForTasks"] = stagesList;
                ViewData["categories"] = categoriesList;

                List<PlannedTasksModel> PlannedTasks = new List<PlannedTasksModel>();
                List<ChecklistTaskTemplateModel> checklistTemplateTak = new List<ChecklistTaskTemplateModel>();
                List<Stagelist> StageList = new List<Stagelist>();
                if (!string.IsNullOrEmpty(wellid))
                {
                    var wellData = db.WellRegister.FirstOrDefault(x => x.well_id == wellid);

                    PlannedTasks = await commonBusiness.GetPlanDetailsTasks(wellid, DrillingPlanId, tndid);

                    if (PlannedTasks.Count == 0)
                    {
                        checklistTemplateTak = await commonBusiness.ChecklistTemplateFordrillplan(wellData.welltype_id, wellid);
                        foreach (var tasksdata in checklistTemplateTak)
                        {
                            var Stage = new Stagelist
                            {
                                stage = tasksdata.StageType,
                                StageName = tasksdata.StageTypeName
                            };

                            stagesList.Add(Stage);
                        }
                    }
                    else
                    {
                        foreach (var tasksdata in PlannedTasks)
                        {
                            var Stage = new Stagelist
                            {
                                stage = tasksdata.stage,
                                StageName = tasksdata.StageName
                            };

                            stagesList.Add(Stage);
                        }
                    }

                    ViewBag.StageList = StageList;
                }

               return PartialView("_DrillingPlanTabContent", PlanWellData);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DrillingPlanTabContent Drillingplan", User.Identity.Name);
                //_logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        [HttpGet]
        public IActionResult GetWellName(string wellid)
        {
            try
            {
                if (!string.IsNullOrEmpty(wellid))
                {
                    var wellDetails = db.WellRegister.Where(x => x.well_id == wellid).FirstOrDefault();
                    return Json(new { wellname = wellDetails.wellname });

                }
                return Json(new { wellname = "" });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellName Drillingplan", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }

        }



        public PartialViewResult Tasks(string wellId, string DrillPlanId)
        {
            try
            {
                ViewBag.wellId = wellId;
                ViewBag.DrillPlanId = DrillPlanId;
                return PartialView("_Tasks");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Tasks Drillingplan", User.Identity.Name);
                return PartialView();
            }
        }


        public async Task<ActionResult> Task_Read([DataSourceRequest] DataSourceRequest request, string wellId, string DrillPlanId)
        {
            try
            {
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tasks = await commonBusiness.GetTasks();

                if (DrillPlanId != null && wellId != null)
                {
                    var TasksExits = await commonBusiness.GetPlanDetailsTasks(wellId, DrillPlanId, TenantId);
                    var TaskId = TasksExits.Select(x => x.TaskId).ToList();

                    var TasksList = from c in tasks
                                    where !TaskId.Contains(c.TaskId)
                                    select c;

                    TasksList.OrderBy(x => x.Name);
                    return Json(TasksList.ToDataSourceResult(request));
                }

                tasks.OrderBy(x => x.Name);

                return Json(tasks.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ChecklistTemplateDetails", User.Identity.Name);
                return null;
            }
        }

        public async Task<IActionResult> SaveDrillingPlan(IFormCollection form, DrillPlandetailsViewModel input)
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                 WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var Userdetails = await _userManager.GetUserAsync(User);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                await commonBusiness.SaveDrillplanHeader(form,input, tndid, Userdetails.Id.ToString());
                DrillingPlanList PlanData = new DrillingPlanList();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveAndUpdateDrillPlanTasks([FromBody] PlanDetailsModel PlanDetails)
        {
            try
            {
                int Result = 0;
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                 WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                //var Userdetails = _userManager.GetUserAsync(User);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                Result = await commonBusiness.SaveUpdatePlandetails(PlanDetails, tndid);
                Result = 1;
                return Json(new { status = Result });
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SaveAndUpdateDrillPlanTasks", User.Identity.Name);
                return null;
            }
        }

        [HttpPost]
        public ActionResult DrillingPlanDetailModel_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<PlannedTasksModel> planDetail)
        {
            List<PlannedTasksModel> list = (List<PlannedTasksModel>)TempData["trans"];
            if (planDetail != null && ModelState.IsValid)
            {
                //PlannedTasksModel plan = planDetail.First();
                //string plandDetailId = plan.DrillPlanDetailsId;
                //string taskId = plan.TaskId;
                //var target = list.Find(p => p.TaskId == taskId);
                //if (target != null)
                //{
//                    target.TaskName = plan.TaskName;
                    //target.Category = txn.Category;
                    //target.Amount = txn.Amount;
                //}
            }
            //TempData["trans"] = list;

            return Json(ModelState.ToDataSourceResult());
        }


        [AcceptVerbs("Post")]
        public async Task<ActionResult> Task_CreateAsync([DataSourceRequest] DataSourceRequest request, ActiveDrilPlanTasks input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            string category = ""; //db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
            string stagetype = "";// db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Id).FirstOrDefault(),
            string hours = "";
            string mins = "";
            string days = "";
            if (input != null)
            {
                if (input.ExportToMaster)
                {
                    var TasksExits = db.Tasks.Where(x => x.Name == input.TaskName && x.IsSpecialServices == Convert.ToInt32(input.IsSpecialServices)).FirstOrDefault();
                    if (TasksExits != null)
                    {
                        ModelState.AddModelError("Tasks", "This task is already exits");
                        return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                    }
                }

                category = db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                stagetype = db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault();
                hours = input.ServiceDurationHours ?? "00";
                mins = input.ServiceDurationMinutes ?? "00";
                days = input.ServiceDurationDays ?? "00";
            }

            float duration = await commonBusiness.CalculateHours(Convert.ToInt32(days), Convert.ToInt32(hours), Convert.ToInt32(mins));

            PlannedTasksModel PlannedTasks = new PlannedTasksModel
            {
                TaskId = Guid.NewGuid().ToString(),
                IsSpecialServices = input.IsSpecialServices,
                CategoryName = category,
                ServiceCategoryId = input.ServiceCategoryId,
                TaskName = input.TaskName,
                ServiceDuration = input.ServiceDurationDays + ":" + hours + ":" + mins,
                Depth = input.Depth,
                Description = input.Description,
                SeletedDependency = input.SeletedDependency == null ? "" : input.SeletedDependency.Replace(";", ","),
                StageType = input.StageType,
                StageTypeName = stagetype,
                LeadTime = input.LeadTime,
                Day = input.Day,
                ScheduleTime = input.ScheduleTime,
                ServiceDurationDays = input.ServiceDurationDays ?? "00",
                ServiceDurationHours = hours,
                ServiceDurationMinutes = mins,
                IsActive = input.IsActive,
                IsBiddable = input.IsBiddable,
                ExportToMaster = input.ExportToMaster,
                PlanStart = input.PlanStart,
                PlanFinishedDate = input.PlanStart != null ? Convert.ToDateTime(input.PlanStart).AddHours((double)duration) : input.PlanStart,
                ActualPlanStart = input.ActualPlanStart,
                commands = input.commands,
                IsPlanTask = input.IsPlanTask,
                EmployeeId = input.EmployeeId,
                Vendor = input.Vendor,
                VendorName = db.CorporateProfile.Where(x => x.TenantId == input.Vendor).Select(y => y.Name).FirstOrDefault(),
                EmployeeName = db.WellIdentityUser.Where(x => x.Id == input.EmployeeId).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault(),
                OperationDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                OperationHours = (decimal)Math.Round(duration, 2),
                AccumulatedDays = (int?)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                IsBenchMark = input.IsBenchMark,
                IsPreSpud = input.IsPreSpud
            };

            return Json(new[] { PlannedTasks }.ToDataSourceResult(request, ModelState));

        }


        [HttpPost]
        public async Task<IActionResult> Task_UpdateAsync([DataSourceRequest] DataSourceRequest request, ActiveDrilPlanTasks input)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                string category = ""; //db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                string stagetype = "";// db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Id).FirstOrDefault(),
                string hours = "";
                string mins = "";
                string days = "";
                if (input != null)
                {
                    category = db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                    stagetype = db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault();
                    hours = input.ServiceDurationHours ?? "00";
                    mins = input.ServiceDurationMinutes ?? "00";
                    days = input.ServiceDurationDays ?? "00";
                }
                float duration = await commonBusiness.CalculateHours(Convert.ToInt32(days), Convert.ToInt32(hours), Convert.ToInt32(mins));
                PlannedTasksModel PlannedTasks = new PlannedTasksModel
                {
                    TaskId = input.TaskId,
                    IsSpecialServices = input.IsSpecialServices,
                    CategoryName = category,
                    ServiceCategoryId = input.ServiceCategoryId,
                    TaskName = input.TaskName,
                    ServiceDuration = input.ServiceDurationDays + ":" + hours + ":" + mins,
                    Depth = input.Depth,
                    Description = input.Description,
                    SeletedDependency = input.SeletedDependency == null ? "" : input.SeletedDependency.Replace(";", ","),
                    StageType = input.StageType,
                    StageTypeName = stagetype,
                    LeadTime = input.LeadTime,
                    Day = input.Day,
                    ScheduleTime = input.ScheduleTime,
                    ServiceDurationDays = input.ServiceDurationDays ?? "00",
                    ServiceDurationHours = hours,
                    ServiceDurationMinutes = mins,
                    IsActive = input.IsActive,
                    IsBiddable = input.IsBiddable,
                    ExportToMaster = input.ExportToMaster,
                    PlanStart = input.PlanStart,
                    PlanFinishedDate = input.PlanStart != null ? Convert.ToDateTime(input.PlanStart).AddHours((double)duration) : input.PlanStart,
                    ActualPlanStart = input.ActualPlanStart,
                    commands = input.commands,
                    IsPlanTask = input.IsPlanTask,
                    EmployeeId = input.EmployeeId,
                    Vendor = input.Vendor,
                    VendorName = db.CorporateProfile.Where(x => x.TenantId == input.Vendor).Select(y => y.Name).FirstOrDefault(),
                    EmployeeName = db.WellIdentityUser.Where(x => x.Id == input.EmployeeId).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault(),
                    OperationDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                    OperationHours = (decimal)Math.Round(duration, 2, MidpointRounding.AwayFromZero),
                    AccumulatedDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                    IsRowModified = true,
                    IsBenchMark = input.IsBenchMark,
                    IsPreSpud = input.IsPreSpud
                };

                return Json(new[] { PlannedTasks }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController Tasks", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDrillPlan(string planId)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = await commonBusiness.DeleteDrillPlan(planId, tenantId);
                return Json(new { result });
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController Tasks", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RecalculatePlan([FromBody] PlanDetailsModel planDetails)
        {
            GeneralHttpResponse result = new GeneralHttpResponse();
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                //Call azure function
                var drillPlanFunctionURL = _configuration.GetValue<string>("Azure:DrillPlan:Url");
                //dynamic content = new { PlanDetailsModel = planDetails };
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // required to use ReadAsAsync
                    using (var request = new HttpRequestMessage(HttpMethod.Post, drillPlanFunctionURL))
                    using (var httpContent = General.CreateHttpContent(planDetails))
                    {
                        request.Content = httpContent;

                        using (var newresponse = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            //result = JsonConvert.DeserializeObject<GeneralHttpResponse>(newresponse.Content.ToString());
                            //var result1 = await newresponse.Content.ReadAsAsync<GeneralHttpResponse>();
                            //result = (GeneralHttpResponse)result1;

                            var result2 = await newresponse.Content.ReadAsAsync<string>();
                            result = JsonConvert.DeserializeObject<GeneralHttpResponse>(result2);
                        }
                    }
                }

                return Json(result);

            }
            catch (Exception ex)
            {
                result.resultCode = 0;
                result.resultMessage = "Failure";
                result.messageContent = ex.Message.ToString();
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SaveAndUpdateDrillPlanTasks", User.Identity.Name);
                return Json(result);
            }
        }


        public JsonResult GetWellIds(string PlanId)
        {
            try
            {               
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var WellList = db.WellRegister.Where(wel => wel.customer_id.Equals(TenantId)).ToList();

                var PlanWells = (from Plan in db.DrillPlanHeader
                                 join DrillWells in db.DrillPlanWells on Plan.DrillPlanId equals DrillWells.DrillPlanId
                                 where Plan.TenantId == TenantId
                                 select DrillWells).ToList();

                List<WellRegister> wellsList = new List<WellRegister>();
                if (PlanWells != null && PlanWells.Count > 0)
                {                 
                        var WellIds = PlanWells.Where(p => p.DrillPlanId != PlanId).Select(x => x.Wellid).Distinct().ToList();
                       
                            wellsList = (from well in WellList
                                         where !WellIds.Contains(well.well_id)
                                         select well).ToList();                       
                        return Json(wellsList);
                 }                    

                return Json(WellList);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController Tasks", User.Identity.Name);
                return Json(new { });
            }
        }


        //Import Task Changes
        public async Task<JsonResult> ImportTaskChanges(string wellId, string drillPlanId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                DrillPlanWellViewModel PlanWellData = new DrillPlanWellViewModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var Result = await commonBusiness.ImportTaskForDrillingPlan(wellId, drillPlanId, tndid);
                return Json(new { result = Result });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ImportTaskChanges ", User.Identity.Name);
                return Json(new { result = 0 });
            }
        }


        [HttpGet]
        public IActionResult GetCategoriesList()
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = commonBusiness.GetCategoriesList();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetCategoriesList", User.Identity.Name);
                return null;
            }
        }
    }
}
