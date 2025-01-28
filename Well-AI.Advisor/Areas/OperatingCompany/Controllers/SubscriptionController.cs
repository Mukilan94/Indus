using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    public class SubscriptionController : BaseController
    {
        private readonly ILogger<SubscriptionController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private TenantOperatingDbContext _tdbContext;
        private readonly IConfiguration _configuration;
        public SubscriptionController(
            UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext,
           TenantOperatingDbContext tdbContext,
           ILogger<SubscriptionController> logger,IConfiguration configuration) : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _tdbContext = tdbContext;
            this._configuration = configuration;
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> Index(IFormCollection formData)
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                //checking invalid user//
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                string val = formData["item.PackageId"].ToString();
                Guid PackageId = new Guid(val);
                SubscriptionPackageViewModel viewModel = new SubscriptionPackageViewModel();
                var packageDetail = db.SubscriptionPackage.Where(x => x.IsActive == true && x.PackageId == PackageId).FirstOrDefault();
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var oldSubscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                SubscriptionPackageModel subscriptionPackageDetail = new SubscriptionPackageModel()
                {
                    PackageName = packageDetail.PackageName,
                    AccountType = packageDetail.AccountType,
                    AccountTypeDescription = packageDetail.AccountTypeDescription,
                    IsActive = packageDetail.IsActive,
                    PackageAmount = packageDetail.PackageAmount,
                    PackageId = packageDetail.PackageId,
                    PackageOrder = packageDetail.PackageOrder,
                    Length = packageDetail.Length,
                    Unit = packageDetail.Unit,
                    TotalOccurrences = packageDetail.TotalOccurrences,
                    TrialOccurrences = packageDetail.TrialOccurrences,
                    NewPackageAmount = packageDetail.PackageAmount
                };
                if (oldSubscription != null && oldSubscription.PackageId == val)
                {
                    subscriptionPackageDetail.Unit = oldSubscription.SubscriptionCount;
                    subscriptionPackageDetail.PackageAmount = "$" + oldSubscription.PackageAmount;
                }
                viewModel.SubscriptionPackageModel = subscriptionPackageDetail;
                var paytypes = new List<SelectListItem>();
                var paytypelist = db.PaymentType.ToList();
                for (int i = 0; i < paytypelist.Count; i++)
                {
                    paytypes.Add(new SelectListItem
                    {
                        Text = paytypelist[i].Name,
                        Value = paytypelist[i].ID
                    });
                }
                ViewData["PayTypes"] = paytypes;
                ViewData["PackageType"] = packageDetail.Name;
                List<PaymentMethod> model = new List<PaymentMethod>();
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                model = await operrepo.GetPaymentMethods(tenantId);
                if (model == null && model.Any())
                    model = new List<PaymentMethod>();
                var PayMethod = (from pay in model
                                 join countyList in db.Countries on pay.Country equals countyList.ID into C1
                                 from countryresult in C1.DefaultIfEmpty()
                                 join state in db.USAStates on pay.State equals state.StateId.ToString()
                                 select new
                                 {
                                     pay.Default,
                                     pay.AccountName,
                                     pay.AccountNumber,
                                     pay.Address1,
                                     pay.Address2,
                                     pay.CardType,
                                     pay.CheckNumber,
                                     pay.ExpireMonth,
                                     pay.ExpireYear,
                                     pay.FirstName,
                                     pay.LastName,
                                     pay.MiddleInitial,
                                     pay.Holder,
                                     pay.ID,
                                     pay.Nickname,
                                     pay.Number,
                                     pay.PayType,
                                     pay.Country,
                                     pay.RoutingNumber,
                                     pay.State,
                                     pay.Zip,
                                     pay.City,
                                     pay.Agreement,
                                     countryresult.Name,
                                     state.StateId,
                                     USAStates = state.Name
                                 }).ToList();
                List<PaymentMethodViewModel> payViewModel = new List<PaymentMethodViewModel>();
                foreach (var item in PayMethod)
                {
                    //ValidTill = await operrepo.DecryptData(item.ExpireMonth.ToString()) + "/" + await operrepo.DecryptData(item.ExpireYear.ToString()),
                    // "/" changed to "-" to compatible with other logics
                    PaymentMethodViewModel payDetail = new PaymentMethodViewModel()
                    {
                        Default = item.Default,
                        AccountName = item.AccountName,
                        AccountNumber = item.AccountNumber,
                        Address = item.Address1 + item.Address2,
                        Agreement = item.Agreement,
                        CardType = item.CardType,
                        CheckNumber = item.CheckNumber,
                        City = item.City,
                        Country = item.Country,
                        ValidTill = await operrepo.DecryptData(item.ExpireMonth.ToString()) + "-" + await operrepo.DecryptData(item.ExpireYear.ToString()),
                        FullName = item.FirstName + " " + item.MiddleInitial + " " + item.LastName,
                        FullAddress = item.Address1 + " " + item.Address2,
                        Holder = item.Holder,
                        ID = item.ID,
                        Nickname = item.Nickname,
                        Number = await operrepo.DecryptData(item.Number),
                        PayType = item.PayType,
                        RoutingNumber = item.RoutingNumber,
                        State = item.State,
                        Zip = item.Zip,
                        CountryName = item.Name,
                        StateName = item.USAStates
                    };


                    payViewModel.Add(payDetail);
                }
                viewModel.PaymentDetail = payViewModel;
                return View(viewModel);
            }
            catch(Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                var userIdentity = (ClaimsIdentity)User.Identity;
                errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                return null;
            }
         }
        
        [HttpPost]
        public async Task<JsonResult> CreateSubscription([FromBody] SubscriptionViewModel jsonRequest)
        {
            try
            {
                var authorizeNetSection = _configuration.GetSection("AuthorizeNet");
                string invoiceId = string.Empty;
                PackageTypeEnum trialOccurrences = (PackageTypeEnum)Enum.Parse(typeof(PackageTypeEnum), jsonRequest.PackageType, true);
                PaymentMethod paymentModel = new PaymentMethod();
                var opprepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                paymentModel = await opprepo.GetSelectedPaymentDetail(jsonRequest.AddressId);
                var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(jsonRequest.PackageId)).FirstOrDefault();
                var services = new ServiceCollection();
                services.UseServices();
                var serviceProvider = services.BuildServiceProvider();
                var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
                Interval interval = new Interval();
                interval.length = (short)subscriptionPackage.Length;
                interval.unit = subscriptionPackage.Unit;  // Monthly
                PaymentSchedule paySechedule = new PaymentSchedule();
                paySechedule.totalOccurrences = (short)subscriptionPackage.TotalOccurrences;
                paySechedule.trialOccurrences = (short)subscriptionPackage.TrialOccurrences;
                paySechedule.startDate = DateTime.Now.ToString();
                var creditCard = new CreditCardType()
                {
                    CardNumber = paymentModel.Number,
                    ExpirationDate = paymentModel.ExpireYear + "-" + paymentModel.ExpireMonth,
                };
                CustomerAddressType addressType = new CustomerAddressType();
                addressType.FirstName = paymentModel.FirstName;
                addressType.LastName = paymentModel.LastName;
                PaymentType _payment = new PaymentType();
                _payment.Item = creditCard;
                SubscriptionType subType = new SubscriptionType();
                subType.amount = Convert.ToInt32(jsonRequest.RigCount) * Convert.ToDecimal(subscriptionPackage.PackageAmount.Replace("$", "").ToString());
                subType.trialAmount = 0;
                subType.paymentSchedule = paySechedule;
                subType.paymentSchedule.interval = new Interval();
                subType.paymentSchedule.interval = interval;
                subType.billTo = new CustomerAddressType();
                subType.billTo = addressType;
                subType.payment = _payment;
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var oldSubscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                SubscriptionResponse response = new SubscriptionResponse();
                if (oldSubscription == null)
                {
                    response = service.CreateSubscription(creditCard, subType, authorizeNetSection["AccountType"]);
                }
                else
                {
                    if (oldSubscription.SubscriptionCount == Convert.ToInt32(jsonRequest.RigCount) && jsonRequest.PackageId == oldSubscription.PackageId)
                    {
                        return Json(new { message = @$"You have been already subscribed {jsonRequest.RigCount} Rigs." }); // @$"You have been already Subscription total {jsonRequest.RigCount} Rigs.";
                    }
                    ANetApiResponse response1 = service.UpdateSubscription(creditCard, subType, oldSubscription.SubscriptionId, authorizeNetSection["AccountType"]);
                    invoiceId = await SaveSubscriptionDetail(paymentModel, oldSubscription.SubscriptionId, jsonRequest);
                    return Json(new { message = "", data = new { name = subscriptionPackage.Name, packageAmount = subType.amount, jsonRequest.RigCount, description = subscriptionPackage.Description, invoiceId } });
                }
                if (!string.IsNullOrEmpty(response.SubscriptionId))
                {
                    string subscriptionId = response.SubscriptionId;
                    invoiceId = await SaveSubscriptionDetail(paymentModel, subscriptionId, jsonRequest);
                    return Json(new { message = "", data = new { name = subscriptionPackage.Name, packageAmount = subType.amount, jsonRequest.RigCount, description = subscriptionPackage.Description, invoiceId } });
                }
                else
                {
                    //Phase II Changes - 03/26/2021
                    if (response != null)
                    {
                        return Json(new
                        {
                            message = response.Message
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            message = ""
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                var userIdentity = (ClaimsIdentity)User.Identity;
                errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        private async Task<string> SaveSubscriptionDetail(PaymentMethod paymentModel, string subscriptionId, SubscriptionViewModel subscriptionDetail)
        {
            string invoiceId = string.Empty;
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                await AddProductSubscription(subscriptionDetail, subscriptionId, tenantId);
                var opprepo = new OperatingTenantRepository(db);
                invoiceId = await opprepo.CreateBillingInvoice(paymentModel, subscriptionDetail, subscriptionId,tenantId);
            }
            catch (Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                var userIdentity = (ClaimsIdentity)User.Identity;
                errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                return ex.Message;
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
                ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                subscription.ID = Guid.NewGuid();
                subscription.SubscriptionId = subscriptionId;
                subscription.IsPaid = isActive;
                subscription.IsEnable = isActive;
                subscription.TenantId = tenantId;
                subscription.SubscriptionCount = int.Parse(sbscriptionDetail.RigCount);
                subscription.PackageId = sbscriptionDetail.PackageId;
                subscription.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                subscription.SubStartdate = DateTime.Now;
                db.Subscription.Add(subscription);
                result = await db.SaveChangesAsync();
            }
            else
            {               
                item.SubscriptionId = subscriptionId;
                item.IsPaid = isActive;
                item.IsEnable = isActive;
                item.TenantId = tenantId;
                item.SubscriptionCount = int.Parse(sbscriptionDetail.RigCount);
                item.PackageId = sbscriptionDetail.PackageId;
                item.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                db.Subscription.Update(item);
                result = await db.SaveChangesAsync();
            }
            return result;
        }
        private bool GetComponentsBasedOnRole()
        {
            var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            List<string> rolesName = (from rl in roles
                                      select rl.Value
                                 ).ToList();

            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
            var rolesResult = roleResult.Result;
            if (roleResult != null)
            {
                List<string> roleIds = (from rl in rolesResult
                                        select rl.Id
                                        ).ToList();
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "ViewDashboard",TenantId);
            }
            else
            {
                return false;
            }
        }

        public ActionResult LoadPopup()
        {
            return PartialView("_ModalPopup");
        }
    }
}

