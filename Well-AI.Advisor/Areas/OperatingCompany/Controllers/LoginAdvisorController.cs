using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.DLL.Data;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class LoginAdvisorController : BaseController
    {
        private readonly ILogger<LoginAdvisorController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        UserManager<WellIdentityUser> _userManager;
        public LoginAdvisorController(SignInManager<WellIdentityUser> signInManager, ILogger<LoginAdvisorController> logger,
                                      UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
            : base(userManager, dbContext)
        {
            _userManager = userManager;
            db = dbContext;
            _logger = logger;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User) == false)
            {
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}