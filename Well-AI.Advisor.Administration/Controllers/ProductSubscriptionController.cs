using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Kendo.Mvc.UI;
using AuthorizeNet.Api.Contracts.V1;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Areas.ServiceCompany.Controllers;
using WellAI.Advisor.DLL.Entity;
using Well_AI_Advisior.API.Authorize.Net;
using Microsoft.Extensions.Configuration;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;
using Well_AI_Advisior.API.Authorize.Net.Model;
using Microsoft.Extensions.DependencyInjection;
using AuthorizeNet.Api.Contracts.V1;
using WellAI.Advisor.API.Repository;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc.Rendering;
using WellAI.Advisor;

using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.BLL.Administration;


namespace Well_AI.Advisor.Administration.Controllers
{
   // [Area("OperatingCompany")]
 //   [SessionTimeOut]
    public class ProductSubscriptionController : BaseController
    {
        //private readonly ILogger<ProductSubscriptionController> _logger;
        //private readonly SignInManager<WellIdentityUser> _signInManager;
        //private readonly UserManager<WellIdentityUser> _userManager;
        //RoleManager<IdentityRole> _roleManager;
        //private readonly WebAIAdvisorContext db;
        //private TenantOperatingDbContext _tdbContext;
        //private TenantServiceDbContext _servicedb;
        //private readonly IConfiguration _configuration;
        //public ProductSubscriptionController(
        //    UserManager<WellIdentityUser> userManager,
        //   SignInManager<WellIdentityUser> signInManager,
        //   RoleManager<IdentityRole> roleManager,
        //   WebAIAdvisorContext dbContext,
        //   TenantOperatingDbContext tdbContext,
        //   IConfiguration configuration, UserManager<StaffWellIdentityUser> staffUserManager,
        //   ILogger<ProductSubscriptionController> logger) : base(userManager, dbContext)
        //{
        //    _logger = logger;
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _roleManager = roleManager;
        //    db = dbContext;
        //    _configuration = configuration;
        //    _tdbContext = tdbContext;

        //}

        private readonly ILogger<ProductSubscriptionController> _logger;
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
        //Phase II - Clear Warning

        //Phase II Changes-01/12/2021 - Add OperatingDBContext to Read Vendor Directory
        private TenantOperatingDbContext _tdbContext;
        private readonly TenantOperatingDbContext _operdb;
        private ISession _session;
        private TenantServiceDbContext _servicedb;
        private readonly IConfiguration _configuration;
      //  private readonly UserManager<WellIdentityUser> _userManager;
        public ProductSubscriptionController(IConfiguration _configuration, UserManager<WellIdentityUser> userManager,
          RoleManager<IdentityRole> roleManager,
           ISingletonAdministration _singleton, WebAIAdvisorContext db, TenantOperatingDbContext operdb, TenantOperatingDbContext tdbContext,
           TenantServiceDbContext servicedb, UserManager<StaffWellIdentityUser> staffUserManager, IConfiguration configuratio)
            : base(_configuration, _singleton, db)
        {
            //_userManager = userManager;
            _roleManager = roleManager;
            _operdb = operdb;
            _tdbContext = tdbContext;
            _session = WellAIAppContext.Current.Session;
            _servicedb = servicedb;
            _staffUserManager = staffUserManager;
            //_configuration = configuration;
        }



        public async Task<IActionResult> Index()
        {

            try
            {
                //if (_signInManager.IsSignedIn(User) == false)
                //{
                //    string returnUrl = @"/Identity/Account/Login";
                //    return LocalRedirect(returnUrl);
                //}

                WellAI.Advisor.Model.Common.Retrieve RetriveDetails = new WellAI.Advisor.Model.Common.Retrieve();


                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                //Customer details

                ViewBag.CustomerDetails = db.CorporateProfile.Where(x => x.TenantId.Equals(tenantId)).FirstOrDefault();

                //Subscription details
                ViewBag.CustomerSubscriptions = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).FirstOrDefault();



                string packageId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.PackageId).FirstOrDefault();
                string paymentMethodId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.PaymentMethodId).FirstOrDefault();

