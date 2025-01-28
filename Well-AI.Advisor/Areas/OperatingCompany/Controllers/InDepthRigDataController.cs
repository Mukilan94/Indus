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
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.DLL.Repository;
using Finbuckle.MultiTenant;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.BLL;
using WellAI.Advisor.Areas.OperatingCompany.Controllers;
using WellAI.Advisor.Model.Tenant.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class InDepthRigDataController : BaseController
    {
        private readonly ILogger<ActivityViewController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantServiceDbContext _servicedb;
        private readonly IConfiguration _configuration;
        private readonly ISingleton _singleton;
        private readonly TenantOperatingDbContext _operdb;
        public InDepthRigDataController(TenantOperatingDbContext operdb, WebAIAdvisorContext dbContext, TenantServiceDbContext servicedb, SignInManager<WellIdentityUser> signInManager,
                                      RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<ActivityViewController> logger,
                                      IConfiguration configuration, ISingleton singleton
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
            _operdb = operdb;
        }
        public async Task<IActionResult> Index(string wellIdParam)
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
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                string wellId = string.Empty, operId = string.Empty;
                var model = new InDepthRigDataModel();
                if (string.IsNullOrWhiteSpace(HttpContext.Request.Query["wellId"]))
                {
                    model.InitWellId = HttpContext.Request.Query["wellId"];
                }
                else
                    wellId = HttpContext.Request.Query["wellId"].ToString();
                operId = WellAIAppContext.Current.Session.GetString("TenantId");
                var operwells = await AIBusiness.GetWellsForOperationCompanyOnServiceSite(operId);
                var reswells = operwells
                    .Select(x => new RigsModel
                    {
                        WellId = x.wellId,
                        Category = x.wellName
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
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager, _configuration);
                var primUser = await commonBusiness.GetPrimaryUser(operId);
                if (primUser != null)
                {
                    model.MSAExist = await commonBusiness.CheckMSAExistFromProviderTenant(operId, primUser.UserID);
                    model.PrimaryContact = primUser.FirstName + " " + primUser.LastName;
                }
                //ProviderDirectoryModel PD = new ProviderDirectoryModel();
                var result = await GetProviderDirectory(false, false);               
               // var result = PD;
                
                ViewBag.PreferredProvider = result.PreferredProvider;
                ViewBag.SecondaryProvider = result.SecondaryProvider;
                return View(model);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigData Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "InDepthRigData", TenantId);
            }
            else
            {
                return false;
            }
        }
        private async Task<List<Model.ServiceCompany.Models.ServiceRig>> GetDepthDataAsync(string operId)
        {
            try
            {
                if (string.IsNullOrEmpty(operId) || operId == DLL.Constants.NoSpecificWellFilterKey)
                {
                    return new List<Model.ServiceCompany.Models.ServiceRig>();
                }
                var depthchartData = new List<Model.ServiceCompany.Models.ServiceRig>();
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
                        var chartdata = new List<Model.ServiceCompany.Models.ServiceRig>();
                        var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                        DrillingDepthBased = await operrepo.GetDepthChartData(connectionstring);
                        if (DrillingDepthBased.Count > 0)
                        {
                            var wellreg = db.WellRegister.Where(x => x.customer_id == operId).ToList();
                            chartdata = (from well in wellreg
                                         join Dril in DrillingDepthBased on well.well_id equals Dril.Wid
                                         select new Model.ServiceCompany.Models.ServiceRig
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
                                var DepthChartRecord = new Model.ServiceCompany.Models.ServiceRig();
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
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigData GetdepthDataAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<ProviderDirectoryModel> GetProviderDirectory(bool pendingFilter = false, bool insureExpireFilter = false)
        {
            try
            {
                var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                var providers = await operrepo.GetProviderDirectories();
                var pecs = (from Provider in _operdb.ProvidersDirectory
                            join pec in _operdb.ProviderDirectoryPECs
                            on Provider.PEC equals pec.Id
                            where pec.Name != "Good"
                            select Provider).Count();
                var providerdir = new ProviderDirectoryModel
                {
                    InsExpiring90days = providers.Count(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0),
                    Pending = providers.Count(x => x.Approval == "Pending review"),
                    Records = providers.Count,
                    ComplienceAlert = providers.Count(p => p.PecStatus != "Good")
                };
                //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                providerdir.PreferredProvider = providers.FirstOrDefault(x => x.Preferred == Convert.ToByte(3));
                providerdir.SecondaryProvider = providers.FirstOrDefault(x => x.Secondary);
                if (pendingFilter == true && insureExpireFilter == false)
                {
                    providers = providers.Where(x => x.Approval == "Pending review").ToList();
                }
                if (insureExpireFilter == true && pendingFilter == false)
                {
                    providers = providers.Where(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0).ToList();
                }
                if (insureExpireFilter == true && pendingFilter == true)
                {
                    providers = providers.Where(x => x.PecStatus != "Good").ToList();
                }
                var msafiles = (List<MSA>)ViewData["msa"];
                var companies = (List<CorporateProfile>)ViewData["AllCompanies"];
                List<string> tenantIds = null;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                if (companies == null)
                {
                    companies = await commonBusiness.GetServiceCompanies();
                    tenantIds = companies.Select(x => x.TenantId).ToList();
                    msafiles = await commonBusiness.GetMSAWellFilesFromServiceTenants(tenantIds, ti.Id, wellId);
                }
                else
                    tenantIds = companies.Select(x => x.TenantId).ToList();
                var insurefiles = await commonBusiness.GetInsuranceWellFilesFromServiceTenants(tenantIds, ti.Id, wellId);
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                foreach (var provider in providers)
                {
                    provider.MSADocument = "";
                    var company = companies.FirstOrDefault(x => x.TenantId.ToString() == provider.CompanyId);//provider.CompanyId=tenantId of Service Company
                    if (company != null)
                    {
                        var site = string.IsNullOrEmpty(company.Website) || company.Website.StartsWith("http:") ? company.Website : "http://" + company.Website;
                        provider.Name = company.Name;
                        provider.CompanyId = company.TenantId;
                        provider.Website = site ?? "";
                        provider.Phone = company.Phone;
                        provider.User = await commonBusiness.GetPrimaryUser(company.TenantId);
                        provider.Location = string.Format("{0}{1},{2},{3},{4}", company.Address1, string.IsNullOrEmpty(company.Address2) ? "" : "," + company.Address2,
                            company.City, company.State, company.Zip);
                        provider.City = company.City;
                        provider.State = company.State;
                        provider.Zip = company.Zip;
                        provider.Address1 = company.Address1;
                        provider.Address2 = company.Address2;
                    }
                    if (!string.IsNullOrEmpty(provider.MSADocumentId))
                    {
                        var msafile = msafiles.FirstOrDefault(x => x.MsaId == provider.MSADocumentId);
                        if (msafile != null)
                            provider.MSADocument = msafile.Attachment;
                    }
                    var activeProjects = await auctionProposalBusiness.GetServiceCompanyAuctionProjects(provider.CompanyId, ti.Id, true, wellId);
                    var currentActivity = new List<CurrentActivity>();
                    foreach (var activeProject in activeProjects)
                    {
                        currentActivity.Add(new CurrentActivity
                        {
                            CurrentActivityId = activeProject.ID,
                            Title = activeProject.ProjectTitle
                        });
                    }
                    provider.CurrentActivity = currentActivity;
                    var notactiveProjects = await auctionProposalBusiness.GetServiceCompanyAuctionProjects(provider.CompanyId, ti.Id, false, wellId);
                    var upcomeActivity = new List<UpcomingActivity>();
                    foreach (var notactiveProject in notactiveProjects)
                    {
                        upcomeActivity.Add(new UpcomingActivity
                        {
                            UpcomingActivityId = notactiveProject.ID,
                            Title = notactiveProject.ProjectTitle
                        });
                    }
                    provider.UpcomingActivity = upcomeActivity;
                    var offerings = await commonBusiness.GetServiceOfferings(provider.CompanyId);
                    provider.ServiceOffering = offerings;
                    if (!string.IsNullOrWhiteSpace(provider.MSADocumentId))
                    {
                        var msafile = msafiles.Where(x => x.MsaId == provider.MSADocumentId).ToList();
                        provider.Msa = msafile;
                    }
                    provider.Insurance = insurefiles;
                    provider.Proposals = await auctionProposalBusiness.GetServiceCompanyActualProposals(provider.CompanyId, ti.Id, wellId);
                }
                providerdir.Providers = providers;
                return providerdir;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigData GetProviderDirectory", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                ProviderDirectoryModel pd = new ProviderDirectoryModel();
                return pd;
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
            if (Request.Cookies["indepthrigdatagetwellbyrigsidcookies"] != null)
            {
                wellId = Request.Cookies["indepthrigdatagetwellbyrigsidcookies"];
            }
            if (wellId == DLL.Constants.NoSpecificWellFilterKey)
            {
                return Json(new List<InDepthRigData>());
            }
            try
            {
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
                customErrorHandler.WriteError(ex, "GetChartAction InDepthRigData", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> GetChartAction_V1(string wellId)
        {
            try
            {
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var aibus = new AIBuiness(db, _roleManager, _userManager);
                if (wellId != null && wellId != "00000000-0000-0000-0000-000000000000")
                {
                    var wellObj = db.WELL_REGISTERs.Where(x => x.well_id == wellId).FirstOrDefault();
                    var connections = db.WellTenants.Where(x => x.Id == wellObj.customer_id).FirstOrDefault();
                    if (connections != null)
                    {
                        var connectionstring = connections.ConnectionString;
                        if (connectionstring != null)
                        {
                            var operrepo = new OperatingTenantRepository(_operdb, HttpContext, _userManager);
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
                customErrorHandler.WriteError(ex, "GetChartAction_V1 InDepthRigData", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> GetGridData([DataSourceRequest] DataSourceRequest request, string wellId)
        {
            try
            {
                var aibus = new AIBuiness(db, _roleManager, _userManager);
                if (Request.Cookies["indepthrigdatagetwellbyrigsidcookies"] != null)
                {
                    wellId = Request.Cookies["indepthrigdatagetwellbyrigsidcookies"];
                }
                var data = await aibus.GetWellDepthGridData(wellId);
                return Json(data.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetGridData InDepthRigData", User.Identity.Name);
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
                if (Request.Cookies["indepthrigdatagetwellbyrigsidcookies"] != null)
                {
                    wellId = Request.Cookies["indepthrigdatagetwellbyrigsidcookies"];
                }
                var data = await projectBusiness.GetProjectsByWellId(tenantId, wellId);
                if (data != null)
                {
                    return Json(data.ToDataSourceResult(request));
                }
                return new JsonResult(request);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetInDepthProjectData InDepthRigData", User.Identity.Name);
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
                if (Request.Cookies["indepthrigdatagetwellbyrigsidcookies"] != null)
                {
                    wellId = Request.Cookies["indepthrigdatagetwellbyrigsidcookies"];
                }
                var result = _singleton.auctionProposalBusiness.GetAuctionsProposalListForInDepath(tenantId, wellId);
                return await Task.FromResult(Json(result.ToDataSourceResult(request)));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetInDepthSubmittedProposal InDepthRigData", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public JsonResult GetWellByRigsId(string id)
        {
            try
            {
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var restul = db.WellRegister.Where(x => x.RigID == id && x.customer_id == tenantId && x.Prediction == true)
                                              .Select(x => new
                                              {
                                                  WellName = x.wellname,
                                                  WellId = x.well_id
                                              }).ToList();
                if (restul.Count == 0 && id == null)
                {
                    restul = (from well in db.WellRegister
                              join rig in db.rig_register on well.RigID equals rig.Rig_id
                              where rig.isActive == true && well.Prediction == true && rig.TenantID == tenantId
                              select new
                              {
                                  WellName = well.wellname,
                                  WellId = well.well_id

                              }).ToList();
                }
                if (restul != null && restul.Count != 0)
                {
                    restul.Insert(0, new
                    {
                        WellName = "Select Well",
                        WellId = DLL.Constants.NoSpecificWellFilterKey
                    });
                }
                return Json(restul);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigData GetWellByRigsId", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> UpdateWellPrediction(string wellId, int prediction)
        {

            try
            {
                if (!string.IsNullOrEmpty(wellId))
                {
                    var aiBusiness = new AIBuiness(db, _roleManager, _userManager);
                    await aiBusiness.UpdatePredictionWellRegisterById(wellId, prediction == 1);
                    return Json(Guid.NewGuid());
                }
                return new JsonResult(null);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigData UpdateWellPrediction", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }

        }

        [AcceptVerbs("GET")]
        public async Task<IActionResult> GetWellPrediction(string wellId)
        {
            try
            {
                bool Well = false;

                if (!string.IsNullOrEmpty(wellId))
                {
                    var aiBusiness = new AIBuiness(db, _roleManager, _userManager);
                    var well = await aiBusiness.GetWellRegisterById(wellId);
                    return Json(well == null ? false : well.Prediction);
                }

                return Json(Well);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigData GetWellPrediction", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        //DWOP
        [HttpGet]
        public async Task<JsonResult> GetDrillplan()
        {
            try
            {
                var tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var rigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = await commonBusiness.GetDrillPlanList(tenantId, rigId);
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigData GetDrillplan", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }

        }

        //DWOP      
        public async Task<JsonResult> GetDrillplanWells(string? DrillPlanId)
        {
            try
            {
                var tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var rigId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = await commonBusiness.GetDrillPlanWells(DrillPlanId, tenantId); ;
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "InDepthRigData GetDrillplan", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
    }
}