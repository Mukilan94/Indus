using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class OngoingProjectsController : BaseController
    {
        private readonly ILogger<OngoingProjectsController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        public OngoingProjectsController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, ILogger<OngoingProjectsController> logger)
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
                return View(Projects());
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OngoingProjects Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }
        }

        public IEnumerable<ProjectViewModel> Projects()
        {
            var data = new ProjectViewModel[]
            {
            };
            return data;
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
            return rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, "OngoingProjects");
        }
        public IActionResult FieldTicket(int id, int pId)
        {
            try
            {
                var tickets = GetTickets();
                var tctprofile = tickets.FirstOrDefault(x => x.ProjectID == pId && x.fdId == id);
                if (tctprofile == null)
                {
                    tctprofile = tickets.FirstOrDefault();
                }
                return View(tctprofile);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OngoingProject FieldTicket", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
                return LocalRedirect(returnUrl);
            }
        }
        private List<FieldTicket> GetTickets()
        {
            var data = new List<FieldTicket>
            {
                new FieldTicket{ fdId=1,ProjectID=1, Ticket="12345",Date=new System.DateTime(2020,2,19,11, 30, 0), Invoice = "12345", Amount=5432,
                 Rig="H&P 613", Lease="Patriot 68 701H", PoAfe="EO19056", County="Reeves Co, TX", BillTo="Diamondback E&P,LLC",
                Items= new List<TicketFieldItem>{
                    new TicketFieldItem { Item="4131", Description="9/5-Roustabout Crew-3 Man Crew (2-Days, 1-Nights)",Qty=36.75,Rate=35,Amount=1286.25},
                    new TicketFieldItem { Item="4411", Description="Mileage-220 Miles @ $.60 per Mile",Qty=330,Rate=0.60,Amount=198},
                    new TicketFieldItem { Item="4131", Description="9/6-Roustabout Crew-2 Man Crew (1-Days, 1-Nights)",Qty=24.5,Rate=35,Amount=857.5},
                    new TicketFieldItem { Item="4411", Description="Mileage-220 Miles @ $.60 per Mile",Qty=220,Rate=0.60,Amount=132},
                    new TicketFieldItem { Item="4131", Description="9/7-Roustabout Crew-2 Man Crew (1-Days, 1-Nights)",Qty=24.5,Rate=35,Amount=857.5},
                    new TicketFieldItem { Item="4411", Description="Mileage-220 Miles @ $.60 per Mile",Qty=220,Rate=0.60,Amount=132},
                    new TicketFieldItem { Item="4131", Description="9/8-Roustabout Crew-2 Man Crew (1-Days, 1-Nights)",Qty=24.5,Rate=35,Amount=857.5},
                    new TicketFieldItem { Item="4411", Description="Mileage-220 Miles @ $.60 per Mile",Qty=220,Rate=0.60,Amount=132},
                    new TicketFieldItem { Item="4131", Description="9/9-Roustabout Crew-3 Man Crew (2-Days, 1-Nights)",Qty=35,Rate=35,Amount=1225},
                    new TicketFieldItem { Item="4411", Description="Mileage-330 Miles @ $.60 per Mile",Qty=330,Rate=0.60,Amount=198},
                    },
                    Subtotal=5875.75, SalesTaxCount=8.25, SalesTaxValue=484.75,Total=6360.5,ItemsDescription="Roustabouts to Work on Rig",
                },
                new FieldTicket{ fdId=2,ProjectID=1, Ticket="23465",Date=new System.DateTime(2020,2,19,10, 30, 0), Invoice = "12345", Amount=8764,
                Rig="H&P 715", Lease="Patriot 75 702S", PoAfe="EO17057", County="Reeves Co, TX", BillTo="Diamondback E&P,LLC",
                    Items= new List<TicketFieldItem>{
                    new TicketFieldItem { Item="4131", Description="8/5-Roustabout Crew-3 Man Crew (2-Days, 1-Nights)",Qty=36.75,Rate=35,Amount=1286.25},
                    new TicketFieldItem { Item="4411", Description="Mileage-220 Miles @ $.60 per Mile",Qty=330,Rate=0.60,Amount=198},
                    new TicketFieldItem { Item="4131", Description="8/6-Roustabout Crew-2 Man Crew (1-Days, 1-Nights)",Qty=24.5,Rate=35,Amount=857.5},
                    new TicketFieldItem { Item="4411", Description="Mileage-220 Miles @ $.60 per Mile",Qty=220,Rate=0.60,Amount=132},
                    new TicketFieldItem { Item="4131", Description="8/7-Roustabout Crew-2 Man Crew (1-Days, 1-Nights)",Qty=24.5,Rate=35,Amount=857.5},
                    new TicketFieldItem { Item="4411", Description="Mileage-220 Miles @ $.60 per Mile",Qty=220,Rate=0.60,Amount=132},
                    new TicketFieldItem { Item="4131", Description="8/8-Roustabout Crew-2 Man Crew (1-Days, 1-Nights)",Qty=24.5,Rate=35,Amount=857.5},
                    new TicketFieldItem { Item="4411", Description="Mileage-220 Miles @ $.60 per Mile",Qty=220,Rate=0.60,Amount=132},
                    new TicketFieldItem { Item="4131", Description="8/9-Roustabout Crew-3 Man Crew (2-Days, 1-Nights)",Qty=35,Rate=35,Amount=1225},
                    new TicketFieldItem { Item="4411", Description="Mileage-330 Miles @ $.60 per Mile",Qty=330,Rate=0.60,Amount=198},
                    },
                    Subtotal=5875.75, SalesTaxCount=8.25, SalesTaxValue=484.75,Total=6360.5,ItemsDescription="Roustabouts to Work on Rig",
                },
                new FieldTicket{ fdId=3,ProjectID=1, Ticket="34567",Date=new System.DateTime(2020,2,20,1, 13, 30, 0), Invoice = "12345", Amount=2342},
                new FieldTicket{ fdId=4,ProjectID=1, Ticket="63432",Date=new System.DateTime(2020,2,22,1, 14, 00, 0), Invoice = "12345", Amount=12432},
                new FieldTicket{ fdId=1,ProjectID=2, Ticket="21324",Date=new System.DateTime(2020,2,19,11, 30, 0), Invoice = "12345", Amount=5432},
                new FieldTicket{ fdId=2,ProjectID=2, Ticket="98765",Date=new System.DateTime(2020,2,19,10, 30, 0), Invoice = "12345", Amount=8764},
                new FieldTicket{ fdId=3,ProjectID=2, Ticket="87643",Date=new System.DateTime(2020,2,20,1, 13, 30, 0), Invoice = "12345", Amount=2342},
                new FieldTicket{ fdId=4,ProjectID=2, Ticket="23432",Date=new System.DateTime(2020,2,22,1, 14, 00, 0), Invoice = "12345", Amount=12432},
                new FieldTicket{ fdId=1,ProjectID=3, Ticket="87565",Date=new System.DateTime(2020,2,19,11, 30, 0), Invoice = "12345", Amount=5432},
                new FieldTicket{ fdId=2,ProjectID=3, Ticket="21323",Date=new System.DateTime(2020,2,19,10, 30, 0), Invoice = "12345", Amount=8764},
                new FieldTicket{ fdId=3,ProjectID=3, Ticket="12325",Date=new System.DateTime(2020,2,20,1, 13, 30, 0), Invoice = "12345", Amount=2342},
                new FieldTicket{ fdId=4,ProjectID=3, Ticket="23414",Date=new System.DateTime(2020,2,22,1, 14, 00, 0), Invoice = "12345", Amount=12432},
                new FieldTicket{ fdId=1,ProjectID=4, Ticket="54321",Date=new System.DateTime(2020,2,19,11, 30, 0), Invoice = "12345", Amount=5432},
                new FieldTicket{ fdId=2,ProjectID=4, Ticket="34534",Date=new System.DateTime(2020,2,19,10, 30, 0), Invoice = "12345", Amount=8764},
                new FieldTicket{ fdId=3,ProjectID=4, Ticket="54352",Date=new System.DateTime(2020,2,20,1, 13, 30, 0), Invoice = "12345", Amount=2342},
                new FieldTicket{ fdId=4,ProjectID=4, Ticket="23324",Date=new System.DateTime(2020,2,22,1, 14, 00, 0), Invoice = "12345", Amount=12432},
            };
            return data;
        }
        public async Task<IActionResult> GetTicketsByJob(int jobId, [DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var data = GetTickets();
                var result = await data.Where(x => x.ProjectID == jobId).ToList().ToDataSourceResultAsync(request);
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OngoingProject FieldTicket", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                _logger.LogInformation(ex.Message);
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