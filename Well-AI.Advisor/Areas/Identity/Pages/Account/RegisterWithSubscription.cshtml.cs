using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.BLL.Business;
 
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterWithSubscriptionModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;
        
        public RegisterWithSubscriptionModel(
            
            UserManager<WellIdentityUser> userManager,
            SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            WebAIAdvisorContext dbContext)
        {
           
            _userManager = userManager;
            this.roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Confirm Email")]
            [Compare("Email", ErrorMessage = "Your entered emails do not match.")]
            public string ConfirmEmail { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Required]
            [Display(Name = "Company")]
            public string Company { get; set; }

            [Required]
            [Display(Name = "Phone")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
            public string Phone { get; set; }

           
            public string AccountType { get; set; }

            
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Your entered passwords do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "You must accept the Terms of Services")]
            public bool Agreement { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            var returnUrls = Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                returnUrl = returnUrl ?? Url.Content("~/");
                if (ModelState.IsValid)
                {
                    ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                    EmailHandler emailHandler = new EmailHandler();

                    //getting already existing login//
                    var userExisting = await commonBusiness.GetUserByEmail(Input.Email);
                    if (userExisting != null)
                    {
                        if(userExisting.TenantId != "")
                        {
                            ModelState.AddModelError(string.Empty, "This address has already been registered with Well-Al, please login or reset your password.");
                            return Page();
                        }                        
                    }
                    //Phase II Changes - 03/22/2021
                    //var user = new WellIdentityUser { UserName = Input.Email, Email = Input.Email, PhoneNumber = Input.Phone ,WellUser=true};
                    var user = new WellIdentityUser { UserName = Input.Email, Email = Input.Email, PhoneNumber = Input.Phone, WellUser = Input.AccountType == "Rig Operator" ? true : false };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        
                            string roleName = Input.AccountType == "Rig Operator" ? "Operator" : Input.AccountType == "Dispatch" ? "Dispatch" : "Service Provider";

                            //this code is temporary untill all the register process is completed//
                            await commonBusiness.AddUserRole(user, roleName);

                            
                            int accountType = Input.AccountType == "Rig Operator" ? 0 : Input.AccountType == "Dispatch" ? 2 : 1;
                            var crmUserBasicDetail = new CrmUserBasicDetail {
                                UserId = user.Id, Name = Input.Name, Company = Input.Company, IsMaster = true,
                                Title = Input.Title, AccountType = accountType, IsActive = true, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow };
                            var status = commonBusiness.CreateUserBasicDetail(crmUserBasicDetail);
                            if (status == true)
                            {
                                
                                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                                var callbackUrl = "http://" + Request.Host.Value + "/Identity/Account/ConfirmEmail?userId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id)) + "&code=" + code; //Url.Page("Account/ResetPassword", pageHandler: null, values: new { area = "Identity", code },
                                var flagStatus = await emailHandler.SendEmailAsync("Member", Input.Email, "Confirm Email", $"Dear Member, please click on  <a href='" + callbackUrl + "'>link</a> to confirm you email address, thanks");
                                // ADDEd BY GP TO GET ACTIVATION LINK
                                //ErrorHandler errorHandler = new ErrorHandler(_signInManager, roleManager, _userManager, db);
                                //errorHandler.ErrorLog(callbackUrl, "Activation Link");

                                commonBusiness.UpdateUserPagesCompleteStatus(1, user.Id);
                                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, type = accountType.ToString() });
                                
                            }
                        
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                // If we got this far, something failed, redisplay form
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong! Please contact the Well-AI Support Team.");
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, roleManager, _userManager, db);
                errorHandler.ErrorLog(ex.Message, "Register page",ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                return Page();
            }
        }
    }
}
