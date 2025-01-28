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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.BLL.Business;
 
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class SubscriptionModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly ILogger<CompanyModel> _logger;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IWebHostEnvironment _env;
        private IMapper _mapper;
        public SubscriptionModel(
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
                crf.CreateMap<InputModel, CrmCompanies>();
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

            public string Subscription { get; set; }

            public List<SubscriptionPackage> Subscriptions { get; set; }

            public string CurrentSubscription { get; set; }
            public int? NoOfRigs { get; set; }
        }

        public IActionResult OnGet(string userId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError(string.Empty, "A userId must be supplied to add subscription.");
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                ISubscriptionBusiness subscriptionBusiness = new SubscriptionBusiness(db, roleManager, _userManager);

                var result = subscriptionBusiness.GetSubscriptionPackages();
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "No data existing related to subscription options.");
                }
                var resultSubscription = subscriptionBusiness.GetCurrentSubscription(userId);
                
                if (resultSubscription != null)
                {
                    Input = new InputModel
                    {
                        UserId = userId,
                        Subscriptions = result,
                        CurrentSubscription = resultSubscription,
                        NoOfRigs = commonBusiness.GetNoOfItemsSubscribe(userId)
                };
                }
                else
                {
                    Input = new InputModel
                    {
                        UserId = userId,
                        Subscriptions = result,
                        NoOfRigs = 0
                    };
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Register- subcription page.", User.Identity.Name);

                _logger.LogInformation(ex.Message);
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
            }
            return Page();
        }

        public IActionResult OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                    commonBusiness.UpdateUserSubscription(Input.Subscription, Input.UserId, Convert.ToInt32(Input.NoOfRigs));

                    //Updating page status//
                    commonBusiness.UpdateUserPagesCompleteStatus(6, Input.UserId);

                    return RedirectToPage("PaymentMethod", new { userId = Input.UserId });
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "Register- subcription OnPostAsync.", User.Identity.Name);

                    _logger.LogInformation(ex.Message);
                    ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                }
            }
            return Page();
        }
    }
}

