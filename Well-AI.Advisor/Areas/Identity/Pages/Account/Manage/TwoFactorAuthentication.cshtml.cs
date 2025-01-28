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
    public class TwoFactorAuthenticationModel : PageModel
    {
        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}";

        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly ILogger<TwoFactorAuthenticationModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;

        public TwoFactorAuthenticationModel(
            UserManager<WellIdentityUser> userManager,
            SignInManager<WellIdentityUser> signInManager,
            ILogger<TwoFactorAuthenticationModel> logger,
            WebAIAdvisorContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            db = dbContext;
        }

        public bool HasAuthenticator { get; set; }

        public int RecoveryCodesLeft { get; set; }

        [BindProperty]
        public bool Is2faEnabled { get; set; }

        public bool IsMachineRemembered { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
                Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TwoFactorAuth Onget", User.Identity.Name);

                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                await _signInManager.ForgetTwoFactorClientAsync();
                StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "TwoFactorAuth OnPost", User.Identity.Name);

                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
            }
            return Page();
        }
    }
}