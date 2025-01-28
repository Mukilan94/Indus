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
using System.Web.Mvc;
using System.ComponentModel;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class ARBSubscriptionController : BaseController
    {
        private readonly ILogger<BillingInvoiceHistorySRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantServiceDbContext _servicedb;
        public ARBSubscriptionController(TenantServiceDbContext servicedb,UserManager<WellIdentityUser> userManager,
                                                  SignInManager<WellIdentityUser> signInManager,RoleManager<IdentityRole> roleManager,
                                                  WebAIAdvisorContext dbContext,ILogger<BillingInvoiceHistorySRVController> logger)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _servicedb = servicedb;
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
     
       
     
        public void GetBillinghistory()
        {
            List<ARBSubscriptionModel> ARBSubscriptionModel = new List<ARBSubscriptionModel>();
            try
            {
                //DispatchDashboardModel dispatch = new DispatchDashboardModel();
                //data = _singleton.auctionProposalBusiness.GetAuctionsProposalListForSRV(tenantid, operId, servrepo); //GetBidsData(valueFrom, valueTo);

                //DispatchDashboardModel model = new DispatchDashboardModel("1234", "ACTIVE");
                //driverStatus.Add(model);


                ARBSubscriptionModel model = new ARBSubscriptionModel();

                model.Cardnumber = "102450001";
                model.Expirationdate = Convert.ToDateTime("06-06-2022");
                model.Amount = "155";
               

               // ARBSubscriptionModel.Add(model);

              


            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            //return Json(driverStatus.ToDataSourceResult(request));
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}