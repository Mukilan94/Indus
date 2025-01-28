using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
using WellAI.Advisor.DLL;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Areas.ServiceCompany.Controllers;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.OperatingCompany;

using AutoMapper;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using Microsoft.Extensions.DependencyInjection;
using Well_AI_Advisior.API.Authorize.Net;
using Microsoft.Extensions.Configuration;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;
using Well_AI_Advisior.API.Authorize.Net.Model;
using System.Text;
using WellAI.Advisor.Tenant;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using SubscriptionPackage = WellAI.Advisor.DLL.Entity.SubscriptionPackage;
using Microsoft.AspNetCore.WebUtilities;
using WellAI.Advisor.DLL.Data;
using Newtonsoft.Json;
using WellAI.Advisor.Function.DrillPlan;

namespace WellAI.Advisor.Areas.Registration.Controllers
{
    [Area("Registration")]
    public class RegistrationController : BaseController
    {
        private readonly ILogger<ProductSubscriptionSRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private TenantServiceDbContext _servicedb;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private TenantOperatingDbContext _tdbContext;
        private readonly IMultiTenantStore _store;
        protected readonly SignInManager<StaffWellIdentityUser> signInManager;
        public RegistrationController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           TenantOperatingDbContext tdbContext,
           WebAIAdvisorContext dbContext, ILogger<ProductSubscriptionSRVController> logger, TenantServiceDbContext servicedb, 
           IMapper mapper, IConfiguration configuration)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _servicedb = servicedb;
            _mapper = mapper;
            _configuration = configuration;
            _tdbContext = tdbContext;
           
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {
            var StateList = (from USA in db.USAStates
                             select new USAState
                             {
                                 StateId = USA.StateId,
                                 Name = USA.Name
                             }).OrderBy(o => o.Name).ToList();
            ViewData["StateList"] = StateList;

            return View();
            //return View();
        }

        public IActionResult _SubscriptionSelector()

        {
            try
            {
                WellAI.Advisor.Model.Common.Registration registration = new WellAI.Advisor.Model.Common.Registration();
                SubscriptionDetails pricing = new SubscriptionDetails();
                //SubscriptionItems items = new SubscriptionItems();

                SubscriptionBusiness objComBusiness = new SubscriptionBusiness(db, _roleManager, _userManager);
                var result = objComBusiness.GetNewSubscriptionPackages();

                if (result.Count > 0)
                {

                    var dispatchPrice = result.Where(x => x.PackageId.ToString().ToUpper() == "D350AC56-27E8-4442-9F1D-3EB6088DB9DA").FirstOrDefault();
                    pricing.DispatchUnitPrice = Convert.ToDouble(dispatchPrice.PackageAmount);
                    pricing.DispatchDescription = Convert.ToString(dispatchPrice.Description);
                    pricing.DispatchName = Convert.ToString(dispatchPrice.PackageName);
                    //ViewBag.Description = dispatchPrice.Description;



                    var providerPrice = result.Where(x => x.PackageId.ToString().ToUpper() == "0D280653-7177-4DD3-9915-6CB83433BF70").FirstOrDefault();
                    pricing.ProviderUnitPrice = Convert.ToDouble(providerPrice.PackageAmount);
                    pricing.ProviderDescription = Convert.ToString(providerPrice.Description);
                    pricing.ProviderName = Convert.ToString(providerPrice.PackageName);
                    //ViewBag.SubscriptionPricing = pricing.Description;

                    var operatorPrice = result.Where(x => x.PackageId.ToString().ToUpper() == "558911F7-876E-40C2-AF4F-388DBD83CB6D").FirstOrDefault();
                    pricing.OperatorUnitPrice = Convert.ToDouble(operatorPrice.PackageAmount);
                    pricing.OperatorDescription = Convert.ToString(operatorPrice.Description);
                    pricing.OperatorName = Convert.ToString(operatorPrice.PackageName);
                    // ViewBag.SubscriptionPricing = pricing.Description;
                }
                registration.SubscriptionDetails = pricing;
                return PartialView(registration);
                //CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //result = objComBusiness.GetDispatchRoutes(userId, false).Result;
            }
            catch (Exception ex)
            {
                //Partial1ViewModel model = _mapper.Map<Partial1ViewModel>(mainViewModel);
                //return PartialView(model);
                return PartialView();
            }
        }
        public IActionResult _CompanyDetails()//Partial1ViewModel partial1ViewModel
        {
            //Partial2ViewModel model = _mapper.Map<Partial2ViewModel>(partial1ViewModel);
            var StateList = (from USA in db.USAStates
                             select new USAState
                             {
                                 StateId = USA.StateId,
                                 Name = USA.Name
                             }).OrderBy(o => o.Name).ToList();
            ViewData["StateList"] = StateList;
            return PartialView();
        }

        public IActionResult _Termsandcondition()//Partial1ViewModel partial1ViewModel
        {
            //Partial2ViewModel model = _mapper.Map<Partial2ViewModel>(partial1ViewModel);

            return PartialView();
        }
        public IActionResult _Payment()//Partial2ViewModel partial2ViewModel
        {
            //MainViewModel model = _mapper.Map<MainViewModel>(partial2ViewModel);
            return PartialView();
            //return PartialView(model);
        }

        public IActionResult _OS()//Partial2ViewModel partial2ViewModel
        {
            //MainViewModel model = _mapper.Map<MainViewModel>(partial2ViewModel);
            return PartialView();
            //return PartialView(model);
        }
        public IActionResult _CostSummary()//Partial2ViewModel partial2ViewModel
        {
            //MainViewModel model = _mapper.Map<MainViewModel>(partial2ViewModel);
            return PartialView();
            //return PartialView(model);
        }
        public IActionResult _OrderSummary()//Partial2ViewModel partial2ViewModel
        {
            //MainViewModel model = _mapper.Map<MainViewModel>(partial2ViewModel);
            return PartialView();
            //return PartialView(model);
        }

