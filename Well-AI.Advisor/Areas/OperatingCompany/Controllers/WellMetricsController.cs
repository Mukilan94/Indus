using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class WellMetricsController : BaseController
    {
        private readonly ILogger<WellMetricsController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        public WellMetricsController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, ILogger<WellMetricsController> logger)
            : base(userManager, dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _logger = logger;
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
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var wellboard = new WellMetricsModel
                {
                    TotalWells = 280,
                    ActiveWells = 170,
                    InactiveWells = 80,
                    WellsOnService = 30,
                    ChartWellData = GetWellData(),
                    CompanyWells = new List<CompanyWellData> {
                    new CompanyWellData {TotalWells=23, Company="XTO ENSIGN", InactiveWells=2, ActiveWells=20, WellsOnService=1 },
                    new CompanyWellData {TotalWells=50, Company="EOG H&P", InactiveWells=20, ActiveWells=20, WellsOnService=10 },
                    new CompanyWellData {TotalWells=27, Company="Rigs Service", InactiveWells=4, ActiveWells=20, WellsOnService=3 },
                    new CompanyWellData {TotalWells=25, Company="Abbot Group", InactiveWells=5, ActiveWells=10, WellsOnService=10 },
                    new CompanyWellData {TotalWells=42, Company="Permian Services Group", InactiveWells=2, ActiveWells=30, WellsOnService=10 },
                    new CompanyWellData {TotalWells=29, Company="Silvertip Completion Services", InactiveWells=2, ActiveWells=20, WellsOnService=7 },
                    new CompanyWellData {TotalWells=28, Company="TDJ Oilfield Services", InactiveWells=12, ActiveWells=16, WellsOnService=0 }
                },
                };
                return View(wellboard);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellMetrics", User.Identity.Name);
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
            return rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, "WellMetrics");
        }
        private List<Well> GetWellData()
        {
            return new List<Well> {
                new Well {Value=12500, Name="Well 1", Color="#007BFF", RotarySpeed=90, PumpRate1=120, PumpRate2=90, Standpipe=1400, BlockHeight=28,HaltOfBootom=4,WobRop=true,BitDepth=3.2,HoleDepth=3.2,Hookload=30.3,WOB=8.6,ROP=29.5 },
                new Well {Value=5000, Name="Well 2", Color="#26DDCC", RotarySpeed=91, PumpRate1=121, PumpRate2=91, Standpipe=1400, BlockHeight=28,HaltOfBootom=5,WobRop=true, Circ=true,BitDepth=3.2,HoleDepth=3.2,Hookload=30.3,WOB=8.6,ROP=29.5 },
                new Well {Value=19500, Name="Well 3", Color="#3639A4", RotarySpeed=92, PumpRate1=122, PumpRate2=92, Standpipe=1400, BlockHeight=28,HaltOfBootom=6,WobRop=false, Drill=true,BitDepth=3.2,HoleDepth=3.2,Hookload=30.3,WOB=8.6,ROP=29.5 },
                new Well {Value=15500, Name="Well 4", Color="#F4AF00", RotarySpeed=93, PumpRate1=123, PumpRate2=93, Standpipe=1400, BlockHeight=28,HaltOfBootom=7,WobRop=true,BitDepth=3.2,HoleDepth=3.2,Hookload=30.3,WOB=8.6,ROP=29.5 },
                new Well {Value=9000, Name="Well 5", Color="#FF6344", RotarySpeed=94, PumpRate1=124, PumpRate2=94, Standpipe=1400, BlockHeight=28,HaltOfBootom=8,WobRop=true, Circ=true, Drill=true,BitDepth=3.2,HoleDepth=3.2,Hookload=30.3,WOB=8.6,ROP=29.5 },
                new Well {Value=12000, Name="Well 6", Color="#77BD27", RotarySpeed=95, PumpRate1=125, PumpRate2=95, Standpipe=1400, BlockHeight=28,HaltOfBootom=9,WobRop=false, Circ=true,BitDepth=3.2,HoleDepth=3.2,Hookload=30.3,WOB=8.6,ROP=29.5 },
                new Well {Value=20000, Name="Well 7", Color="#0422A1", RotarySpeed=96, PumpRate1=126, PumpRate2=96, Standpipe=1400, BlockHeight=28,HaltOfBootom=10,WobRop=true, Drill=true,BitDepth=3.2,HoleDepth=3.2,Hookload=30.3,WOB=8.6,ROP=29.5 }
            };
        }
        public IActionResult WellMetricsProfile(string id)
        {
            try
            {
                var wells = GetWellData();
                var wellprofile = wells.FirstOrDefault(x => x.Name == id);
                if (wellprofile == null)
                {
                    wellprofile = wells.FirstOrDefault();
                }
                return View(wellprofile);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellMetricsProfile", User.Identity.Name);
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
    }
}