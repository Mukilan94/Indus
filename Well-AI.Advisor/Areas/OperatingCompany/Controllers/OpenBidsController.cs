using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.DLL.Data;
using System;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class OpenBidsController : BaseController
    {
        private readonly ILogger<OpenBidsController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        public OpenBidsController(UserManager<WellIdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<WellIdentityUser> signInManager, ILogger<OpenBidsController> logger, WebAIAdvisorContext dbContext)
            : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
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
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OngoingProject FieldTicket", User.Identity.Name);
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