        public IActionResult UpdateSubscription()//Partial2ViewModel partial2ViewModel
        {
            //MainViewModel model = _mapper.Map<MainViewModel>(partial2ViewModel);
            return View();
            //return PartialView(model);
        }
        Random r = new Random();
        private string getRandomChar(string fullString)
        {
            
            return fullString.ToCharArray()[(int)Math.Floor(r.NextDouble() * fullString.Length)].ToString();
        }
        private int getRandomPosition(ref string posArray)
        {
            int pos;
            string randomChar = posArray.ToCharArray()[(int)Math.Floor(r.NextDouble() * posArray.Length)].ToString();
            pos = int.Parse(randomChar);
            posArray = posArray.Replace(randomChar, "");
            return pos;
        }
        [HttpPost]
        //public IActionResult RegistrationDetails([FromBody]CompanyDetails companyDetails)
        //{
        //        return Json(companyDetails);
        //}
        public async Task<IActionResult> RegistrationDetails([FromBody] WellAI.Advisor.Model.Common.Registration registrationDetails)
        {

           

            SubscriptionDetails subscriptionDetails = registrationDetails.SubscriptionDetails;
            CompanyDetails companyDetails = registrationDetails.CompanyDetails;
            PaymentDetails paymentDetails = registrationDetails.PaymentDetails;

            ///Random rnd = new Random();

            string userId = "1"; // test 
           // string _subscriptionId = "7225836";

            //var _tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

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

                totalpackageamt_OP = Convert.ToDecimal( subscriptionDetails.OperatorTotal);
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

            Random rand = new Random(DateTime.Now.Millisecond);
            rand.Next();

            String generatedPassword = "";
            string alphaCaps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
             string alphaLow = "abcdefghijklmnopqrstuvwxyz";
             string numerics = "1234567890";
             string special = "@#$-=/";
            string allChars = alphaCaps + alphaLow + numerics + special;
          int  length = 14;
            int lowerpass, upperpass, numpass, specialchar;
            string posarray = "0123456789";
            if (length < posarray.Length)
                posarray = posarray.Substring(0, length);
            lowerpass = getRandomPosition(ref posarray);
            upperpass = getRandomPosition(ref posarray);
            numpass = getRandomPosition(ref posarray);
            specialchar = getRandomPosition(ref posarray);


            for (int i = 0; i < length; i++)
            {
                if (i == lowerpass)
                    generatedPassword += getRandomChar(alphaCaps);
                else if (i == upperpass)
                    generatedPassword += getRandomChar(alphaLow);
                else if (i == numpass)
                    generatedPassword += getRandomChar(numerics);
                else if (i == specialchar)
                    generatedPassword += getRandomChar(special);
                else
                    generatedPassword += getRandomChar(allChars);
            }

            string randomPassword = generatedPassword;
         //   string Password = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(randomPassword.ToString()));
            //string user_id = Guid.NewGuid().ToString("D");
            var user = new WellIdentityUser { UserName = companyDetails.CompanyEmail, Email = companyDetails.CompanyEmail,
                PhoneNumber = companyDetails.CompanyPhone, FirstName = companyDetails.Name ,MiddleName ="",LastName = "",Address =companyDetails.CompanyAddress1,City= companyDetails.CompanyCity,
                Mobile= companyDetails.CompanyPhone,State= companyDetails.CompanyState,JobTitle =companyDetails.Title,
                Zip= companyDetails.CompanyZip,  WellUser = _SubscriptionType== "OperatorWithDispatch" ?true: _SubscriptionType == "Advisor Operator"?true:false
            };
            var result = await _userManager.CreateAsync(user, randomPassword.ToString());

            //if(result.Errors.Count==0)
            if (result.ToString() == "Succeeded")
            {


                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                //var userExisting = commonBusiness.GetUserByEmail(companyDetails.CompanyEmail);

                //if (userExisting != null)
                //{
                //    ModelState.AddModelError(string.Empty, "This address has already been registered with Well-Al, please login or reset your password.");
                //    return Json(registrationDetails);
                //}

                var _ProfileId = "";


                //using (var trans = db.Database.BeginTransaction())
                //{
                //try
                //{






                // commonBusiness.UpdateUserPagesCompleteStatus(1, userId);

                CorporateProfile CorporateProfile = new CorporateProfile();

                //corparate profile
                CorporateProfile.ID = Guid.NewGuid().ToString("D");
                //CorporateProfile.TenantId = _tenantId;
                CorporateProfile.UserId = user.Id;
                CorporateProfile.Name = companyDetails.Name;
                CorporateProfile.Phone = companyDetails.CompanyPhone;
                CorporateProfile.Address1 = companyDetails.CompanyAddress1;
                CorporateProfile.Address2 = companyDetails.CompanyAddress2;
                CorporateProfile.State = commonBusiness.GetStateId(companyDetails.CompanyState).ToString();
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
                await db.SaveChangesAsync();
                //GET PROFILE ID 
                _ProfileId = CorporateProfile.ID;



                //  string roleName = Input.AccountType == "Rig Operator" ? "Operator" : Input.AccountType == "Dispatch" ? "Dispatch" : "Service Provider";
                //  string roleName = "Operator";

                if(_SubscriptionType== "OperatorWithDispatch")
                {
                   // string roleName = _SubscriptionType == "Advisor Operator" ? "Operator" : _SubscriptionType == "Dispatch" ? "Dispatch" : _SubscriptionType == "Advisor Provider" ? "Service Provider" : _SubscriptionType;
                    // string roleName = _SubscriptionType;
                    //this code is temporary untill all the register process is completed//
                   // await commonBusiness.AddUserRole(user, roleName);
                    await commonBusiness.AddUserRole(user, "Operator");
                    await commonBusiness.AddUserRole(user, "Dispatch");
                }
               else if (_SubscriptionType == "ProviderWithDispatch")
                {
                    // string roleName = _SubscriptionType;
                    //this code is temporary untill all the register process is completed//
                    await commonBusiness.AddUserRole(user, "Service Provider");
                    await commonBusiness.AddUserRole(user, "Dispatch");
                }
                //else
                //{
                //  //  string roleName = _SubscriptionType == "Advisor Operator" ? "Operator" : _SubscriptionType == "Dispatch" ? "Dispatch" : _SubscriptionType == "Advisor Provider" ? "Service Provider" : _SubscriptionType;
                //    // string roleName = _SubscriptionType;
                //    //this code is temporary untill all the register process is completed//
                //    await commonBusiness.AddUserRole(user, "Dispatch");
                //}
             

                //crmuserdetails
                var crmUserBasicDetail = new CrmUserBasicDetail
                {
                    UserId = user.Id,
                    Name = companyDetails.Name,
                    Company = companyDetails.CName,
                    IsMaster = true,
                    Title = companyDetails.Title,
                    AccountType = _accountType,
                    IsActive = true,
                    //SubscriptionId = _subscriptionId,
                    CorporateProfileId = _ProfileId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };
                var status = commonBusiness.CreateUserBasicDetail(crmUserBasicDetail);

                Model.OperatingCompany.Models.SubscriptionViewModel subscriptionViewModel = new Model.OperatingCompany.Models.SubscriptionViewModel();


                //pass values to CreateSubscription
                string _packageId = Guid.NewGuid().ToString("D");


                SubscriptionAndPaymentDetails subscriptionAndPaymentDetails = new SubscriptionAndPaymentDetails();
                subscriptionAndPaymentDetails.subscriptionViewModel = subscriptionViewModel;
                subscriptionAndPaymentDetails.SubscriptionDetails = subscriptionDetails;

               string   Paymentmethod_Id=   Guid.NewGuid().ToString("D");

                //Probability

                //Operator && Dispatch (Operator Qty>0 && Dispatch Qty>0)

                List<SubscriptionPackage> subscriptionlist = new List<SubscriptionPackage>();
                decimal subscriptionAmt = 0;
                if (Convert.ToInt32(subscriptionDetails.OperatorQuantity) > 0 && Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0)
                {

                    //operatore
                    var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("558911F7-876E-40C2-AF4F-388DBD83CB6D")).FirstOrDefault();
                    // objSubscriptionList - subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(subscriptionViewModel.PackageId)).FirstOrDefault();

                    ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                    subscription.ID = Guid.NewGuid();
                    subscription.TenantId = "";
                    // subscription.SubscriptionId = _subscriptionId;
                    subscription.SubscriptionCount = _SubscriptionCount;
                    subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                    subscription.CorporateProfileId = _ProfileId;
                    subscription.SubscriptionType = (byte?)_accountType;
                    subscription.PackageId = subscriptionPackage.PackageId.ToString();

                    subscription.PackageAmount = totalpackageamt_OP;
                    subscription.PaymentMethodId = Paymentmethod_Id;
                    //subscription.IsPaid = isActive
                    //subscription.IsEnable = isActive;
                    //subscription.PackageId = sbscriptionDetail.PackageId;
                    //subscription.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                    //subscription.SubStartdate = DateTime.Now;
                    // db.Subscription.Add(subscription);
                    db.Subscription.Add(subscription);
                    await db.SaveChangesAsync();
                    subscriptionlist.Add(subscriptionPackage);
                   

                    //dispatch
                    var subscriptionPackage2 = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("D350AC56-27E8-4442-9F1D-3EB6088DB9DA")).FirstOrDefault();
                    // objSubscriptionList - subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(subscriptionViewModel.PackageId)).FirstOrDefault();


                    subscription.ID = Guid.NewGuid();
                    subscription.TenantId = "";
                    // subscription.SubscriptionId = _subscriptionId;
                    subscription.SubscriptionCount = _SubscriptionDispatchCount;
                    subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                    subscription.CorporateProfileId = _ProfileId;
                    subscription.SubscriptionType = (byte?)_accountType;
                    subscription.PackageId = subscriptionPackage2.PackageId.ToString();

                    subscription.PackageAmount = totalpackageamt_DP;
                    subscription.PaymentMethodId = Paymentmethod_Id;
                    //subscription.IsPaid = isActive
                    //subscription.IsEnable = isActive;
                    //subscription.PackageId = sbscriptionDetail.PackageId;
                    //subscription.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                    //subscription.SubStartdate = DateTime.Now;
                    // db.Subscription.Add(subscription);
                    db.Subscription.Add(subscription);
                    await db.SaveChangesAsync();
                    subscriptionlist.Add(subscriptionPackage2);


                    //  subscriptionAmt = Convert.ToDecimal(subscriptionPackage2.PackageAmount) + Convert.ToDecimal(subscriptionPackage.PackageAmount);
                    subscriptionAmt = Convert.ToDecimal(totalpackageamt_DP) + Convert.ToDecimal(totalpackageamt_OP);

                }

