using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    public class ProgramPermitsController : BaseController
    {
        public ProgramPermitsController(UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
            : base(userManager, dbContext)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}