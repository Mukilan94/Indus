using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
    public class DispatchController : BaseController
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
        public DispatchController(IConfiguration _configuration, UserManager<WellIdentityUser> userManager,
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

        public async Task<IActionResult> GetDispatchList([DataSourceRequest] DataSourceRequest request, string userId)
        {
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            List<RigList> rigList = new List<RigList>();
            try
            {
            
                 string tenantId = HttpContext.Session.GetString("AdminSessionCurrentTenantId"); //HttpContext.GetMultiTenantContext().TenantInfo.Id;

                //Passing Rigs and Wells 
                CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);

                IAuctionProposalBusiness objAuctionBusiness = new AuctionProposalBusiness(db, _userManager);
                var auctionsList = objAuctionBusiness.GetAuctionsListByTenantid_V1(tenantId, "00000000-0000-0000-0000-000000000000");
                var auctionsActiveList = auctionsList.Where(x => x.AuctionBidStatusId == 2).ToList();

                objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    result = objComBusiness.GetDispatchRoutesForOperator(auctionsActiveList, tenantId).Result;
                //}
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
                var user = JsonConvert.DeserializeObject<WellIdentityUser>(
                           WellAIAppContext.Current.Session.GetString(userId));

                string tenantId = HttpContext.Session.GetString("AdminSessionCurrentTenantId"); ;

                    IAuctionProposalBusiness objAuctionBusiness = new AuctionProposalBusiness(db, _userManager);
                    var auctionsList = objAuctionBusiness.GetAuctionsListByTenantid(user, "00000000-0000-0000-0000-000000000000");
                    var auctionsActiveList = auctionsList.Where(x => x.AuctionBidStatusId == 2).ToList();

                    //rigList = await GetRigs(tenantId);

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
    }
}
