using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Xls;
//DWOP
using Telerik.Windows.Documents.Spreadsheet.Model;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.SubcriptionPackage;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Well_AI.Advisor.API.PEC.Services.IServices;
using WellAI.Advisor;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Well_AI.Advisor.API.PEC.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Well_AI.Advisor.Log.Error;
using Microsoft.Extensions.Logging;
using System.Net;
using Telerik.Web.PDF;
using WellAI.Advisor.Areas.Identity;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.DLL.IRepository;
namespace Well_AI.Advisor.Administration.Controllers
{

    public class SubscriptionPackageController : BaseController
    {

        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
        //Phase II - Clear Warning

        //Phase II Changes-01/12/2021 - Add OperatingDBContext to Read Vendor Directory
        private TenantOperatingDbContext _tdbContext;
        private readonly TenantOperatingDbContext _operdb;
        private ISession _session;
        private TenantServiceDbContext _servicedb;
        //private readonly IConfiguration _configuration;
        public SubscriptionPackageController(IConfiguration _configuration, UserManager<WellIdentityUser> userManager,
          RoleManager<IdentityRole> roleManager,
           ISingletonAdministration _singleton, WebAIAdvisorContext db, TenantOperatingDbContext operdb, TenantOperatingDbContext tdbContext,
           TenantServiceDbContext servicedb, UserManager<StaffWellIdentityUser> staffUserManager, IConfiguration configuratio)
            : base(_configuration, _singleton, db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _operdb = operdb;
            _tdbContext = tdbContext;
            _session = WellAIAppContext.Current.Session;
            _servicedb = servicedb;
            _staffUserManager = staffUserManager;
            //_configuration = configuration;
        }
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult GetSubscriptionPackages([DataSourceRequest] DataSourceRequest request)
        {
            List<SubcriptionPackageModel> SubcriptionPackages = new List<SubcriptionPackageModel>();
            try
            {
                //DispatchDashboardModel dispatch = new DispatchDashboardModel();
                //data = _singleton.auctionProposalBusiness.GetAuctionsProposalListForSRV(tenantid, operId, servrepo); //GetBidsData(valueFrom, valueTo);

                //DispatchDashboardModel model = new DispatchDashboardModel("1234", "ACTIVE");
                //driverStatus.Add(model);


                SubcriptionPackageModel model = new SubcriptionPackageModel();

                model.SubscriptionId = "01";
                model.SubscriptionName = "Advisor";
                model.SubscriptionType = "Advisor";
                model.PricePerMonth = "$20/Rig";
                //  model.PricePerYear = "$240";
                model.Description = "";
                model.Features = "";
                //   model.SubscriptionStatus = "Active";
                //  model.CreatedDate = DateTime.Now;
                //  model.ModifiedDate = DateTime.Today;
                SubcriptionPackages.Add(model);

                model = new SubcriptionPackageModel();
                model.SubscriptionId = "02";
                model.SubscriptionName = "Dispatch";
                model.SubscriptionType = "Dispatch";
                model.PricePerMonth = "$15/User";
                // model.PricePerYear = "$480";
                model.Description = "";
                model.Features = "";
                //  model.SubscriptionStatus = "Active";
                //   model.CreatedDate = DateTime.Now;
                //  model.ModifiedDate = DateTime.Today;
                SubcriptionPackages.Add(model);

                model = new SubcriptionPackageModel();
                model.SubscriptionId = "03";
                model.SubscriptionName = "Advisor and Dispatch  ";
                model.SubscriptionType = "Advisor and Dispatch ";
                model.PricePerMonth = "$20/Rig, $15/User";
                //  model.PricePerYear = "$360";
                model.Description = "";
                model.Features = "";
                //  model.SubscriptionStatus = "Active";
                //   model.CreatedDate = DateTime.Now;
                //   model.ModifiedDate = DateTime.Today;

                SubcriptionPackages.Add(model);


            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            //return Json(driverStatus.ToDataSourceResult(request));
            return Json(SubcriptionPackages.ToDataSourceResult(request));
        }
    }
}

