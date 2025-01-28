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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using Well_AI_Advisior.API.Authorize.Net;
using Well_AI_Advisior.API.Authorize.Net.Model;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;
using WellAI.Advisor.BLL.Business;
 
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class SubscribeModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly ILogger<CompanyModel> _logger;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IWebHostEnvironment _env;
        private IMapper _mapper;
        private TenantOperatingDbContext _tdbContext;
        private readonly IConfiguration _configuration;


        public SubscribeModel(
            IWebHostEnvironment env,
            UserManager<WellIdentityUser> userManager,
            SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<CompanyModel> logger,
            WebAIAdvisorContext dbContext,
            IMapper mapper,
            TenantOperatingDbContext tdbContext, IConfiguration configuration

           )
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
            _tdbContext = tdbContext;
            this._configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            public string UserId { get; set; }
            public int NoOfRigs { get; set; }
            public CrmCompanies Company { get; set; }
            public CrmPaymentMethods PaymentMethod { get; set; }
            
            public SubscriptionPackage  Subscription { get; set; }
            
        }

        public async Task<IActionResult> OnGet(string userId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError(string.Empty, "A userId must be supplied to add subscription.");
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                IPaymentMethodBusiness paymentMethodBusiness = new PaymentMethodBusiness(db, roleManager, _userManager);
                ISubscriptionBusiness subscriptionBusiness = new SubscriptionBusiness(db, roleManager, _userManager);
                
                //getting company detail//
                var resultCompany = commonBusiness.GetCompanyDetail(userId);

                //Get all categories
                if (resultCompany != null)
                {
                    resultCompany.StateRegionName = commonBusiness.GetStateRegion(Convert.ToInt32(resultCompany.StateRegion));
                    if (!string.IsNullOrEmpty(resultCompany.Category))
                    {
                        var userCategories = resultCompany.Category.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();

                        var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                        var categories = await auctionProposalBusiness.GetServiceCategorys();
                        var usercategory = categories.Where(x => userCategories.Any(y => y == x.ServiceCategoryId.ToString())).Select(x => x.Name).ToList();
                        var category = usercategory.Aggregate("", (current, next) => current + ", " + next).TrimStart(',').TrimStart(' ');

                        if (category != null)
                            resultCompany.CategoryName = category;
                    }
                }

                
                //getting noof items purcharsed
                var noOfItems = commonBusiness.GetNoOfItemsSubscribe(userId);

                //getting Subscription//
                var subscriptionPackage = subscriptionBusiness.GetSubscription(userId);

                if (subscriptionPackage != null)
                {
                    subscriptionPackage.PackageAmount = Convert.ToString(noOfItems * Convert.ToDouble(subscriptionPackage.PackageAmount.Replace("$", " ").Trim()));
                }

                //getting Subscription//
                var resultPayment = await paymentMethodBusiness.GetPaymentMethod(userId);

                if(resultPayment != null)
                {
                    resultPayment.CreditCardNumber = await paymentMethodBusiness.DecryptData(resultPayment.CreditCardNumber);
                    resultPayment.ValidUptoDate = await paymentMethodBusiness.DecryptData(resultPayment.ValidUptoDate);
                }

                Input = new InputModel
                {
                    UserId = userId,
                    Company = resultCompany,                   
                    Subscription = subscriptionPackage,
                    PaymentMethod = resultPayment,
                    NoOfRigs = noOfItems
                    
                };
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");

                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Register- Subscribe Page", User.Identity.Name);

                _logger.LogInformation(ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = "/login";
            if (ModelState.IsValid)
            {
                try
                {
                    PaymentMethod paymentModel = new PaymentMethod();
                    ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                    IPaymentMethodBusiness paymentMethodBusiness = new PaymentMethodBusiness(db, roleManager, _userManager);
                    ISubscriptionBusiness subscriptionBusiness = new SubscriptionBusiness(db, roleManager, _userManager);

                    //getting Subscription//
                    var resultPayment = await paymentMethodBusiness.GetPaymentMethod(Input.UserId);
                    //getting Subscription//
                    var resultSubscription = subscriptionBusiness.GetSubscription(Input.UserId);

                    //getting noof items purcharsed
                    var noOfItems = commonBusiness.GetNoOfItemsSubscribe(Input.UserId);

                    string invoiceId = string.Empty;

                    var packageAmount = Convert.ToInt32(Convert.ToDouble(resultSubscription.PackageAmount.Replace("$", "")));

                    SubscriptionViewModel subscriptionViewModel = new SubscriptionViewModel
                    {
                        PackageId = Convert.ToString(resultSubscription.PackageId),
                        RigCount = Convert.ToString(noOfItems),
                        TotalAmount = Convert.ToString(noOfItems * packageAmount)
                        
                       
                    };
                    ViewData["RigCount"] = Convert.ToString(noOfItems);

                    //PackageTypeEnum trialOccurrences = (PackageTypeEnum)Enum.Parse(typeof(PackageTypeEnum), jsonRequest.PackageType, true);
                    var services = new ServiceCollection();
                    services.UseServices();
                    var serviceProvider = services.BuildServiceProvider();
                    var service = serviceProvider.GetRequiredService<IRecurringBillingService>();

                    Interval interval = new Interval();
                    interval.length = 1;
                    interval.unit = 1;  // Monthly
                    PaymentSchedule paySechedule = new PaymentSchedule();
                    paySechedule.totalOccurrences = 12;
                    paySechedule.trialOccurrences = (short)1;

                    //Phase II Changes - 03/05/2021

                    var creditCardNumber = await paymentMethodBusiness.DecryptData(Convert.ToString(resultPayment.CreditCardNumber));
                    var expirationDate = await paymentMethodBusiness.DecryptData(Convert.ToString(resultPayment.ValidUptoDate));
                    var creditCard = new CreditCardType()
                    {
                        CardNumber = creditCardNumber,
                        ExpirationDate = expirationDate
                    };

                    //expirationDate - split by "-" instead of "/"
                    paymentModel.Number = creditCardNumber;// resultPayment.CreditCardNumber;
                    if (expirationDate.Split("-").Length > 0)
                    {
                        paymentModel.ExpireMonth = Convert.ToString(expirationDate.Split("-")[0]);
                        paymentModel.ExpireYear = Convert.ToString(expirationDate.Split("-")[1]);
                    }
                   
                    CustomerAddressType addressType = new CustomerAddressType();
                    paymentModel.FirstName = addressType.FirstName = resultPayment.CustomerName;
                    addressType.LastName = addressType.FirstName;
                    PaymentType _payment = new PaymentType();
                    _payment.Item = creditCard;
                    SubscriptionType subType = new SubscriptionType();
                    subType.amount = decimal.Parse(Convert.ToString(noOfItems * packageAmount));
                    subType.trialAmount = 0;
                    subType.paymentSchedule = paySechedule;
                    subType.paymentSchedule.interval = new Interval();
                    subType.paymentSchedule.interval = interval;
                    subType.billTo = new CustomerAddressType();
                    subType.billTo = addressType;
                    subType.payment = _payment;

                    var response = service.CreateSubscription(creditCard, subType,"SandBox");

                    if (!string.IsNullOrEmpty(response.SubscriptionId))
                    {
                        string subscriptionId = response.SubscriptionId;
                        invoiceId = await SaveSubscriptionDetail(paymentModel, subscriptionId, Input.UserId, subscriptionViewModel);

                        //Paid status user payment//
                        var updatePayment = commonBusiness.UpdateUserPaymentStatus(1, Input.UserId);
                        if (updatePayment == true)
                        {
                            //Updating page status//
                            commonBusiness.UpdateUserPagesCompleteStatus(8, Input.UserId);
                            
                            return RedirectToPage("Login");
                            
                        }
                    }
                    else
                    {
                     if(response.Message!="")
                        {
                            ModelState.AddModelError(string.Empty, response.Message);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Your transaction is failed please contact with support team.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");

                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "Register- Subscribe Onpost", User.Identity.Name);

                    _logger.LogInformation(ex.Message);
                }
            }
            return Page();
        }

        private async Task<string> SaveSubscriptionDetail(PaymentMethod paymentModel, string subscriptionId, string userId, SubscriptionViewModel subscriptionDetail)
        {
            string invoiceId = string.Empty;
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                var tenantId = commonBusiness.GetTenantIdByUserId(userId).Result;

                await AddProductSubscription(subscriptionDetail, subscriptionId, tenantId);
                var opprepo = new OperatingTenantRepository(db);
                invoiceId = await opprepo.CreateBillingInvoice(paymentModel, subscriptionDetail, subscriptionId, tenantId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");

                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Register- Subscribe Page", User.Identity.Name);

                _logger.LogInformation(ex.Message);
            }
            return invoiceId;
        }

        public async Task<int> AddProductSubscription(SubscriptionViewModel sbscriptionDetail, string subscriptionId, string tenantId)
        {
            int result = 0;
            bool isActive = string.IsNullOrEmpty(subscriptionId) ? false : true;
            var item = (from x in db.Subscription
                        where x.TenantId.Equals(tenantId)
                        select x).FirstOrDefault();
            if (item == null)
            {
                ProductSubscriptionModel pr = new ProductSubscriptionModel();
                pr.ID = Guid.NewGuid();
                pr.SubscriptionId = subscriptionId;
                pr.IsPaid = isActive;
                pr.IsEnable = isActive;
                pr.TenantId = tenantId;
                pr.SubscriptionCount = int.Parse(sbscriptionDetail.RigCount);
                pr.PackageId = sbscriptionDetail.PackageId;
                pr.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                pr.SubStartdate = DateTime.Now;
                db.Subscription.Add(pr);
                result = await db.SaveChangesAsync();
                
            }
            return result;
        }
    }
}

