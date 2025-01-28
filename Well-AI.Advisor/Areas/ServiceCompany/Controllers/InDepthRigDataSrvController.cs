using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.DLL.Repository;
using Finbuckle.MultiTenant;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.BLL;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Tenant.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    [SessionTimeOut]
    public class InDepthRigDataSrvController : BaseController
    {
        private readonly ILogger<ActivityViewSRVController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantServiceDbContext _servicedb;
        private readonly IConfiguration _configuration;
        private readonly ISingleton _singleton;
        private TenantServiceDbContext _tdbContext;
        private TenantOperatingDbContext _operdb;
        public InDepthRigDataSrvController(WebAIAdvisorContext dbContext, TenantServiceDbContext servicedb, SignInManager<WellIdentityUser> signInManager,
                                      RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<ActivityViewSRVController> logger,
                                      IConfiguration configuration, ISingleton singleton, TenantServiceDbContext tdbContext, TenantOperatingDbContext operdb
                                      )
            : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _servicedb = servicedb;
            _configuration = configuration;
            _singleton = singleton;
            _tdbContext = tdbContext;
            _operdb = operdb;
        }
        public async Task<IActionResult> Index()
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
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                string wellId = string.Empty, operId = string.Empty;
                if (string.IsNullOrWhiteSpace(HttpContext.Request.Query["wellId"]))
                {
                    //Phase II Changes
                    if (Request.Cookies["operfilterlayout"] != null)
                    {
                        var rigs = await GetDepthDataAsync(Request.Cookies["operfilterlayout"].ToString());
                        if (rigs.Count > 0)
                        {
                            var RigsValues = (from rig in rigs
                                              select new RigsModel
                                              {
                                                  WellId = rig.WellId,
                                                  Category = rig.Category
                                              }).ToList();
                            ViewData["wells"] = RigsValues;
                            wellId = rigs.FirstOrDefault().WellId;
                        }
                    }
                    
                }
                else
                wellId = HttpContext.Request.Query["wellId"].ToString();
                var model = new InDepthRigDataModel();
                operId = Request.Cookies["operfilterlayout"].ToString();
                var nospecificTenant = operId == DLL.Constants.NoSpecificWellFilterKey;
                //var operwells = await AIBusiness.GetWellsForOperationCompanyOnServiceSite(operId);
                var operwells = _servicedb.subscriptionOperatorRigs.Where(x => x.CompanyId == operId && !nospecificTenant || nospecificTenant).ToList();
                var reswells = (from rig in operwells
                                join Well in db.WellRegister on rig.RigId equals Well.RigID
                                where (rig.CompanyId == operId && !nospecificTenant || nospecificTenant)
                                select new RigsModel
                                {
                                    WellId = Well.well_id,
                                    Category = Well.wellname
                                }).OrderBy(x => x.Category).ToList();

                ViewData["wells"] = reswells;
                if (string.IsNullOrEmpty(wellId))
                {
                    if (reswells.Count == 0)
                        wellId = DLL.Constants.NoSpecificWellFilterKey;
                    else
                        wellId = reswells[0].WellId;
                }
                model.InitWellId = wellId;
                var serviceRepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var operatorDir = await serviceRepo.GetProviderDirectoryByTenantId(operId);
                if (operatorDir != null)
                {
                    model.Address1 = operatorDir.Address1;
                    model.Address2 = operatorDir.Address2;
                    model.City = operatorDir.City;
                    model.OperatorName = operatorDir.Name;
                    model.Zip = operatorDir.Zip;
                    model.State = operatorDir.State;
                    model.Phone = operatorDir.Phone;
                    model.City = operatorDir.City;
                    model.Country = operatorDir.Country;
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager, _configuration);
                var primUser = await commonBusiness.GetPrimaryUser(operId);
                if (primUser != null)
                {
                    model.MSAExist = await commonBusiness.CheckMSAExistFromProviderTenant(operId, primUser.UserID);
                    model.PrimaryContact = primUser.FirstName + " " + primUser.LastName;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigDataSrv Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task<List<ServiceRig>> GetDepthDataAsync(string operId)
        {
            if (string.IsNullOrEmpty(operId) || operId == DLL.Constants.NoSpecificWellFilterKey)
            {
                return new List<ServiceRig>();

            }
            var depthchartData = new List<ServiceRig>();
            List<ErdosDrillingDepthBased> DrillingDepthBased = new List<ErdosDrillingDepthBased>();
            var connections = db.WellTenants.Where(x => x.Id == operId).ToList();
            var providers = _servicedb.OperatingDirectory.Where(x => x.CompanyId == operId).ToList();
            var SrvTenantIds = (from crp in db.CorporateProfile
                                join crmp in db.CrmCompanies on crp.TenantId equals crmp.TenantId
                                select new
                                {
                                    TenantId = crp.TenantId
                                }
                             ).ToList();
            var ProTenantIds = db.CorporateProfile.Where(x => x.TenantId == operId).ToList();
            foreach (var id in providers)
            {
                var connectionstring = connections.Where(X => X.Id.Equals(id.CompanyId)).Select(x => x.ConnectionString).FirstOrDefault();
                if (connectionstring != null)
                {
                    var chartdata = new List<ServiceRig>();
                    var operrepo = new ServiceTenantRepository(_tdbContext, HttpContext, _userManager, db);
                    DrillingDepthBased = await operrepo.GetDepthChartData(connectionstring);
                    if (DrillingDepthBased.Count > 0)
                    {
                        var wellreg = db.WellRegister.Where(x => x.customer_id == operId).ToList();
                        chartdata = (from well in wellreg
                                     join Dril in DrillingDepthBased on well.well_id equals Dril.Wid
                                     select new ServiceRig
                                     {
                                         Value = (float?)Dril.Ecdt == null ? 0 : (float)Dril.Ecdt,
                                         Category = well.wellname,
                                         Color = (Dril.Ecdt > 0 && Dril.Ecdt < 2501) ? "#007BFF" :
                                                                     (Dril.Ecdt > 2500 && Dril.Ecdt < 5001) ? "#26DDCC" :
                                                                      (Dril.Ecdt > 5000 && Dril.Ecdt < 7501) ? "#3639A4" :
                                                                      (Dril.Ecdt > 7500 && Dril.Ecdt < 10001) ? "#F4AF00" :
                                                                      (Dril.Ecdt > 10000 && Dril.Ecdt < 12501) ? "#FF6344" :
                                                                      (Dril.Ecdt > 12500 && Dril.Ecdt < 15001) ? "#77BD27" :
                                                                      (Dril.Ecdt > 15000 && Dril.Ecdt < 17501) ? "#0422A1" : "#0422A1",
                                         WellId = well.well_id,
                                         OperatorTenantId = operId
                                     }
                                     ).ToList();
                    }
                    if (chartdata.Count > 0)
                    {
                        foreach (var item in chartdata)
                        {
                            var DepthChartRecord = new ServiceRig();
                            DepthChartRecord.Value = item.Value;
                            DepthChartRecord.Category = item.Category;
                            DepthChartRecord.Color = item.Color;
                            DepthChartRecord.OperatorTenantId = item.OperatorTenantId;
                            DepthChartRecord.WellId = item.WellId;
                            depthchartData.Add(DepthChartRecord);
                        }
                    }
                }
            }
            return depthchartData;
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "InDepthRigData", TenantId);
            }
            else
            {
                return false;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> GetChartAction(string wellId)
        {
            try
            {
                if (Request.Cookies["indepthrigdatagetwellbyrigsidcookiessrv"] != null)
                {
                    wellId = Request.Cookies["indepthrigdatagetwellbyrigsidcookiessrv"];
                }
                var aibus = new AIBuiness(db, _roleManager, _userManager);
                var data = await aibus.GetWellDepthTimeChartFromTasks(wellId);
                var currentDepth = await aibus.GetWellCurrentDepth(wellId);
               var indexLine = 0;
                for (var i = 0; i < data.Count; i++)
                {
                    if (data[i].Value < currentDepth)
                    {
                        data[i].Series = 1;

                        if (data[i].Value == currentDepth)
                        {
                            indexLine = i;
                        }
                    }
                    else
                    {
                        if (data[i - 1].Series == 1)
                        {
                            indexLine = i - 1;
                        }
                        data[i].Series = 2;
                    }
                }
                if (indexLine > 0 && indexLine < data.Count - 2)
                {
                    data.Insert(indexLine + 1, new InDepthRigData
                    {
                        Day = data[indexLine + 1].Day,
                        Value = data[indexLine + 1].Value,
                        Color = data[indexLine].Color,
                        WellId = data[indexLine].WellId,
                        Series = 1
                    });
                }
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Value < currentDepth)
                    {
                        data[i].Series = 1;
                    }
                    else
                    {
                        data[i].Series = 2;
                    }
                }
                if (data.Count() == 0)
                {
                    data.Insert(0, new InDepthRigData
                    {
                        Day = 1,
                        Value = 22000,
                        Color = "",
                        WellId = wellId,
                        Series = 1
                    });
                }
                return Json(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigDataSrv GetChartAction", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetGridData([DataSourceRequest] DataSourceRequest request, string wellId)
        {
            try
            {
                var aibus = new AIBuiness(db, _roleManager, _userManager);
                if (Request.Cookies["indepthrigdatagetwellbyrigsidcookiessrv"] != null)
                {
                    wellId = Request.Cookies["indepthrigdatagetwellbyrigsidcookiessrv"];
                }
                var data = await aibus.GetWellDepthGridData(wellId);
                return Json(data.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigDataSrv GetGridData", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetInDepthProjectData([DataSourceRequest] DataSourceRequest request, string wellId)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                if (Request.Cookies["indepthrigdatagetwellbyrigsidcookiessrv"] != null)
                {
                    wellId = Request.Cookies["indepthrigdatagetwellbyrigsidcookiessrv"];
                }
                var data = await projectBusiness.GetProjectsByWellIdSRV(tenantId, wellId);
                return Json(data.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetInDepthProjectData InDepthRigDataSrv", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetInDepthSubmittedProposal([DataSourceRequest] DataSourceRequest request, string wellId)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                if (Request.Cookies["indepthrigdatagetwellbyrigsidcookiessrv"] != null)
                {
                    wellId = Request.Cookies["indepthrigdatagetwellbyrigsidcookiessrv"];
                }
                var result = _singleton.auctionProposalBusiness.GetAuctionsProposalListForInDepathSRV(tenantId, wellId);
                return await Task.FromResult(Json(result.ToDataSourceResult(request)));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetInDepthSubmittedProposal InDepthRigDataSrv", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Phase II Changes
        /// </summary>
        /// <param name="RigId"></param>
        /// <returns></returns>
        public IActionResult SubcribedRig_Status(string WellId)
        {
            var result = "";
            try
            {
                if (!string.IsNullOrEmpty(WellId))
                {
                    var Rid = db.WellRegister.Where(x => x.well_id == WellId).Select(y => y.RigID).FirstOrDefault();
                    var RigCount = _servicedb.subscriptionOperatorRigs.Where(x => x.RigId == Rid).Count();
                    result = RigCount == 1 ? "true" : "false";
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SubcribedRig_Status InDepthRigDataSrv", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        //Depth Chart
        public async Task<IActionResult> GetChartAction_V1(string wellId)
        {
            try
            {
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var aibus = new AIBuiness(db, _roleManager, _userManager);              
                var wellObj = db.WELL_REGISTERs.Where(x => x.well_id == wellId).FirstOrDefault();
                var connections = db.WellTenants.Where(x => x.Id == wellObj.customer_id).FirstOrDefault();
                if (connections != null)
                {
                    var connectionstring = connections.ConnectionString;
                    if (connectionstring != null)
                    {
                        var operrepo = new ServiceTenantRepository(_tdbContext, HttpContext, _userManager, db);
                        var depthdata = await operrepo.GetWellDepthData(connectionstring, wellId);
                        if (depthdata.Count == 0)
                        {
                            depthdata.Insert(0, new InDepthRigData
                            {
                                Day = 1,
                                Value = 22000,
                                Color = "",
                                WellId = wellId,
                                Series = 1
                            });
                        }
                        else
                        {
                            var currentDepth = await aibus.GetWellCurrentDepth(wellId);
                            var indexLine = 0;
                            for (var i = 0; i < depthdata.Count; i++)
                            {
                                if (depthdata[i].Value < currentDepth)
                                {
                                    depthdata[i].Series = 1;

                                    if (depthdata[i].Value == currentDepth)
                                    {
                                        indexLine = i;
                                    }
                                }
                                else
                                {
                                    if (i >= 1)
                                    {
                                        if (depthdata[i - 1].Series == 1)
                                        {
                                            indexLine = i - 1;
                                        }
                                    }
                                    depthdata[i].Series = 2;
                                }
                            }
                            if (indexLine > 0 && indexLine < depthdata.Count - 2)
                            {
                                depthdata.Insert(indexLine + 1, new InDepthRigData
                                {
                                    Day = depthdata[indexLine + 1].Day,
                                    Value = depthdata[indexLine + 1].Value,
                                    Color = depthdata[indexLine].Color,
                                    WellId = depthdata[indexLine].WellId,
                                    Series = 1
                                });
                            }
                            for (int i = 0; i < depthdata.Count; i++)
                            {
                                if (depthdata[i].Value < currentDepth)
                                {
                                    depthdata[i].Series = 1;
                                }
                                else
                                {
                                    depthdata[i].Series = 2;
                                }
                            }
                        }
                        return Json(depthdata);
                    }
                    var depthEmptyData = new List<InDepthRigData>();
                    return Json(depthEmptyData);
                }
                else
                {
                    var depthEmptyData = new List<InDepthRigData>();
                    return Json(depthEmptyData);
                }               
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetChartAction InDepthRigDataSrv", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        public async Task<IActionResult> GetCasingStringAndCurrentStage(string WellId)
        {
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var data = await projectBusiness.GetCasingStringAndCurrentStage(tenantId, WellId);              
                return Json(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetCasingStringAndCurrentStage InDepthRigDataSrv", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

            //Staging Chart
            public async Task<IActionResult> GetStagingChart_Data (string WellId)
        {

            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;               
                var data = await projectBusiness.GetStagingData(tenantId, WellId);
                return Json(data);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetStagingChart_Data InDepthRigDataSrv", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        /// <summary>
        /// Phase II Changes 
        /// </summary>
        /// <param name="WellId"></param>
        /// <returns></returns>
        public IActionResult GetAuthorizedVendorPermission(string WellId)
        {
            try
            {
                int Result = 0;
                if (WellId != null)
                {
                    IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);

                    var GetOprTenantId = db.WellRegister.Where(x => x.well_id == WellId).Select(y => y.customer_id).FirstOrDefault();

                    var dbprefix = "oper";
                    var servguid = new Guid(GetOprTenantId);
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + servguid.ToString("N"));
                    var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                    var OperDBContext = new TenantOperatingDbContext(ti);
                    _operdb = OperDBContext;
                    var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                    string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;

                    Result = (from prov in _operdb.ProvidersDirectory
                              join app in _operdb.ProviderDirectoryAppovals on prov.Approval equals app.Id
                              where app.Name == "Approved" && prov.CompanyId == tenantId && (prov.MSA != null)
                              select prov).Count();
                }

                var Status = "";

                 if (Result == 1)
                {
                    Status = "true";
                }
                else
                {
                    Status = "False";

                }

                return Json(Status);

             }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigDataSrv GetAuthorizedVendorPermission", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }


        }

        /// <summary>
        /// Phase II Changes - 02/08/2021 - Depth permission for WellId
        /// </summary>
        /// <param name="WellId"></param>
        /// <returns></returns>
        public IActionResult GetDepthPermission(string WellId)
        {
            //var result = "";
            try
            {
                var GetOprTenantId = db.WellRegister.Where(x => x.well_id == WellId).Select(y => y.customer_id).FirstOrDefault();

                var dbprefix = "oper";
                var servguid = new Guid(GetOprTenantId);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var OperDBContext = new TenantOperatingDbContext(ti);
                _operdb = OperDBContext;
                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                string permission = "false";
                if (!string.IsNullOrEmpty(WellId))
                {

                    RigsDepthPermission_Model depthPermission = (from permsn in db.RigsDepth_Permissions
                                                                 where permsn.WellId == WellId && permsn.SerTenantId == tenantId
                                                                 select new RigsDepthPermission_Model
                                                                 {
                                                                     DepthPermission = permsn.DepthPermission
                                                                 }
                                                                ).FirstOrDefault();
                    if (depthPermission != null)
                    {
                        if (depthPermission.DepthPermission == true)
                        {
                            permission = "true";
                        }
                        else
                        {
                            permission = "False";
                        }
                    }
                    
                }
                return Json(permission);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetDepthPermission InDepthRigDataSrv", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        } 
    }

}