                else if (Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0 && Convert.ToInt32(subscriptionDetails.ProviderQuantity) > 0)
                {

                    //dispatch
                    var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("D350AC56-27E8-4442-9F1D-3EB6088DB9DA")).FirstOrDefault();
                    // objSubscriptionList - subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(subscriptionViewModel.PackageId)).FirstOrDefault();

                    ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                    subscription.ID = Guid.NewGuid();
                    subscription.TenantId = "";
                    // subscription.SubscriptionId = _subscriptionId;
                    subscription.SubscriptionCount = _SubscriptionDispatchCount;
                    subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                    subscription.CorporateProfileId = _ProfileId;
                    subscription.SubscriptionType = (byte?)_accountType;
                    subscription.PackageId = subscriptionPackage.PackageId.ToString();

                    subscription.PackageAmount = totalpackageamt_DP;
                    subscription.PaymentMethodId = Paymentmethod_Id;
                    //subscription.IsPaid = isActive
                    //subscription.IsEnable = isActive;
                    //subscription.PackageId = sbscriptionDetail.PackageId;
                    //subscription.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                    //subscription.SubStartdate = DateTime.Now;
                    // db.Subscription.Add(subscription);
                    db.Subscription.Add(subscription);
                    await db.SaveChangesAsync();

                    subscriptionlist.Add(subscriptionPackage);
                 

