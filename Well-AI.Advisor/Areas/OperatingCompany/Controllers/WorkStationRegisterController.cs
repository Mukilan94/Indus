using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class WorkStationRegisterController : BaseController
    {
        private readonly ILogger<WorkStationRegisterController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;

        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        public WorkStationRegisterController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
                                      RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<WorkStationRegisterController> logger) : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
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
                customErrorHandler.WriteError(ex, "Pad Index", User.Identity.Name);
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
        public async Task<IActionResult> GetWorkstationRead([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var result = await GetWorkstationDetail();
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWorkstationRead", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task<IEnumerable<WorkstationModel>> GetWorkstationDetail()
        {
            IAIBusiness AIBusiness = new AIBuiness(db, _roleManager, _userManager);
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var result = new List<WorkstationModel>();
            try
            {
                var userId = _userManager.GetUserId(User);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId);
                result = await AIBusiness.GetWorkstationDetail(tenantId.Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWorkstationDetail", User.Identity.Name);
                _logger.LogInformation(ex.Message);
            }
            return result;
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> WorkstationData_Create([DataSourceRequest] DataSourceRequest request, WorkstationModel input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            bool isUnique = await commonBusiness.IsUniqueWorkStation(input.WorkstationIdentifier);
            if (isUnique)
            {
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                try
                {
                    RegistrationSecrateKeyUtil keyUtil = new RegistrationSecrateKeyUtil();
                    WorkstationRegister workStation = new WorkstationRegister();
                    ModelState.Remove("RegisterationId");
                    if (!ModelState.IsValid)
                    {
                        workStation.RegisterationId = Guid.NewGuid();
                        workStation.CustomerAccountIdentifier = tenantId;
                        workStation.DeviceName = input.DeviceName;
                        workStation.WorkstationIdentifier = input.WorkstationIdentifier;
                        workStation.IsActive = input.IsActive;
                        workStation.CreatedDate = DateTime.Now;
                        workStation.WorkstationToken = keyUtil.GenerateKey();
                    };
                    db.WorkstationRegister.Add(workStation);
                    await db.SaveChangesAsync();
                    input.CreatedDate = workStation.CreatedDate;
                    input.WorkstationToken = workStation.WorkstationToken;
                    return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                }
                catch (SqlException ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "PadData_Create", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "PadData_Create", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
            }
            else
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(new Exception(), "Workstation Id Already Exists", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> workstationData_Update([DataSourceRequest] DataSourceRequest request, WorkstationModel input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            try
            {
                WorkstationRegister workStation = new WorkstationRegister();
                if (ModelState.IsValid)
                {
                    workStation.RegisterationId = input.RegisterationId;
                    workStation.CustomerAccountIdentifier = tenantId;
                    workStation.DeviceName = input.DeviceName;
                    workStation.WorkstationIdentifier = input.WorkstationIdentifier;
                    workStation.WorkstationToken = input.WorkstationToken;
                    workStation.IsActive = input.IsActive;
                    workStation.ModifiedDate = input.CreatedDate;
                    workStation.ModifiedDate = DateTime.UtcNow;
                };
                db.WorkstationRegister.Update(workStation);
                await db.SaveChangesAsync();
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "workStationData_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "workStationData_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> workstationData_Destroy([DataSourceRequest] DataSourceRequest request, WorkstationModel input)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
            try
            {
                var workstationObj= db.WorkstationRegister.FirstOrDefault(x => x.RegisterationId == input.RegisterationId);
                db.WorkstationRegister.Remove(workstationObj);
                 db.SaveChanges();
                var result = await GetWorkstationDetail();
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "workstationData_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "workstationData_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
    }
}
