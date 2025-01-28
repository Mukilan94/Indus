using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Repository;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Well_AI.Advisor.Log.Error;
using Twilio.AspNet.Core;
using Microsoft.Extensions.Configuration;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class OperatingDashboardController : BaseController
    {
        private readonly ILogger<OperatingDashboardController> _logger;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private readonly TenantOperatingDbContext _operdb;
        private int Recommendations = 0;
        public OperatingDashboardController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<OperatingDashboardController> logger,
            TenantOperatingDbContext operdb, IConfiguration configuration) : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _operdb = operdb;
            _configuration = configuration;
        }
        public async Task<List<ProviderProfile>> GetProviderRecords()
        {
            var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
            try
            {
                var providers = await operrepo.GetProviderDirectories();
                Recommendations = providers.Count(x => x.Approval == "Pending review");
                return providers;
            }
            catch (Exception e)
            {
                return new List<ProviderProfile>();
            }

        }
        public async Task<IActionResult> OperatorIndex()
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
                    //if (GetComponentsBasedOnRole() == false)
                    //{
                    //    string returnUrl = @"/Identity/Account/Login";
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                   WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                var providers = await GetProviderRecords();
                string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var Servicerigs = db.rig_register.Where(x => x.TenantID == tenantid && x.isActive == true && (x.Rig_id == RigId && !checkwellFilter || checkwellFilter)).ToList();
                var TottalRigs = (from rigs in Servicerigs
                                  where rigs.TenantID == tenantid && (rigs.Rig_id == RigId && !checkwellFilter || checkwellFilter)
                                  group rigs by rigs.Rig_id into g
                                  select new
                                  {
                                      TottalRigs = g.Count()
                                  }
                                );
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var Auctiondata = new AuctionBidsModel();
                Auctiondata = auctionProposalBusiness.AuctionDashboardOperatorStatus(userwell, RigId);
                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                var dashboard = new OperatingDashboardModel
                {
                    Rigs = Servicerigs.Count(),
                    AwardedBidsVal = Convert.ToString("$" + Auctiondata.AwardedBidsThisMonthValue),
                    AwardedBids = Auctiondata.AwardedBidsThisMonthCount,
                    OpenBidsVal = Convert.ToString("$" + Auctiondata.ActiveBidsValue),
                    OpenBidsCount = Convert.ToString(Auctiondata.ActiveBidsCount),
                    ComplianceAlertCount = providers.Count(p => p.PecStatus != "Good"),
                    Recommendations = Recommendations,
                    OpenFieldTickets = 18
                };
                return View(dashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)),
                Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDashboard Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
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
                    //if (GetComponentsBasedOnRole() == false)
                    //{
                    //    string returnUrl = @"/Identity/Account/Login";
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                   WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                var providers = await GetProviderRecords();
                string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var Servicerigs = db.rig_register.Where(x => x.TenantID == tenantid && x.isActive == true && (x.Rig_id == RigId && !checkwellFilter || checkwellFilter)).ToList();
                var TottalRigs = (from rigs in Servicerigs
                                  where rigs.TenantID == tenantid && (rigs.Rig_id == RigId && !checkwellFilter || checkwellFilter)
                                  group rigs by rigs.Rig_id into g
                                  select new
                                  {
                                      TottalRigs = g.Count()
                                  }
                                );
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var Auctiondata = new AuctionBidsModel();
                Auctiondata = auctionProposalBusiness.AuctionDashboardOperatorStatus(userwell, RigId);
                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                var dashboard = new OperatingDashboardModel
                {
                    Rigs = Servicerigs.Count(),
                    AwardedBidsVal = Convert.ToString("$" + Auctiondata.AwardedBidsThisMonthValue),
                    AwardedBids = Auctiondata.AwardedBidsThisMonthCount,
                    OpenBidsVal = Convert.ToString("$" + Auctiondata.ActiveBidsValue),
                    OpenBidsCount = Convert.ToString(Auctiondata.ActiveBidsCount),
                    ComplianceAlertCount = providers.Count(p => p.PecStatus != "Good"),
                    Recommendations = Recommendations,
                    OpenFieldTickets = 18
                };
                return View(dashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)),
                Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDashboard Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult Error()
        {
            return View();
        }
        public async Task<IActionResult> Counts()
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                  WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                var providers = await GetProviderRecords();
                string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var Servicerigs = db.rig_register.Where(x => x.TenantID == tenantid && x.isActive == true && (x.Rig_id == RigId && !checkwellFilter || checkwellFilter)).ToList();
                var TottalRigs = (from rigs in Servicerigs
                                  where rigs.TenantID == tenantid && (rigs.Rig_id == RigId && !checkwellFilter || checkwellFilter)
                                  group rigs by rigs.Rig_id into g
                                  select new
                                  {
                                      TottalRigs = g.Count()
                                  }
                                );
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var Auctiondata = new AuctionBidsModel();
                Auctiondata = auctionProposalBusiness.AuctionDashboardOperatorStatus(userwell, RigId);
                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                var dashboard = new OperatingDashboardModel
                {
                    Rigs = Servicerigs.Count(),
                    AwardedBidsVal = Convert.ToString("$" + Auctiondata.AwardedBidsThisMonthValue),
                    AwardedBids = Auctiondata == null ? 0 : Auctiondata.AwardedBidsThisMonthCount,
                    OpenBidsVal = Convert.ToString("$" + Auctiondata.ActiveBidsValue),
                    OpenBidsCount = Convert.ToString(Auctiondata.ActiveBidsCount),
                    ComplianceAlertCount = providers.Count(p => p.PecStatus != "Good"),
                    Recommendations = Recommendations,
                    OpenFieldTickets = 18
                };
                return Json(dashboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)),
                Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDashboard Counts", User.Identity.Name);
                return null;
            }
        }
        public async Task<ActionResult> CorporateProfileData()
        {
            try
            {
                string TenantID = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var result = db.CorporateProfile.Where(x => x.TenantId == TenantID).Select(y => y.LogoPath).FirstOrDefault();
                var LogoPath = await GetUrlOfImage(result);
                return Json(LogoPath);
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
        public JsonResult TimeFunction()
        {
            try
            {
                DateTime Date = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(Date, cstZone);
                var hours = cstTime.Hour;
                var minutes = cstTime.Minute;
                var Seconds = cstTime.Second;
                var CurrentDate = cstTime.ToString("MM/dd/yyyy");
                string value = "10";
                var Hour = Convert.ToString(hours).Length < value.Length ? "0" + Convert.ToString(hours) : Convert.ToString(hours);
                var Min = Convert.ToString(minutes).Length < value.Length ? "0" + Convert.ToString(minutes) : Convert.ToString(minutes);
                var Sec = Convert.ToString(Seconds).Length < value.Length ? "0" + Convert.ToString(Seconds) : Convert.ToString(Seconds);
                var Time = Hour + ":" + Min + ":" + Sec;
                var CstDateTime = CurrentDate + " " + Time + " " + "CST";
                return Json(CstDateTime);
            }
            catch (Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                var userIdentity = (ClaimsIdentity)User.Identity;
                errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                return null;
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
                customErrorHandler.WriteError(ex, "OperatingDashboard Index", User.Identity.Name);
                return string.Empty;
            }
        }
        public async Task<IActionResult> GetDepthChartActionAsync([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var Rigid = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var data = await GetDepthDataAsync(tenantid, Rigid);
                return Json(data);
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
        public async Task<IActionResult> GetTimeChartActionAsync([DataSourceRequest] DataSourceRequest request, string operId)
        {
            try
            {
                string tenantid = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var Rigid = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var data = await GetTimeDataAsync(tenantid, Rigid);
                return Json(data);
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
        private async Task<List<OperatingRig>> GetDepthDataAsync(string operId, string Rigid)
        {
            var chartdata = new List<OperatingRig>();
            var newResult = new List<OperatingRig>();
            var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
            try
            {
                var checkwellFilter = DLL.Constants.NoSpecificWellFilterKey == Rigid;
                var wellreg = db.WellRegister.Where(x => x.customer_id == operId && x.Prediction == true).ToList();
                //var DrillingDepth = (from wd in db.WellDepthDataStages
                //                     where wd.TENANTID == operId 
                //                     group wd by new { wd.WID, wd.DATE, wd.TIME } into g
                //                     select new
                //                     {
                //                         WID = g.Key.WID,
                //                         DMEA = g.Max(wd => wd.DMEA.HasValue ? wd.DMEA.Value : 0.0),
                //                         DATE = (DateTime.ParseExact(g.Key.DATE.ToString(), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddSeconds((double)g.Key.TIME))
                //                     }).ToList();
                //
                //DWOP
                var DrillingDepthFirst = (from wd in db.WellDepthDataStages
                                          where wd.TENANTID == operId
                                          group wd by new { wd.WID } into g
                                          select new
                                          {
                                              WID = g.Key.WID,
                                              DMEA = g.Max(wd => wd.DMEA.HasValue ? wd.DMEA.Value : 0.0),
                                              DATE = (DateTime.ParseExact(g.Max(wd => wd.DATE).ToString(), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture))
                                          }).ToList();


                var DrillingDepth = (from wd in db.WellDepthDataStages
                                     where wd.TENANTID == operId
                                     group wd by new { wd.WID, wd.DATE, wd.TIME } into g
                                     select new
                                     {
                                         WID = g.Key.WID,
                                         DMEA = g.Max(wd => wd.DMEA.HasValue ? wd.DMEA.Value : 0.0),
                                         DATE = (DateTime.ParseExact(g.Key.DATE.ToString(), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddSeconds((double)g.Key.TIME))
                                     }).ToList();


                var rigRegister = await db.rig_register.Where(x => x.isActive == true).ToListAsync();
                //Phase II Changes - (float)g.Key.DMEA == null ? 0 : (float)g.Key.DMEA => (float)g.Key.DMEA
                newResult = (from wd in DrillingDepthFirst
                             join wr in wellreg on wd.WID equals wr.well_id
                             join rig in rigRegister on wr.RigID equals rig.Rig_id
                             where (wr.Prediction == true && rig.Rig_id == Rigid && rig.isActive == true && !checkwellFilter || checkwellFilter)
                             group wd by new { wd.DMEA, rig.Rig_Name, rig.Rig_id, wr.ChartColor, wr.well_id, wr.wellname } into g
                             select new OperatingRig
                             {
                                 Value = (float)g.Key.DMEA,
                                 Category = g.Key.Rig_Name,
                                 Color = (g.Key.ChartColor != null) ? g.Key.ChartColor : "#0257b1",
                                 WellId = g.Key.well_id,
                                 WellName = g.Key.wellname,
                                 RigName = g.Key.Rig_Name,
                                 RigId = g.Key.Rig_id
                             }
                       ).OrderBy(o => o.RigName).ThenBy(o => o.WellName).Distinct().ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                var userIdentity = (ClaimsIdentity)User.Identity;
                errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                throw;
            }
            return newResult;
        }
        public async Task<List<OperatingRig>> GetTimeDataAsync(string operId, string Rigid)
        {
            var timeData = new List<OperatingRig>();
            try
            {
                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                var checkWellFilter = DLL.Constants.NoSpecificWellFilterKey == Rigid;
                var GeneralTimeBaseddata1 = (from wd in db.WellDepthDataStages
                                             where wd.TENANTID == operId
                                             select new
                                             {
                                                 WID = wd.WID,
                                                 DATD = (DateTime.ParseExact(wd.DATE.ToString(), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddSeconds((double)wd.TIME))
                                             }).ToList();

                var GeneralTimeBaseddata = (from t in GeneralTimeBaseddata1
                                            group t by new { t.WID } into g
                                            select new
                                            {
                                                WID = g.Key.WID,
                                                DATD = g.Max(e => e.DATD)
                                            }).ToList();
                var wellRegResult = db.WellRegister.Where(x => x.customer_id == operId && x.Prediction == true)
                    .Select(x =>
                        new
                        {
                            x.well_id,
                            CreatedDate = x.CreatedDate.Date,
                            x.wellname,
                            x.RigID,
                            x.ChartColor
                        }).ToList();

                //Value2 = Convert.ToInt32((Convert.ToInt32(wt.DATD.ToString("yyyyMMdd")) - Convert.ToInt32(wg.CreatedDate.ToString("yyyyMMdd")))),

                var rigRegister = await db.rig_register.Where(x => x.isActive == true).ToListAsync();
                var wellreg = (from wg in wellRegResult
                               join wt in GeneralTimeBaseddata on wg.well_id equals wt.WID
                               join rig in rigRegister on wg.RigID equals rig.Rig_id
                               where (rig.Rig_id == Rigid && !checkWellFilter || checkWellFilter)
                               select new
                               {
                                   wg.well_id,
                                   wg.wellname,
                                   wg.ChartColor,
                                   Value2 = wt.DATD.Subtract(wg.CreatedDate).Days,
                                   rig.Rig_id,
                                   rig.Rig_Name
                               }).ToList();
                timeData = (from well in wellreg
                            select new OperatingRig
                            {
                                Value2 = well.Value2,
                                Category = well.Rig_Name,
                                Color = (well.ChartColor != null) ? well.ChartColor : "#0257b1",
                                WellId = well.well_id,
                                WellName = well.wellname
                            }).OrderBy(o => o.Category).ThenBy(o => o.WellName).Distinct().ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                var userIdentity = (ClaimsIdentity)User.Identity;
                errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
            }
            return await Task.FromResult(timeData);
        }

        public async Task<IActionResult> AdvisorWithDispatch()
        {
            if (_signInManager.IsSignedIn(User) == false)
            {
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
            return View("Advisor");
        }
    }
}