                    //Provider
                    var subscriptionPackage2 = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("0D280653-7177-4DD3-9915-6CB83433BF70")).FirstOrDefault();
                    // objSubscriptionList - subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(subscriptionViewModel.PackageId)).FirstOrDefault();


                    subscription.ID = Guid.NewGuid();
                    subscription.TenantId = "";
                    // subscription.SubscriptionId = _subscriptionId;
                    subscription.SubscriptionCount = _SubscriptionCount;
                    subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                    subscription.CorporateProfileId = _ProfileId;
                    subscription.SubscriptionType = (byte?)_accountType;
                    subscription.PackageId = subscriptionPackage2.PackageId.ToString();

                    subscription.PackageAmount = totalpackageamt_P;
                    subscription.PaymentMethodId = Paymentmethod_Id;
                    //subscription.IsPaid = isActive
                    //subscription.IsEnable = isActive;
                    //subscription.PackageId = sbscriptionDetail.PackageId;
                    //subscription.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                    //subscription.SubStartdate = DateTime.Now;
                    // db.Subscription.Add(subscription);
                    db.Subscription.Add(subscription);
                    await db.SaveChangesAsync();
                    subscriptionlist.Add(subscriptionPackage2);


                    //  subscriptionAmt = Convert.ToDecimal(subscriptionPackage2.PackageAmount) + Convert.ToDecimal(subscriptionPackage.PackageAmount);
                    subscriptionAmt = Convert.ToDecimal(totalpackageamt_P) + Convert.ToDecimal(totalpackageamt_DP);

                }


                else if (Convert.ToInt32(subscriptionDetails.OperatorQuantity) > 0)
                {

                    //operatore
                    var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("558911F7-876E-40C2-AF4F-388DBD83CB6D")).FirstOrDefault();
                    // objSubscriptionList - subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(subscriptionViewModel.PackageId)).FirstOrDefault();

                    ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                    subscription.ID = Guid.NewGuid();
                    subscription.TenantId = "";
                    // subscription.SubscriptionId = _subscriptionId;
                    subscription.SubscriptionCount = _SubscriptionCount;
                    subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                    subscription.CorporateProfileId = _ProfileId;
                    subscription.SubscriptionType = (byte?)_accountType;
                    subscription.PackageId = subscriptionPackage.PackageId.ToString();

                    subscription.PackageAmount = totalpackageamt_OP;
                    subscription.PaymentMethodId = Paymentmethod_Id;
                    //subscription.IsPaid = isActive
                    //subscription.IsEnable = isActive;
                    //subscription.PackageId = sbscriptionDetail.PackageId;
                    //subscription.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                    //subscription.SubStartdate = DateTime.Now;
                    // db.Subscription.Add(subscription);
                    db.Subscription.Add(subscription);
                    await db.SaveChangesAsync();
                    subscriptionlist.Add(subscriptionPackage);


                    // subscriptionAmt = Convert.ToDecimal(subscriptionPackage.PackageAmount);
                    subscriptionAmt = Convert.ToDecimal(totalpackageamt_OP);

                }
                else if (Convert.ToInt32(subscriptionDetails.DispatchQuantity) > 0)
                {

                    //Dispatch
                    var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("D350AC56-27E8-4442-9F1D-3EB6088DB9DA")).FirstOrDefault();
                    // objSubscriptionList - subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(subscriptionViewModel.PackageId)).FirstOrDefault();

                    ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                    subscription.ID = Guid.NewGuid();
                    subscription.TenantId = "";
                    // subscription.SubscriptionId = _subscriptionId;
                    subscription.SubscriptionCount = _SubscriptionCount;
                    subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                    subscription.CorporateProfileId = _ProfileId;
                    subscription.SubscriptionType = (byte?)_accountType;
                    subscription.PackageId = subscriptionPackage.PackageId.ToString();

                    subscription.PackageAmount = totalpackageamt_DP;
                    subscription.PaymentMethodId = Paymentmethod_Id;
                    //subscription.IsPaid = isActive
                    //subscription.IsEnable = isActive;
                    //subscription.PackageId = sbscriptionDetail.PackageId;
                    //subscription.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                    //subscription.SubStartdate = DateTime.Now;
                    // db.Subscription.Add(subscription);
                    db.Subscription.Add(subscription);
                    await db.SaveChangesAsync();
                    subscriptionlist.Add(subscriptionPackage);


                    //   subscriptionAmt = Convert.ToDecimal(subscriptionPackage.PackageAmount);
                    subscriptionAmt = Convert.ToDecimal(totalpackageamt_DP);

                }

                else if (Convert.ToInt32(subscriptionDetails.ProviderQuantity) > 0)
                {

                    //provider
                    var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid("0D280653-7177-4DD3-9915-6CB83433BF70")).FirstOrDefault();
                    // objSubscriptionList - subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(subscriptionViewModel.PackageId)).FirstOrDefault();

                    ProductSubscriptionModel subscription = new ProductSubscriptionModel();
                    subscription.ID = Guid.NewGuid();
                    subscription.TenantId = "";
                    // subscription.SubscriptionId = _subscriptionId;
                    subscription.SubscriptionCount = _SubscriptionCount;
                    subscription.SubscriptionDispatchCount = _SubscriptionDispatchCount;
                    subscription.CorporateProfileId = _ProfileId;
                    subscription.SubscriptionType = (byte?)_accountType;
                    subscription.PackageId = subscriptionPackage.PackageId.ToString();

                    subscription.PackageAmount = totalpackageamt_P;
                    subscription.PaymentMethodId = Paymentmethod_Id;
                    //subscription.IsPaid = isActive
                    //subscription.IsEnable = isActive;
                    //subscription.PackageId = sbscriptionDetail.PackageId;
                    //subscription.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
                    //subscription.SubStartdate = DateTime.Now;
                    db.Subscription.Add(subscription);
                    await db.SaveChangesAsync();

                    subscriptionlist.Add(subscriptionPackage);

                    //   subscriptionAmt = Convert.ToDecimal(subscriptionPackage.PackageAmount);
                    subscriptionAmt = Convert.ToDecimal(totalpackageamt_P);
                }

                
                subscriptionAndPaymentDetails.PaymentDetails= registrationDetails.PaymentDetails;
       
                var SubscriptionDetails = await CreateSubscription(subscriptionAndPaymentDetails,
                    subscriptionlist, subscriptionAmt, _ProfileId);

                if (ViewData["CardError"].ToString() != "ok")
                {
                    //error message for ajax in registration.js              
                    return Json(new { email = ViewData["CardError"].ToString(), type = "Error" });
                }


                if (paymentDetails.PaymentType == "Credit Card")
                {

                    PaymentMethod paymentMethod = new PaymentMethod();
                   
                    paymentMethod.ID = Paymentmethod_Id;
                    //paymentMethod.TenantId = _tenantId;
                    paymentMethod.Holder = paymentDetails.CardName;
                    paymentMethod.AccountName = paymentDetails.CardName;
                    //   paymentMethod.CardType = paymentDetails.PaymentType;
                    paymentMethod.PayType= "1";
                    paymentMethod.AccountNumber = paymentDetails.CardNumber;
                    paymentMethod.Number = paymentDetails.CardNumber;
                    //paymentMethod.AccountName = paymentDetails.CardVerificationNumber;
                    paymentMethod.ExpireMonth = paymentDetails.Month;
                    paymentMethod.ExpireYear = paymentDetails.Year;
                    paymentMethod.CVV = paymentDetails.CardVerificationNumber;

                    paymentMethod.FirstName = companyDetails.BillingName;
                    paymentMethod.Address1 = companyDetails.BillingAddress1;
                    paymentMethod.Address2 = companyDetails.BillingAddress2;
                    paymentMethod.City = companyDetails.BillingCity;
                    paymentMethod.State = companyDetails.BillingState;
                    paymentMethod.Zip = companyDetails.CompanyZip;
                   

                    var CardNumBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.CardNumber));
                    var CardHashCode = Convert.ToBase64String(CardNumBytes);
                    paymentMethod.Number = CardHashCode;
                    var MothBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.Month));
                    var ExpireMothHashCode = Convert.ToBase64String(MothBytes);
                    paymentMethod.ExpireMonth = ExpireMothHashCode;
                    var ExpireYearBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.Year));
                    var ExpireYearHashCode = Convert.ToBase64String(ExpireYearBytes);
                    paymentMethod.ExpireYear = ExpireYearHashCode;

                    db.PaymentMethods.Add(paymentMethod);
                 await   db.SaveChangesAsync();
                }
                else if (paymentDetails.PaymentType == "Debit Card")
                {

                    PaymentMethod paymentMethod = new PaymentMethod();
                    paymentMethod.ID = Paymentmethod_Id;
                    //  paymentMethod.ID = Guid.NewGuid().ToString("D");
                    //paymentMethod.TenantId = _tenantId;
                    paymentMethod.Holder = paymentDetails.CardName;
                    paymentMethod.AccountName = paymentDetails.CardName;
                    //  paymentMethod.CardType = paymentDetails.PaymentType;
                    paymentMethod.PayType = "2";
                    paymentMethod.AccountNumber = paymentDetails.CardNumber;
                    paymentMethod.Number = paymentDetails.CardNumber;
                    paymentMethod.CVV = paymentDetails.CardVerificationNumber;
                     paymentMethod.ExpireMonth = paymentDetails.Month;
                    paymentMethod.ExpireYear = paymentDetails.Year;

                    paymentMethod.FirstName = companyDetails.BillingName;
                    paymentMethod.Address1 = companyDetails.BillingAddress1;
                    paymentMethod.Address2 = companyDetails.BillingAddress2;
                    paymentMethod.City = companyDetails.BillingCity;
                    paymentMethod.State = companyDetails.BillingState;
                    paymentMethod.Zip = companyDetails.CompanyZip;

                    var CardNumBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.CardNumber));
                    var CardHashCode = Convert.ToBase64String(CardNumBytes);
                    paymentMethod.Number = CardHashCode;
                    var MothBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.Month));
                    var ExpireMothHashCode = Convert.ToBase64String(MothBytes);
                    paymentMethod.ExpireMonth = ExpireMothHashCode;
                    var ExpireYearBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.Year));
                    var ExpireYearHashCode = Convert.ToBase64String(ExpireYearBytes);
                    paymentMethod.ExpireYear = ExpireYearHashCode;

                    db.PaymentMethods.Add(paymentMethod);
                    await db.SaveChangesAsync();
                }
                else if (paymentDetails.PaymentType == "ACH Payment")
                {

                    PaymentMethod paymentMethod = new PaymentMethod();

                    // paymentMethod.ID = Guid.NewGuid().ToString("D");
                    paymentMethod.ID = Paymentmethod_Id;
                    //paymentMethod.TenantId = _tenantId;
                    // paymentMethod.Holder = paymentDetails.CustomerName;
                    paymentMethod.CustomerName = paymentDetails.CustomerName;
                    // paymentMethod.CardType = paymentDetails.PaymentType;
                    paymentMethod.PayType = "3";
                    paymentMethod.AccountNumber = paymentDetails.AccountNumber;
                    //paymentMethod.AccountName = paymentDetails.CardVerificationNumber;
                 //   paymentMethod.ExpireMonth = paymentDetails.Month;
                  //  paymentMethod.ExpireYear = paymentDetails.Year;
                    paymentMethod.TypeofAccount = paymentDetails.TypeofAccount;
                    paymentMethod.RoutingNumber = paymentDetails.RoutingNumber;
                    paymentMethod.BankName = paymentDetails.BankName;
                    paymentMethod.CVV = paymentDetails.CardVerificationNumber;

                    paymentMethod.FirstName = companyDetails.BillingName;
                    paymentMethod.Address1 = companyDetails.BillingAddress1;
                    paymentMethod.Address2 = companyDetails.BillingAddress2;
                    paymentMethod.City = companyDetails.BillingCity;
                    paymentMethod.State = companyDetails.BillingState;
                    paymentMethod.Zip = companyDetails.CompanyZip;

                   // var CardNumBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.AccountNumber));
                   // var CardHashCode = Convert.ToBase64String(CardNumBytes);
                   // paymentMethod.Number = CardHashCode;
                   //var MothBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.Month));
                   // var ExpireMothHashCode = Convert.ToBase64String(MothBytes);
                   // paymentMethod.ExpireMonth = ExpireMothHashCode;
                   // var ExpireYearBytes = Encoding.UTF32.GetBytes(Convert.ToString(paymentDetails.Year));
                   // var ExpireYearHashCode = Convert.ToBase64String(ExpireYearBytes);
                   // paymentMethod.ExpireYear = ExpireMothHashCode;

                    db.PaymentMethods.Add(paymentMethod);
                    await db.SaveChangesAsync();
                }
                //Email invitation to send to user




                if (status == true)
                {
                    EmailHandler emailHandler = new EmailHandler();
                    //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    //  string Password = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(randomPassword.ToString()));
                    //  companyDetails.CompanyEmail = "murugesan.c@techunity.com";
                    var Url = _configuration.GetSection("Url")["Url"];
                    var callbackUrl = Url + "/Identity/Account/ConfirmEmail?userId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id)) + "&code=" + randomPassword; //Url.Page("Account/ResetPassword", pageHandler: null, values: new { area = "Identity", code },

                    //var callbackUrl = "http://" + Request.Host.Value + "/Identity/Account/ConfirmEmail?userId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id)) + "&code=" + randomPassword; //Url.Page("Account/ResetPassword", pageHandler: null, values: new { area = "Identity", code },
                    var flagStatus = await emailHandler.SendEmailAsync("Member", companyDetails.CompanyEmail, "Confirm Email", $"Dear Member, check your login password: "+ randomPassword + " ,please click on  <a href='" + callbackUrl + "'><b>Link</b></a> to confirm you email address, thanks");
                    // ADDEd BY GP TO GET ACTIVATION LINK
                    //ErrorHandler errorHandler = new ErrorHandler(_signInManager, roleManager, _userManager, db);
                    //errorHandler.ErrorLog(callbackUrl, "Activation Link");

                    if(flagStatus==true)
                    {
                        commonBusiness.UpdateUserPagesCompleteStatus(1, user.Id);
                        // return RedirectToPage("RegisterConfirmation", new { email = companyDetails.CompanyEmail, type = _SubscriptionType.ToString() });
                        
                        //create permission
                        var _tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                        CreatePermission("Dispatcher", companyDetails.Name, _tenantId);
                        CreatePermission("Driver", companyDetails.Name, _tenantId);

                    }

                }


                //return success


                //    trans.Commit();

                //}
                //});

                var _corparateId_temp = _ProfileId;

                return Json(new { email = companyDetails.CompanyEmail, type = _SubscriptionType.ToString() });
            }
            else
            {
                string mailerror = "Already Exist! Please check your email.(" + companyDetails.CompanyEmail + ")";
                return Json(new { email = mailerror + "", type = "Error" });
                //  CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //   customErrorHandler.WriteError(null, "Registration", User.Identity.Name);
                //  string returnUrl = @"/Registration/Error";
                //return LocalRedirect(returnUrl);

            }
            //  return Json(registrationDetails);
           
        }




        private async Task<JsonResult> CreateSubscription(WellAI.Advisor.Model.Common.SubscriptionAndPaymentDetails subscriptionAndPaymentDetails, List<SubscriptionPackage> subscriptionPackage,decimal subscriptionAmt, string _ProfileId)
        {
            //try
            //{
                Model.OperatingCompany.Models.SubscriptionViewModel subscriptionViewModel = subscriptionAndPaymentDetails.subscriptionViewModel;
                SubscriptionDetails subscriptionDetails = subscriptionAndPaymentDetails.SubscriptionDetails;
                CompanyDetails companyDetails = subscriptionAndPaymentDetails.CompanyDetails;
                PaymentDetails paymentDetails = subscriptionAndPaymentDetails.PaymentDetails;

                var authorizeNetSection = _configuration.GetSection("AuthorizeNet");
                string invoiceId = string.Empty;
               
                PaymentMethod paymentModel = new PaymentMethod();

               // var subscriptionPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(subscriptionViewModel.PackageId)).FirstOrDefault();


            var services = new ServiceCollection();
                services.UseServices();
                var serviceProvider = services.BuildServiceProvider();
                var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
                Interval interval = new Interval();

            //interval.length = (short)subscriptionPackage.Length; //1
            //interval.unit = subscriptionPackage.Unit;  // Monthly //
            //PaymentSchedule paySechedule = new PaymentSchedule();
            //paySechedule.totalOccurrences = (short)subscriptionPackage.TotalOccurrences;

            interval.length = 1; //1
            interval.unit = 1;  // Monthly //
            PaymentSchedule paySechedule = new PaymentSchedule();
            paySechedule.totalOccurrences = 12;

            // paySechedule.trialOccurrences = (short)subscriptionPackage.TrialOccurrences;
            paySechedule.startDate = DateTime.Now.ToString();
                var creditCard = new CreditCardType()
                {
                    
                    CardNumber = paymentDetails.CardNumber,
                    ExpirationDate = paymentDetails.Year + "-" + paymentDetails.Month,
                    CardCode = paymentDetails.CardVerificationNumber,
                    //TokenRequestorName= paymentDetails.CardName,
                };
                CustomerAddressType addressType = new CustomerAddressType();
                addressType.FirstName = paymentDetails.CardName;
                addressType.LastName = paymentDetails.CardName;
            //   addressType.LastName = paymentModel.LastName;
            PaymentType _payment = new PaymentType();
                _payment.Item = creditCard;
                SubscriptionType subType = new SubscriptionType();

                subType.amount = subscriptionAmt;//variable total  value from subscription line items
                subType.paymentSchedule = paySechedule;
                subType.paymentSchedule.interval = new Interval();
                subType.paymentSchedule.interval = interval;
                subType.billTo = new CustomerAddressType();
                subType.billTo = addressType;
                subType.payment = _payment;

                SubscriptionResponse response = new SubscriptionResponse();

            //Authorize Net function

            if(paymentDetails.PaymentType== "Credit Card")
            {
                response = service.CreateSubscription(creditCard, subType, authorizeNetSection["AccountType"]);
            }
           else if (paymentDetails.PaymentType == "Debit Card")
            {
                response = service.CreateSubscription(creditCard, subType, authorizeNetSection["AccountType"]);
            }
            else if (paymentDetails.PaymentType == "ACH Payment")
            {
                response = service.CreateSubscription(creditCard, subType, authorizeNetSection["AccountType"]);
            }

            if (!string.IsNullOrEmpty(response.SubscriptionId))
                {
                    string subscriptionId = response.SubscriptionId;


                   invoiceId = await SaveSubscriptionDetail(paymentModel, subscriptionId, subscriptionViewModel, _ProfileId);
                   ViewData["CardError"] = "ok";
                return Json(new { message = "", data = new { name = subscriptionPackage[0].Name, packageAmount = subType.amount, subscriptionViewModel.RigCount, description = subscriptionPackage[0].Description, invoiceId, subscriptionId } });
                }
                else
                {
                    //Phase II Changes - 03/26/2021
                    if (response != null)
                    {
                    ViewData["CardError"] = response.Message;
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
            //}
            //catch (Exception ex)
            //{
            //    ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
            //    var userIdentity = (ClaimsIdentity)User.Identity;
            //    errorHandler.ErrorLog(ex.Message, userIdentity.Name, ex.HResult.ToString());
            //    _logger.LogInformation(ex.Message);
            //    return null;
            //}
        }

        private async Task<string> SaveSubscriptionDetail(PaymentMethod paymentModel, string subscriptionId, Model.OperatingCompany.Models.SubscriptionViewModel subscriptionDetail, string _ProfileId)
        {
            string invoiceId = string.Empty;
            try
            {
              //  var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                await AddProductSubscription(subscriptionDetail, subscriptionId, _ProfileId);
                var opprepo = new OperatingTenantRepository(db);
             ///   invoiceId = await opprepo.CreateBillingInvoice(paymentModel, subscriptionDetail, subscriptionId, tenantId);
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
        public async Task<int> AddProductSubscription(Model.OperatingCompany.Models.SubscriptionViewModel sbscriptionDetail, string subscriptionId, string _ProfileId)
        {
            int result = 0;

            //bool isActive = string.IsNullOrEmpty(subscriptionId) ? false : true;
            //var item = (from x in db.Subscription
            //            where x.CorporateProfileId.Equals(_ProfileId)
            //            select x).FirstOrDefault();

            //if (item == null)
            //{
            //    ProductSubscriptionModel subscription = new ProductSubscriptionModel();
            //    subscription.ID = Guid.NewGuid();
            //    subscription.SubscriptionId = subscriptionId;
            //    subscription.IsPaid = isActive;
            //    subscription.IsEnable = isActive;
            //    subscription.TenantId = tenantId;
            //    subscription.SubscriptionCount = int.Parse(sbscriptionDetail.RigCount);
            //    subscription.PackageId = sbscriptionDetail.PackageId;
            //    subscription.PackageAmount = decimal.Parse(sbscriptionDetail.subscriptionAmt);
            //    subscription.SubStartdate = DateTime.Now;
            //    db.Subscription.Add(subscription);
            //    result = await db.SaveChangesAsync();
            //}
            //else
            //{
            //    item.SubscriptionId = subscriptionId;
            //    item.IsPaid = isActive;
            //    item.IsEnable = isActive;
            //    item.TenantId = tenantId;
            //    item.SubscriptionCount = int.Parse(sbscriptionDetail.RigCount);
            //    item.PackageId = sbscriptionDetail.PackageId;
            //    item.PackageAmount = decimal.Parse(sbscriptionDetail.TotalAmount);
            //    db.Subscription.Update(item);
            //    result = await db.SaveChangesAsync();
            //}

            bool isActive = string.IsNullOrEmpty(subscriptionId) ? false : true;
            var item = (from x in db.Subscription
                        where x.CorporateProfileId.Equals(_ProfileId)
                        select x);

            foreach (var record in item)
            {
                record.SubscriptionId = subscriptionId;
                record.IsPaid = isActive;
                record.IsEnable = isActive;
                record.SubStartdate = DateTime.Now;

            }

            db.SaveChanges();

            return result;
        }

        private void UpdateTransactionHistory(string SubscriptionID)
        {
            WellAIFunctionHandlerContext dbFun = new WellAIFunctionHandlerContext();

            var services1 = new ServiceCollection();
            services1.UseServices();
            var serviceProvider1 = services1.BuildServiceProvider();
            var service1 = serviceProvider1.GetRequiredService<IRecurringBillingService>();

            // string sFirstdate = "2022-09-10T16:00:00Z";
            DateTime Firstdate = DateTime.Now.AddHours(-1);

            // string sLastdate = "2022-09-30T16:00:00Z";
            // DateTime Lastdate = Convert.ToDateTime(sLastdate);
            DateTime Lastdate = DateTime.Now.AddDays(1);

            var Batch = service1.GetSettledBatchListStatus("", Firstdate, Lastdate);

            var JsonBatch = JsonConvert.SerializeObject(new { BodyParameter = Batch });
            dynamic BatchStatus = JsonConvert.DeserializeObject(JsonBatch);

            int RecCount = Enumerable.Count(BatchStatus.BodyParameter.batchList);
            String type = "";
            string subscriptionnId = "";
            string AddressId = "";
            for (int i = 0; i < RecCount; i++)
            {
                string batchId = BatchStatus.BodyParameter.batchList[i].batchId;
                if (batchId != null)
                {
                    var Trans = service1.GetTransactionListRequestStatus("", batchId);
                    var JsonTrans = JsonConvert.SerializeObject(new { BodyParameter = Trans });
                    dynamic TransStatus = JsonConvert.DeserializeObject(JsonTrans);
                  
                    int TransRecCount = Enumerable.Count(TransStatus.BodyParameter.transactions);
                    for (int j = 0;j < TransRecCount; j++)
                    {
                        if (TransStatus.BodyParameter.transactions[j].subscription.id != null)
                        {
                            if (TransStatus.BodyParameter.transactions[j].subscription.id != SubscriptionID)
                            {
                                subscriptionnId = TransStatus.BodyParameter.transactions[j].subscription.id;
                                string Invoice = TransStatus.BodyParameter.transactions[j].invoice;
                                string Billdatee = TransStatus.BodyParameter.transactions[j].submitTimeUTC;
                                string TransactionID = TransStatus.BodyParameter.transactions[j].transId;
                                DateTime Billdate = Convert.ToDateTime(Billdatee);

                                var SubDetails = service1.GetListOfSubscriptions("");
                                var JsonSubDetails = JsonConvert.SerializeObject(new { BodyParameter = SubDetails });
                                dynamic SubDetailsStatus = JsonConvert.DeserializeObject(JsonSubDetails);

                                int RecCount1 = Enumerable.Count(SubDetailsStatus.BodyParameter.subscriptionDetails);
                                for (int k = 0; k < RecCount; k++)
                                {
                                    string subid = SubDetailsStatus.BodyParameter.subscriptionDetails[k].id;
                                    if (subid == SubscriptionID)
                                    {
                                        type = SubDetailsStatus.BodyParameter.subscriptionDetails[k].paymentMethod;
                                        break;
                                    }
                                }
                                
                                var Subitem = (from x in dbFun.ProductSubscriptionModel
                                               where x.SubscriptionId.Equals(SubscriptionID)
                                               select x).FirstOrDefault();
                                if (Subitem.TenantId.ToString() != null)
                                {
                                    var Addid = (from x in dbFun.PaymentMethod
                                                 where x.TenantId.Equals(Subitem.TenantId.ToString())
                                                 select x).FirstOrDefault();
                                    if (Addid.ID.ToString() != null)
                                    {
                                        AddressId = Addid.ID.ToString();
                                    }

                                }

                                if (subscriptionnId == SubscriptionID && TransactionID != null)
                                {
                                    var item = (from x in dbFun.billingHistoryModel
                                                where x.TransactionID.Equals(TransactionID)
                                                select x).FirstOrDefault();
                                    if (item == null)
                                    {
                                        BillingHistory bh = new BillingHistory();
                                        bh.ID = Guid.NewGuid().ToString("D");
                                        bh.Name = TransStatus.BodyParameter.transactions[0].firstName + " " + TransStatus.BodyParameter.transactions[0].lastName;
                                        bh.Invoice = Invoice;
                                        bh.BillDate = Billdate;
                                        bh.Amount = TransStatus.BodyParameter.transactions[0].settleAmount;
                                        bh.Subscriptions = 1;
                                        bh.PayMethod = type;
                                        bh.AddressId = AddressId;
                                        bh.TenantId = Subitem.TenantId.ToString();
                                        bh.TransactionID = TransactionID;
                                        dbFun.billingHistoryModel.Add(bh);
                                        dbFun.SaveChanges();
                                    }
                                }
                                break;
                            }
                               
                           
                        }
                    }
                     
                }
            }
        }

        public async Task<string> ConfirmEmail(string userId)
        {
            string res = "";
            var tUserId = Encoding.UTF8.GetString(Convert.FromBase64String(userId));
            var user = await _userManager.FindByIdAsync(tUserId);
            if (user == null)
            {
                return "Unable to load user with ID '{userId}'.";
            }

            var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var detail = commonBusiness.GetUserBasicDetail(user.Id);

                var tenantid = Guid.NewGuid();

                var tenantExistId = await commonBusiness.GetTenantIdByUserId(user.Id);
                if (string.IsNullOrEmpty(tenantExistId))
                {
                    user.TenantId = tenantid.ToString("D");
                    var result1 = await _userManager.UpdateAsync(user);
                    CorporateProfile corporateProfile = new CorporateProfile()
                    {
                        ID = Guid.NewGuid().ToString(),
                        UserId = user.Id,
                        Name = detail.Company,
                        Phone = user.PhoneNumber,
                        TenantId = user.TenantId
                    };
                    db.CorporateProfile.Add(corporateProfile);
                    db.SaveChanges();
                    await commonBusiness.CreateTenantUser(tUserId, tenantid.ToString("D"));

                    var blobSection = _configuration.GetSection("AzureBlob");

                    await AzureBlobStorage.EnsureBlobContainerForTenant(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenantid.ToString("D"));

                    var folders = await commonBusiness.GetWellFileFolders();
                    var folderForAccount = folders.Where(x => x.AccountType == detail.AccountType).ToList();

                    await AzureBlobStorage.EnsureFolderTreeInContainerForTenant(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenantid.ToString("D"), folderForAccount);

                    var dbprefix = "oper";
                    if (detail.AccountType == 1)
                        dbprefix = "serv";

                    //Create Tenant DB only if not Dispatch type
                    if (detail.AccountType != 2)
                    {
                        var newDbName = "wellai_" + dbprefix + "db_" + tenantid.ToString("N");
                        var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", newDbName);

                        var repo = new TenantRepository(_configuration.GetConnectionString("WebAIAdvisorContextConnection"));
                        var resnewdb = await repo.CreateTenantDB(newDbName);

                        commonBusiness.CreateWellTenant(tenantid.ToString("D"), connString, tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"));

                        var ti = new TenantInfo(tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"), connString, null);

                        if (detail.AccountType == 0)
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
                    else
                    {
                        var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "");

                        var repo = new TenantRepository(_configuration.GetConnectionString("WebAIAdvisorContextConnection"));
                        //var resnewdb = await repo.CreateTenantDB(newDbName);
                        commonBusiness.CreateWellTenant(tenantid.ToString("D"), connString, tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"));
                        var ti = new TenantInfo(tenantid.ToString("D"), tenantid.ToString("D"), tenantid.ToString("D"), connString, null);
                        await commonBusiness.CreateTenantRoles(tenantid.ToString("D"));
                    }

                    var r = _store as MultiTenantStoreWrapper<TenantConfigurationStore>;

                    var id = Guid.NewGuid().ToString("D");

                    var res1 = await r.Store.TryUpdateAsync(new TenantInfo(id, id, id, "", null));
                }
            }
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return res;
        }

        public async Task<bool> UpdateTenentId()
        {
            try
            {
                var _tenantId = WellAIAppContext.Current.Session.GetString("TenantId");


                //update tenantid to subscriotion table
                ProductSubscriptionModel productSubscriptionModel = new ProductSubscriptionModel();
                productSubscriptionModel.TenantId = _tenantId;

                db.Subscription.Add(productSubscriptionModel);
                db.Entry(productSubscriptionModel).State = EntityState.Modified;
                db.SaveChanges();

                //update tenantid to corporateProfile table
                CorporateProfile corporateProfile = new CorporateProfile();
                corporateProfile.TenantId = _tenantId;

                db.CorporateProfile.Add(corporateProfile);
                db.Entry(corporateProfile).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                return false;
            }


            return true;
        }
        private async Task<Boolean> CreatePermission(string permissionTitle, string UserName, string tenantId)
        {
            try
            {

                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var components = db.Components.Where(x => x.IsActive == true && x.AccountType == 1).ToList();

                //var userIdentityName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                bool result = false;
                var permcomps = new List<RolePermissionComponentModel>();
                //if (db.RolePermissions.Where(x => x.RolePermissionId == permission.Id).FirstOrDefault() == null)
                //{
                foreach (var newItem in components)
                {
                    var permcomp = new RolePermissionComponentModel
                    {
                        ComponentId = newItem.ComponentId,
                        ComponentName = newItem.Label,
                        IsPermitted = false
                    };
                    permcomps.Add(permcomp);
                }
                result = await CreatePermissionComponents(permissionTitle, permcomps, UserName, tenantId);
                // if (result == true)
                //{
                CreateRole(permissionTitle, UserName, tenantId);
                //}


                return result;
            }
            catch (Exception ex)
            {
                //CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                //customErrorHandler.WriteError(ex, "Customer SavePermission", User.Identity.Name);

                return false;
            }

        }
        public async Task<bool> CreatePermissionComponents(string permissionName, List<RolePermissionComponentModel> actualPermComps, string userName, string tenatId)
        {
            var result = false;

            try
            {
                if (db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName && x.TenantId == tenatId) == null)// permission with same name not exists
                {
                    var newperm = db.RolePermissions.Add(new RolePermissions
                    {
                        RolePermissionName = permissionName,
                        IsActive = true,
                        TenantId = tenatId,
                        CreatedBy = userName,
                        ModifiedBy = userName,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                    await db.SaveChangesAsync();
                }
                else
                    return false;

                var permission = db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName && x.TenantId == tenatId);
                var oldRoleComps = db.RolePermissionComponentLinks.Where(x => x.RolePermissionId == permission.RolePermissionId).ToList(); // all components that were before current updates
                var components = db.Components;

                for (int i = 0; i < actualPermComps.Count; i++)
                {
                    var odlcomponent = components.First(x => x.Label == actualPermComps[i].ComponentName);
                    var oldpermcomp = oldRoleComps.FirstOrDefault(x => x.ComponentId == actualPermComps[i].ComponentId);

                    if (oldpermcomp != null)
                    {
                        oldpermcomp.IsPermitted = true;
                    }
                    else
                    {
                        db.RolePermissionComponentLinks.Add(new RolePermissionComponentLinks
                        {
                            RolePermissionId = permission.RolePermissionId,
                            ComponentId = odlcomponent.ComponentId,
                            IsPermitted = true
                        });
                    }
                }

                await db.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                // CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                //customErrorHandler.WriteError(ex, "ICustomerProfile CreatePermissionComponents", null);
                result = false;
            }

            return result;
        }


        private async Task<Boolean> CreateRole(string Title, string UserName, string tenantId)
        {
            try
            {
                if (tenantId == null)
                {
                    return false;
                }
                var userIdentityName = UserName;// User.FindFirst(ClaimTypes.NameIdentifier).Value;
                                                //var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                                                //var tocreate = input.Where(x => x["Id"] == null || x["Id"] == "").ToList();
                                                //foreach (var newItem in tocreate)
                                                //{
                var roleperms = new List<RolePermissions>();

                //foreach (var permstatus in newItem)
                //{
                //    if (permstatus.Key != "Id" && permstatus.Key != "Title")
                //    {
                //        var roleperm = new RolePermissions
                //        {
                //            RolePermissionName = permstatus.Key,
                //            IsActive = permstatus.Value == "true"
                //        };
                //        roleperms.Add(roleperm);
                //    }
                //}

                await CreateRolePermissions(Title, roleperms, userIdentityName, tenantId);
                //}



                return true;// Json(dt.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                // CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                // customErrorHandler.WriteError(ex, "Customer ManageRoles_CreateAsync", User.Identity.Name);

                return false;
            }
        }
        public async Task<bool> CreateRolePermissions(string roleName, List<RolePermissions> actualRolePerms, string userName, string tenantId)
        {
            var result = false;

            try
            {
                var exists = (from r in db.Roles
                              join rt in db.TenantRoles on r.Id equals rt.RoleId
                              where r.Name.ToLower() == roleName.ToLower() && rt.TenantId == tenantId
                              select r).FirstOrDefault();

                if (exists == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    });
                }
                else
                    return false;

                var role = (from r in db.Roles
                            where r.Name.ToLower() == roleName.ToLower()
                            select r).FirstOrDefault();

                db.TenantRoles.Add(new TenantRoles { RoleId = role.Id, TenantId = tenantId });
                db.SaveChanges();

                var oldRolePermissions = db.RolePermissionLinks.Where(x => x.RoleId == role.Id).ToList();
                var permissions = db.RolePermissions;

                for (int i = 0; i < actualRolePerms.Count; i++)
                {
                    var odlPermission = permissions.First(x => x.RolePermissionName == actualRolePerms[i].RolePermissionName);
                    var oldroleperm = oldRolePermissions.FirstOrDefault(x => x.RolePermissionId == odlPermission.RolePermissionId);

                    if (oldroleperm != null)
                    {
                        oldroleperm.IsPermitted = actualRolePerms[i].IsActive;
                    }
                    else
                    {
                        db.RolePermissionLinks.Add(new RolePermissionLinks
                        {
                            RoleId = role.Id,
                            RolePermissionId = odlPermission.RolePermissionId,
                            IsPermitted = actualRolePerms[i].IsActive
                        });
                    }
                }

                await db.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                // CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                //customErrorHandler.WriteError(ex, "ICustomerProfile CreateRolePermissions", null);
                result = false;
            }

            return result;
        }

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    //Phase II Changes - 03/16/2021
        //   // await DeleteUserSession();
        //    return RedirectToAction("login", "account");
        //}

        //private async Task<int> DeleteUserSession()
        // {
        //     var email = WellAIAppContext.Current.Session.GetString(Constants.SessionAdminUser);
        //     try
        //     {
        //         if (email != null)
        //         {
        //             var sessionDetails = db.StaffUserSessions.Where(x => x.UserName == email).AsNoTracking();

        //             DLL.Data.StaffUserSessions session = new DLL.Data.StaffUserSessions();
        //             if (sessionDetails != null)
        //             {

        //                 foreach (var usrSession in sessionDetails)
        //                 {
        //                     if (usrSession.SessionId != null)
        //                     {
        //                         db.StaffUserSessions.Remove(usrSession);
        //                     }

        //                 }
        //                 await db.SaveChangesAsync();
        //             }
        //         }
        //         return 1;

        //     }
        //     catch (Exception ex)
        //     {


        //         return 0;
        //     }
        // }

    }
}

