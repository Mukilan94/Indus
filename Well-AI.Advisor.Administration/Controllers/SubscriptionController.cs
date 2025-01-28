using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using Finbuckle.MultiTenant;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
using WellAI.Advisor;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.OperatingCompany.Models;
using SubscriptionViewModel = WellAI.Advisor.Model.OperatingCompany.Models.SubscriptionViewModel;

namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    //[SessionTimeOut]
    public class SubscriptionController : BaseController
    {
        //Phase II - Clear Warning
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<StaffWellIdentityUser> _userManager;
        public SubscriptionController(IConfiguration configuration, ISingletonAdministration singleton, WebAIAdvisorContext db,
            RoleManager<IdentityRole> roleManager,
            UserManager<StaffWellIdentityUser> userManager) :base(configuration,singleton,db)
        {
            this.db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
               
        [AcceptVerbs("Post")]
        public IActionResult Index(IFormCollection formData)
        {
            try
            {
                string tenantId = formData["TenantId"].ToString();

                TempData["TenantId"] = tenantId;

                string val = formData["item.PackageId"].ToString();
                int accountType = Convert.ToInt32(formData["item.AccountType"]) == 2 ? 0 : 1;
                Guid PackageId = new Guid(val);
                SubscriptionPackageViewModel viewModel = new SubscriptionPackageViewModel();
                var packageDetail = db.SubscriptionPackage.Where(x => x.IsActive == true && x.PackageId == PackageId).FirstOrDefault();
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
                    TenantId = tenantId,
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
                var dbprefix = "oper";
                if (accountType == 1)
                    dbprefix = "serv";

                var tId = Guid.Parse(tenantId);

                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + tId.ToString("N"));

                var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);
                var operContext = new TenantOperatingDbContext(ti);
                model = db.PaymentMethods.Where(x=>x.TenantId==tenantId).ToList();
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
                    //ValidTill - / changed to - in order to compatible with other logic
                    //ValidTill = !item.ExpireMonth.All(Char.IsDigit) ? Encoding.UTF32.GetString(Convert.FromBase64String(item.ExpireMonth)) + "/" + Encoding.UTF32.GetString(Convert.FromBase64String(item.ExpireYear)) : item.ExpireMonth.ToString() + "/" + item.ExpireYear.ToString(),
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
                        
                        ValidTill = !item.ExpireMonth.All(Char.IsDigit) ? Encoding.UTF32.GetString(Convert.FromBase64String(item.ExpireMonth)) + "-" + Encoding.UTF32.GetString(Convert.FromBase64String(item.ExpireYear)) : item.ExpireMonth.ToString() + "/" + item.ExpireYear.ToString(),
                        FullName = item.FirstName + " " + item.MiddleInitial + " " + item.LastName,
                        FullAddress = item.Address1 + " " + item.Address2,
                        Holder = item.Holder,
                        ID = item.ID,
                        Nickname = item.Nickname,
                        Number = !item.ExpireMonth.All(Char.IsDigit) ? Encoding.UTF32.GetString(Convert.FromBase64String(item.Number)) : item.Number,
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Subscription index", User.Identity.Name);
                
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> CreateSubscription([FromBody]SubscriptionViewModel jsonRequest)
        {
            try
            {
                string invoiceId = string.Empty;
                PackageTypeEnum trialOccurrences = (PackageTypeEnum)Enum.Parse(typeof(PackageTypeEnum), jsonRequest.PackageType, true);
                PaymentMethod paymentModel = new PaymentMethod();
                string tenantId = jsonRequest.TenantId;
                var dbprefix = "oper";
                
                if (jsonRequest.AccountType == 1)
                    dbprefix = "serv";

                var tId = Guid.Parse(tenantId);

                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + tId.ToString("N"));

                var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);
                var operContext = new TenantOperatingDbContext(ti);

                paymentModel = db.PaymentMethods.Where(x => x.ID == jsonRequest.AddressId).FirstOrDefault();//await opprepo.GetSelectedPaymentDetail(jsonRequest.AddressId);
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
                var oldSubscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                SubscriptionResponse response = new SubscriptionResponse();
                if (oldSubscription == null)
                {
                    response = service.CreateSubscription(creditCard, subType,"");
                }
                else
                {
                    if (oldSubscription.SubscriptionCount == Convert.ToInt32(jsonRequest.RigCount) && jsonRequest.PackageId == oldSubscription.PackageId)
                    {
                        return Json(new { message = @$"You have been already subscribed {jsonRequest.RigCount} Rigs." }); // @$"You have been already Subscription total {jsonRequest.RigCount} Rigs.";

                    }
                    subType.paymentSchedule.startDate = oldSubscription.SubStartdate.ToString();
                    ANetApiResponse response1 = service.UpdateSubscription(creditCard, subType, oldSubscription.SubscriptionId,"");
                    invoiceId = await SaveSubscriptionDetail(paymentModel, oldSubscription.SubscriptionId, jsonRequest, tenantId);
                    return Json(new { message = "", data = new { name = subscriptionPackage.Name, packageAmount = subType.amount, jsonRequest.RigCount, description = subscriptionPackage.Description, invoiceId } });
                }

                if (!string.IsNullOrEmpty(response.SubscriptionId))
                {
                    string subscriptionId = response.SubscriptionId;
                    invoiceId = await SaveSubscriptionDetail(paymentModel, subscriptionId, jsonRequest, tenantId);
                    return Json(new { message = "", data = new { name = subscriptionPackage.Name, packageAmount = subType.amount, jsonRequest.RigCount, description = subscriptionPackage.Description, invoiceId } });
                }
                else
                {
                    return Json(new
                    {
                        message = response.Message
                    });
                }
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Subscription CreateSubscription", User.Identity.Name);
                
                return null;
            }
        }

        private async Task<string> SaveSubscriptionDetail(PaymentMethod paymentModel, string subscriptionId, SubscriptionViewModel subscriptionDetail,string tenantId)
        {
            string invoiceId = string.Empty;
            try
            {
                await AddProductSubscription(subscriptionDetail, subscriptionId, tenantId);

                string InvoiceId = Guid.NewGuid().ToString("D");
                BillingHistory billingDetail = new BillingHistory()
                {
                    ID = InvoiceId,
                    Name = paymentModel.Holder,
                    Subscriptions = int.Parse(subscriptionDetail.RigCount),
                    Invoice = subscriptionId,
                    Amount = double.Parse(subscriptionDetail.TotalAmount),
                    BillDate = DateTime.Now,
                    PayMethod = paymentModel.PayType,
                    AddressId = subscriptionDetail.AddressId,
                    TenantId = tenantId,
                };
                db.BillingHistoryInvoices.Add(billingDetail);
                var result = await db.SaveChangesAsync();
                return InvoiceId;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Subscription SaveSubscriptionDetail", User.Identity.Name);
                
            }
            return invoiceId;
        }

        public async Task<int> AddProductSubscription(SubscriptionViewModel sbscriptionDetail, string subscriptionId, string tenantId)
        {
            try
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
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Subscription AddProductSubscription", User.Identity.Name);
                
                return 0;
            }
        }


        #region Subscription Package


        public ActionResult Subscription_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var serviceCategory = _singleton.subscriptionBusiness.GetSubscriptions().Result;

                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Subscription Subscription_Read", User.Identity.Name);
                
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Subscription_Create([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.Administration.SubscriptionViewModel input)
        {
            try
            {
                var result = await _singleton.subscriptionBusiness.AddSubscription(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }

                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Subscription Subscription_Create", User.Identity.Name);
                
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Subscription_Update([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.Administration.SubscriptionViewModel input)
        {
            try
            {
                ModelState.Remove("Password");

                var result = await _singleton.subscriptionBusiness.UpdateSubscription(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Subscription Subscription_Update", User.Identity.Name);
                
                return null;
            }
        }
        #endregion
    }
}