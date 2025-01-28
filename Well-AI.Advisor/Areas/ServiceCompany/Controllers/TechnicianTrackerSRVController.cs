using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.BLL;
using Finbuckle.MultiTenant;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Well_AI.Advisor.API.Samsara.Services.IServices;
using Well_AI.Advisor.Log.Error;
using System;
using WellAI.Advisor.Helper;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.Model.Common;
using System.Security.Claims;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class TechnicianTrackerSRVController : BaseController
    {
        private readonly ILogger<TechnicianTrackerSRVController> _logger;
        private readonly ISingleton singleton;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly IVechicleService _vechicleService;
        private readonly WebAIAdvisorContext db;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TechnicianTrackerSRVController(SignInManager<WellIdentityUser> signInManager, 
                                              ILogger<TechnicianTrackerSRVController> logger, ISingleton singleton, IVechicleService vechicleService,
                                              UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext, RoleManager<IdentityRole> roleManager)
        : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.singleton = singleton;
            _vechicleService = vechicleService; 
            db = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //Phase-II Changes
        public IActionResult Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                return View();
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TechnicianTrackerSRV Index", User.Identity.Name);
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
        }
        //Phase-II Changes
        public IActionResult TechnicianTracker()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                return View();
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TechnicianTrackerSRV TechnicianTracker", User.Identity.Name);
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetActiveTechnicianAndProjectByTenantId([DataSourceRequest] DataSourceRequest request)
        {
            List<ServiceVehicleViewModel> result = new List<ServiceVehicleViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                { 
                    string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                    result = await singleton.serviceVehicleBusiness.GetActiveTechnicianAndProjectByTenantId(tenantId);
                }
            }
            catch (System.Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TechnicianTrackerSRV GetActiveTechnicianAndProjectByTenantId", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        public async Task<IActionResult> GetLatLngOfVehicle(string Id)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var result = await _vechicleService.GetMostRecentVechicleLocationByVechicleIdAsync(Id, tenantId);
                return Json(result);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TechnicianTrackerSRV GetLatLngOfVehicle", User.Identity.Name);
                return Json(ex);
            }
        }
        //Phase-II Changes
        public IActionResult SendTechnicianTrackDetail(string Id,string tId)
        {
            try
            {
                string corporateEmail = string.Empty;
                string creatorEmail = string.Empty;
                var callbackUrl = "https://" + Request.Host.Value;
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                MessageToQueue message = singleton.projectBusiness.GetProposalCreatorOperatorAndUserEmail(Id, UserId, tId).Result;
                if (message != null)
                {
                    callbackUrl = $"{callbackUrl}/TechnicianTracker/Project?id={Id}&tId={tId}";
                    message.MsgBody = message.MsgBody.Replace("[ProjectIDUrl]", callbackUrl);
                    EmailHandler emailHandler = new EmailHandler();
                    emailHandler.SendMessageToQueue(message);
                }
                return Json(true);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TechnicianTrackerSRV SendTechnicianTrackDetail", User.Identity.Name);
                return Json(ex);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
