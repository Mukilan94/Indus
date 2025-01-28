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
    public class VoiceTextVideoChatController : BaseController
    {
        private readonly ILogger<VoiceTextVideoChatController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext _context;
        private readonly UserManager<WellIdentityUser> _userManager;
        public VoiceTextVideoChatController(SignInManager<WellIdentityUser> signInManager, ILogger<VoiceTextVideoChatController> logger,
                                           WebAIAdvisorContext context, UserManager<WellIdentityUser> userManager)
            : base(userManager, context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
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