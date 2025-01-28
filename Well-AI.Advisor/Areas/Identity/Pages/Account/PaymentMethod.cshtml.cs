using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Business;
 
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class PaymentMethodModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly ILogger<CompanyModel> _logger;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IWebHostEnvironment _env;
        private IMapper _mapper;
        public PaymentMethodModel(
            IWebHostEnvironment env,
            UserManager<WellIdentityUser> userManager,
            SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<CompanyModel> logger,
            WebAIAdvisorContext dbContext,
            IMapper mapper)
        {
            _env = env;
            _userManager = userManager;
            this.roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
            _mapper = mapper;

            var config = new MapperConfiguration(crf =>
            {
                crf.CreateMap<InputModel, CrmPaymentMethods>();
            });
            _mapper = config.CreateMapper();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            public string UserId { get; set; }

            [Required]
            [Display(Name = "CreditCard Holder Name")]
            public string CustomerName { get; set; }

            [Required]
            [StringLength(16, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 16)]
            public string CreditCardNumber { get; set; }

            [Display(Name = "Valid Upto")]
            public string ValidUptoDate { get; set; }

            [Required]
            [Display(Name = "Month")]
            [StringLength(2, ErrorMessage = "The {0} must be at least {2} and at max {1} number.", MinimumLength = 2)]
            public string ValidUptoMonth { get; set; }

            [Required]
            [Display(Name = "Year")]
            [StringLength(4, ErrorMessage = "The {0} must be at least {2} and at max {1} number.", MinimumLength = 4)]
            public string ValidUptoYear { get; set; }

            public CrmPaymentMethods PaymentMethod { get; set; }
        }

        public async Task<IActionResult> OnGet(string userId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError(string.Empty, "A userId must be supplied to add payment method.");
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                IPaymentMethodBusiness paymentMethodBusiness = new PaymentMethodBusiness(db, roleManager, _userManager);
                var result = await paymentMethodBusiness.GetPaymentMethod(userId);

                if (result != null)
                {
                    result.CreditCardNumber = await paymentMethodBusiness.DecryptData(result.CreditCardNumber);
                    result.ValidUptoDate = await paymentMethodBusiness.DecryptData(result.ValidUptoDate);
                    Input = new InputModel
                    {
                        UserId = userId,
                        CustomerName = result.CustomerName,
                        CreditCardNumber =result.CreditCardNumber,
                        ValidUptoMonth = result.ValidUptoDate.Split('/')[0],
                        ValidUptoYear = result.ValidUptoDate.Split('/')[1],
                        PaymentMethod = result
                    };
                }
                else
                {
                    Input = new InputModel
                    {
                        UserId = userId
                    };
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Register- payment page" + userId, User.Identity.Name);

                _logger.LogInformation(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                try
                {
                    ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                    IPaymentMethodBusiness paymentMethodBusiness = new PaymentMethodBusiness(db, roleManager, _userManager);
                    var paymentMethod = _mapper.Map<CrmPaymentMethods>(Input);
                    // "/" change to "-" to compatible with other areas
                    //paymentMethod.ValidUptoDate = (Input.ValidUptoMonth + '/' + Input.ValidUptoYear);
                    paymentMethod.ValidUptoDate = (Input.ValidUptoMonth + '-' + Input.ValidUptoYear);
                    var status = await paymentMethodBusiness.CreatePaymentMethod(paymentMethod);
                    if (status)
                    {
                        //Updating page status//
                        commonBusiness.UpdateUserPagesCompleteStatus(7, Input.UserId);
                        return RedirectToPage("Subscribe", new { userId = Input.UserId });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "server side error is coming.");
                    }
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "Register- payment page OnPost" + Input.UserId, User.Identity.Name);

                    _logger.LogInformation(ex.Message);
                    ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                }
            }
            return Page();
        }
    }
}

