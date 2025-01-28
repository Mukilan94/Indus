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
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.DLL.Entity;
using System.Data.Entity;
using Kendo.Mvc.Extensions;
using static WellAI.Advisor.BLL.Business.AuctionProposalBusiness;
using Finbuckle.MultiTenant;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class DashboardSRVController : BaseController
    {
        private readonly ILogger<DashboardSRVController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        public DashboardSRVController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager, 
                                      RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<DashboardSRVController> logger)
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
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                var mapdata = GetWellMap();
                var dashboard = new DashboardSRVModel
                {
                    Accounts = 280,
                    OnlineContacts = 2,
                    LastContact = "Jane Smith",
                    ScheduledAppointments = 1,
                    UpcomingServices = UpcomingProjectCount(),                    
                    BidsWon= BidsWonCount(),               
                    OpenFieldTickets = OpenFieldTicket(),
                    DepthData = GetDepthData(),
                    TimeData = GetTimeData(),
                    Map = mapdata
                };
               return View(dashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard SRV", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }

      
        public int OpenFieldTicket()
        {
            string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
            int openticket = db.hdIssues.Count(c => c.StatusID == 4 && c.TenantId.Equals(tenantid));
            return openticket;
       }
        public int BidsWonCount()
        {
            string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
            DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);
            var result = (from auproject in db.AuctionProjects
                          join ap in db.AuctionProposals on auproject.ProposalId equals ap.ProposalId
                          join ab in db.AuctionBids on auproject.ProposalId equals ab.ProposalId
                          where (int)ab.BidStatus == (int)AuBidStatus.Accepted && auproject.ServiceTenantID == tenantid
                          && (ap.ProjectStartDate >= LastMonthFirstDate && ap.ProjectStartDate <= DateTime.Now)
                          select new { auproject.Id, ProjectDateTime = Convert.ToDateTime(ap.ProjectStartDate), Months = Convert.ToDateTime(ap.ProjectStartDate).Month, ab.BidAmount, ap.ProjectDuration }).ToList();
            var awardedBids = (from r in result where r.Months == DateTime.Now.Month group r by r.ProjectDateTime.Month into rg select new { MonthCount = rg.Count(), MonthValue = rg.Sum(x => x.BidAmount) }).FirstOrDefault();
            return awardedBids.MonthCount;
        }
        public int UpcomingProjectCount()
        {
            DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);
            string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
            var result = (from project in db.Projects
                          where project.ServiceCompID == tenantid
                          && (project.DateCreated >= LastMonthFirstDate && project.DateCreated <= DateTime.Now)
                          select new { project.ID, project.ProjectStatus, ProjectStartDate = project.ActualStart.Value, ProjectEndDate = project.ActualEnd.Value, CreatedDate = project.DateCreated, Months = Convert.ToDateTime(project.ActualStart).Month, CreatedMonths = Convert.ToDateTime(project.DateCreated).Month }).ToList();        
            var ProjectsActive = (from r in result where r.ProjectStatus == (int)ProjectStatusList.OnGoingProjects group r by r.ProjectStatus into rg select new { MonthCount = rg.Count() }).FirstOrDefault();
            return ProjectsActive.MonthCount;
        }
        public WellAI.Advisor.Model.ServiceCompany.Models.Map GetWellMap()
        {
           var mapdata = new WellAI.Advisor.Model.ServiceCompany.Models.Map();
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
                customErrorHandler.WriteError(ex, "DashboardSRV GetMarkersList", User.Identity.Name);
                return null;
            }
        }
        public ActionResult GetWellAIStatusData()
        {
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                IEnumerable<WellAIStatusViewModel> aIWellData;
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                aIWellData = AIBusiness.GetWellAIStatusChartData(tenantId.Result);
                return Json(aIWellData.ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DashboardSRV GetMarkersList", User.Identity.Name);
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
                customErrorHandler.WriteError(ex, "DashboardSRV GetMarkersList", User.Identity.Name);
                return null;
            }
        }
        public ActionResult GetWellAIRAWStatusData()
        {
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                IEnumerable<WellAIRAWStatusViewModel> aIWellData;
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                aIWellData = AIBusiness.GetWellAIRAWStatusChartData(tenantId.Result);
                return Json(aIWellData.ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DashboardSRV GetWellAIRAWStatusData", User.Identity.Name);
                return null;
            }
        }
    
        public DashboardSRVModel GetProviderDirectory()
        {
            var providerdir = new DashboardSRVModel
            {
                Accounts = 200,
                OnlineContacts = 2,
                LastContact = "Jane Smith",
                ScheduledAppointments = 1,
                UpcomingServices = 8,
                BidsWon = 3,
                OpenFieldTickets = 18,
                DepthData = GetDepthData(),
                TimeData = GetTimeData()
            };
         return providerdir;
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult GetDepthChartAction([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                var data = GetDepthData();
                return Json(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DashboardSRV GetDepthChartAction", User.Identity.Name);
                return null;
            }
        }

        /// <summary>
        /// Phase II Changes - 02/10/2021 - Dashboard Staging Chart for the Subscribed Rigs
        /// </summary>
        /// <param name="request"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        public IActionResult GetDashboardStagingChart([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                var data = GetDepthData();
                return Json(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DashboardSRV GetDashboardStagingChart", User.Identity.Name);
                return null;
            }
        }

        public IActionResult GetTimeChartAction([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                var data = GetTimeData();

                return Json(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DashboardSRV GetDashboardStagingChart", User.Identity.Name);
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
            ViewBag.RoleName = roleName;
            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRoleByName(roleName);
            return rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, "ViewDashboard");
        }
        public List<Rig> GetDepthData()
        {
            var depthData = new List<Rig> {
                    new Rig { Value = 16500, Name = "Rig 1", Color = "#007BFF" },
                    new Rig { Value = 15000, Name = "Rig 2", Color = "#26DDCC" },
                    new Rig { Value = 19500, Name = "Rig 3", Color = "#3639A4" },
                    new Rig { Value = 15500, Name = "Rig 4", Color = "#F4AF00" },
                    new Rig { Value = 9000,  Name = "Rig 5", Color = "#FF6344" },
                    new Rig { Value = 12000, Name = "Rig 6", Color = "#77BD27" },
                    new Rig { Value = 21000, Name = "Rig 7", Color = "#0422A1" }
                };

            return depthData;
        }
        public List<Rig> GetTimeData()
        {
            var timeData = new List<Rig> {
                    new Rig { Value = 24, Name = "Rig 1", Color = "#007BFF" },
                    new Rig { Value = 10, Name = "Rig 2", Color = "#26DDCC" },
                    new Rig { Value = 27, Name = "Rig 3", Color = "#3639A4" },
                    new Rig { Value = 25, Name = "Rig 4", Color = "#F4AF00" },
                    new Rig { Value = 12, Name = "Rig 5", Color = "#FF6344" },
                    new Rig { Value = 19, Name = "Rig 6", Color = "#77BD27" },
                    new Rig { Value = 30, Name = "Rig 7", Color = "#0422A1" }
                };

            return timeData;
        }
        public IActionResult Depthtimechart(string id)
        {
            CompanyDepthRigModel Depthtimechart = new CompanyDepthRigModel();
            try
            {
                var dir = GetProviderDirectory();
                Depthtimechart = dir.CompanyDepthRigModel.FirstOrDefault(x => x.RigID == id);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DashboardSRV Depthtimechart", User.Identity.Name);

                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
                      return View(Depthtimechart);
        }
        public IActionResult TimeDateProfile(string id)
        {
            Rig TimeDateProfile = new Rig();
            try
            {
                var TimeDate = GetTimeData();
                TimeDateProfile = TimeDate.FirstOrDefault(x => x.Name == id);
                if (TimeDateProfile == null)
                {
                    TimeDateProfile = TimeDate.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "DashboardSRV TimeDateProfile", User.Identity.Name);

                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return View(TimeDateProfile);
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
                customErrorHandler.WriteError(ex, "DashboardSRV GetAllTasks", User.Identity.Name);
                return null;
            }
            
        }
        public ActionResult BasicUsage_PopulationUSA()
        {
            return Json(PopulationUSAData());
        }
        public List<PopulationUSA> PopulationUSAData()
        {
            List<PopulationUSA> result = new List<PopulationUSA>();
            List<ServiceTypeHead> head = db.ServiceTypeHead.ToList();
            List<ServiceTypeDetail> detail = db.ServiceTypeDetail.ToList();
            PopulationUSA usa = new PopulationUSA("Well Tasks", 316128839, new List<PopulationUSA>());
            result.Add(usa);
            foreach (var item in head)
            {
                PopulationUSA alabama = new PopulationUSA(item.servicetype, item.servicetypecount, new List<PopulationUSA>());
                usa.Items.Add(alabama);
                foreach (var det in detail)
                {
                    if (item.parent_id == det.parent_id)
                    {
                        alabama.Items.Add(new PopulationUSA(det.subservicetype, det.servicetypecount, null));
                    }
                }
            }
            return result;
        }
    }
}
