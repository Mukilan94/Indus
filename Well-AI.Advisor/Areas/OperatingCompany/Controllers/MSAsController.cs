using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    public class MSAsController : BaseController
    {
        public MSAsController(UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
            : base(userManager, dbContext)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}