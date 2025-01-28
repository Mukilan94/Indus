using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Entity;
using System.Data.Entity;
using Kendo.Mvc.Extensions;
using static WellAI.Advisor.BLL.Business.AuctionProposalBusiness;
using Finbuckle.MultiTenant;
using Newtonsoft.Json;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
 
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        public DashboardController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<DashboardController> logger)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
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
                //checking invalid user//
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                var mapdata = GetWellMap();
                var dashboard = new DashboardModel
                {
                    Accounts = 280,
                    OnlineContacts = 2,
                    LastContact = "Jane Smith",
                    ScheduledAppointments = 1,
                    UpcomingServices = UpcomingProjectCount(),
                    BidsWon = BidsWonCount(),
                    OpenFieldTickets = OpenFieldTicket(),
                    Map = mapdata
                };
                return View(dashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard SRV", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public int OpenFieldTicket()
        {            
            string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int Opencount = db.hdIssues.Count(c => c.UserID == userId && c.StatusID == 4);
            return Opencount;
        }
        public int BidsWonCount()
        {
            string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
            DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);        
            var result = (from project in db.Projects
                          where project.OprTenantID == tenantid
                          && (project.DateCreated >= LastMonthFirstDate && project.DateCreated <= DateTime.Now)
                          select new
                          {
                              project.ID,
                              project.ProjectStatus,
                              ProjectStartDate = project.ActualStart.Value,
                              ProjectEndDate = project.ActualEnd.Value,
                              CreatedDate = project.DateCreated,
                              Months = Convert.ToDateTime(project.ActualStart).Month,
                              CreatedMonths = Convert.ToDateTime(project.DateCreated).Month,
                              project.WellID
                          }).ToList();
            var projectsawardedThisMonth = (from r in result where r.ProjectStatus == (int)ProjectStatusList.CloseProject && r.ProjectEndDate != null && r.Months == DateTime.Now.Month group r by r.ProjectEndDate.Month into rg select new { MonthCount = rg.Count() }).FirstOrDefault();
            return projectsawardedThisMonth == null ? 0 : Convert.ToInt32(projectsawardedThisMonth.MonthCount);
        }
        public int UpcomingProjectCount()
        {
            try
            {
                DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
                DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);
                string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var result = (from project in db.Projects
                              where project.OprTenantID == tenantid
                              && (project.DateCreated >= LastMonthFirstDate && project.DateCreated <= DateTime.Now)
                              select new
                              {
                                  project.ID,
                                  project.ProjectStatus,
                                  ProjectStartDate = project.ActualStart.Value,
                                  ProjectEndDate = project.ActualEnd.Value,
                                  CreatedDate = project.DateCreated,
                                  Months = Convert.ToDateTime(project.ActualStart).Month,
                                  CreatedMonths = Convert.ToDateTime(project.DateCreated).Month,
                                  project.WellID
                              }).ToList();         
                var ProjectsActive = (from r in result where r.ProjectStatus == (int)ProjectStatusList.OnGoingProjects group r by r.ProjectStatus into rg select new { MonthCount = rg.Count() }).FirstOrDefault();
                return ProjectsActive == null ? 0 : ProjectsActive.MonthCount;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard UpcomingProjectCount", User.Identity.Name);
                return 0;
            }
        }
        public WellAI.Advisor.Model.OperatingCompany.Models.Map GetWellMap()
        {
            var mapdata = new WellAI.Advisor.Model.OperatingCompany.Models.Map();
            var markersList = new Marker();
            mapdata.CenterLatitude = 30.268107;
            mapdata.CenterLongitude = -97.744821;
            mapdata.Markers = GetMarkersList();
            return mapdata;
        }
        private IEnumerable<Marker> GetMarkersList()
        {
            try
            {
                var markersList = new Marker();
                List<WellRegister> WellRegister = db.WellRegister.ToList();
                var Location = (from wr in WellRegister
                                select new Marker
                                {
                                    latlng = new double[] { wr.Latitude, wr.Longitude },
                                    Name = wr.Country
                                }
                               ).ToList();
                return Location;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard GetMarkersList", User.Identity.Name);
                return null;
            }
        }
        public IActionResult Slot_Read()
        {
            try
            {
                List<AIPredictiveTasks> PredictiveTasks = db.AIPredictiveTasks.ToList();
                var predictivetasks = (from pr in PredictiveTasks
                                       select new PredectiveSchedule
                                       {
                                           Title = null,
                                           OwnerID = 2,
                                           TaskID = 4,
                                           Start = System.DateTime.Now,
                                           StartTimezone = null,
                                           End = System.DateTime.Now,
                                           EndTimezone = null,
                                           Description = "1",
                                           RecurrenceRule = null,
                                           RecurrenceException = null,
                                           RecurrenceID = null,
                                           IsAllDay = true,
                                       }
              ).ToList();
                return Json(predictivetasks);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard Slot_Read", User.Identity.Name);
                return null;
            }
        }
        public ActionResult GetWellAIStatusData()
        {
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                IEnumerable<OperatingWellAIStatusViewModel> aIWellData;
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                aIWellData = AIBusiness.GetWellAIStatusChartDataForOpr(tenantId.Result);
                return Json(aIWellData.ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard GetWellAIStatusData", User.Identity.Name);
                return null;
            }
        }
        public ActionResult GetWellAIRAWStatusData()
        {
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                IEnumerable<OperatingWellAIRAWStatusViewModel> aIWellData;
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                aIWellData = AIBusiness.GetWellAIRAWStatusChartDataForOpr(tenantId.Result);
                return Json(aIWellData.ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard GetWellAIRAWStatusData", User.Identity.Name);
                return null;
            }
        }
       public virtual JsonResult Event_Selection_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetAll().ToDataSourceResult(request));
        }
        public virtual IQueryable<TaskViewModel> GetAll()
        {
            return GetAllTasks().AsQueryable();
        }
        private IList<TaskViewModel> GetAllTasks()
        {
            try
            {
                var TaskList = db.AIPredictiveTasks.ToList();
                List<AIPredictiveTasks> PredictiveTasks = db.AIPredictiveTasks.ToList();
                var PredictiveTasks1 = TaskList.GroupBy(x => new { x.taskname, x.welltask_id, x.day, x.ActionDate, x.StartTime })
                                                                   .Select(y => new AIPredictiveTasks()
                                                                   {
                                                                       taskname = y.Key.taskname,
                                                                       welltask_id = y.Key.welltask_id,
                                                                       day = y.Key.day,
                                                                       ActionDate = y.Key.ActionDate,
                                                                       StartTime = y.Key.StartTime
                                                                   }
                                                                   ).ToList();
                var taskcount = (from pr in PredictiveTasks
                                 group pr by pr.ActionDate into g
                                 select new
                                 {
                                     ActionDate = g.Key,
                                     eventcount = g.Count(),
                                 }
                                ).ToList();
                List<TaskViewModel> result = new List<TaskViewModel>();
                int i = 0;
                foreach (var item in taskcount)
                {
                    TaskViewModel tasks = new TaskViewModel();
                    foreach (var data in PredictiveTasks)
                    {
                        if (item.ActionDate == data.ActionDate)
                        {
                            if (i == 0)
                            {
                                i = i + 1;
                                tasks.TaskID = data.welltask_id;
                                tasks.Title = "";
                                tasks.Start = DateTime.SpecifyKind(data.StartTime, DateTimeKind.Utc);
                                tasks.End = DateTime.SpecifyKind(data.StartTime, DateTimeKind.Utc);
                                tasks.StartTimezone = null;
                                tasks.EndTimezone = null;
                                tasks.Description = "";
                                tasks.IsAllDay = false;
                                tasks.RecurrenceRule = "";
                                tasks.RecurrenceException = "";
                                tasks.RecurrenceID = null;
                                tasks.OwnerID = (item.eventcount) > 1 && (item.eventcount) < 10 ? 1 :
                                                   ((item.eventcount) > 10 && (item.eventcount) < 20) ? 2 :
                                                   ((item.eventcount) > 20 && (item.eventcount) < 30) ? 3 :
                                                  ((item.eventcount) > 30 && (item.eventcount) < 40) ? 4 :
                                                 ((item.eventcount) > 40 && (item.eventcount) < 50) ? 5 :
                                                  ((item.eventcount) > 50 && (item.eventcount) < 60) ? 6 :
                                                  ((item.eventcount) > 60 && (item.eventcount) < 70) ? 7 :
                                                  ((item.eventcount) > 70 && (item.eventcount) < 80) ? 8 :
                                                    ((item.eventcount) > 80 && (item.eventcount) < 90) ? 9 : 10;
                                result.Add(tasks);
                            }
                        }
                    }
                    i = 0;
                }
                var tasklist = (from pr in PredictiveTasks
                                join subresult in taskcount on pr.ActionDate equals subresult.ActionDate //into j1
                                select new TaskViewModel
                                {
                                    TaskID = pr.welltask_id,
                                    Title = pr.taskname,
                                    Start = DateTime.SpecifyKind(pr.StartTime, DateTimeKind.Utc),
                                    End = DateTime.SpecifyKind(pr.ScheduleDate, DateTimeKind.Utc).AddDays(pr.day),
                                    StartTimezone = null,
                                    EndTimezone = null,
                                    Description = pr.taskname,
                                    IsAllDay = false,
                                    RecurrenceRule = "",
                                    RecurrenceException = "",
                                    RecurrenceID = null,
                                    OwnerID = (subresult.eventcount) > 1 && (subresult.eventcount) < 10 ? 1 :
                                               ((subresult.eventcount) > 10 && (subresult.eventcount) < 20) ? 2 :
                                               ((subresult.eventcount) > 20 && (subresult.eventcount) < 30) ? 3 :
                                              ((subresult.eventcount) > 30 && (subresult.eventcount) < 40) ? 4 :
                                             ((subresult.eventcount) > 40 && (subresult.eventcount) < 50) ? 5 :
                                              ((subresult.eventcount) > 50 && (subresult.eventcount) < 60) ? 6 :
                                              ((subresult.eventcount) > 60 && (subresult.eventcount) < 70) ? 7 :
                                              ((subresult.eventcount) > 70 && (subresult.eventcount) < 80) ? 8 :
                                                ((subresult.eventcount) > 80 && (subresult.eventcount) < 90) ? 9 : 10
                                }
            ).ToList();
                return result.ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard GetAllTasks", User.Identity.Name);
                return null;
            }
       }
        public ActionResult BasicUsage_PopulationUSA()
        {
            return Json(PopulationUSAData());
        }
        public List<OperatingTreeMap> PopulationUSAData()
        {
            List<OperatingTreeMap> result = new List<OperatingTreeMap>();
            List<ServiceTypeHead> head = db.ServiceTypeHead.ToList();
            List<ServiceTypeDetail> detail = db.ServiceTypeDetail.ToList();
            OperatingTreeMap usa = new OperatingTreeMap("Well Tasks", 316128839, new List<OperatingTreeMap>());
            result.Add(usa);
            foreach (var item in head)
            {
                OperatingTreeMap alabama = new OperatingTreeMap(item.servicetype, item.servicetypecount, new List<OperatingTreeMap>());
                usa.Items.Add(alabama);
                foreach (var det in detail)
                {
                    if (item.parent_id == det.parent_id)
                    {
                        alabama.Items.Add(new OperatingTreeMap(det.subservicetype, det.servicetypecount, null));
                    }
                }
            }
            return result;
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult GetDepthChartAction([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                var data = GetDepthData(operId);
                return Json(data);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard GetDepthChartAction", User.Identity.Name);
                return null;
            }
        }
        public IActionResult GetTimeChartAction([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                var data = GetTimeData(operId);
                return Json(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard GetTimeChartAction", User.Identity.Name);
                return null;
            }
        }
        private bool GetComponentsBasedOnRole()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            string roleName = roles.FirstOrDefault().Value;
            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRoleByName(roleName);
            return rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, "ViewDashboard");
        }
        public List<Rig> GetDepthData(string operId)
        {
            var depthData = new List<Rig> {
                    new Rig { Value = 12500, Category = "Rig 1", Color = "#007BFF" },
                    new Rig { Value = 5000, Category = "Rig 2", Color = "#26DDCC" },
                    new Rig { Value = 19500, Category = "Rig 3", Color = "#3639A4" },
                    new Rig { Value = 15500, Category = "Rig 4", Color = "#F4AF00" },
                    new Rig { Value = 9000, Category = "Rig 5", Color = "#FF6344" },
                    new Rig { Value = 12000, Category = "Rig 6", Color = "#77BD27" },
                    new Rig { Value = 20000, Category = "Rig 7", Color = "#0422A1" }
                };
            return depthData;
        }
        public List<Rig> GetTimeData(string operId)
        {
            var timeData = new List<Rig> {
                    new Rig { Value = 23, Category = "Rig 1", Color = "#007BFF" },
                    new Rig { Value = 10, Category = "Rig 2", Color = "#26DDCC" },
                    new Rig { Value = 27, Category = "Rig 3", Color = "#3639A4" },
                    new Rig { Value = 25, Category = "Rig 4", Color = "#F4AF00" },
                    new Rig { Value = 12, Category = "Rig 5", Color = "#FF6344" },
                    new Rig { Value = 19, Category = "Rig 6", Color = "#77BD27" },
                    new Rig { Value = 28, Category = "Rig 7", Color = "#0422A1" }
                };
            return timeData;
        }

        /// <summary>
        /// Returns wells for dropdown list
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetWells()
        {
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                        WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wells = await AIBusiness.GetWellMaster(Guid.Empty.ToString("D"), userwell);
                var result = new List<WellListItem>();
                foreach (var well in wells)
                {
                    result.Add(new WellListItem
                    {
                        WellId = well.wellId,
                        Title = well.wellName
                    });
                }
                return Json(result);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard GetWells", User.Identity.Name);
                return null;
            }
        }
    }
}
