using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.DLL.Repository;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    public class BillingInvoiceHistorySRVNewController : BaseController
    {
        private readonly ILogger<BillingInvoiceHistorySRVNewController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantOperatingDbContext _servicedb;
        public BillingInvoiceHistorySRVNewController(TenantOperatingDbContext tdbContext, UserManager<WellIdentityUser> userManager,
                                                  SignInManager<WellIdentityUser> signInManager,RoleManager<IdentityRole> roleManager,
                                                  WebAIAdvisorContext dbContext,ILogger<BillingInvoiceHistorySRVNewController> logger)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _servicedb = tdbContext;
        }
        public IActionResult Index()
        {
            try
            {
               
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "BillingInvoiceHistorySRV Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult GetBillinghistory_old([DataSourceRequest] DataSourceRequest request)
        {
            List<BillingInvoiceHistorySRVNewModel> BillingInvoiceHistory = new List<BillingInvoiceHistorySRVNewModel>();
            try
            {
                //DispatchDashboardModel dispatch = new DispatchDashboardModel();
                //data = _singleton.auctionProposalBusiness.GetAuctionsProposalListForSRV(tenantid, operId, servrepo); //GetBidsData(valueFrom, valueTo);

                //DispatchDashboardModel model = new DispatchDashboardModel("1234", "ACTIVE");
                //driverStatus.Add(model);


                BillingInvoiceHistorySRVNewModel model = new BillingInvoiceHistorySRVNewModel();

                model.InvoiceId = "01";
                model.InvoiceNo = "2264861009";
                model.Product = "Dispatch";
                model.BillDate = Convert.ToDateTime("06-06-2022");
                model.Amount = "$100";
                model.Paymentmethod = "Credit Card";

                BillingInvoiceHistory.Add(model);

                model = new BillingInvoiceHistorySRVNewModel();

                model.InvoiceId = "02";
                model.InvoiceNo = "2264861009";
                model.Product = "Dispatch";
                model.BillDate = Convert.ToDateTime("06-07-2022");
                model.Amount = "$100";
                model.Paymentmethod = "Credit Card";
                BillingInvoiceHistory.Add(model);

                //model = new BillingInvoiceHistorySRVNewModel();

                //model.InvoiceId = "03";
                //model.InvoiceNo = "2264861010";
                //model.Product = "Advisor";
                //model.BillDate = Convert.ToDateTime("07-09-2022");
                //model.Amount = "$51.98";
                //model.Paymentmethod = "Debit Card";
                //BillingInvoiceHistory.Add(model);

                //model = new BillingInvoiceHistorySRVNewModel();

                //model.InvoiceId = "04";
                //model.InvoiceNo = "2264861010";
                //model.Product = "Advisor";
                //model.BillDate = Convert.ToDateTime("08-09-2022");
                //model.Amount = "$51.98";
                //model.Paymentmethod = "Debit Card";

                //BillingInvoiceHistory.Add(model);


            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            //return Json(driverStatus.ToDataSourceResult(request));
            return Json(BillingInvoiceHistory.ToDataSourceResult(request));
        }


        //public ActionResult Pdf_Export_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    using (var northwind = new SampleEntities())
        //    {
        //        return Json(northwind.Employees.ToDataSourceResult(request, e => new EmployeeViewModel
        //        {
        //            EmployeeID = e.EmployeeID,
        //            Country = e.Country,
        //            Title = e.Title,
        //            FirstName = e.FirstName,
        //            LastName = e.LastName
        //        }));
        //    }
        //}

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
           
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        // [HttpPost]
        //public ActionResult Pdf(string TransactionID, string billDate)
        public ActionResult Pdf(string TransactionID)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

            BillingHistory model = new BillingHistory();
            //  model = await GetBillingHistoryInvoicespdf(tenantId, invoice, billDate);
            BillingHistory results = null;
            //var BH = from his in db.BillingHistoryInvoices
            //             where his.TenantId == tenantId && his.TransactionID.ToString() == TransactionID
            //         select his;

            //var SP = from his in db.Subscription
            //             join pm in db.PaymentMethods on his.PaymentMethodId equals pm.ID // && pm.TenantId = tenantId;
            //             where his.TenantId == tenantId 
            //             select his;

            ViewBag.BillingHistoryInvoices = db.BillingHistoryInvoices.Where(x => x.TenantId.Equals(tenantId) && x.TransactionID.Equals(TransactionID)).FirstOrDefault();

          

            ViewBag.CustomerDetails = db.CorporateProfile.Where(x => x.TenantId.Equals(tenantId)).FirstOrDefault();

            //Subscription details
            ViewBag.CustomerSubscriptions = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).ToList();


    ///        WellAIAppContext.Current.Session.SetString("CompanyName", companyName);
    //        WellAIAppContext.Current.Session.SetString("LoginU\serName", user.Email);

            string packageId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.PackageId).FirstOrDefault();
            string paymentMethodId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.PaymentMethodId).FirstOrDefault();

            //package details
            ViewBag.CustomerSubscriptionsPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(packageId)).FirstOrDefault();

            // ViewBag.PaymentDetails = db.PaymentMethods.Where(x => x.ID == paymentMethodId).FirstOrDefault();
            ViewBag._PaymentDetails = db.PaymentMethods.Where(x => x.ID == paymentMethodId).FirstOrDefault();
            //ViewBag.PackageDetailsList = db.SubscriptionPackage.Where(x => x.PackageId.ToString().ToUpper()
            //== "D350AC56-27E8-4442-9F1D-3EB6088DB9DA" || x.PackageId.ToString().ToUpper() 
            //== "558911F7-876E-40C2-AF4F-388DBD83CB6D" || x.PackageId.ToString().ToUpper() 
            //== "0D280653-7177-4DD3-9915-6CB83433BF70").OrderBy(x => x.PackageAmount).ToList();
            ViewBag.PackageDetailsList = db.SubscriptionPackage.Where(x => x.PackageId.ToString().ToUpper()
            == "D350AC56-27E8-4442-9F1D-3EB6088DB9DA" || x.PackageId.ToString().ToUpper()
            == "558911F7-876E-40C2-AF4F-388DBD83CB6D" || x.PackageId.ToString().ToUpper()
            == "0D280653-7177-4DD3-9915-6CB83433BF70").OrderBy(x => x.PackageAmount).ToList();

            // return PartialView("Pdf");
            return View();

        }

        //public Task<ActionResult> GetBillingHistoryInvoicespdf(string tenantId, string invoice, string billDate)
        //{
           
        //    try
        //    {
        //        BillingHistory results = null;

        //        //result = (from his in db.BillingHistoryInvoices
        //        //              //  join ptype in _db.PaymentType on his.PayMethod equals ptype.ID
        //        //          where his.TenantId == tenantId
        //        //          select new BillingHistory { Invoice = his.Invoice, BillDate = his.BillDate, Name = his.Name, Amount = his.Amount }
        //        //             );

             

        //                  var result = from his in db.BillingHistoryInvoices
        //                                        where his.TenantId == tenantId
        //                                        select his;

              
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }
        //}
        public async Task<IActionResult> GetBillinghistory([DataSourceRequest] DataSourceRequest request)
        {
            List<BillingHistory> model = new List<BillingHistory>();
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var servoppe = new OperatingTenantRepository(_servicedb, HttpContext, _userManager, db);
                //model = await operrepo.GetBillingHistoryInvoices();
                model = await servoppe.GetBillingHistoryInvoices(tenantId);
                if (model == null)
                    model = new List<BillingHistory>();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "BillingInvoiceHistorySRV BillingHistory_Read", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(model.ToDataSourceResult(request, ModelState));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}