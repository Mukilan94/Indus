using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class OngoingProjectsSRVController : BaseController
    {
        private readonly ILogger<OngoingProjectsSRVController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
       public OngoingProjectsSRVController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, ILogger<OngoingProjectsSRVController> logger)
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
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageOngoingProjectSRV Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/DashboardSRV/Error";
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
            return rolePermissionBusiness.GetSRVComponentBasedOnRole(roleResult.Id, "ViewDashboard");
        }
        public async Task<IActionResult> OnGoingProject_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ProjectViewSRVModel> result = new List<ProjectViewSRVModel>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = await commonBusiness.GetTenantIdByUserId(userId);
                result = await GetOnGoingProjects(tenantId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageOngoingProjectSRV OnGoingProject_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        public async Task<List<ProjectViewSRVModel>> GetOnGoingProjects(string userId)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);       
            var tenantId = await commonBusiness.GetTenantIdByUserId(userId);            
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var project = (from prj in db.Projects.Where(x => x.ServiceCompID.Equals(userId) && x.ProjectStatus.Equals(1))
                           select new ProjectViewSRVModel()
                           {
                               ProjectId = prj.ID,
                               ProjectCode = prj.ProjectID,
                               OperatorCompanyName = userName,
                               ExpectedStartDate = prj.ActualStart.Value,
                               ExpectedEndDate = prj.ActualEnd.Value,
                               Title = prj.ProjectTitle,
                               Description = prj.ProjectDescription ,
                           }).ToList();
            return project;
        }
        public IActionResult GetProjectInvoice_Read(string  jobId, [DataSourceRequest] DataSourceRequest request)
        {
            List<FieldTicketSRV> result = new List<FieldTicketSRV>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                result = commonBusiness.GetProjectInvoice(jobId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ManageOngoingProjectSRV GetProjectInvoice_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
           }
    }
}