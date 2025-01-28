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
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Helper;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class BiddingController : BaseController
    {
        private readonly ILogger<BiddingController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        public BiddingController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager, 
                                 RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<BiddingController> logger)
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
                var data = new BiddingModel[]
            {
                new BiddingModel{ Cat_id="MUD001", Cat_name="Earth Sediments", Item_id="MUDEAR001", Item_name="Gravel", Item_description="Gravel Deposited during Drilling", Item_amount=5000, Item_date_open=new DateTime(2019,08,01,12,30,0), Item_date_close= new DateTime(2019,10,10,12,30,45), Item_date_bid= new DateTime(2019,12,20,12,30,45), Item_location="Texas", Item_seller="Almaco"},
                new BiddingModel{ Cat_id= "MUD001", Cat_name= "Earth Sediments", Item_id= "MUDEAR002", Item_name= "Clay", Item_description= "Clay Deposited during Drilling", Item_amount=6000, Item_date_open=new DateTime(2019,08,01,12,30,0), Item_date_close= new DateTime(2019,10,10,12,30,45), Item_date_bid= new DateTime(2019,12,20,12,30,45), Item_location= "Texas", Item_seller= "Almaco"},
                new BiddingModel{ Cat_id= "MUD001", Cat_name= "Earth Sediments", Item_id= "MUDEAR003", Item_name= "Lime Stone", Item_description= "Lime Stone Deposited during Drilling", Item_amount=6000, Item_date_open=new DateTime(2019,08,01,12,30,0), Item_date_close= new DateTime(2019,10,10,12,30,45), Item_date_bid= new DateTime(2019,12,20,12,30,45), Item_location= "Texas", Item_seller= "Almaco"},
                new BiddingModel{ Cat_id= "SLU001", Cat_name= "Sludge", Item_id= "SLUDGE001", Item_name= "Sludge Loose", Item_description= "Dissolved solids which precipitate from produced water as its temperature and pressure change", Item_amount=7000, Item_date_open=new DateTime(2019,08,01,12,30,0), Item_date_close= new DateTime(2019,10,10,12,30,45), Item_date_bid= new DateTime(2019,12,20,12,30,45), Item_location= "Texas", Item_seller= "Almaco"},
                new BiddingModel{ Cat_id= "SLU001", Cat_name= "Sludge", Item_id= "SLUDGE001", Item_name= "Sludge Dried", Item_description= "Dissolved solids which precipitate from produced water as its temperature and pressure change", Item_amount=4000, Item_date_open=new DateTime(2020,02,01,12,30,0), Item_date_close= new DateTime(2019,3,10,12,30,45), Item_date_bid= new DateTime(2020,12,20,12,30,45), Item_location= "Texas", Item_seller= "Almaco"}
            };
                return View(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Bidding Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult Error()
        {
            return View();
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
    }
}
