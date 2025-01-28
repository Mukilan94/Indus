using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.Identity.Pages.Account.Manage
{
    public class GenerateRecoveryCodesModel : PageModel
    {
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly ILogger<GenerateRecoveryCodesModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;

        public GenerateRecoveryCodesModel(
            UserManager<WellIdentityUser> userManager,
            ILogger<GenerateRecoveryCodesModel> logger,
            WebAIAdvisorContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            db = dbContext;
        }

        [TempData]
        public string[] RecoveryCodes { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                if (!isTwoFactorEnabled)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' because they do not have 2FA enabled.");
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GenerateRecoveryCodes OnGet", User.Identity.Name);

                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                var userId = await _userManager.GetUserIdAsync(user);
                if (!isTwoFactorEnabled)
                {
                    throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' as they do not have 2FA enabled.");
                }

                var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                RecoveryCodes = recoveryCodes.ToArray();

                _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);
                StatusMessage = "You have generated new recovery codes.";
                return RedirectToPage("./ShowRecoveryCodes");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GenerateRecoveryCodes OnPost", User.Identity.Name);

                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
            }
            return Page();
        }
    }
}