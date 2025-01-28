using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.Identity;
using System.Collections.Generic;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using System;
using WellAI.Advisor.Areas.Identity;
using System.Linq;
using WellAI.Advisor.DLL.Entity;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Routing;
using WellAI.Advisor.Helper;
using System.Security.Claims;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    [SessionTimeOut]
    public class WellDataSRVController : BaseController
    {
        private readonly ILogger<WellDataSRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        public WellDataSRVController(SignInManager<WellIdentityUser> signInManager, ILogger<WellDataSRVController> logger,
                                     WebAIAdvisorContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
            : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.db = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
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
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
               customErrorHandler.WriteError(ex, "wellDataSRV Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Phase-II Changes
        public IActionResult GetWellMasterRead([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(GetServiceWellMaster().ToDataSourceResult(request));
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "wellDataSRV GetWellMasterRead", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json(ex);
            }
        }
        private IEnumerable<WellDataServiceCompanyViewModel> GetServiceWellMaster()
        {
            IEnumerable<WellDataServiceCompanyViewModel> result = null;
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                result = AIBusiness.GetServiceWellMaster(tenantId.Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "wellDataSRV GetServiceWellMaster", User.Identity.Name);
            }
            return result;
        }
        public List<RigList> GetServiceRigs()
        {
            IEnumerable<WellDataServiceCompanyViewModel> result = null;
            List<RigList> rigList = new List<RigList>();
            List<WellMasterDataViewModel> wellsList = new List<WellMasterDataViewModel>();
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                result = AIBusiness.GetServiceWellMaster(tenantId.Result);

                rigList = (from r in result
                             join well in db.WELL_REGISTERs on r.wellId equals well.well_id
                             join rig in db.rig_register on well.RigID equals rig.Rig_id
                               group rig by new { rig.Rig_id, rig.Rig_Name } into g
                               select new RigList
                               {
                                   Rig_Id = g.Key.Rig_id,
                                   Rig_Name = g.Key.Rig_Name
                               }
                             ).ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "wellDataSRV GetServiceRigs", User.Identity.Name);
            }
            return rigList;
        }

        public List<WellMasterDataViewModel> GetServiceWells(string rigId)
        {
            IEnumerable<WellDataServiceCompanyViewModel> result = null;
            List<RigList> rigList = new List<RigList>();
            List<WellMasterDataViewModel> wellsList = new List<WellMasterDataViewModel>();
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                result = AIBusiness.GetServiceWellMaster(tenantId.Result);

                wellsList = (from r in result
                           join well in db.WELL_REGISTERs on r.wellId equals well.well_id                           
                           where well.RigID == rigId
                             group r by new { r.wellId, r.wellName } into g
                           select new WellMasterDataViewModel
                           {
                               wellId = g.Key.wellId,
                               wellName = g.Key.wellName
                           }
                             ).ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "wellDataSRV GetServiceWells", User.Identity.Name);
            }
            return wellsList;

        }
        public ActionResult GetWellRIGData(string wellId)
        {
            IEnumerable<AIWellDataModel> aIWellData = null;
            try
            {
                IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                aIWellData = AIBusiness.GetRIGAIResult(wellId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "wellDataSRV GetWellRIGData", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return View("IndexWellAI", aIWellData);
        }
    }
}