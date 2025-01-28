using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.OperatingCompany;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Well_AI.Advisor.Log.Error;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using WellAI.Advisor.Model.Identity;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.DLL.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WellAI.Advisor.BLL.Business;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.Model.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    public class ActiveDrillPlanController : Controller
    {
        private readonly WebAIAdvisorContext _db;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private TenantOperatingDbContext _tenantdb;
        private readonly IConfiguration _configuration;

        public ActiveDrillPlanController(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, TenantOperatingDbContext tenantdb, IConfiguration configuration)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _tenantdb = tenantdb;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            var wellIdCookie = Request.Cookies["wellfilterlayout"];
            var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
            var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
            var drillplanlist = (from planHeader in _db.DrillPlanHeader
                                 join PlanWels in _db.DrillPlanWells on planHeader.DrillPlanId equals PlanWels.DrillPlanId
                                 where planHeader.TenantId == tenantId && (PlanWels.RigId == RigId && !checkwellFilter || checkwellFilter)
                                 select new DrillPlanHeader
                                 {
                                     DrillPlanId = planHeader.DrillPlanId,
                                     DrillPlanName = planHeader.DrillPlanName
                                 }).Distinct().OrderBy(x => x.DrillPlanName).ToList();

           
            ViewBag.Drillingplanlist = drillplanlist;
            ViewBag.Stage = _db.Stages.ToList();
            var employees = (from user in _db.WellIdentityUser
                             where user.IsUser == false
                             select new Employeelist
                             {
                                 EmployeeId = user.Id,
                                 EmployeeName = user.FirstName ?? ""
                             }).OrderBy(e => e.EmployeeName).ToList();

            var defaultval = new Employeelist
            {
                EmployeeName = "--Select employee--",
                EmployeeId = null
            };
            var operrepo = new OperatingTenantRepository(_tenantdb);
            var providerDirectoryId = operrepo.GetProviderDirectoryId(tenantId).Result;
            var corpProfiles = _db.CorporateProfile.Where(x => providerDirectoryId.Contains(x.TenantId)).Select(x => new VendorViewModel { Vendor = x.TenantId, VendorName = x.Name }).OrderBy(e => e.VendorName).ToList();
            var defaultval1 = new VendorViewModel
            {
                VendorName = "--Select--",
                Vendor = "null2"
            };
            corpProfiles.Insert(0, defaultval1);
            ViewData["vendors"] = corpProfiles;
            ViewBag.vendors = corpProfiles;
            employees.Insert(0, defaultval);
            ViewData["employeeList"] = employees;

            return View();
        }

        public async Task<IActionResult> DrillingPlanDetail_Read([DataSourceRequest] DataSourceRequest request, string wellId, string drillPlanId)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                List<PlannedTasksModel> PlannedTasks = new List<PlannedTasksModel>();
                List<ChecklistTaskTemplateModel> checklistTemplateTaks = new List<ChecklistTaskTemplateModel>();
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);

                if (!string.IsNullOrEmpty(wellId))
                {
                    var wellData = _db.WellRegister.FirstOrDefault(x => x.well_id == wellId);

                    PlannedTasks = await commonBusiness.GetPlanDetailsTasks(wellId, drillPlanId, TenantId);

                    //Will get from  Drilling Plan
                    if (PlannedTasks.Count == 0) 
                    {
                        checklistTemplateTaks = await commonBusiness.ChecklistTemplateFordrillplan(wellData.welltype_id, wellId);
                        if (checklistTemplateTaks.Count > 0)
                        {
                            //float accumDays = 0;
                            foreach (var checklist in checklistTemplateTaks)
                            {
                                string serviceDuration = checklist.ServiceDuration == null ? "00:00:00" : Convert.ToString(checklist.ServiceDuration);
                                var Days = serviceDuration.Split(":")[0];
                                var hours = serviceDuration.Split(":")[1];
                                var minitus = serviceDuration.Split(":")[2];

                                float duration = await commonBusiness.CalculateHours(Convert.ToInt32(Days), Convert.ToInt32(hours), Convert.ToInt32(minitus));

                                PlannedTasksModel PlannedTask = new PlannedTasksModel
                                {
                                    TaskId = checklist.TaskId,
                                    TaskName = checklist.Name,
                                    Category = checklist.Name   ,                            
                                    stage = checklist.StageType,
                                    IsBiddable = checklist.IsBiddable,
                                    OperationDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                                    OperationHours = (decimal)Math.Round(duration, 2, MidpointRounding.AwayFromZero),
                                    AccumulatedDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                                    IsSpecialServices = Convert.ToByte(checklist.IsSpecialServices),
                                    CategoryName = checklist.Name,    
                                    ServiceCategoryId = checklist.ServiceCategoryId,
                                    ServiceDuration = Days + ":" + hours + ":" + minitus,
                                    Depth = checklist.Depth,
                                    Description = checklist.Description,
                                    StageType = checklist.StageType,
                                    StageTypeName = checklist.StageTypeName,
                                    LeadTime = checklist.LeadTime,
                                    Day = checklist.Day,
                                    ServiceDurationDays = checklist.ServiceDurationDays ?? "00",
                                    ServiceDurationHours = hours,
                                    ServiceDurationMinutes = minitus,
                                    IsActive = (bool)checklist.IsActive,
                                    ExportToMaster = (bool)checklist.ExportToMaster,
                                    ScheduleTime = Convert.ToString(checklist.ScheduleTime),
                                    SeletedDependency = checklist.SeletedDependency != null ? checklist.SeletedDependency.Replace(";", ",") : null,
                                    Vendor = "",
                                    commands = null,
                                    EmployeeId = "",
                                    IsRowModified = false,
                                    IsPreSpud = (bool)checklist.IsPreSpud,
                                    IsBenchMark = (bool)checklist.IsBenchMark
                                };
                                
                                PlannedTasks.Add(PlannedTask);
                            }
                            //Accum Days calculation
                            float accumDays = 0;
                            foreach (var item in PlannedTasks)
                            {
                                accumDays = accumDays + (float)item.OperationDays;
                                item.AccumulatedDays = (decimal)accumDays;
                            }
                        }

                    }

                }

                return Json(PlannedTasks.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DrillingPlanDetail_Read ", User.Identity.Name);
                //_logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDrillPlanWells(string DrillPlanId)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);
                var wellList = await commonBusiness.GetDrillPlanWells(DrillPlanId, tenantId);
                var DrillinplanData = _db.DrillPlanHeader.Where(x => x.DrillPlanId == DrillPlanId).FirstOrDefault();
                DrillingPlanList PlanData = new DrillingPlanList
                {
                    DrillingPlanId = DrillinplanData.DrillPlanId,
                    DrillingPlanName = DrillinplanData.DrillPlanName,
                    PlanStartDate = (DateTime?)DrillinplanData.PlanStartDate, 
                    Predictable = DrillinplanData.Prediction
                };

                return Json(new { wellList, PlanData });
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetDrillPlanWells ", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> DrillPlanTabContentAsync(string wellId, string drillPlanId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);
                DrillPlanWellViewModel PlanWellData = new DrillPlanWellViewModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                PlanWellData = await commonBusiness.DrillingPlanTabContent(wellId, drillPlanId, tndid);               
                return PartialView("_DrillPlanTabContent", PlanWellData);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DrillingPlanDetail_Read ", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return null;
            }
        }


        public IActionResult Tasks(string wellId, string DrillPlanId)
        {
            try
            {
                ViewBag.wellId = wellId;
                ViewBag.DrillPlanId = DrillPlanId;
                return PartialView("_Tasks");
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController Tasks", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        public async Task<ActionResult> Task_Read([DataSourceRequest] DataSourceRequest request, string wellId, string DrillPlanId)
        {
            try
            {
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ChecklistTemplateDetails", User.Identity.Name);
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> Task_CreateAsync([DataSourceRequest] DataSourceRequest request, ActiveDrilPlanTasks input, int currentRowIndex)
        {
            var currentIndex = Convert.ToInt32(Request.Cookies["DrillPlanCurrentRowIndex"]);
            ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);
            string category = ""; //db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
            string stagetype = "";// db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Id).FirstOrDefault(),
            string hours = "";
            string mins = "";
            string days = "";
            try
            {
                if (input != null)
                {
                    if (input.ExportToMaster)
                    {
                        var TasksExits = _db.Tasks.Where(x => x.Name == input.TaskName && x.IsSpecialServices == Convert.ToInt32(input.IsSpecialServices)).FirstOrDefault();
                        if (TasksExits != null)
                        {
                            ModelState.AddModelError("Tasks", "This task is already exits in Task Master :" + input.TaskName);
                            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                        }
                        else
                        {
                            days = input.ServiceDurationDays == "" ? "00" : input.ServiceDurationDays == null ? "00" : input.ServiceDurationDays;
                            hours = input.ServiceDurationHours == null ? "00" : input.ServiceDurationHours;
                            mins = input.ServiceDurationMinutes == null ? "00" : input.ServiceDurationMinutes;
                            var ServiceDuration = days + ":" + hours + ":" + mins;
                            dynamic ScheduleTime = input.ScheduleTime == "" ? "00:00" : input.ScheduleTime == null ? "00:00" : input.ScheduleTime;
                            //TasksExits = _db.Tasks.Where(x => x.Name == input.TaskName && x.IsSpecialServices == Convert.ToInt32(input.IsSpecialServices)).FirstOrDefault();
                            var TaskId = Guid.NewGuid().ToString();
                            Tasks tasks = new Tasks()
                            {
                                //CreatedBy = taskModel.,
                                CreatedDate = DateTime.UtcNow,
                                Description = input.Description,
                                IsActive = true,
                                Day = input.Day,
                                Dependency = input.SeletedDependency,
                                Depth = input.Depth,
                                LeadTime = input.LeadTime,
                                ScheduleTime = (TimeSpan?)TimeSpan.Parse(ScheduleTime),
                                Name = input.TaskName,
                                IsSpecialServices = Convert.ToInt32(input.IsSpecialServices),
                                TaskId = TaskId,
                                IsBiddable = (bool)input.IsBiddable,
                                StageType = input.StageType,
                                ServiceDuration = ServiceDuration
                            };

                            CategoryTask CategoryTask = new CategoryTask
                            {
                                CategoryTaskId = Guid.NewGuid().ToString(),
                                ServiceCategoryId = input.ServiceCategoryId,
                                TaskId = TaskId,
                                //CreatedBy = userId,
                                CreatedDate = DateTime.UtcNow,
                                IsActive = true
                            };
                            _db.CategoryTasks.Add(CategoryTask);
                            _db.Tasks.Add(tasks);
                            await _db.SaveChangesAsync();
                        }
                    }

                    category = _db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                    stagetype = _db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault();
                    hours = input.ServiceDurationHours ?? "00";
                    mins = input.ServiceDurationMinutes ?? "00";
                    days = input.ServiceDurationDays ?? "00";
                }

                float duration = await commonBusiness.CalculateHours(Convert.ToInt32(days), Convert.ToInt32(hours), Convert.ToInt32(mins));

                PlannedTasksModel PlannedTasks = new PlannedTasksModel
                {
                    TaskId = Guid.NewGuid().ToString(),
                    TaskName = input.TaskName,
                    IsSpecialServices = input.IsSpecialServices,
                    CategoryName = category,
                    ServiceCategoryId = input.ServiceCategoryId,
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
                    ActualPlanFinishedDate = input.ActualPlanFinishedDate,
                    commands = input.commands,
                    IsPlanTask = input.IsPlanTask,
                    VendorName = _db.CorporateProfile.Where(x => x.TenantId == input.Vendor).Select(y => y.Name).FirstOrDefault(),
                    EmployeeName = _db.WellIdentityUser.Where(x => x.Id == input.EmployeeId).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault(),
                    EmployeeId = input.EmployeeId,
                    Vendor = input.Vendor,
                    OperationDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                    OperationHours = (decimal)Math.Round(duration, 2),
                    AccumulatedDays = (int?)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                    TaskOrder = currentIndex + 1
                };

                return Json(new[] { PlannedTasks }.ToDataSourceResult(request, ModelState));

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TaskCreateAsync", User.Identity.Name);
                return null;
            }


       }


        [HttpPost]

        public async Task<IActionResult> Task_UpdateAsync([DataSourceRequest] DataSourceRequest request, ActiveDrilPlanTasks input)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);

                string category = ""; //db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                string stagetype = "";// db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Id).FirstOrDefault(),
                string hours = "";
                string mins = "";
                string days = "";
                if (input != null)
                {
                    category = _db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                    stagetype = _db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault();
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
                    Category= category,                    
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
                    ActualPlanFinishedDate = input.ActualPlanFinishedDate,
                    commands = input.commands,
                    IsPlanTask = input.IsPlanTask,
                    EmployeeId = input.EmployeeId,
                    Vendor = input.Vendor,
                    VendorName = _db.CorporateProfile.Where(x => x.TenantId == input.Vendor).Select(y => y.Name).FirstOrDefault(),
                    EmployeeName = _db.WellIdentityUser.Where(x => x.Id == input.EmployeeId).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault(),
                    OperationDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                    OperationHours = (decimal)Math.Round(duration, 2),
                    AccumulatedDays = (decimal)Math.Round(duration / 24, 2, MidpointRounding.AwayFromZero),
                    IsRowModified = true,
                    IsBenchMark = input.IsBenchMark,
                    IsPreSpud = input.IsPreSpud
                };

                return Json(new[] { PlannedTasks }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController Tasks", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            }


        [HttpPost]

        public async Task<IActionResult> SaveAndUpadeteActiveDrilPlanTasks([FromBody] PlanDetailsModel ActiveDrilPlanTasks)
        {
            try
            {
                int Result = 0;
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                 WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                //var Userdetails = _userManager.GetUserAsync(User);
                ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);
                Result = await commonBusiness.SaveUpdatePlandetails(ActiveDrilPlanTasks, tndid);
                return Json(new { status = Result });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SaveAndUpdateDrillPlanTasks", User.Identity.Name);
                return null;
            }
        }

        [HttpPost]
        public JsonResult GetSatageData([FromBody] List<StageChartData> Tasks)
        {
            try
            {
                if (Tasks != null)
                {
                    var StageData = Tasks.GroupBy(g => g.StageTypeName).
                        Select(x => new
                        {
                            StageName = x.Key,
                            PlanFinishedHours = x.Sum(s => s.PlanFinishedOperationHours),
                            ActualFinishedHours = x.Sum(s => s.ActualOperationFinishedHours)
                        }).OrderBy(O => O.StageName).ToList();

                    return Json(StageData);
                }

                return Json(new { StageName = "null", PlanFinishedHours = 0, ActualFinishedHours = 0});
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetSatageData", User.Identity.Name);
                return null;
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "RecalculatePlan", User.Identity.Name);
                return Json(result);
            }
        }

        public JsonResult GetEmployeeList()
        {
            try
            {
                var employees = (from user in _db.WellIdentityUser
                                 where user.IsUser == false
                                 select new Employeelist
                                 {
                                     EmployeeId = user.Id,
                                     EmployeeName =string.Concat(user.FirstName + " " , user.LastName),
                                 }).OrderBy(e => e.EmployeeName).ToList();

                return Json(employees);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return null;
            }
        }

        public JsonResult GetVendorList()
        {
            try
            {
               List<ProviderDirectory> ProviderDirectory = new List<ProviderDirectory>();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var operrepo = new OperatingTenantRepository(_tenantdb);
                var providerDirectoryId = operrepo.GetProviderDirectory(tndid).Result;

                foreach(var provider in providerDirectoryId)
                {
                    if(provider.MSA != null)
                    {
                        var Msa = _db.ProviderMSALinks.Where(x => x.FileId == provider.MSA).FirstOrDefault();                      
                        if(Msa != null)
                        {
                           if(Msa.IsApproved == true)
                            {
                                ProviderDirectory.Add(provider);
                            }
                        }
                    }
                }

                var corpProfiles = _db.CorporateProfile.Where(x => ProviderDirectory.Select(s => s.CompanyId).Contains(x.TenantId))
                    .Select(x => new VendorViewModel { Vendor = x.TenantId, VendorName = x.Name }).OrderBy(e => e.VendorName).ToList();

                return Json(corpProfiles);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return null;
            }
        }


        //Import Task Changes
        public async Task<JsonResult> ImportTaskChanges(string wellId, string drillPlanId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);
                DrillPlanWellViewModel PlanWellData = new DrillPlanWellViewModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var tndid = userwell.TenantId;
                var Result = await commonBusiness.ImportTaskForDrillingPlan(wellId, drillPlanId, tndid);
                return Json(new { result = Result });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ImportTaskChanges ", User.Identity.Name);
                return Json(new { result = 0 });
            }
        }


        public IActionResult AddAuctionProposal(AddAuctionProposalViewModel AddAuctionProposal)
        {
            try
            {
                return PartialView("_AddAuctionProposal", AddAuctionProposal);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return null;
            }
        }


        public async Task<JsonResult> GetRepeatChangingTasks(string PlanId)
        {
            try
            {
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                List<CheckListTaskModel> CheckListTask = new List<CheckListTaskModel>();
                List<ChecklistReview> CompletedPlanList = new List<ChecklistReview>();
                if (!string.IsNullOrEmpty(PlanId))
                {
                    var IsActualFinishDate = _db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanId && x.TenantId == TenantId && x.ActualCompletedDate != null).Count();

                    if(IsActualFinishDate > 0)
                    {

                        var wellTypeList = (from wtype in _db.WellType
                                            join well in _db.WellRegister on wtype.welltype_id equals well.welltype_id
                                            join dpwell  in _db.DrillPlanWells on well.well_id equals dpwell.Wellid
                                            join dh in _db.DrillPlanHeader on dpwell.DrillPlanId equals dh.DrillPlanId
                                            where dpwell.DrillPlanId.Equals(PlanId) && dh.TenantId.Equals(TenantId)
                                            select wtype).ToList();

                        foreach(var welltype in wellTypeList)
                        {
                            var PlanList = (from dh in _db.DrillPlanHeader.OrderByDescending(x => x.ActualCompletedDate).Take(5)
                                            join dpwell in _db.DrillPlanWells on dh.DrillPlanId equals dpwell.DrillPlanId
                                            join well in _db.WellRegister on dpwell.Wellid equals well.well_id into w
                                            from Well in w.DefaultIfEmpty()
                                            where Well.welltype_id.Equals(welltype.welltype_id) && dh.TenantId.Equals(TenantId)
                                            select new ChecklistReview
                                            {
                                                PlanId = dh.DrillPlanId,
                                                WellId = dpwell.Wellid,
                                                WellTypeId = Well.welltype_id,
                                                ChecklistTemplateId = Well.ChecklistTemplateId,
                                                PlanWellsId = dpwell.DrillPlanWellsId
                                            }).ToList();

                            CompletedPlanList.AddRange(PlanList);
                        }


                        var tasks = await GetTemplateRepeatedTasks(CompletedPlanList);

                        return Json(tasks);
                    }

                }
                return Json(CheckListTask);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return Json(new { });
            }
        }


        public async Task<List<ChecklistReview>> GetTemplateRepeatedTasks(List<ChecklistReview> ChecklistReview)
        {
            try
            {
                List<ChecklistReview> CheckListTask = new List<ChecklistReview>();

                ChecklistTemplateTaskListModel ChecklistTemplate = new ChecklistTemplateTaskListModel();
                CheckListTemplate defaultCheckList = new CheckListTemplate();

                if (ChecklistReview != null)
                {
                    foreach(var Checklist in ChecklistReview)
                    {
                        var drillPlanTasks = _db.DrillPlanDetails.Where(x => x.DrillPlanWellsId == Checklist.PlanWellsId).ToList();
                        var checkListTemplate = _db.ChecklistTemplate.Where(x => x.CheckListTemplateId == Checklist.ChecklistTemplateId).FirstOrDefault();
                        if(checkListTemplate != null)
                        {
                            ChecklistTemplate = JsonConvert.DeserializeObject<ChecklistTemplateTaskListModel>(checkListTemplate.Checklist);
                        }
                        else
                        {
                            defaultCheckList = _db.ChecklistTemplate.Where(x => x.WellTypeId == Checklist.WellTypeId && x.IsDefault == true).FirstOrDefault();
                            if (defaultCheckList != null)
                            {
                                ChecklistTemplate = JsonConvert.DeserializeObject<ChecklistTemplateTaskListModel>(defaultCheckList.Checklist);
                            }
                        }

                        var commonTasksList = drillPlanTasks.Where(x => ChecklistTemplate.checklist.Select(t => t.TaskId).Contains(x.TaskId)).ToList();

                        foreach (var Task in commonTasksList)
                        {
                            var templatelist = ChecklistTemplate.checklist.Where(x => x.TaskId == Task.TaskId).FirstOrDefault();
                            if (Task.TaskName != templatelist.Name || Task.CategoryId != templatelist.ServiceCategoryId || Task.ServiceDuration != templatelist.ServiceDuration)
                            {
                                var isExits = CheckListTask.Where(x => x.TaskId == Task.TaskId).FirstOrDefault();
                                if(isExits != null)
                                {
                                    CheckListTask.Remove(isExits);
                                }

                                string checklistTemplateId = "";
                                if (checkListTemplate != null)
                                {
                                    checklistTemplateId = checkListTemplate.CheckListTemplateId; 
                                }
                                else
                                {
                                    checklistTemplateId = defaultCheckList.CheckListTemplateId;
                                }
                                    
                                var TaskList = new ChecklistReview
                                {
                                    PlanId = Checklist.PlanId,
                                    WellId = Checklist.WellId,
                                    WellTypeId = Checklist.WellTypeId,
                                    ChecklistTemplateId = checklistTemplateId,
                                    PlanWellsId = Checklist.PlanWellsId,
                                    TaskId = Task.TaskId,
                                    Name = Task.TaskName,
                                    SeletedDependency = Task.Dependency,
                                    IsSpecialServices = Convert.ToString(Task.IsSpecialServices),
                                    CategoryName = _db.serviceCategories.Where(x => x.ServiceCategoryId == Task.CategoryId).Select(y => y.Name).FirstOrDefault(),
                                    ServiceCategoryId = Task.CategoryId,
                                    Day = Task.Day,
                                    Depth = Task.Depth,
                                    Description = Task.Description,
                                    IsBiddable = (bool)Task.IsBiddable,
                                    LeadTime = Task.LeadTime,
                                    ScheduleTime = Task.ScheduleTime,
                                    ServiceDuration = Task.ServiceDuration,
                                    StageType = Task.StageId,
                                    StageTypeName = _db.Stages.Where(x => x.Id == Task.StageId).Select(y => y.Name).FirstOrDefault(),
                                    TaskOrder =(int)Task.TaskOrder,                                
                                };

                                CheckListTask.Add(TaskList);
                            }
                        }
                    }
                }

                return await Task.FromResult(CheckListTask);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return null;
            }
        }


        [HttpPost]

        public async Task<IActionResult> AddMasterToList([FromBody] List<ChecklistReview> Checklist)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(_db, _roleManager, _userManager);

                if (Checklist != null)
                {
                    foreach (var Tasks in Checklist)
                    {
                        List<WellAI.Advisor.Model.Administration.ChecklistTemplateModel> TasksList = new List<WellAI.Advisor.Model.Administration.ChecklistTemplateModel>();
                        ChecklistTemplateTaskListModel TasksLists = new ChecklistTemplateTaskListModel();
                        var checkListTemplate = _db.ChecklistTemplate.Where(x => x.CheckListTemplateId == Tasks.ChecklistTemplateId).FirstOrDefault();
                        if (checkListTemplate != null)
                        {
                            TasksLists = JsonConvert.DeserializeObject<ChecklistTemplateTaskListModel>(checkListTemplate.Checklist);
                        }

                        if (TasksLists != null)
                        {
                            var taskExits = TasksLists.checklist.Where(x => x.TaskId == Tasks.TaskId).FirstOrDefault();
                            if (taskExits != null)
                            {
                                TasksLists.checklist.Remove(taskExits);
                                taskExits.Name = Tasks.Name;
                                taskExits.StageType = Tasks.StageType;
                                taskExits.ServiceDuration = Tasks.ServiceDuration;
                                taskExits.IsSpecialServices = Tasks.IsSpecialServices;
                                taskExits.SeletedDependency = Tasks.SeletedDependency == null ? null : Tasks.SeletedDependency.Replace(",", ";");
                                taskExits.Day = Tasks.Day;
                                taskExits.Description = Tasks.Description;
                                taskExits.LeadTime = Tasks.LeadTime;
                                taskExits.ScheduleTime = Convert.ToString(Tasks.ScheduleTime);
                                taskExits.LeadTime = Tasks.LeadTime;
                                taskExits.StageTypeName = Tasks.StageTypeName;
                                taskExits.IsBiddable = Tasks.IsBiddable;
                                taskExits.IsActive = Tasks.IsActive;
                                taskExits.Depth = Tasks.Depth;
                                taskExits.TaskOrder = Tasks.TaskOrder;

                                TasksLists.checklist.Add(taskExits);
                            }

                            var updateChecklist = new ChecklistTemplateTaskListModel
                            {
                                count = TasksLists.checklist.Count(),
                                checklist = TasksLists.checklist
                            };

                            checkListTemplate.Checklist = JsonConvert.SerializeObject(updateChecklist);
                            _db.ChecklistTemplate.Update(checkListTemplate);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
                return Json(new { });
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return Json(new { });
            }
        }


        [HttpPost]

        public IActionResult PlanPrediction(string PlanId, bool? Prediction)
        {
            try
            {
                if(!string.IsNullOrEmpty(PlanId) && Prediction != null)
                {
                    var planDetails = _db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanId).FirstOrDefault();

                    if(planDetails != null)
                    {
                        planDetails.Prediction = (bool)Prediction;
                    }

                    _db.DrillPlanHeader.Update(planDetails);
                    _db.SaveChanges();
                }

                return Json(new { });
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return Json(new { });
            }
        }

        public IActionResult GetNextBopValue(string wellId)
        {
            try
            {
                if (!string.IsNullOrEmpty(wellId))
                {
                    var wellDetails = _db.WellRegister.Where(x => x.well_id == wellId).FirstOrDefault();

                    if (wellDetails.ChecklistTemplateId != null)
                    {
                        var templateDetails = _db.ChecklistTemplate.Where(x => x.CheckListTemplateId == wellDetails.ChecklistTemplateId).FirstOrDefault();

                        return Json(new { templateDetails });
                    }
                    else
                    {
                        var templateDetails = _db.ChecklistTemplate.Where(x => x.WellTypeId == wellDetails.welltype_id && x.IsDefault == true ).FirstOrDefault();
                        return Json(new { templateDetails });
                    }
                }

                return Json(new { });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return Json(new { });
            }
        }


        [HttpGet]

        public IActionResult TaksValidation(string taskId)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var isTasksExisted = _db.AuctionProposals.Where(x => x.TenantID == tenantId && x.JobId == taskId).Count();
                return Json(new { isTasksExisted });
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ActiveDrillPlanController GetEmployeeList", User.Identity.Name);
                return Json(new { });
            }
        }

    }
}
