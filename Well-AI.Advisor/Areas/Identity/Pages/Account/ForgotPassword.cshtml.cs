using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WellAI.Advisor.Helper;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using System.Security.Claims;
using WellAI.Advisor.Model.Identity;


namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;

        public ForgotPasswordModel(
             SignInManager<WellIdentityUser> signInManager,
            UserManager<WellIdentityUser> userManager, RoleManager<IdentityRole> roleManager, WebAIAdvisorContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this.roleManager = roleManager;
            db = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task OnGetAsync(string userid = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(userid))
                {
                    var user = await _userManager.FindByIdAsync(userid);
                    if (user != null)
                    {
                        ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                        user.EmailConfirmed = true;
                        var result = await _userManager.UpdateAsync(user);
                        //Updating page status//
                        commonBusiness.UpdateUserPagesCompleteStatus(2, user.Id);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "Server side error is coming.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "invalid link, please check it again.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, roleManager, _userManager, db);
                errorHandler.ErrorLog(ex.Message, "Register- company page.",ex.HResult.ToString());
                
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        // Don't reveal that the user does not exist or is not confirmed
                        EmailHandler emailHandler = new EmailHandler();
                        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = "http://" + Request.Host.Value + "/Identity/Account/ResetPassword?code=" + code; //Url.Page("Account/ResetPassword", pageHandler: null, values: new { area = "Identity", code },
                        var flagStatus = await emailHandler.SendEmailAsync("Customer", Input.Email, "Reset Password", $"Dear Customer, please click on  <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a> to reset password, thanks");
                        return RedirectToPage("./ForgotPasswordConfirmation");

                        
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "email address does not exist in the system.");
                    }
                }
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, roleManager, _userManager, db);
                errorHandler.ErrorLog(ex.Message, "Register- company page.", ex.HResult.ToString());
                return Page();
            }
        }
    }
}
