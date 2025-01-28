using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> roleManager;

        public LogoutModel(SignInManager<WellIdentityUser> signInManager, ILogger<LogoutModel> logger,
                          WebAIAdvisorContext dbContext,RoleManager<IdentityRole> roleManager,UserManager<WellIdentityUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.roleManager = roleManager;
            _logger = logger;
            db = dbContext;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);

            //If logged in successfull then need to add status of user online

            commonBusiness.UpdateUserStatus(_userManager.GetUserId(User), false);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            //Phase II Changes - 03/16/2021
            await commonBusiness.DeleteUsersession(_userManager.GetUserName(User));
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage("./Login");
            }
        }
    }
}
