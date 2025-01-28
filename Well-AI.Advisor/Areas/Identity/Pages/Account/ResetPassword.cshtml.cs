using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;

        public ResetPasswordModel(
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

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);

                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToPage("./ResetPasswordConfirmation");
                }
                var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
                if (result.Succeeded)
                {
                    var resultUserDetail = commonBusiness.GetUserBasicDetail(user.Id);
                    if (resultUserDetail.RegisterPagesCompleteStatus == 2)
                    {
                        //Updating page status//
                        commonBusiness.UpdateUserPagesCompleteStatus(3, user.Id);
                    }
                    return RedirectToPage("./ResetPasswordConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            catch (Exception ex) {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, roleManager, _userManager, db);
                errorHandler.ErrorLog(ex.Message, "Register- Reset password.", ex.HResult.ToString());
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                return Page();
            }
        }
    }
}
