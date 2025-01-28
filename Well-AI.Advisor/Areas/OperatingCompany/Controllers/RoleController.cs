using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class RoleController : BaseController
    {
        private readonly ILogger<ManagePermissionsController> _logger;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;
        UserManager<WellIdentityUser> userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        public RoleController(SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager, 
            ILogger<ManagePermissionsController> logger, WebAIAdvisorContext dbContext, UserManager<WellIdentityUser> userManager)
            : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            this.roleManager = roleManager;
            _logger = logger;
            db = dbContext;
            this.userManager = userManager;
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
                IRolePermissionRepository repository = new RolePermissionRepository(db, roleManager, userManager);
                List<RoleViewModel> roleViewModelList = new List<RoleViewModel>();
                var roles = roleManager.Roles.ToList();
                foreach (var item in roles)
                {
                    RoleViewModel roleViewModel = new RoleViewModel();
                }
                ViewData["roles"] = roleViewModelList;
                return View();
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, userManager, db, Guid.Parse(userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Role Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        public IActionResult Create()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                return View(new IdentityRole());
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, userManager, db, Guid.Parse(userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Role Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            try
            {
                await roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, userManager, db, Guid.Parse(userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Role Create_role", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}