                //package details
                ViewBag.CustomerSubscriptionsPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(packageId)).FirstOrDefault();

                ViewBag.PaymentDetails = db.PaymentMethods.Where(x => x.ID == paymentMethodId).FirstOrDefault();
                ViewBag.PackageDetailsList = db.SubscriptionPackage.Where(x => x.PackageId.ToString().ToUpper()
                == "D350AC56-27E8-4442-9F1D-3EB6088DB9DA" || x.PackageId.ToString().ToUpper()
                == "558911F7-876E-40C2-AF4F-388DBD83CB6D" || x.PackageId.ToString().ToUpper()
                == "0D280653-7177-4DD3-9915-6CB83433BF70").OrderBy(x => x.PackageAmount).ToList();

                //---
                var months = new List<SelectListItem> {
                    new SelectListItem {
                        Text = "1- January",
                        Value = "1"
                    },
                    new SelectListItem {
                        Text = "2 - February",
                        Value = "2"
                    },
                    new SelectListItem {
                        Text = "3 - March",
                        Value = "3"
                    },
                    new SelectListItem {
                        Text = "4 - April",
                        Value = "4"
                    },
                    new SelectListItem {
                        Text = "5 - May",
                        Value = "5"
                    },
                    new SelectListItem {
                        Text = "6 - June",
                        Value = "6"
                    },
                    new SelectListItem {
                        Text = "7 - July",
                        Value = "7"
                    },
                    new SelectListItem {
                        Text = "8 - August",
                        Value = "8"
                    },
                    new SelectListItem {
                        Text = "9 - September",
                        Value = "9"
                    },
                    new SelectListItem {
                        Text = "10 - October",
                        Value = "10"
                    },
                    new SelectListItem {
                        Text = "11 - November",
                        Value = "11"
                    },
                    new SelectListItem {
                        Text = "12 - December",
                        Value = "12"
                    },
                };
                ViewData["MonthsData"] = months;
                var years = new List<SelectListItem>();
                var startYear = DateTime.Now.Year;
                for (int i = 0; i < 6; i++)
                {
                    years.Add(new SelectListItem
                    {
                        Text = (startYear + i).ToString(),
                        Value = (startYear + i).ToString()
                    });
                }
                ViewData["YearsData"] = years;
                var states = new List<SelectListItem>();
                var usastates = db.USAStates.ToList();
                for (int i = 0; i < usastates.Count; i++)
                {
                    states.Add(new SelectListItem
                    {
                        Text = usastates[i].Name,
                        Value = usastates[i].StateId.ToString()
                    });
                }
                ViewData["States"] = states;
                var countries = new List<SelectListItem>();
                var countrylist = db.Countries.ToList();
                for (int i = 0; i < countrylist.Count; i++)
                {
                    countries.Add(new SelectListItem
                    {
                        Text = countrylist[i].Name,
                        Value = countrylist[i].ID
                    });
                }
                ViewData["Countries"] = countries;
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
                var creditCardTypes = new List<SelectListItem>();
                var creditCardTypeList = db.CreditcardType.ToList();
                for (int i = 0; i < creditCardTypeList.Count; i++)
                {
                    creditCardTypes.Add(new SelectListItem
                    {
                        Text = creditCardTypeList[i].CardType,
                        Value = creditCardTypeList[i].ID.ToString()
                    });
                }
                ViewData["CreditCardTypes"] = creditCardTypes;

                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Index", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }


        public IActionResult Changenwepaymentop()
        {
            try
            {
                //if (_signInManager.IsSignedIn(User) == false)
                //{
                //    string returnUrl = @"/Identity/Account/Login";
                //    return LocalRedirect(returnUrl);
                //}
                //if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                //{
                //    if (GetComponentsBasedOnRole() == false)
                //    {
                //        string returnUrl = @"/OperatingDashboard";
                //        return LocalRedirect(returnUrl);
                //    }
                //}
                var months = new List<SelectListItem> {
                    new SelectListItem {
                        Text = "1- January",
                        Value = "1"
                    },
                    new SelectListItem {
                        Text = "2 - February",
                        Value = "2"
                    },
                    new SelectListItem {
                        Text = "3 - March",
                        Value = "3"
                    },
                    new SelectListItem {
                        Text = "4 - April",
                        Value = "4"
                    },
                    new SelectListItem {
                        Text = "5 - May",
                        Value = "5"
                    },
                    new SelectListItem {
                        Text = "6 - June",
                        Value = "6"
                    },
                    new SelectListItem {
                        Text = "7 - July",
                        Value = "7"
                    },
                    new SelectListItem {
                        Text = "8 - August",
                        Value = "8"
                    },
                    new SelectListItem {
                        Text = "9 - September",
                        Value = "9"
                    },
                    new SelectListItem {
                        Text = "10 - October",
                        Value = "10"
                    },
                    new SelectListItem {
                        Text = "11 - November",
                        Value = "11"
                    },
                    new SelectListItem {
                        Text = "12 - December",
                        Value = "12"
                    },
                };
                ViewData["MonthsData"] = months;
                var years = new List<SelectListItem>();
                var startYear = DateTime.Now.Year;
                for (int i = 0; i < 6; i++)
                {
                    years.Add(new SelectListItem
                    {
                        Text = (startYear + i).ToString(),
                        Value = (startYear + i).ToString()
                    });
                }
                ViewData["YearsData"] = years;
                var states = new List<SelectListItem>();
                var usastates = db.USAStates.ToList();
                for (int i = 0; i < usastates.Count; i++)
                {
                    states.Add(new SelectListItem
                    {
                        Text = usastates[i].Name,
                        Value = usastates[i].StateId.ToString()
                    });
                }
                ViewData["States"] = states;
                var countries = new List<SelectListItem>();
                var countrylist = db.Countries.ToList();
                for (int i = 0; i < countrylist.Count; i++)
                {
                    countries.Add(new SelectListItem
                    {
                        Text = countrylist[i].Name,
                        Value = countrylist[i].ID
                    });
                }
                ViewData["Countries"] = countries;
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
                var creditCardTypes = new List<SelectListItem>();

                var creditCardTypeList = db.CreditcardType.ToList();

                for (int i = 0; i < creditCardTypeList.Count; i++)
                {
                    creditCardTypes.Add(new SelectListItem
                    {
                        Text = creditCardTypeList[i].CardType,
                        Value = creditCardTypeList[i].ID.ToString()
                    });
                }
                ViewData["CreditCardTypes"] = creditCardTypes;


                //   return View();
                return PartialView("Changenwepaymentop");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethods Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        [AcceptVerbs("Post")]
        public async Task<IActionResult> PaymentMethods_Update([DataSourceRequest] DataSourceRequest request, PaymentMethod method)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                if (method != null && ModelState.IsValid)
                {
                    method.TenantId = tenantId;
                    method.TenantId = tenantId;
                    method.Agreement = true;
                    method.Default = true;
                    // method.ExpireMonth = "";
                    // method.ExpireYear = "";

                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    method = await operrepo.EncryptData(method);
                    var res = await operrepo.UpdatePaymentMethod(method);

                    method = await operrepo.DecryptDataall(method);

                }




            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethodSRV Update", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }


            return Json(new[] { method }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs("Post")]
        public async Task<IActionResult> PaymentMethods_Create([DataSourceRequest] DataSourceRequest request, PaymentMethod method)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            try
            {
                if (method != null && ModelState.IsValid)
                {
                    method.TenantId = tenantId;
                    method.Agreement = true;
                    method.Default = true;
                    // method.ExpireMonth = "";
                    //  method.ExpireYear = "";

                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    method = await operrepo.EncryptData(method);
                    var res = await operrepo.UpdatePaymentMethod(method);
                    method = await operrepo.DecryptDataall(method);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethodSRV Create", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new[] { method }.ToDataSourceResult(request, ModelState));
        }


        public IActionResult Billinginfo()
        {
            return PartialView("Billinginfo");
        }
        public IActionResult addPayment()
        {
            var months = new List<SelectListItem> {
                    new SelectListItem {
                        Text = "1- January",
                        Value = "1"
                    },
                    new SelectListItem {
                        Text = "2 - February",
                        Value = "2"
                    },
                    new SelectListItem {
                        Text = "3 - March",
                        Value = "3"
                    },
                    new SelectListItem {
                        Text = "4 - April",
                        Value = "4"
                    },
                    new SelectListItem {
                        Text = "5 - May",
                        Value = "5"
                    },
                    new SelectListItem {
                        Text = "6 - June",
                        Value = "6"
                    },
                    new SelectListItem {
                        Text = "7 - July",
                        Value = "7"
                    },
                    new SelectListItem {
                        Text = "8 - August",
                        Value = "8"
                    },
                    new SelectListItem {
                        Text = "9 - September",
                        Value = "9"
                    },
                    new SelectListItem {
                        Text = "10 - October",
                        Value = "10"
                    },
                    new SelectListItem {
                        Text = "11 - November",
                        Value = "11"
                    },
                    new SelectListItem {
                        Text = "12 - December",
                        Value = "12"
                    },
                };
            ViewData["MonthsData"] = months;
            var years = new List<SelectListItem>();
            var startYear = DateTime.Now.Year;
            for (int i = 0; i < 6; i++)
            {
                years.Add(new SelectListItem
                {
                    Text = (startYear + i).ToString(),
                    Value = (startYear + i).ToString()
                });
            }
            ViewData["YearsData"] = years;
            var states = new List<SelectListItem>();
            var usastates = db.USAStates.ToList();
            for (int i = 0; i < usastates.Count; i++)
            {
                states.Add(new SelectListItem
                {
                    Text = usastates[i].Name,
                    Value = usastates[i].StateId.ToString()
                });
            }
            ViewData["States"] = states;
            var countries = new List<SelectListItem>();
            var countrylist = db.Countries.ToList();
            for (int i = 0; i < countrylist.Count; i++)
            {
                countries.Add(new SelectListItem
                {
                    Text = countrylist[i].Name,
                    Value = countrylist[i].ID
                });
            }
            ViewData["Countries"] = countries;
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
            var creditCardTypes = new List<SelectListItem>();
            var creditCardTypeList = db.CreditcardType.ToList();
            for (int i = 0; i < creditCardTypeList.Count; i++)
            {
                creditCardTypes.Add(new SelectListItem
                {
                    Text = creditCardTypeList[i].CardType,
                    Value = creditCardTypeList[i].ID.ToString()
                });
            }
            ViewData["CreditCardTypes"] = creditCardTypes;

            //return PartialView("_AssignDispatch");
            return PartialView("addPayment");
            //  return View("_AssignDispatchWithPreview");
        }

        public async Task<IActionResult> PaymentMethods_Read([DataSourceRequest] DataSourceRequest request)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            try
            {
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var model = await operrepo.GetPaymentMethods(tenantId);
                foreach (var Carddetails in model)
                {
                    Carddetails.Number = await operrepo.DecryptData(Carddetails.Number);
                    Carddetails.ExpireMonth = await operrepo.DecryptData(Carddetails.ExpireMonth);
                    Carddetails.ExpireYear = await operrepo.DecryptData(Carddetails.ExpireYear);
                }
                if (model == null)
                    model = new List<PaymentMethod>();
                return Json(model.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethods_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> BillingMethods_Read([DataSourceRequest] DataSourceRequest request)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            try
            {
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                var model = await operrepo.GetBillingMethods(tenantId);
                //foreach (var Carddetails in model)
                //{
                //    Carddetails.Number = await operrepo.DecryptData(Carddetails.Number);
                //    Carddetails.ExpireMonth = await operrepo.DecryptData(Carddetails.ExpireMonth);
                //    Carddetails.ExpireYear = await operrepo.DecryptData(Carddetails.ExpireYear);
                //}
                //if (model == null)
                //    model = new List<CorporateProfile>();
                return Json(model.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethods_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult Paymentlist([DataSourceRequest] DataSourceRequest request)
        {
            List<PaymentMethodmodel> model = new List<PaymentMethodmodel>();
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                model = (from pm in db.PaymentMethods
                         where pm.TenantId == tenantId
                         select new PaymentMethodmodel
                         {
                             ID = pm.ID,
                             FirstName = pm.FirstName,

                         }).OrderBy(x => x.FirstName).ToList();


                //  return Json((PaymentMethods));

                //  model.Add(PaymentMethods);



                return Json(model.ToDataSourceResult(request));

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Paymentlist", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> addnewPaymentMethods_Create([DataSourceRequest] DataSourceRequest request, PaymentMethod method)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            try
            {
                if (method != null && ModelState.IsValid)
                {
                    method.TenantId = tenantId;
                    method.Agreement = true;
                    method.Default = true;
                    method.ExpireMonth = "";
                    method.ExpireYear = "";

                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    method = await operrepo.EncryptData(method);
                    var res = await operrepo.UpdatePaymentMethod(method);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethodSRV Create", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new[] { method }.ToDataSourceResult(request, ModelState));
        }

        //------
        [HttpPost]
        public async Task<IActionResult> UpdateDetails([FromBody] WellAI.Advisor.Model.Common.UpdateSubscriptionViewModel updateSubscriptionViewModel)
        {

            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                string _SubscriptionId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.SubscriptionId).FirstOrDefault();


                // checking authorize.net subscription
                var result = UpdateSubscription(updateSubscriptionViewModel, _SubscriptionId);


                if (result.Status.ToString().ToUpper() != "OK")
                {
                    //return View();
                    return Json("0");
                }
                else
                {



                    UpdateSubscriptionDetails subscriptionDetails = updateSubscriptionViewModel.updateSubscriptionDetails;
                    UpdateCompanyDetails companyDetails = updateSubscriptionViewModel.updateCompanyDetails;
                    UpdatePaymentDetails paymentDetails = updateSubscriptionViewModel.updatePaymentDetails;

                    if (paymentDetails.PaymentMethodId == null)
                    {
                        return View();
                    }

                    var packageId = db.Subscription.Where(s => s.TenantId == tenantId).Select(s => s.PackageId).FirstOrDefault();
                    var CurrentAccountType = db.Subscription.Where(s => s.TenantId == tenantId).Select(s => s.SubscriptionType).FirstOrDefault();




                    int _accountType = 0;
                    string _SubscriptionType = "";
                    int _SubscriptionCount = 0;
                    int _SubscriptionDispatchCount = 0;

                    decimal totalpackageamt_OP = 0;
                    decimal totalpackageamt_DP = 0;
                    decimal totalpackageamt_P = 0;


                    if ((subscriptionDetails.DispatchQuantity != null && subscriptionDetails.OperatorQuantity != null) && (Convert.ToInt32(subscriptionDetails.DispatchQuantity) != 0 && Convert.ToInt32(subscriptionDetails.OperatorQuantity) != 0))
                    {
                        _accountType = 3;
                        _SubscriptionType = "OperatorWithDispatch";
                        _SubscriptionCount = int.Parse(subscriptionDetails.OperatorQuantity);
                        _SubscriptionDispatchCount = int.Parse(subscriptionDetails.DispatchQuantity);

                        totalpackageamt_OP = Convert.ToDecimal(subscriptionDetails.OperatorTotal);
                        totalpackageamt_DP = Convert.ToDecimal(subscriptionDetails.DispatchTotal);
                    }
                    else if ((subscriptionDetails.DispatchQuantity != null && subscriptionDetails.ProviderQuantity != null) && (Convert.ToInt32(subscriptionDetails.DispatchQuantity) != 0 && Convert.ToInt32(subscriptionDetails.ProviderQuantity) != 0))
                    {
                        _accountType = 4;
                        _SubscriptionType = "ProviderWithDispatch";
                        _SubscriptionCount = int.Parse(subscriptionDetails.ProviderQuantity);
                        _SubscriptionDispatchCount = int.Parse(subscriptionDetails.DispatchQuantity);

                        totalpackageamt_DP = Convert.ToDecimal(subscriptionDetails.DispatchTotal);
                        totalpackageamt_P = Convert.ToDecimal(subscriptionDetails.ProviderTotal);
                    }
                    else if (subscriptionDetails.OperatorQuantity != null && Convert.ToInt32(subscriptionDetails.OperatorQuantity) != 0)
                    {
                        _accountType = 0;
                        _SubscriptionType = "Advisor Operator";
                        _SubscriptionCount = int.Parse(subscriptionDetails.OperatorQuantity);

                        totalpackageamt_OP = Convert.ToDecimal(subscriptionDetails.OperatorTotal);
                    }
                    else if (subscriptionDetails.ProviderQuantity != null && Convert.ToInt32(subscriptionDetails.ProviderQuantity) != 0)
                    {
                        _accountType = 1;
                        _SubscriptionType = "Advisor Provider";
                        _SubscriptionCount = int.Parse(subscriptionDetails.ProviderQuantity);

                        totalpackageamt_P = Convert.ToDecimal(subscriptionDetails.ProviderTotal);
                    }
                    else if (subscriptionDetails.DispatchQuantity != null && Convert.ToInt32(subscriptionDetails.DispatchQuantity) != 0)
                    {
                        _accountType = 2;
                        _SubscriptionType = "Dispatch";
                        _SubscriptionCount = int.Parse(subscriptionDetails.DispatchQuantity);
                        _SubscriptionDispatchCount = int.Parse(subscriptionDetails.DispatchQuantity);

                        totalpackageamt_DP = Convert.ToDecimal(subscriptionDetails.DispatchTotal);
                    }

                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);



                    var _ProfileId = "";


                    //corparate profile
                    CorporateProfile CorporateProfile = new CorporateProfile();

                    CorporateProfile = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();

                    CorporateProfile.Name = companyDetails.Name;
                    CorporateProfile.Phone = companyDetails.CompanyPhone;
                    CorporateProfile.Address1 = companyDetails.CompanyAddress1;
                    CorporateProfile.Address2 = companyDetails.CompanyAddress2;
                    CorporateProfile.State = companyDetails.CompanyState;
                    CorporateProfile.City = companyDetails.CompanyCity;
                    CorporateProfile.Zip = companyDetails.CompanyZip;
                    //billing
                    CorporateProfile.BillingAddress1 = companyDetails.BillingAddress1;
                    CorporateProfile.BillingAddress2 = companyDetails.BillingAddress2;
                    CorporateProfile.BillingCity = companyDetails.BillingCity;
                    CorporateProfile.BillingZip = companyDetails.CompanyZip;
                    CorporateProfile.BillingState = companyDetails.CompanyState;
                    CorporateProfile.BillingPhone = companyDetails.BillingPhone;
                    CorporateProfile.BillingEmail = companyDetails.BillingEmail;


                    db.CorporateProfile.Add(CorporateProfile);
                    db.Entry(CorporateProfile).State = EntityState.Modified;
                    db.SaveChanges();

                    //GET PROFILE ID 
                    _ProfileId = CorporateProfile.ID;



                    CrmUserBasicDetail crmUserBasicDetail = new CrmUserBasicDetail();

                    crmUserBasicDetail = db.CrmUserBasicDetail.Where(x => x.CorporateProfileId == _ProfileId).FirstOrDefault();
                    //crmUserBasicDetail = db.CrmUserBasicDetail.Where(x => x.SubscriptionId == _SubscriptionId).FirstOrDefault();
                    if (crmUserBasicDetail != null)
                    {
                        crmUserBasicDetail.Name = companyDetails.Name;
                        crmUserBasicDetail.Company = companyDetails.CName;
                        crmUserBasicDetail.IsMaster = true;
                        crmUserBasicDetail.Title = companyDetails.Title;
                        crmUserBasicDetail.AccountType = _accountType;
                        crmUserBasicDetail.IsActive = true;
                        crmUserBasicDetail.SubscriptionId = _SubscriptionId;
                        crmUserBasicDetail.CorporateProfileId = _ProfileId;
                        crmUserBasicDetail.CreatedDate = DateTime.UtcNow;
                        crmUserBasicDetail.ModifiedDate = DateTime.UtcNow;
                        var status = commonBusiness.UpdateUserBasicDetail(crmUserBasicDetail);
                    }
                    //else
                    //{
                    //    CrmUserBasicDetail newcrmUserBasicDetail = new CrmUserBasicDetail();

                    //    newcrmUserBasicDetail.Name = companyDetails.Name;
                    //    newcrmUserBasicDetail.Company = companyDetails.CName;
                    //    newcrmUserBasicDetail.IsMaster = true;
                    //    newcrmUserBasicDetail.Title = companyDetails.Title;
                    //    newcrmUserBasicDetail.AccountType = _accountType;
                    //    newcrmUserBasicDetail.IsActive = true;
                    //    newcrmUserBasicDetail.SubscriptionId = _SubscriptionId;
                    //    newcrmUserBasicDetail.CorporateProfileId = _ProfileId;
                    //    newcrmUserBasicDetail.CreatedDate = DateTime.UtcNow;
                    //    newcrmUserBasicDetail.ModifiedDate = DateTime.UtcNow;
                    //    var status = commonBusiness.CreateUserBasicDetail(newcrmUserBasicDetail);
                    //}

                    WellAI.Advisor.Model.OperatingCompany.Models.SubscriptionViewModel subscriptionViewModel = new WellAI.Advisor.Model.OperatingCompany.Models.SubscriptionViewModel();

                 
                    UpdateSubscriptionAndPaymentDetails updateSubscriptionAndPaymentDetails = new UpdateSubscriptionAndPaymentDetails();
                    updateSubscriptionAndPaymentDetails.subscriptionViewModel = subscriptionViewModel;


                    //Operator && Dispatch (Operator Qty>0 && Dispatch Qty>0)

                    //  List<SubscriptionPackage> subscriptionlist = new List<SubscriptionPackage>();
                    decimal subscriptionAmt = 0;
                    if (Convert.ToInt32(subscriptionDetails.OperatorQuantity) > 0 && Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0)
                    {

                        //operator

                        ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                        subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                        subscription.PaymentMethodId = paymentDetails.PaymentMethodId;
                        subscription.SubscriptionCount = _SubscriptionCount;
                        subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                        subscription.SubscriptionType = (byte?)_accountType;

                        subscription.PackageAmount = totalpackageamt_OP;

                        db.Subscription.Add(subscription);
                        db.Entry(subscription).State = EntityState.Modified;
                        db.SaveChanges();


                    }

                    else if (Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0 && Convert.ToInt32(subscriptionDetails.ProviderQuantity) > 0)
                    {

                        ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                        subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                        subscription.PaymentMethodId = paymentDetails.PaymentMethodId;
                        subscription.SubscriptionCount = _SubscriptionDispatchCount;
                        subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                        subscription.SubscriptionType = (byte?)_accountType;

                        subscription.PackageAmount = totalpackageamt_DP;

                        db.Subscription.Add(subscription);
                        db.Entry(subscription).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    else if (Convert.ToInt32(subscriptionDetails.OperatorQuantity) > 0)
                    {

                        ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                        subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                        subscription.PaymentMethodId = paymentDetails.PaymentMethodId;

                        subscription.SubscriptionCount = _SubscriptionCount;
                        subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                        subscription.SubscriptionType = (byte?)_accountType;

                        subscription.PackageAmount = totalpackageamt_OP;

                        db.Subscription.Add(subscription);
                        db.Entry(subscription).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else if (Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0)
                    {

                        ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                        subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                        subscription.PaymentMethodId = paymentDetails.PaymentMethodId;

                        subscription.SubscriptionCount = _SubscriptionCount;
                        subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                        subscription.SubscriptionType = (byte?)_accountType;

                        subscription.PackageAmount = totalpackageamt_DP;
                        db.Subscription.Add(subscription);

                        db.Subscription.Add(subscription);
                        db.Entry(subscription).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else if (Convert.ToInt32(subscriptionDetails.ProviderQuantity) > 0)
                    {
                        //provider
                        //  var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("0D280653-7177-4DD3-9915-6CB83433BF70")).FirstOrDefault();

                        ProductSubscriptionModel subscription = new ProductSubscriptionModel();

                        subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();
                        subscription.PaymentMethodId = paymentDetails.PaymentMethodId;
                        subscription.SubscriptionCount = _SubscriptionCount;
                        subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                        subscription.SubscriptionType = (byte?)_accountType;

                        subscription.PackageAmount = totalpackageamt_P;

                        db.Subscription.Add(subscription);
                        db.Entry(subscription).State = EntityState.Modified;
                        db.SaveChanges();

                    }


                    //tenant db creation
                    await DbCreation((int)CurrentAccountType, _accountType);
                    return Json("1");
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Index", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }

           // string retUrl = @"/ProductSubscriptionSRVNew";
          //  return LocalRedirect(retUrl);

            // return View();
        }
        //    public async Task<IActionResult> UpdateDetails([FromBody] WellAI.Advisor.Model.Common.UpdateSubscriptionViewModel updateSubscriptionViewModel)
        //    {

        //        try
        //        {
        //        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

        //        string _SubscriptionId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.SubscriptionId).FirstOrDefault();

        //        var result = UpdateSubscription(updateSubscriptionViewModel, _SubscriptionId);


        //        if (result.Status.ToString().ToUpper() != "OK")
        //        {
        //            return View();
        //        }

        //        UpdateSubscriptionDetails subscriptionDetails = updateSubscriptionViewModel.updateSubscriptionDetails;
        //        UpdateCompanyDetails companyDetails = updateSubscriptionViewModel.updateCompanyDetails;
        //        UpdatePaymentDetails paymentDetails = updateSubscriptionViewModel.updatePaymentDetails;



        //        var packageId = db.Subscription.Where(s => s.TenantId == tenantId).Select(s => s.PackageId).FirstOrDefault();
        //        var CurrentSubscriptionType = db.Subscription.Where(s => s.TenantId == tenantId).Select(s => s.SubscriptionType).FirstOrDefault();




        //        int _accountType = 0;
        //        string _SubscriptionType = "";
        //        int _SubscriptionCount = 0;
        //        int _SubscriptionDispatchCount = 0;

        //        decimal totalpackageamt_OP = 0;
        //        decimal totalpackageamt_DP = 0;
        //        decimal totalpackageamt_P = 0;


        //        if ((subscriptionDetails.DispatchQuantity != null && subscriptionDetails.OperatorQuantity != null) && (Convert.ToInt32(subscriptionDetails.DispatchQuantity) != 0 && Convert.ToInt32(subscriptionDetails.OperatorQuantity) != 0))
        //        {
        //            _accountType = 3;
        //            _SubscriptionType = "OperatorWithDispatch";
        //            _SubscriptionCount = int.Parse(subscriptionDetails.OperatorQuantity);
        //            _SubscriptionDispatchCount = int.Parse(subscriptionDetails.DispatchQuantity);

        //            totalpackageamt_OP = Convert.ToDecimal(subscriptionDetails.OperatorTotal);
        //            totalpackageamt_DP = Convert.ToDecimal(subscriptionDetails.DispatchTotal);
        //        }
        //        else if ((subscriptionDetails.DispatchQuantity != null && subscriptionDetails.ProviderQuantity != null) && (Convert.ToInt32(subscriptionDetails.DispatchQuantity) != 0 && Convert.ToInt32(subscriptionDetails.ProviderQuantity) != 0))
        //        {
        //            _accountType = 4;
        //            _SubscriptionType = "ProviderWithDispatch";
        //            _SubscriptionCount = int.Parse(subscriptionDetails.ProviderQuantity);
        //            _SubscriptionDispatchCount = int.Parse(subscriptionDetails.DispatchQuantity);

        //            totalpackageamt_DP = Convert.ToDecimal(subscriptionDetails.DispatchTotal);
        //            totalpackageamt_P = Convert.ToDecimal(subscriptionDetails.ProviderTotal);
        //        }
        //        else if (subscriptionDetails.OperatorQuantity != null && Convert.ToInt32(subscriptionDetails.OperatorQuantity) != 0)
        //        {
        //            _accountType = 0;
        //            _SubscriptionType = "Advisor Operator";
        //            _SubscriptionCount = int.Parse(subscriptionDetails.OperatorQuantity);

        //            totalpackageamt_OP = Convert.ToDecimal(subscriptionDetails.OperatorTotal);
        //        }
        //        else if (subscriptionDetails.ProviderQuantity != null && Convert.ToInt32(subscriptionDetails.ProviderQuantity) != 0)
        //        {
        //            _accountType = 1;
        //            _SubscriptionType = "Advisor Provider";
        //            _SubscriptionCount = int.Parse(subscriptionDetails.ProviderQuantity);

        //            totalpackageamt_P = Convert.ToDecimal(subscriptionDetails.ProviderTotal);
        //        }
        //        else if (subscriptionDetails.DispatchQuantity != null && Convert.ToInt32(subscriptionDetails.DispatchQuantity) != 0)
        //        {
        //            _accountType = 2;
        //            _SubscriptionType = "Dispatch";
        //            _SubscriptionCount = int.Parse(subscriptionDetails.DispatchQuantity);
        //            _SubscriptionDispatchCount = int.Parse(subscriptionDetails.DispatchQuantity);

        //            totalpackageamt_DP = Convert.ToDecimal(subscriptionDetails.DispatchTotal);
        //        }

        //        ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);



        //        var _ProfileId = "";


        //        //corparate profile
        //        CorporateProfile CorporateProfile = new CorporateProfile();

        //        CorporateProfile = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();

        //        CorporateProfile.Name = companyDetails.Name;
        //        CorporateProfile.Phone = companyDetails.CompanyPhone;
        //        CorporateProfile.Address1 = companyDetails.CompanyAddress1;
        //        CorporateProfile.Address2 = companyDetails.CompanyAddress2;
        //        CorporateProfile.State = companyDetails.CompanyState;
        //        CorporateProfile.City = companyDetails.CompanyCity;
        //        CorporateProfile.Zip = companyDetails.CompanyZip;
        //        //billing
        //        CorporateProfile.BillingAddress1 = companyDetails.BillingAddress1;
        //        CorporateProfile.BillingAddress2 = companyDetails.BillingAddress2;
        //        CorporateProfile.BillingCity = companyDetails.BillingCity;
        //        CorporateProfile.BillingZip = companyDetails.CompanyZip;
        //        CorporateProfile.BillingState = companyDetails.CompanyState;
        //        CorporateProfile.BillingPhone = companyDetails.BillingPhone;
        //        CorporateProfile.BillingEmail = companyDetails.BillingEmail;

        //        db.CorporateProfile.Add(CorporateProfile);
        //        db.Entry(CorporateProfile).State = EntityState.Modified;
        //        db.SaveChanges();

        //        //GET PROFILE ID 
        //        _ProfileId = CorporateProfile.ID;



        //        CrmUserBasicDetail crmUserBasicDetail = new CrmUserBasicDetail();

        //        crmUserBasicDetail = db.CrmUserBasicDetail.Where(x => x.SubscriptionId == _SubscriptionId).FirstOrDefault();

        //        crmUserBasicDetail.Name = companyDetails.Name;
        //        crmUserBasicDetail.Company = companyDetails.CName;
        //        crmUserBasicDetail.IsMaster = true;
        //        crmUserBasicDetail.Title = companyDetails.Title;
        //        crmUserBasicDetail.AccountType = _accountType;
        //        crmUserBasicDetail.IsActive = true;
        //        crmUserBasicDetail.SubscriptionId = _SubscriptionId;
        //        crmUserBasicDetail.CorporateProfileId = _ProfileId;
        //        crmUserBasicDetail.CreatedDate = DateTime.UtcNow;
        //        crmUserBasicDetail.ModifiedDate = DateTime.UtcNow;
        //        var status = commonBusiness.UpdateUserBasicDetail(crmUserBasicDetail);


        //          Model.OperatingCompany.Models.SubscriptionViewModel subscriptionViewModel = new Model.OperatingCompany.Models.SubscriptionViewModel();


        //        UpdateSubscriptionAndPaymentDetails updateSubscriptionAndPaymentDetails = new UpdateSubscriptionAndPaymentDetails();
        //        updateSubscriptionAndPaymentDetails.subscriptionViewModel = subscriptionViewModel;


        //        //Operator && Dispatch (Operator Qty>0 && Dispatch Qty>0)

        //        List<SubscriptionPackage> subscriptionlist = new List<SubscriptionPackage>();
        //        decimal subscriptionAmt = 0;
        //        if (Convert.ToInt32(subscriptionDetails.OperatorQuantity) > 0 && Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0)
        //        {

        //            //operator

        //            ProductSubscriptionModel subscription = new ProductSubscriptionModel();
        //            subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();

        //            subscription.SubscriptionCount = _SubscriptionCount;
        //            subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
        //            subscription.SubscriptionType = (byte?)_accountType;

        //            subscription.PackageAmount = totalpackageamt_OP;

        //            db.Subscription.Add(subscription);
        //            db.Entry(subscription).State = EntityState.Modified;
        //            db.SaveChanges();


        //        }

        //        else if (Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0 && Convert.ToInt32(subscriptionDetails.ProviderQuantity) > 0)
        //        {

        //            ProductSubscriptionModel subscription = new ProductSubscriptionModel();
        //            subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();

        //            subscription.SubscriptionCount = _SubscriptionDispatchCount;
        //            subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
        //            subscription.SubscriptionType = (byte?)_accountType;

        //            subscription.PackageAmount = totalpackageamt_DP;

        //            db.Subscription.Add(subscription);
        //            db.Entry(subscription).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }


        //        else if (Convert.ToInt32(subscriptionDetails.OperatorQuantity) > 0)
        //        {

        //            ProductSubscriptionModel subscription = new ProductSubscriptionModel();
        //            subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();


        //            subscription.SubscriptionCount = _SubscriptionCount;
        //            subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
        //            subscription.SubscriptionType = (byte?)_accountType;

        //            subscription.PackageAmount = totalpackageamt_OP;

        //            db.Subscription.Add(subscription);
        //            db.Entry(subscription).State = EntityState.Modified;
        //            db.SaveChanges();

        //        }
        //        else if (Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0)
        //        {

        //            ProductSubscriptionModel subscription = new ProductSubscriptionModel();
        //            subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();


        //            subscription.SubscriptionCount = _SubscriptionCount;
        //            subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
        //            subscription.SubscriptionType = (byte?)_accountType;

        //            subscription.PackageAmount = totalpackageamt_DP;
        //            db.Subscription.Add(subscription);

        //            db.Subscription.Add(subscription);
        //            db.Entry(subscription).State = EntityState.Modified;
        //            db.SaveChanges();

        //        }
        //        else if (Convert.ToInt32(subscriptionDetails.ProviderQuantity) > 0)
        //        {
        //            //provider
        //            var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("0D280653-7177-4DD3-9915-6CB83433BF70")).FirstOrDefault();

        //            ProductSubscriptionModel subscription = new ProductSubscriptionModel();

        //            subscription = db.Subscription.Where(x => x.TenantId == tenantId).FirstOrDefault();

        //            subscription.SubscriptionCount = _SubscriptionCount;
        //            subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
        //            subscription.SubscriptionType = (byte?)_accountType;

        //            subscription.PackageAmount = totalpackageamt_P;

        //            db.Subscription.Add(subscription);
        //            db.Entry(subscription).State = EntityState.Modified;
        //            db.SaveChanges();

        //        }


        //        updateSubscriptionAndPaymentDetails.updatePaymentDetails = updateSubscriptionViewModel.updatePaymentDetails;

        //      //  var SubscriptionDetails = await CreateSubscription(updateSubscriptionAndPaymentDetails, subscriptionlist, subscriptionAmt, _ProfileId);

        //        if (paymentDetails.PaymentType == "Credit Card")
        //        {

        //            PaymentMethod paymentMethod = new PaymentMethod();

        //            paymentMethod = db.PaymentMethods.Where(x => x.TenantId == tenantId).FirstOrDefault();


        //            paymentMethod.Holder = paymentDetails.CardName;
        //            paymentMethod.AccountName = paymentDetails.CardName;
        //            paymentMethod.PayType = "1";
        //            paymentMethod.AccountNumber = paymentDetails.CardNumber;
        //            paymentMethod.ExpireMonth = paymentDetails.Month;
        //            paymentMethod.ExpireYear = paymentDetails.Year;

        //            paymentMethod.FirstName = companyDetails.BillingName;
        //            paymentMethod.Address1 = companyDetails.BillingAddress1;
        //            paymentMethod.Address2 = companyDetails.BillingAddress2;
        //            paymentMethod.City = companyDetails.BillingCity;
        //            paymentMethod.State = companyDetails.BillingState;
        //            paymentMethod.Zip = companyDetails.CompanyZip;

        //            db.PaymentMethods.Add(paymentMethod);
        //            db.Entry(paymentMethod).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        else if (paymentDetails.PaymentType == "Debit Card")
        //        {

        //            PaymentMethod paymentMethod = new PaymentMethod();

        //            paymentMethod = db.PaymentMethods.Where(x => x.TenantId == tenantId).FirstOrDefault();


        //            paymentMethod.Holder = paymentDetails.CardName;
        //            paymentMethod.AccountName = paymentDetails.CardName;
        //            paymentMethod.PayType = "2";
        //            paymentMethod.AccountNumber = paymentDetails.CardNumber;
        //            paymentMethod.ExpireMonth = paymentDetails.Month;
        //            paymentMethod.ExpireYear = paymentDetails.Year;

        //            paymentMethod.FirstName = companyDetails.BillingName;
        //            paymentMethod.Address1 = companyDetails.BillingAddress1;
        //            paymentMethod.Address2 = companyDetails.BillingAddress2;
        //            paymentMethod.City = companyDetails.BillingCity;
        //            paymentMethod.State = companyDetails.BillingState;
        //            paymentMethod.Zip = companyDetails.CompanyZip;

        //            db.PaymentMethods.Add(paymentMethod);
        //            db.Entry(paymentMethod).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        else if (paymentDetails.PaymentType == "ACH Payment")
        //        {

        //            PaymentMethod paymentMethod = new PaymentMethod();
        //            paymentMethod = db.PaymentMethods.Where(x => x.TenantId == tenantId).FirstOrDefault();


        //            paymentMethod.CustomerName = paymentDetails.CustomerName;

        //            paymentMethod.PayType = "3";
        //            paymentMethod.AccountNumber = paymentDetails.AccountNumber;
        //            paymentMethod.ExpireMonth = paymentDetails.Month;
        //            paymentMethod.ExpireYear = paymentDetails.Year;
        //            paymentMethod.TypeofAccount = paymentDetails.TypeofAccount;
        //            paymentMethod.RoutingNumber = paymentDetails.RoutingNumber;
        //            paymentMethod.BankName = paymentDetails.BankName;

        //            paymentMethod.FirstName = companyDetails.BillingName;
        //            paymentMethod.Address1 = companyDetails.BillingAddress1;
        //            paymentMethod.Address2 = companyDetails.BillingAddress2;
        //            paymentMethod.City = companyDetails.BillingCity;
        //            paymentMethod.State = companyDetails.BillingState;
        //            paymentMethod.Zip = companyDetails.CompanyZip;

        //            db.PaymentMethods.Add(paymentMethod);
        //            db.Entry(paymentMethod).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }

        //      //  await  DbCreation(_accountType);

        //    }
        //        catch (Exception ex)
        //        {
        //            CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //    customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Index", User.Identity.Name);
        //            string returnUrl = @"/DashboardSRV/Error";
        //            return LocalRedirect(returnUrl);
        //}
        //        return View();
        //    }





        public IActionResult NewPayment()
        {

            return PartialView("NewPaymnet");
        }

        private async Task<string> DbCreation(int CurrentPackageType, int NewPackageType)
        {
            string res = "";

            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

            bool dbcreationStatus = false;
            bool DispatchCreationStatus = false;

            if ((CurrentPackageType == 0 || CurrentPackageType == 1) && (NewPackageType == 3 || NewPackageType == 4)) // operator/Provider + dispatch
            {
                dbcreationStatus = false;
                DispatchCreationStatus = true;

            }
            else if ((CurrentPackageType == 2) && (NewPackageType == 3 || NewPackageType == 4))  // dispatch  + operator/provider
            {
                dbcreationStatus = true;
                DispatchCreationStatus = false;
            }



            var dbprefix = "oper";
            if (NewPackageType == 1 || NewPackageType == 4)
                dbprefix = "serv";

            var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

            //Create Tenant DB only if not Dispatch type

            // operator / provider - db creation
            if (dbcreationStatus == true)
            {
                var newDbName = "wellai_" + dbprefix + "db_" + tenantId.ToString().Replace("-", "");                 //.ToString("N");
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", newDbName);

                var repo = new WellAI.Advisor.Tenant.TenantRepository(_configuration.GetConnectionString("WebAIAdvisorContextConnection"));

                var resnewdb = await repo.CreateTenantDB(newDbName);

                commonBusiness.CreateWellTenant(tenantId, connString, tenantId, tenantId, tenantId);
                //commonBusiness.CreateWellTenant(tenantid.ToString("D"), connString, tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"));

                var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);
                //var ti = new TenantInfo(tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"), connString, null);
                if (NewPackageType == 0 || NewPackageType == 3)
                {
                    using (var appcontext = new TenantOperatingDbContext(ti))
                    {
                        appcontext.Database.EnsureCreated();
                    }
                }
                else
                {
                    using (var appcontext = new TenantServiceDbContext(ti))
                    {
                        appcontext.Database.EnsureCreated();
                    }
                }
            }

            //Dispatch only  - no db creation
            if (DispatchCreationStatus == true)
            {
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "");

                var repo = new WellAI.Advisor.Tenant.TenantRepository(_configuration.GetConnectionString("WebAIAdvisorContextConnection"));
                //var resnewdb = await repo.CreateTenantDB(newDbName);
                commonBusiness.CreateWellTenant(tenantId, connString, tenantId, tenantId, tenantId);
                var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);
                await commonBusiness.CreateTenantRoles(tenantId);
            }


            return res;
        }





        public async Task<IActionResult> SubscriptionView()
        {
            try
            {
                var model = new UserDetailsModel()
                {
                    AccountDetails = new AccountDetailsModel() { Username = "johny", Email = "john.doe@email.com", Password = "pass123" },
                    PersonalDetails = new PersonalDetailsModel() { FullName = "", Country = "", Gender = "", About = "" },
                    PaymentDetails = new PaymentDetailsModel() { PaymentType = "", CardNumber = "", CSVNumber = "", ExpirationDate = "", CardHolderName = "" }
                };
                return View(model);
                //return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Index", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult ordersummary()
        {
            return PartialView("OrderSummary");
        }

        private async Task<JsonResult> UpdateSubscription(WellAI.Advisor.Model.Common.UpdateSubscriptionViewModel updateSubscriptionViewModel, string _SubscriptionId)
        {

            UpdateSubscriptionDetails updateSubscriptionDetails = updateSubscriptionViewModel.updateSubscriptionDetails;
            UpdateCompanyDetails updateCompanyDetails = updateSubscriptionViewModel.updateCompanyDetails;
            UpdatePaymentDetails updatePaymentDetails = updateSubscriptionViewModel.updatePaymentDetails;

            PaymentDetails paymentDetails = new PaymentDetails();

            PaymentMethod paymentmethod = new PaymentMethod();

            paymentmethod = db.PaymentMethods.Where(x => x.ID == updatePaymentDetails.PaymentMethodId).FirstOrDefault();

            string subscriptionId = _SubscriptionId;

            var authorizeNetSection = _configuration.GetSection("AuthorizeNet");
            string invoiceId = string.Empty;
            paymentDetails.AccountNumber = paymentmethod.AccountNumber;
            paymentDetails.CardNumber = paymentmethod.Number;
            paymentDetails.Year = paymentmethod.ExpireYear;
            paymentDetails.Month = paymentmethod.ExpireMonth;
            paymentDetails.CardVerificationNumber = paymentmethod.CVV;

            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
            Interval interval = new Interval();

            interval.length = 1;
            interval.unit = 1;

            PaymentSchedule paySechedule = new PaymentSchedule();
            paySechedule.totalOccurrences = 12;
            paySechedule.startDate = DateTime.Now.ToString();

            var creditCard = new CreditCardType()
            {
                CardNumber = paymentDetails.CardNumber,
                ExpirationDate = paymentDetails.Year + "-" + paymentDetails.Month,
                CardCode = paymentDetails.CardVerificationNumber,

            };

            CustomerAddressType addressType = new CustomerAddressType();
            addressType.FirstName = updateCompanyDetails.BillingName;
            addressType.LastName = "";
            addressType.Company = updateCompanyDetails.CName;
            addressType.Address = updateCompanyDetails.BillingAddress1 + "," + updateCompanyDetails.BillingAddress2;
            addressType.City = updateCompanyDetails.BillingCity;
            addressType.State = updateCompanyDetails.BillingState;
            addressType.Zip = updateCompanyDetails.BillingZip;
            //addressType.Country = "";

            PaymentType _payment = new PaymentType();
            _payment.Item = creditCard;
            SubscriptionType subType = new SubscriptionType();

            subType.amount = 30;
            subType.paymentSchedule = paySechedule;
            subType.paymentSchedule.interval = new Interval();
            subType.paymentSchedule.interval = interval;
            subType.billTo = new CustomerAddressType();
            subType.billTo = addressType;
            subType.payment = _payment;

            ANetApiResponse response = new ANetApiResponse();

            response = service.UpdateSubscriptionApi(creditCard, subType, subscriptionId, authorizeNetSection["AccountType"]);

            // return Json(new { message = response });
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> CancelSubscription()
        {

            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

            string _SubscriptionId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.SubscriptionId).FirstOrDefault();


            string subscriptionId = _SubscriptionId;
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
            var authorizeNetSection = _configuration.GetSection("AuthorizeNet");

            ANetApiResponse response = new ANetApiResponse();
            response = service.CancelSubscription(subscriptionId, authorizeNetSection["AccountType"]);

            if (response.messages.resultCode.ToString() == "ok")
            {

                var SUB = db.Subscription.Where(X => X.SubscriptionId == subscriptionId).FirstOrDefault();

                SUB.IsEnable = false;
                SUB.IsPaid = false;

                db.Subscription.Add(SUB);
                db.Entry(SUB).State = EntityState.Modified;
                db.SaveChanges();

            }
            else
            {
                response.messages.message[0].text = "Something went wrong. Pleasae check subscription details and proceed";
            }
            return Json(response);
        }





        //------------------------------old
        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {
        //        if (_signInManager.IsSignedIn(User) == false)
        //        {
        //            string returnUrl = @"/Identity/Account/Login";
        //            return LocalRedirect(returnUrl);
        //        }
        //        //checking invalid user//
        //        if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
        //        {
        //            if (GetComponentsBasedOnRole() == false)
        //            {
        //                string returnUrl = @"/OperatingDashboard";
        //                return LocalRedirect(returnUrl);
        //            }
        //        }
        //        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
        //        ViewBag.CustomerSubscriptions = (from sub in db.Subscription
        //                                         join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
        //                                         where sub.TenantId == tenantId && sub.IsPaid == true
        //                                         select new CustomerSubscriptions
        //                                         {
        //                                             SubscriptionName = pcg.Name,
        //                                             SubscriptionStart = sub.SubStartdate,
        //                                             SubscriptionEnd = sub.SubEndDate,
        //                                             SubscriptionTotalAmount = sub.PackageAmount,
        //                                             SubscriptionUsersCount = sub.SubscriptionCount,
        //                                             IsEnableSubscription = sub.IsEnable == true ? "Active" : "Deactive",
        //                                             PackageOrder = pcg.PackageOrder
        //                                         }
        //                                         ).FirstOrDefault();
        //        var result = await db.SubscriptionPackage.Where(x => x.IsActive == true && x.AccountType == 2).OrderBy(x => x.PackageOrder).ToListAsync();
        //        return View(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscription Index", User.Identity.Name);
        //        string returnUrl = @"/Dashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}

        //private bool GetComponentsBasedOnRole()
        //{
        //    var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
        //    var userIdentity = (ClaimsIdentity)User.Identity;
        //    var claims = userIdentity.Claims;
        //    var roleClaimType = userIdentity.RoleClaimType;
        //    var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

        //    List<string> rolesName = (from rl in roles
        //                              select rl.Value
        //                         ).ToList();

        //    IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
        //    var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
        //    var rolesResult = roleResult.Result;
        //    if (roleResult != null)
        //    {
        //        List<string> roleIds = (from rl in rolesResult
        //                                select rl.Id
        //                                ).ToList();
        //        return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "BillingHistory", TenantId);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ////public async Task<IActionResult> Index()
        ////{
        ////    try
        ////    {
        ////        if (_signInManager.IsSignedIn(User) == false)
        ////        {
        ////            string returnUrl = @"/Identity/Account/Login";
        ////            return LocalRedirect(returnUrl);
        ////        }
        ////        //checking invalid user//
        ////        if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
        ////        {
        ////            if (GetComponentsBasedOnRole() == false)
        ////            {
        ////                string returnUrl = @"/ServiceDashboard";
        ////                return LocalRedirect(returnUrl);
        ////            }
        ////        }
        ////        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
        ////        var Provider = await GetProviderDirectory();
        ////        ViewData["Providers"] = Provider.Providers;
        ////        ViewBag.CustomerSubscriptions = (from sub in db.Subscription
        ////                                         join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
        ////                                         where sub.TenantId == tenantId && sub.IsPaid == true
        ////                                         select new CustomerSubscriptions
        ////                                         {
        ////                                             SubscriptionName = pcg.Name,
        ////                                             SubscriptionStart = sub.SubStartdate,
        ////                                             SubscriptionEnd = sub.SubEndDate,
        ////                                             SubscriptionTotalAmount = sub.PackageAmount,
        ////                                             SubscriptionUsersCount = sub.SubscriptionCount,
        ////                                             IsEnableSubscription = sub.IsEnable == true ? "Active" : "Deactive",
        ////                                             PackageOrder = pcg.PackageOrder
        ////                                         }
        ////                                         ).FirstOrDefault();
        ////        var result = await db.SubscriptionPackage.Where(x => x.IsActive == true && x.AccountType == 1).OrderBy(x => x.PackageOrder).ToListAsync();
        ////        return View(result);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        ////        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Index", User.Identity.Name);
        ////        string returnUrl = @"/DashboardSRV/Error";
        ////        return LocalRedirect(returnUrl);
        ////    }
        ////}


        ////private bool GetComponentsBasedOnRole()
        ////{
        ////    var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
        ////    var userIdentity = (ClaimsIdentity)User.Identity;
        ////    var claims = userIdentity.Claims;
        ////    var roleClaimType = userIdentity.RoleClaimType;
        ////    var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

        ////    List<string> rolesName = (from rl in roles
        ////                              select rl.Value
        ////                         ).ToList();

        ////    IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
        ////    var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
        ////    var rolesResult = roleResult.Result;
        ////    if (roleResult != null)
        ////    {
        ////        List<string> roleIds = (from rl in rolesResult
        ////                                select rl.Id
        ////                                ).ToList();
        ////        return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "ProductSubscription", TenantId);
        ////    }
        ////    else
        ////    {
        ////        return false;
        ////    }
        ////}

        //public async Task<OperatingDirectoryModel> GetProviderDirectory(bool pendingFilter = false, bool insureExpireFilter = false)
        //{
        //    try
        //    {
        //        var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
        //        ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
        //        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
        //        var providers = await operrepo.GetSubsriptionOperators();
        //        var providerdir = new OperatingDirectoryModel
        //        {
        //            InsExpiring90days = providers.Count(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0),
        //            Pending = providers.Count(x => x.Approval == "Pending review"),
        //            Records = providers.Count
        //        };
        //        //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
        //        providerdir.PreferredProvider = providers.FirstOrDefault(x => x.Preferred);
        //        providerdir.SecondaryProvider = providers.FirstOrDefault(x => x.Secondary);
        //        if (pendingFilter)
        //        {
        //            providers = providers.Where(x => x.Approval == "Pending review").ToList();
        //        }
        //        if (insureExpireFilter)
        //        {
        //            providers = providers.Where(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0).ToList();
        //        }
        //        var msafiles = (List<ServiceMSA>)ViewData["msa"];
        //        var companies = (List<Model.OperatingCompany.Models.CorporateProfile>)ViewData["AllCompanies"];
        //        List<string> tenantIds = null;
        //        if (companies == null)
        //        {
        //            companies = await commonBusiness.GetOperatingCompanies();
        //            tenantIds = companies.Select(x => x.TenantId).ToList();
        //            msafiles = await commonBusiness.GetMSAWellFilesFromOperatingTenants(tenantIds, tenantId);
        //        }
        //        else
        //            tenantIds = companies.Select(x => x.TenantId).ToList();
        //        IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
        //        foreach (var provider in providers)
        //        {
        //            var cmpprofile = db.CorporateProfile.Where(x => x.TenantId.Equals(provider.CompanyId)).FirstOrDefault();
        //            var company = companies.FirstOrDefault(x => x.TenantId.ToString() == provider.CompanyId);//provider.CompanyId=tenantId of Service Company
        //            if (cmpprofile != null)
        //            {
        //                var site = string.IsNullOrEmpty(cmpprofile.Website) || cmpprofile.Website.StartsWith("http:") ? cmpprofile.Website : "http://" + cmpprofile.Website;
        //                provider.Name = cmpprofile.Name;
        //                provider.CompanyId = cmpprofile.TenantId;
        //                provider.Website = site;
        //                provider.Phone = cmpprofile.Phone;
        //                provider.User = await commonBusiness.GetPrimaryUserSRV(cmpprofile.TenantId);
        //                provider.Location = string.Format("{0}{1},{2},{3},{4}", cmpprofile.Address1, string.IsNullOrEmpty(cmpprofile.Address2) ? "" : "," + cmpprofile.Address2,
        //                cmpprofile.City, cmpprofile.State, cmpprofile.Zip);
        //                provider.City = cmpprofile.City;
        //                provider.State = db.USAStates.Where(x => x.StateId == Convert.ToInt32(cmpprofile.State)).Select(y => y.Name).FirstOrDefault();
        //                provider.Zip = cmpprofile.Zip;
        //                provider.Address1 = cmpprofile.Address1;
        //                provider.Address2 = cmpprofile.Address2;
        //            }
        //            provider.MSADocument = "";
        //            var msa = msafiles.FirstOrDefault(x => x.MsaId == provider.MSADocumentId);
        //            if (msa != null)
        //            {
        //                provider.MSADocumentId = msa.MsaId;
        //                provider.MSADocument = msa.Attachment;
        //            }
        //        }
        //        providerdir.Providers = providers;
        //        return providerdir;
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV GetProviderDirectory", User.Identity.Name);
        //        return null;
        //    }
        //}
        //public async Task<IActionResult> Operators_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    try
        //    {
        //        var result = await GetProviderDirectory();
        //        return Json(result.Providers.ToDataSourceResult(request, ModelState));
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Read", User.Identity.Name);
        //        _logger.LogInformation(ex.Message);
        //        string returnUrl = @"/Dashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}
        //[AcceptVerbs("Post")]
        //public async Task<IActionResult> Operators_Create([DataSourceRequest] DataSourceRequest request, OperatingProviderProfile input)
        //{
        //    try
        //    {
        //        if (input != null && ModelState.IsValid)
        //        {
        //            var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
        //            var res = await operrepo.UpdateSubsriptionOperators(input);
        //            res = await operrepo.UpdateProviderDirectory(input);
        //        }
        //        return Json(new[] { input }.ToDataSourceResult(request, ModelState));
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Create", User.Identity.Name);
        //        _logger.LogInformation(ex.Message);
        //        string returnUrl = @"/ServiceDashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}
        //[AcceptVerbs("Post")]
        //public async Task<IActionResult> Operators_Update([DataSourceRequest] DataSourceRequest request, OperatingProviderProfile input)
        //{
        //    try
        //    {
        //        if (input != null && ModelState.IsValid)
        //        {
        //            var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
        //            var res = await operrepo.UpdateSubsriptionOperators(input);
        //            res = await operrepo.UpdateProviderDirectory(input);
        //        }
        //        return Json(new[] { input }.ToDataSourceResult(request, ModelState));
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Update", User.Identity.Name);
        //        _logger.LogInformation(ex.Message);
        //        string returnUrl = @"/Dashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}
        //[AcceptVerbs("Post")]
        //public async Task<IActionResult> Operators_Destroy(string companyId)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(companyId))
        //        {
        //            var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
        //            var res = await operrepo.DeleteSubsriptionOperators(companyId);
        //            var res2 = await operrepo.DeleteProviderDirectoryByCompanyId(companyId);
        //            return Json(new[] { res });
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Destroy", User.Identity.Name);
        //        _logger.LogInformation(ex.Message);
        //        string returnUrl = @"/ServiceDashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}

        ///// <summary>
        ///// Phase II Changes - 12/01/2021 - Read Subscribe Rigs for Add Rigs
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="TenantId"></param>
        ///// <returns></returns>
        //public IActionResult ReadSubscibeOPerator_Rigs([DataSourceRequest] DataSourceRequest request, string TenantId)
        //{
        //    try
        //    {
        //        List<RigViewModel> RigList = new List<RigViewModel>();

        //        RigList = (from rig in db.rig_register
        //                   where rig.TenantID == TenantId && rig.isActive == true
        //                   select new RigViewModel
        //                   {
        //                       RigName = rig.Rig_Name,
        //                       RigId = rig.Rig_id
        //                   }).OrderBy(x => x.RigName).ToList();

        //        return Json(RigList.ToDataSourceResult(request));

        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV ReadRigs", User.Identity.Name);
        //        string returnUrl = @"/ServiceDashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }

        //}
        ///// <summary>
        ///// Save operator rigs // phase 2
        ///// </summary>
        ///// <param name="SelectedRigs"></param>
        ///// <returns></returns>
        //[AcceptVerbs("post")]
        //public async Task<IActionResult> SaveSubscibeOPerator_Rigs([FromBody] List<SubscriptionOperatorRigs> SelectedRigs)
        //{

        //    try
        //    {
        //        int result = 0;
        //        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
        //        var OperatorsRigsCount = (from sub in db.Subscription
        //                                  join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
        //                                  where sub.TenantId == tenantId && sub.IsPaid == true
        //                                  select new CustomerSubscriptions
        //                                  {
        //                                      SubscriptionUsersCount = sub.SubscriptionCount,
        //                                  }).FirstOrDefault();
        //        var Rigs = new List<string>();
        //        for (var i = 0; i < SelectedRigs.Count; i++)
        //        {
        //            var RigList = SelectedRigs[i].RigId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        //            Rigs.AddRange(RigList);
        //        }
        //        var ExitsRig = _servicedb.subscriptionOperatorRigs.Where(x => Rigs.Contains(x.RigId)).Count();
        //        var RigCount = Rigs.Count - ExitsRig;
        //        var GetSubscribeRigs = _servicedb.subscriptionOperatorRigs.Count();
        //        var TotlRigCounts = RigCount + GetSubscribeRigs;

        //        if (TotlRigCounts > OperatorsRigsCount.SubscriptionUsersCount)
        //        {
        //            ModelState.AddModelError(string.Empty, "Your subscription does not allow adding more operator rigs. Upgrade your subscription if you need more rigs.");
        //            return Json(new { success = true });
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);

        //            result = await operrepo.SaveSubsciberProviderRigs(SelectedRigs);

        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV SaveRigs", User.Identity.Name);
        //        string returnUrl = @"/ServiceDashboard/Error";
        //        return LocalRedirect(returnUrl);

        //    }

        //}
        ///// <summary>
        ///// Phase  II Changes // 18/01/2021 - Update Subscribe Operator Rigs
        ///// </summary>
        ///// <param name="SelectedRigs"></param>
        ///// <returns></returns>
        //[AcceptVerbs("post")]
        //public async Task<IActionResult> Update_SubscibeOPerator_Rigs([FromBody] List<SubscriptionOperatorRigs> SelectedRigs)
        //{

        //    try
        //    {
        //        int result = 0;
        //        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
        //        var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
        //        var OperatorsRigsCount = (from sub in db.Subscription
        //                                  join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
        //                                  where sub.TenantId == tenantId && sub.IsPaid == true
        //                                  select new CustomerSubscriptions
        //                                  {
        //                                      SubscriptionUsersCount = sub.SubscriptionCount,
        //                                  }).FirstOrDefault();
        //        var Rigs = new List<string>();
        //        for (var i = 0; i < SelectedRigs.Count; i++)
        //        {
        //            var RigList = SelectedRigs[i].RigId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        //            Rigs.AddRange(RigList);
        //        }

        //        //Changed to validate against the selected Rigs Count and not against calculated count
        //        if (Rigs.Count > OperatorsRigsCount.SubscriptionUsersCount)
        //        {
        //            ModelState.AddModelError(string.Empty, "Your subscription does not allow adding more operator rigs. Upgrade your subscription if you need more rigs.");
        //            return Json(new { success = true });
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);

        //            var CompanyId = SelectedRigs[0].CompanyId;
        //            //var RigId = SelectedRigs.RigId;
        //            var getRigs = await operrepo.Remove_SubsciberProviderRigs(CompanyId);

        //            result = await operrepo.SaveSubsciberProviderRigs(SelectedRigs);

        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Update_SubscibeOPerator_Rigs", User.Identity.Name);
        //        string returnUrl = @"/ServiceDashboard/Error";
        //        return LocalRedirect(returnUrl);

        //    }

        //}

        ///// <summary>
        ///// Phase II Chnages - 18/01/2021 - Read Subcribe Operator Rigs For Edit OPerator Rigs
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <returns></returns>
        //public IActionResult Read_SubscribeOPerator_Rigs(string CompanyId)
        //{
        //    try
        //    {
        //        var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
        //        var result = operrepo.Get_SubsciberProviderRigs(CompanyId);
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Read", User.Identity.Name);
        //        _logger.LogInformation(ex.Message);
        //        string returnUrl = @"/Dashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}

        //public IActionResult Providers_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    try
        //    {
        //        var SrvTenantIds = (from crp in db.CorporateProfile
        //                            join crmp in db.CrmCompanies on crp.TenantId equals crmp.TenantId
        //                            select new
        //                            {
        //                                TenantId = crp.TenantId
        //                            }
        //                       ).ToList();
        //        var ProTenantIds = db.CorporateProfile.Where(X => !SrvTenantIds.Select(Y => Y.TenantId).Contains(X.TenantId)).Select(x => x.TenantId).ToList();
        //        var corpProfiles = db.CorporateProfile.Where(x => ProTenantIds.Contains(x.TenantId)).ToList();
        //        var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
        //        var existingList = operrepo.GetSubsriptionOperators();
        //        var existingId = existingList.Result.Select(e => e.CompanyId);
        //        var allVendors = corpProfiles.Select(e => e.TenantId);//.Except(existingId).ToList();
        //        string output = String.Join(",", allVendors);
        //        var openVendors = corpProfiles
        //                           .Where(vendor => allVendors.Contains(vendor.TenantId))
        //                           .Select(cp => new Model.ServiceCompany.Models.ServiceCorporateProfile
        //                           {
        //                               ID = cp.TenantId,
        //                               UserId = cp.UserId,
        //                               Name = cp.Name,
        //                               Website = cp.Website,
        //                               City = cp.City,
        //                               State = cp.State
        //                           }).ToList();
        //        return Json(openVendors.ToDataSourceResult(request));
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Providers_Read", User.Identity.Name);
        //        string returnUrl = @"/ServiceDashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> SaveProvider([FromBody] OperatingProviderProfile company)
        //{
        //    CommunicationSRVViewModel model = new CommunicationSRVViewModel();
        //    try
        //    {
        //        var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
        //        ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
        //        var companies = await commonBusiness.GetOperatingCompanies();
        //        var comp = companies.FirstOrDefault(cm => cm.ID == company.CompanyId);
        //        var companyId = "";
        //        if (comp != null)
        //        {
        //            companyId = comp.TenantId;
        //        }
        //        if (companyId != "")
        //        {
        //            company.CompanyId = companyId;
        //            var pendingReview = (from ap in _servicedb.OperatingDirectoryAppovals
        //                                 where ap.Name == "Pending review"
        //                                 select new OperatingDirectoryApproval
        //                                 {
        //                                     Id = ap.Id
        //                                 }
        //                            ).SingleOrDefault();
        //            var inactiveStatus = (from st in _servicedb.OperatingDirectoryStatuses
        //                                  where st.Name == "Inactive"
        //                                  select new OperatingDirectoryStatus
        //                                  {
        //                                      Id = st.Id
        //                                  }
        //                                  ).SingleOrDefault();
        //            company.ApprovalId = pendingReview.Id;
        //            company.StatusId = inactiveStatus.Id;
        //        }
        //        int result = 0;
        //        if (company != null)
        //        {
        //            var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
        //            var res = await operrepo.UpdateSubsriptionOperators(company);
        //            res = await operrepo.UpdateProviderDirectory(company);
        //            result = res;
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "ProductSubscriptionSRV SaveProvider", User.Identity.Name);
        //        string returnUrl = @"/Dashboard/Error";
        //        return LocalRedirect(returnUrl);
        //    }
        //}

    }
}
