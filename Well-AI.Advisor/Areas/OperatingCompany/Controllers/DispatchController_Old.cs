using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Entity;
using System.Data.Entity;
using Kendo.Mvc.Extensions;
using static WellAI.Advisor.BLL.Business.AuctionProposalBusiness;
using Finbuckle.MultiTenant;
using Newtonsoft.Json;
using System;
using WellAI.Advisor.Model.ServiceCompany.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Common;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    public class Dispatch_OldController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private TenantServiceDbContext _servicedb;
        private TenantOperatingDbContext _tdbContext;
        private readonly IConfiguration _configuration;
        public Dispatch_OldController(UserManager<WellIdentityUser> userManager,
           RoleManager<IdentityRole> roleManager, WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
           TenantServiceDbContext servicedb, IConfiguration configuration)
        : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
            _servicedb = servicedb;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dashboard SRV", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> GetDispatchList([DataSourceRequest] DataSourceRequest request, string userId)
        {
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            List<RigList> rigList = new List<RigList>();
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                {
                    string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                    //rigList = await GetRigs(tenantId);

                    var user = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));

                    //Passing Rigs and Wells 
                    CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    //result = objComBusiness.GetDispatchRoutesForOperator(rigList).Result;

                    IAuctionProposalBusiness objAuctionBusiness = new AuctionProposalBusiness(db, _userManager);
                    var auctionsList = objAuctionBusiness.GetAuctionsListByTenantid(user, "00000000-0000-0000-0000-000000000000");                                       
                    var auctionsActiveList = auctionsList.Where(x => x.AuctionBidStatusId == 2).ToList();
                    result = objComBusiness.GetDispatchRoutesForOperator(auctionsActiveList, tenantId).Result;
                }
            }
            catch (System.Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchList", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }

        [HttpGet]
        public async Task<JsonResult> GetUserDestinations(string userId)
        {
            userdestinations destinations = new userdestinations();
            CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
            List<RigList> rigList = new List<RigList>();
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                {
                    string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                    rigList = await GetRigs(tenantId);
                    //var result = objComBusiness.GetDispatchRoutesForOperator(rigList).Result;

                    var user = JsonConvert.DeserializeObject<WellIdentityUser>(
                     WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));

                    //Passing Rigs and Wells 
                    //CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    //result = objComBusiness.GetDispatchRoutesForOperator(rigList).Result;

                    IAuctionProposalBusiness objAuctionBusiness = new AuctionProposalBusiness(db, _userManager);
                    var auctionsList = objAuctionBusiness.GetAuctionsListByTenantid(user, "00000000-0000-0000-0000-000000000000");
                    var auctionsActiveList = auctionsList.Where(x => x.AuctionBidStatusId == 2).ToList();
                    var result = objComBusiness.GetDispatchRoutesForOperator(auctionsActiveList, tenantId).Result;

                    if (result != null)
                    {
                        destinations.type = "FeatureCollection";

                        List<features> featuresList = new List<features>();
                        foreach (var item in result)
                        {
                            features feature = new features();
                            feature.type = "Feature";
                            geometry geometry = new geometry();
                            geometry.type = "Point";
                            float[] coordinates = new float[2];
                            coordinates[0] = Convert.ToSingle(item.longitude); coordinates[1] = Convert.ToSingle(item.latitude);
                            geometry.coordinates = coordinates;

                            properties properties = new properties();
                            properties.title = item.username;
                            properties.description = item.locationname;

                            feature.geometry = geometry;
                            feature.properties = properties;
                            featuresList.Add(feature);
                        }
                        if (featuresList.Count > 0)
                        {
                            destinations.features = featuresList.ToArray();
                        }
                    }
                }

                return Json(destinations);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch return Json(destinations);", User.Identity.Name);
                return Json(destinations);
                //return true;
            }
        }
        private async Task<List<RigList>> GetRigs(string tenantId)
        {
            List<RigList> rigList = new List<RigList>();
            try
            {
                //Getting Service Providers
                var tId = Guid.Parse(tenantId);
                var dbprefix = "oper";
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + tId.ToString("N"));
                var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);
                var operContext = new TenantOperatingDbContext(ti);
                _tdbContext = operContext;
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                var providers = await operrepo.GetServiceProviderDirectoriesForAdmin();

                providers = providers.Where(pr => pr.Preferred == 3).ToList();

                //Getting Service Providers Rigs List
                foreach (var provider in providers)
                {
                    var serviceTenantId = provider.CompanyId;
                    tId = Guid.Parse(serviceTenantId);
                    dbprefix = "serv";
                    connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + tId.ToString("N"));

                    ti = new TenantInfo(serviceTenantId, serviceTenantId, serviceTenantId, connString, null);
                    var servicedbContext = new TenantServiceDbContext(ti);
                    _servicedb = servicedbContext;
                    var servrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var serviceRigsList = servrepo.Get_SubsciberProviderRigs(tenantId).Result;
                    if (serviceRigsList.Count > 0)
                    {
                        foreach (var serviceRig in serviceRigsList)
                        {
                            RigList rig = new RigList();
                            rig.Rig_Id = serviceRig.RigId;
                            rig.Rig_Name = serviceRig.RigName;
                            rigList.Add(rig);
                        }
                    }
                }
                return rigList;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetRigsLits", User.Identity.Name);
                return rigList;
                //return true;
            }
        }

        public IActionResult Dashboard()
        {
            try
            {
                List<StatusViewModel> status = new List<StatusViewModel>();
                List<AuctionBidStatusViewModel> bidstatus = new List<AuctionBidStatusViewModel>();

                var bidstatusItem = new AuctionBidStatusViewModel
                {
                    AuctionBidStatusName = "--Select--",
                    AuctionBidStatus = "null2"
                };
                bidstatus.Add(bidstatusItem);

                bidstatusItem = new AuctionBidStatusViewModel
                {
                    AuctionBidStatusName = "Completed",
                    AuctionBidStatus = "Completed"
                };
                bidstatus.Add(bidstatusItem);

                bidstatusItem = new AuctionBidStatusViewModel
                {
                    AuctionBidStatusName = "Active",
                    AuctionBidStatus = "Active"
                };
                bidstatus.Add(bidstatusItem);

                var statusItem = new StatusViewModel
                {
                    StatusName = "--Select--",
                    DriverStatus = "null2"
                };
                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "ACTIVE",
                    DriverStatus = "ACTIVE"
                };
                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "INACTIVE",
                    DriverStatus = "INACTIVE"
                };
                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "ON-ROUTE",
                    DriverStatus = "ON-ROUTE"
                };

                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "ON-SITE",
                    DriverStatus = "ON-SITE"
                };

                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "OVERDUE",
                    DriverStatus = "OVERDUE"
                };

                status.Add(statusItem);


                ViewData["BidStatus"] = bidstatus;
                ViewData["DispatchStatus"] = status;

                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";


                    return LocalRedirect(returnUrl);
                }
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch Index", User.Identity.Name);
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
        }
        public class StatusViewModel
        {
            public string? DriverStatus { get; set; }
            public string? StatusName { get; set; }
        }

        public class AuctionBidStatusViewModel
        {
            public string? AuctionBidStatusName { get; set; }
            public string? AuctionBidStatus { get; set; }
        }

        [HttpGet]
        public async Task<JsonResult> GetOperatorsharedetails(string userId)
        {
            bool userStatus = false;
        

            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            List<RigList> rigList = new List<RigList>();
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.GetMultiTenantContext().TenantInfo.Id))
                {
                    string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                    //rigList = await GetRigs(tenantId);

                    var user = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));

                    //Passing Rigs and Wells 
                    CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    //result = objComBusiness.GetDispatchRoutesForOperator(rigList).Result;

                    IAuctionProposalBusiness objAuctionBusiness = new AuctionProposalBusiness(db, _userManager);
                    var auctionsList = objAuctionBusiness.GetAuctionsListByTenantid(user, "00000000-0000-0000-0000-000000000000");
                    var auctionsActiveList = auctionsList.Where(x => x.AuctionBidStatusId == 2).ToList();
                    result = objComBusiness.GetOperatorsharedetails(auctionsActiveList, tenantId, userId).Result;
                    ViewBag.OperatorId = tenantId;
                }
            }
            catch (System.Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchList", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return Json("Error");
            }
            return Json(result);



        }


            }
}
