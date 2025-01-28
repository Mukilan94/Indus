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
using WellAI.Advisor.Areas.Identity;
using Newtonsoft.Json;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Tenant.Models;
using WellAI.Advisor.API.Repository;
using Microsoft.Extensions.Configuration;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    [SessionTimeOut]
    public class ServiceDashboardController : BaseController
    {
        private readonly TenantServiceDbContext _servicedb;
        private readonly ILogger<ServiceDashboardController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private readonly TenantOperatingDbContext _operdb;
        int Recommendations = 0;
        public ServiceDashboardController(TenantServiceDbContext servicedb, WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<ServiceDashboardController> logger,
            TenantOperatingDbContext operdb, IConfiguration Configuration) : base(userManager, dbContext)
        {
            _servicedb = servicedb;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _operdb = operdb;
            _configuration = Configuration;
        }
        public async Task getproviderrecord()
        {
            try
            {
                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                var providers = await operrepo.GetProviderDirectories();
                Recommendations = providers.Count(x => x.Approval == "Pending review");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard getproviderrecord", User.Identity.Name);
                _logger.LogInformation(ex.Message);
            }
        }
        public async Task<IActionResult> AdvisorWithDispatch()
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

            var Email = WellAIAppContext.Current.Session.GetString("Email");
            var UserDetails = await _userManager.FindByEmailAsync(Email);

            var profile = db.CrmUserBasicDetail.Where(x=>x.UserId == UserDetails.Id);

            if (_signInManager.IsSignedIn(User) == false)
            {
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
            return View("Advisor");
        }

        public async Task<IActionResult> ServiceIndex()
        {
            try
            {

                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }

                if (WellAIAppContext.Current.Session.GetString("AccountType1")=="2")//Dispatch
                {
                    return View("EmptyAdvisor");
                }

                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    ////checking invalid user//
                    //if (GetComponentsBasedOnRole() == false)
                    //{
                    //    string returnUrl = @"/Identity/Account/Login";
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                string[] providers = _servicedb.OperatingDirectory.Where(x => x.TenantId == ti.Id).Select(x => x.CompanyId).ToArray();
                var Servicerigs = db.rig_register.Where(x => providers.Contains(x.TenantID)).ToList();
                var TottalRigs = (from rigs in Servicerigs
                                  group rigs by rigs.Rig_id into g
                                  select new
                                  {
                                      TottalRigs = g.Count()
                                  }
                                );
                var operId = DLL.Constants.NoSpecificWellFilterKey;
                if (Request.Cookies.ContainsKey("operfilterlayout"))
                {
                    var operIdcooki = Request.Cookies["operfilterlayout"];
                    operId = string.IsNullOrEmpty(operIdcooki) ? DLL.Constants.NoSpecificWellFilterKey : operIdcooki.ToString();
                }
                var opernocheck = operId == DLL.Constants.NoSpecificWellFilterKey;
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var Auctiondata = new Model.ServiceCompany.Models.AuctionBidsModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));


                //Phase II changes - 02/16/2021
                var dbprefix = "serv";
                var servguid = new Guid(ti.Id);

                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));

                var servti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var servDBContext = new TenantServiceDbContext(servti);
                var servrepo = new ServiceTenantRepository(servDBContext, HttpContext, _userManager, db);

                //Phase II changes - 02/16/2021 - Passing ServiceTenantRepository
                Auctiondata = await auctionProposalBusiness.AuctionDashboardServiceStatus(ti.Id, operId, servrepo);


                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                var RigCounts = (from prj in db.Projects
                                 join opcmp in db.CorporateProfile on prj.OprTenantID equals opcmp.TenantId
                                 join wel in db.WellRegister on prj.WellID equals wel.well_id
                                 where wel.Prediction == true && prj.ServiceCompID == ti.Id && (prj.OprTenantID == operId && !opernocheck || opernocheck)
                                 select prj).Count();
                var projectDashboard = new WellAI.Advisor.Model.ServiceCompany.Models.ProjectDashboardSerViewModel();
                var projectBusiness = new ProjectBusiness(db, _userManager);
                projectDashboard = await projectBusiness.ProjectDashboardSerTenantId(ti.Id, operId);
                var ActivityList = (from project in db.Projects
                                    join ap in db.AuctionProposals on project.ProposalID equals ap.ProposalId
                                    where project.ServiceCompID == ti.Id && (ap.TenantID == operId && !opernocheck || opernocheck)
                                    select new Model.ServiceCompany.Models.ActivityViewModel
                                    {
                                        ProjectId = ap.ProposalId,
                                        ProjectStatus = project.ProjectStatus,
                                        ProjectStatusName = project.ProjectStatus == 0 ? "Upcoming" : "Ongoing"
                                    }).ToList();
                try
                {
                    List<OperatingProviderProfile> result = new List<OperatingProviderProfile>();
                    var operrepo2 = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    result = operrepo2.GetProviderDirectoriesByTenantId(ti.Id).Result;
                    ViewBag.Operators = result;
                    var dashboard = new ServiceDashboardModel
                    {
                        UpcomingAppoinment = ActivityList.Where(x => x.ProjectStatusName == "Upcoming").Count(),
                        Rigs = RigCounts,
                        AwardedBids = Auctiondata.AwardedBidsThisMonthCount,
                        OpenBidsVal = Auctiondata.ActiveBidsCount.ToString(),
                        UpcomingServices = 8,
                        Recommendations = db.UserActivityStatuses.Where(x => x.IsLoggedIn == true).Count(),
                        OpenFieldTickets = db.Projects.Where(x => x.ServiceCompID == ti.Id && x.ActualStart == null).Count(),
                    };
                    return View(dashboard);
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "ServiceDashBoard Index", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                    var dashboard = new ServiceDashboardModel();
                    return View(dashboard);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    ////checking invalid user//
                    //if (GetComponentsBasedOnRole() == false)
                    //{
                    //    string returnUrl = @"/Identity/Account/Login";
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                string[] providers = _servicedb.OperatingDirectory.Where(x => x.TenantId == ti.Id).Select(x => x.CompanyId).ToArray();
                var Servicerigs = db.rig_register.Where(x => providers.Contains(x.TenantID)).ToList();
                var TottalRigs = (from rigs in Servicerigs
                                  group rigs by rigs.Rig_id into g
                                  select new
                                  {
                                      TottalRigs = g.Count()
                                  }
                                );
                var operId = DLL.Constants.NoSpecificWellFilterKey;
                if (Request.Cookies.ContainsKey("operfilterlayout"))
                {
                    var operIdcooki = Request.Cookies["operfilterlayout"];
                    operId = string.IsNullOrEmpty(operIdcooki) ? DLL.Constants.NoSpecificWellFilterKey : operIdcooki.ToString();
                }
                var opernocheck = operId == DLL.Constants.NoSpecificWellFilterKey;
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var Auctiondata = new Model.ServiceCompany.Models.AuctionBidsModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));


                //Phase II changes - 02/16/2021
                var dbprefix = "serv";
                var servguid = new Guid(ti.Id);

                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));

                var servti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var servDBContext = new TenantServiceDbContext(servti);
                var servrepo = new ServiceTenantRepository(servDBContext, HttpContext, _userManager, db);

                //Phase II changes - 02/16/2021 - Passing ServiceTenantRepository
                Auctiondata = await auctionProposalBusiness.AuctionDashboardServiceStatus(ti.Id, operId, servrepo);


                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                var RigCounts = (from prj in db.Projects
                                 join opcmp in db.CorporateProfile on prj.OprTenantID equals opcmp.TenantId
                                 join wel in db.WellRegister on prj.WellID equals wel.well_id
                                 where wel.Prediction == true && prj.ServiceCompID == ti.Id && (prj.OprTenantID == operId && !opernocheck || opernocheck)
                                 select prj).Count();
                var projectDashboard = new WellAI.Advisor.Model.ServiceCompany.Models.ProjectDashboardSerViewModel();
                var projectBusiness = new ProjectBusiness(db, _userManager);
                projectDashboard = await projectBusiness.ProjectDashboardSerTenantId(ti.Id, operId);
                var ActivityList = (from project in db.Projects
                                    join ap in db.AuctionProposals on project.ProposalID equals ap.ProposalId
                                    where project.ServiceCompID == ti.Id && (ap.TenantID == operId && !opernocheck || opernocheck)
                                    select new Model.ServiceCompany.Models.ActivityViewModel
                                    {
                                        ProjectId = ap.ProposalId,
                                        ProjectStatus = project.ProjectStatus,
                                        ProjectStatusName = project.ProjectStatus == 0 ? "Upcoming" : "Ongoing"
                                    }).ToList();
                try
                {
                    List<OperatingProviderProfile> result = new List<OperatingProviderProfile>();
                    var operrepo2 = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    result = operrepo2.GetProviderDirectoriesByTenantId(ti.Id).Result;
                    ViewBag.Operators = result;
                    var dashboard = new ServiceDashboardModel
                    {
                        UpcomingAppoinment = ActivityList.Where(x => x.ProjectStatusName == "Upcoming").Count(),
                        Rigs = RigCounts,
                        AwardedBids = Auctiondata.AwardedBidsThisMonthCount,
                        OpenBidsVal = Auctiondata.ActiveBidsCount.ToString(),
                        UpcomingServices = 8,
                        Recommendations = db.UserActivityStatuses.Where(x => x.IsLoggedIn == true).Count(),
                        OpenFieldTickets = db.Projects.Where(x => x.ServiceCompID == ti.Id && x.ActualStart == null).Count(),
                    };
                    return View(dashboard);
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "ServiceDashBoard Index", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                    var dashboard = new ServiceDashboardModel();
                    return View(dashboard);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> Counts()
        {
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                string[] providers = _servicedb.OperatingDirectory.Where(x => x.TenantId == ti.Id).Select(x => x.CompanyId).ToArray();
                var Servicerigs = db.rig_register.Where(x => providers.Contains(x.TenantID)).ToList();
                var TottalRigs = (from rigs in Servicerigs
                                  group rigs by rigs.Rig_id into g
                                  select new
                                  {
                                      TottalRigs = g.Count()
                                  }
                                );
                var operId = DLL.Constants.NoSpecificWellFilterKey;
                if (Request.Cookies.ContainsKey("operfilterlayout"))
                {
                    var operIdcooki = Request.Cookies["operfilterlayout"];
                    operId = string.IsNullOrEmpty(operIdcooki) ? DLL.Constants.NoSpecificWellFilterKey : operIdcooki.ToString();
                }
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var Auctiondata = new Model.ServiceCompany.Models.AuctionBidsModel();
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));


                //Phase II changes - 02/16/2021
                var dbprefix = "serv";
                var servguid = new Guid(ti.Id);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var servti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var servDBContext = new TenantServiceDbContext(servti);
                var servrepo = new ServiceTenantRepository(servDBContext, HttpContext, _userManager, db);

                //Phase II changes - 02/16/2021 - Added ServiceTenantRepository
                Auctiondata = await auctionProposalBusiness.AuctionDashboardServiceStatus(ti.Id, operId, servrepo);
                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                var RigCounts = (from prj in db.Projects
                                 join opcmp in db.CorporateProfile on prj.OprTenantID equals opcmp.TenantId
                                 join wel in db.WellRegister on prj.WellID equals wel.well_id
                                 where prj.ServiceCompID == ti.Id
                                 select prj).Count();
                var projectDashboard = new WellAI.Advisor.Model.ServiceCompany.Models.ProjectDashboardSerViewModel();
                var projectBusiness = new ProjectBusiness(db, _userManager);
                projectDashboard = await projectBusiness.ProjectDashboardSerTenantId(ti.Id, operId);
                var ActivityList = (from project in db.Projects
                                    join ap in db.AuctionProposals on project.ProposalID equals ap.ProposalId
                                    where project.ServiceCompID == ti.Id
                                    select new Model.ServiceCompany.Models.ActivityViewModel
                                    {
                                        ProjectId = ap.ProposalId,
                                        ProjectStatus = project.ProjectStatus,
                                        ProjectStatusName = project.ProjectStatus == 0 ? "Upcoming" : "Ongoing"
                                    }).ToList();
                List<OperatingProviderProfile> result = new List<OperatingProviderProfile>();
                var operrepo2 = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                result = operrepo2.GetProviderDirectoriesByTenantId(ti.Id).Result;
                ViewBag.Operators = result;
                var dashboard = new ServiceDashboardModel
                {
                    UpcomingAppoinment = ActivityList.Where(x => x.ProjectStatusName == "Upcoming").Count(),
                    Rigs = RigCounts,
                    AwardedBids = Auctiondata == null ? 0 : Auctiondata.AwardedBidsThisMonthCount,
                    OpenBidsVal = Auctiondata == null ? "0" : Auctiondata.ActiveBidsCount.ToString(),
                    UpcomingServices = 8,
                    Recommendations = db.UserActivityStatuses.Where(x => x.IsLoggedIn == true).Count(),
                    OpenFieldTickets = db.Projects.Where(x => x.ServiceCompID == ti.Id && x.ActualStart == null).Count(),
                };
                return Json(dashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard Count", User.Identity.Name);
                return null;
            }
        }
        public IActionResult Error()
        {
            return View();
        }
        public async Task<string> GetuserTenantidAsync()
        {
            WellIdentityUser appUser = await _userManager.GetUserAsync(User);
            return appUser.TenantId;
        }
        public async Task<ActionResult> CorporateProfileData()
        {
            try
            {
                string TenantID = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var result = db.CorporateProfile.Where(x => x.TenantId == TenantID).FirstOrDefault();
                if (result.LogoPath != null)
                {
                    var LogoPath = await GetUrlOfImage(result.LogoPath);
                    return Json(LogoPath);
                }
                else
                {
                    return Json("");
                }

            }
            catch (Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                var userIdentity = (ClaimsIdentity)User.Identity;
                errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/OperatingDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<ActionResult> CorporateProfileDataByUserId()
        {
            try
            {
                //string TenantID = HttpContext.GetMultiTenantContext().TenantInfo.Id;

                //var claim = (ClaimsIdentity)User.Identity;
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                var userId = claims?.FirstOrDefault(x => x.Type.Equals("nameidentifier", StringComparison.OrdinalIgnoreCase))?.Value;

        
                var userDetail = db.CrmUserBasicDetail.Where(x => x.UserId == "").FirstOrDefault();

                var result = db.CorporateProfile.Where(x => x.ID == userDetail.CorporateProfileId).FirstOrDefault();
                if (result.LogoPath != null)
                {
                    var LogoPath = await GetUrlOfImage(result.LogoPath);
                    return Json(LogoPath);
                }
                else
                {
                    return Json("");
                }

            }
            catch (Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                var userIdentity = (ClaimsIdentity)User.Identity;
                errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/OperatingDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        private async Task<string> GetUrlOfImage(string filename)
        {
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                var blobSection = _configuration.GetSection("AzureBlob");
                var folderName = _configuration.GetSection("FolderName");

                var items = await AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], ti.Id, folderName["CompanyProfile"], filename);
                return items;

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard GetUrlOfImage", User.Identity.Name);
                return string.Empty;
            }
        }
        public async Task<IActionResult> GetDepthChartActionAsync([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                var result = await GetStagingDataAsync(operId);
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetTimeChartActionAsync([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                var data = await GetTimeDataAsync(operId);
                return Json(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
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
        private async Task<List<ServiceRig>> GetDepthDataAsync(string operId)
        {
            var chartdata = new List<ServiceRig>();
            try
            {
                var opernocheck = operId == DLL.Constants.NoSpecificWellFilterKey;
                var GeneralDepthBaseddata = (from wd in db.WellDepthDataStages
                                             where wd.TENANTID == operId && !opernocheck || opernocheck
                                             group wd by new { wd.WID } into g
                                             select new
                                             {
                                                 WID = g.Key.WID,
                                                 DMEA = g.Max(wd => wd.DMEA.HasValue ? wd.DMEA.Value : 0.0)
                                             }).ToList();
                var wellRegResult = db.WellRegister.Where(x => x.customer_id == operId && x.Prediction == true && !opernocheck || opernocheck)
                    .Select(x => new
                    {
                        x.well_id,
                        x.wellname,
                        x.RigID,
                        x.ChartColor,
                        x.Prediction
                    }).ToList();

                var rigRegister = _servicedb.subscriptionOperatorRigs.Where(x => x.CompanyId == operId && !opernocheck || opernocheck).ToList();
                chartdata = (from well in wellRegResult
                             join Dril in GeneralDepthBaseddata on well.well_id equals Dril.WID
                             join RigReg in db.rig_register on well.RigID equals RigReg.Rig_id
                             join rig in rigRegister on well.RigID equals rig.RigId
                             where RigReg.isActive == true && well.Prediction == true
                             select new ServiceRig
                             {
                                 Value = Dril.DMEA,
                                 Category = RigReg.Rig_Name,
                                 Color = (well.ChartColor != null) ? well.ChartColor : "#0257b1",
                                 OperatorTenantId = operId,
                                 WellId = well.well_id,
                                 WellName = well.wellname,
                                 RigId = well.RigID
                             }).OrderBy(o => o.Category).ThenBy(o => o.WellName).Distinct().ToList();
                return await Task.FromResult(chartdata);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard GetDepthDataAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        private async Task<List<ServiceRig>> GetTimeDataAsync(string operId)
        {
            try
            {
                var timeData = new List<ServiceRig>();
                var opernocheck = operId == DLL.Constants.NoSpecificWellFilterKey;
                var GeneralTimeBaseddata = (from wd in db.WellDepthDataStages
                                            where wd.TENANTID == operId && !opernocheck || opernocheck
                                            group wd by new { wd.WID } into g
                                            select new
                                            {
                                                WID = g.Key.WID,
                                                DATD = g.Max(wd => wd.DATE)
                                            }).ToList();
                var wellRegResult = db.WellRegister.Where(x => x.customer_id == operId && x.Prediction == true && !opernocheck || opernocheck)
                    .Select(x => new {
                        x.well_id,
                        CreatedDate = Convert.ToInt32(x.CreatedDate.Date.ToString("yyMMdd")),
                        x.wellname,
                        x.RigID,
                        x.ChartColor
                    }).ToList();

                //Phase II changes - 02/10/2021 - Filter the Rigs from Subscribed Rigs
                var rigRegister = _servicedb.subscriptionOperatorRigs.Where(x => x.CompanyId == operId && !opernocheck || opernocheck).ToList();
                var wellreg = (from wg in wellRegResult
                               join wt in GeneralTimeBaseddata on wg.well_id equals wt.WID
                               join rig in rigRegister on wg.RigID equals rig.RigId
                               join rg in db.rig_register on rig.RigId equals rg.Rig_id
                               where rg.isActive = true
                               select new
                               {
                                   wg.well_id,
                                   wg.wellname,
                                   wg.ChartColor,
                                   Value2 = ((int)wt.DATD - wg.CreatedDate),
                                   rig.RigId,
                                   rg.Rig_Name
                               }).ToList();

                timeData = (from well in wellreg
                            select new ServiceRig
                            {
                                Value2 = well.Value2,
                                Category = well.Rig_Name,
                                Color = (well.ChartColor != null) ? well.ChartColor : "#0257b1",
                                OperatorTenantId = operId,
                                WellId = well.well_id,
                                WellName = well.wellname
                            }).OrderBy(o => o.Category).ThenBy(o => o.WellName).Distinct().ToList();
                return await Task.FromResult(timeData);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard GetTimeDataAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Phase II Changes - 02/10/2021 - Staging Chart Data for Service Dashboard
        /// </summary>
        /// <param name="operId"></param>
        /// <returns></returns>
        private async Task<List<ServiceRig>> GetStagingDataAsync(string operId)
        {
            var chartdata = new List<ServiceRig>();
            try
            {
                var opernocheck = operId == DLL.Constants.NoSpecificWellFilterKey;
                var GeneralDepthBaseddata = (from wd in db.WellDepthDataStages
                                             where wd.TENANTID == operId && !opernocheck || opernocheck
                                             group wd by new { wd.WID } into g
                                             select new
                                             {
                                                 WID = g.Key.WID,
                                                 DMEA = g.Max(wd => wd.DMEA.HasValue ? wd.DMEA.Value : 0.0)
                                             }).ToList();

                var wellRegister = db.WellRegister.Where(x => x.customer_id == operId && x.Prediction == true && !opernocheck || opernocheck)
                    .Select(x => new
                    {
                        x.well_id,
                        x.wellname,
                        x.RigID,
                        x.ChartColor,
                        x.Prediction
                    }).ToList();

                var subscribedRigs = _servicedb.subscriptionOperatorRigs.Where(x => x.CompanyId == operId && !opernocheck || opernocheck).ToList();


                List<StagingTasksModel> stagingTasksList = new List<StagingTasksModel>();

                //getting last stage of all the subscribed rigs and wells
                if (operId != "00000000-0000-0000-0000-000000000000")
                {
                    //DWOP - Task data from DrillPlanDetails
                    stagingTasksList = (from prj in db.Projects
                                        join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                                        join task in db.DrillPlanDetails on au.JobId equals task.TaskId
                                        join stg in db.Stages on task.StageId equals stg.Id
                                        where prj.OprTenantID == operId
                                        select new StagingTasksModel()
                                        {
                                            Stage = stg.Name,
                                            Day = task.Day,
                                            EndDate = prj.ActualEnd,
                                            RunningDate = prj.ActualStart,
                                            Task = task.TaskName,
                                            ServiceCategory = "",
                                            WellId = prj.WellID,
                                            StageId = Convert.ToInt32(stg.Name.ToString().Substring(0, stg.Name.IndexOf(":") - 1))
                                        }).OrderBy(x => x.RunningDate).ToList();

                    //from prj in db.DrillPlanDetails
                    //join plnwells in db.DrillPlanWells on new { prj.DrillPlanId, prj.DrillPlanWellsId } equals new { plnwells.DrillPlanId, plnwells.DrillPlanWellsId }
                    //join stg in db.Stages on prj.StageId equals stg.Id
                    //join catg in db.serviceCategories on prj.CategoryId equals catg.ServiceCategoryId
                    //where prj.PlanStartDate != null && plnwells.Wellid.Equals(wellId)

                    //stagingTasksList = (from prj in db.DrillPlanDetails
                    //                    join plnwells in db.DrillPlanWells on new { prj.DrillPlanId, prj.DrillPlanWellsId } equals new { plnwells.DrillPlanId, plnwells.DrillPlanWellsId }
                    //                    join plnhdr in db.DrillPlanHeader on new { prj.DrillPlanId } equals new { plnhdr.DrillPlanId }
                    //                    join stg in db.Stages on prj.StageId equals stg.Id
                    //                    where plnhdr.TenantId == operId
                    //                    select new StagingTasksModel()
                    //                    {
                    //                        Stage = stg.Name,
                    //                        Day = prj.Day,
                    //                        EndDate = prj.PlanFinishedDate,
                    //                        RunningDate = prj.ActualStartDate,
                    //                        Task = prj.TaskName,
                    //                        ServiceCategory = "",
                    //                        WellId = plnwells.Wellid,
                    //                        StageId = Convert.ToInt32(stg.Name.ToString().Substring(0, stg.Name.IndexOf(":") - 1))
                    //                    }).OrderBy(x => x.RunningDate).ToList();
                }
                else
                {
                    //DWOP - Task data from DrillPlanDetails
                    stagingTasksList = (from prj in db.Projects
                                        join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                                        join task in db.DrillPlanDetails on au.JobId equals task.TaskId
                                        join stg in db.Stages on task.StageId equals stg.Id
                                        select new StagingTasksModel()
                                        {
                                            Stage = stg.Name,
                                            Day = task.Day,
                                            EndDate = prj.ActualEnd,
                                            RunningDate = prj.ActualStart,
                                            Task = task.TaskName,
                                            ServiceCategory = "",
                                            WellId = prj.WellID,
                                            StageId = Convert.ToInt32(stg.Name.ToString().Substring(0, stg.Name.IndexOf(":") - 1))
                                        }).OrderBy(x => x.RunningDate).ToList();
                }


                var stagingDataSet1 = stagingTasksList.GroupBy(r => new { r.WellId })
                                        .Select(grp => new
                                        {
                                            StageNo = grp.Max(t => t.StageId),
                                            StartDate = grp.Min(t => t.RunningDate),
                                            WellId = grp.Key.WellId
                                        }).ToList();

                chartdata = (from well in wellRegister
                             join rigs in db.rig_register on well.RigID equals rigs.Rig_id
                             join subsrigs in subscribedRigs on well.RigID equals subsrigs.RigId
                             join stage in stagingDataSet1 on well.well_id equals stage.WellId
                             where rigs.isActive == true && well.Prediction == true
                             select new ServiceRig
                             {
                                 Value = stage.StageNo,
                                 Category = rigs.Rig_Name,
                                 Color = (well.ChartColor != null) ? well.ChartColor : "#0257b1",
                                 OperatorTenantId = operId,
                                 WellId = well.well_id,
                                 WellName = well.wellname,
                                 RigId = well.RigID
                             }).OrderBy(o => o.Category).ThenBy(o => o.WellName).Distinct().ToList();


                return await Task.FromResult(chartdata);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ServiceDashBoard GetStagingDataAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return chartdata;
            }
        }
    }
}