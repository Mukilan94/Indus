using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public class CompanyModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly ILogger<CompanyModel> _logger;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IWebHostEnvironment _env;
        private IMapper _mapper;
        public CompanyModel(
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
            _roleManager = roleManager;
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

        public List<USAState> States { get; set; }
        public List<ServiceCategory> Categories { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Company Name")]
            public string CompanyName { get; set; }

            [Required]
            public string UserId { get; set; }

            [Required]
            [Display(Name = "Street 1")]
            public string Street1 { get; set; }

            [Display(Name = "Street 2")]
            public string Street2 { get; set; }

            [Required]
            [Display(Name = "City")]
            public string City { get; set; }

            [Required]
            [Display(Name = "State")]
            public string State { get; set; }

            [Required]
            [Display(Name = "Zip")]
            public string Zip { get; set; }

            [Url]
            [Display(Name = "Web site")]
            public string Website { get; set; }

            [Required]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
            public string Phone { get; set; }

            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
            public string Fax { get; set; }

            [Required]
            public string Category { get; set; }

            [Required]
            public string EIN { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string userId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError(string.Empty, "A userId must be supplied to add company.");
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);

                var result = commonBusiness.GetUserBasicDetail(userId);

                var resultCompany = commonBusiness.GetCompanyDetail(userId);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "invalid user account.");
                }

                if (resultCompany == null)
                {
                    Input = new InputModel
                    {
                        UserId = userId,
                        CompanyName = result.Company
                    };
                }
                else
                {
                    Input = new InputModel
                    {
                        UserId = userId,
                        CompanyName = result.Company,
                        Street1 = resultCompany.Address1,
                        Street2 = resultCompany.Address2,
                        City = resultCompany.City,
                        State = resultCompany.StateRegion,
                        Zip = resultCompany.PostalCode,
                        Website = resultCompany.Website,
                        Phone = resultCompany.Phone,
                        Fax = resultCompany.Fax,
                        Category = resultCompany.Category,
                        EIN = resultCompany.EIN,
                    };
                }
                States = await commonBusiness.GetUSAStates();

                Categories = await auctionProposalBusiness.GetServiceCategorys();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Company OnGet", User.Identity.Name);

                _logger.LogInformation(ex.Message);
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

            var tenantId = await commonBusiness.GetTenantIdByUserId(Input.UserId);

            if (ModelState.IsValid && !string.IsNullOrEmpty(tenantId))
            {
                try
                {
                    var crmCompanyDetail = new CrmCompanies
                    {
                        UserId = Input.UserId,
                        Name = Input.CompanyName,
                        Address1 = Input.Street1,
                        Address2 = Input.Street2,
                        City = Input.City,
                        StateRegion = Input.State,
                        PostalCode = Input.Zip,
                        Phone = Input.Phone,
                        Fax = Input.Fax,
                        Website = Input.Website,
                        Category = Input.Category,
                        EIN = Input.EIN,
                        TenantId = tenantId
                    };

                    var status = commonBusiness.CreateCompanyDetail(crmCompanyDetail);
                    if (status)
                    {
                        await commonBusiness.UpdateCorporateProfile(
                            new Model.OperatingCompany.Models.CorporateProfile
                            {
                                Address1 = Input.Street1,
                                Address2 = Input.Street2,
                                City = Input.City,
                                Name = Input.CompanyName,
                                Phone = Input.Phone,
                                State = Input.State,
                                Website = Input.Website,
                                Zip = Input.Zip
                            }, Input.UserId, tenantId);

                        //Updating page status//
                        commonBusiness.UpdateUserPagesCompleteStatus(4, Input.UserId);
                        commonBusiness.UpdateUserPagesCompleteStatus(5, Input.UserId);
                        return RedirectToPage("Subscription", new { userId = Input.UserId });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "server side error is coming.");
                    }
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "Company OnPost", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                    ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                }
            }
            return Page();
        }

        /// <summary>
        /// Call from multiselect on edit from to setup initial categories of company
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JsonResult> OnGetSetSelectedCategories(string userId)
        {
            List<object> result = new List<object>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var categories = await auctionProposalBusiness.GetServiceCategorys();
                var company = commonBusiness.GetCompanyDetail(userId);

                if (!string.IsNullOrEmpty(company.Category))
                {
                    var userCats = company.Category.Split(";").ToList();

                    foreach (var category in categories)
                    {
                        if (userCats.Any(x=>x == category.ServiceCategoryId.ToString()))
                        {
                            result.Add(new { ServiceCategoryId = category.ServiceCategoryId, Name = category.Name });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Company OnGetSetSelectedCategories", User.Identity.Name);
            }

            return new JsonResult(result);
        }
    }
}

