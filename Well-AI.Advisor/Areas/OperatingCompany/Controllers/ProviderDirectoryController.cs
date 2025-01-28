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
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.API.Repository;
using WellAI.Advisor.API.Models;
using Well_AI.Advisor.API.PEC;
using Well_AI.Advisor.API.PEC.Repository;
using Well_AI.Advisor.API.PEC.Services;
using Microsoft.Extensions.DependencyInjection;
using Well_AI.Advisor.API.PEC.Services.IServices;
using Paket;
using System.Net;
using Telerik.Web.PDF;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Routing;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class ProviderDirectoryController : BaseController
    {
        private readonly ILogger<ProviderDirectoryController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private TenantOperatingDbContext _tdbContext;
        private TenantServiceDbContext _tServdbContext;
        private readonly IConfiguration _configuration;
        private ISession _session;
        private readonly TenantOperatingDbContext _operdb;
        public ISession Session { get { return _session; } }
        public ProviderDirectoryController(TenantOperatingDbContext operdb, WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager, ILogger<ProviderDirectoryController> logger, TenantOperatingDbContext tdbContext, IConfiguration configuration, TenantServiceDbContext servDbContext)
            : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _tdbContext = tdbContext;
            _configuration = configuration;
            _operdb = operdb;
            _session = WellAIAppContext.Current.Session;
            _tServdbContext = servDbContext;
        }
        public async Task<IActionResult> Index(string pending, string expire)
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
                await InitViewDataDicts();
                if (string.IsNullOrEmpty(pending))
                    pending = Session.GetObjectFromJson<string>("pending");
                if (string.IsNullOrEmpty(expire))
                    expire = Session.GetObjectFromJson<string>("expire");
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    ViewData["fieldPermission"] = user.Field == true ? true : false;
                }
                var result = await GetProviderDirectory(pending == "1", expire == "1");
                return View(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task InitViewDataDicts()
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var companies = await commonBusiness.GetServiceCompanies();
            var tenantIds = companies.Select(x => x.TenantId).ToList();
            ViewData["AllCompanies"] = companies;
            var compdat = new List<ProviderCompany>();
            foreach (var company in companies)
            {
                compdat.Add(new ProviderCompany
                {
                    Name = company.Name,
                    CompanyId = company.TenantId
                });
            }
            ViewData["Companies"] = compdat;
            var ti = HttpContext.GetMultiTenantContext().TenantInfo;
            if (ti != null)
            {
                var approvals = _tdbContext.ProviderDirectoryAppovals.ToList();
                var approvaldata = new List<SelectListItem>();
                foreach (var approval in approvals)
                {
                    approvaldata.Add(new SelectListItem
                    {
                        Text = approval.Name,
                        Value = approval.Id
                    });
                }
                ViewData["Approvals"] = approvaldata;
                var statuses = _tdbContext.ProviderDirectoryStatuses.ToList();
                var statusdata = new List<SelectListItem>();
                foreach (var status in statuses)
                {
                    statusdata.Add(new SelectListItem
                    {
                        Text = status.Name,
                        Value = status.Id
                    });
                }
                ViewData["Statuses"] = statusdata;
                var pecs = _tdbContext.ProviderDirectoryPECs.ToList();
                var pecdata = new List<SelectListItem>();
                foreach (var pec in pecs)
                {
                    pecdata.Add(new SelectListItem
                    {
                        Text = pec.Name,
                        Value = pec.Id
                    });
                }
                ViewData["PEC"] = pecdata;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                //MSA Permission
                var msadocuments = await commonBusiness.GetMSAWellFilesFromServiceTenantsForUser(tenantIds, ti.Id, wellId, _userManager.GetUserId(User));
                ViewData["msa"] = msadocuments;
            }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory ProviderProfile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                              
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "ViewDashboard", TenantId);
            }
            else
            {
                return false;
            }
        }

        public async Task<IActionResult> ProviderProfile(string id)
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                //Phase II Changes - 02/26/2021 - Get Provider Profile
                var profile = await GetProviderProfile(id);
                TempData["CompanyId"] = profile.CompanyId;
                return View(profile);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory ProviderProfile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> Counts()
        {
            try
            {
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
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
                return Json(providerdir);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Counts", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<ProviderDirectoryModel> GetProviderDirectory(bool pendingFilter = false, bool insureExpireFilter = false)
        {
            try
            {
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
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

                var operatingcompanies = await commonBusiness.GetOperatingCompanies();

                var servtenantIds = (from cp in db.CorporateProfile
                                     join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                     where cub.AccountType == 1
                                     select new CorporateProfile { TenantId = cp.TenantId }).ToList();


                tenantIds = servtenantIds.Select(x => x.TenantId).ToList();

                msafiles = await commonBusiness.GetMSAWellFilesFromServiceTenants(tenantIds, ti.Id, wellId);
            }
            else
                tenantIds = companies.Select(x => x.TenantId).ToList();

            //Phase II Changes - 02/25/21 - Commented Details Retrieve on Master list load 
            var insurefiles = await commonBusiness.GetInsuranceWellFilesFromServiceTenants(tenantIds, ti.Id, wellId);
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
                if (!string.IsNullOrEmpty(provider.InsuranceId))
                {
                    var Ins = insurefiles.Where(x => x.InsId == provider.InsuranceId).FirstOrDefault();
                    if (Ins != null)
                        provider.InsuranceDocument = Ins.Attachment;
                }                
              }
              providerdir.Providers = providers;
              return providerdir;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
              customErrorHandler.WriteError(ex, "ProviderDirectoryMSA_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);               
                ProviderDirectoryModel pd = new ProviderDirectoryModel();
                return pd;
              }
        }
        public async Task<IActionResult> ProviderDirectoryMSA_Read([DataSourceRequest] DataSourceRequest request, string companyId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var msadocuments = await commonBusiness.GetApprovedMSAWellFilesOfServiceTenant(new List<string> { companyId }, tenantId, wellId);
                return Json(msadocuments);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectoryMSA_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        public async Task<IActionResult> Insurance_Read([DataSourceRequest] DataSourceRequest request, string CompanyId)
        {
            try
            {
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var companies = await commonBusiness.GetServiceCompanies();
                var tenantIds = companies.Select(x => x.TenantId).ToList();
                var insurefiles = await commonBusiness.GetInsuranceWellFilesFromServiceTenants(tenantIds, ti.Id, wellId);
                var Ins = insurefiles.Where(x => x.Value == CompanyId).ToList();
                return Json(Ins);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Insurance_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        //MSA Permission
        public async Task<IActionResult> ProviderProfileMSA([DataSourceRequest] DataSourceRequest request, string companyId, string userId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                //MSA Permission
                var msadocuments = await commonBusiness.GetMSAWellFilesFromServiceTenantsForUser(new List<string> { companyId }, tenantId, wellId, _userManager.GetUserId(User));
                //MSA Permission
                return Json(msadocuments.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectoryMSA_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> ProviderDirectory_Filter([DataSourceRequest] DataSourceRequest request, bool pendingFilter, bool insureExpireFilter)
        {
            try
            {
                var result = await GetProviderDirectory(pendingFilter, insureExpireFilter);
                return Json(result.Providers.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> ProviderDirectory_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var result = await GetProviderDirectory();
                return Json(result.Providers.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public ActionResult AlertComplaince_Counts([DataSourceRequest] DataSourceRequest request, string pecs)
        {
            try
            {
                List<ProviderProfile> result = new List<ProviderProfile>();
                var Providers = (from Provider in _operdb.ProvidersDirectory
                                 join ap in _operdb.ProviderDirectoryAppovals on Provider.Approval equals ap.Id
                                 join aproval in _operdb.ProviderDirectoryStatuses on Provider.Status equals aproval.Id
                                 join pec in _operdb.ProviderDirectoryPECs
                                 on Provider.PEC equals pec.Id
                                 where pec.Name != pecs
                                 select new ProviderProfile
                                 {
                                     CompanyId = Provider.CompanyId,
                                     ProviderId = Provider.ID,
                                     ApprovalId = Provider.Approval,
                                     Approval = aproval == null ? "" : aproval.Name,
                                     StatusId = Provider.Status,
                                     Status = pec == null ? "" : pec.Name,
                                     PecStatusId = Provider.PEC,
                                     PecStatus = pec == null ? "" : pec.Name,
                                     MSADocumentId = Provider.MSA,//id of wellfile table in  master db
                                     InsuranceStart = Provider.InsuranceStart,
                                     InsuranceExpire = Provider.InsuranceExpire,
                                     Preferred = Provider.Preferred,
                                     Secondary = Provider.Secondary,
                                     Rating = Provider.Rating
                                 }).ToList();
                return Json(Providers.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory AlertComplaince_Counts", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [HttpGet]
        public async Task<FileResult> InsurenceDownload(string tenId, string fileId)
        {
            var filebytes = new KeyValuePair<string, byte[]>();
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var msadocument = await commonBusiness.GetWellFileById(fileId);
            string path = msadocument.Category + "/" + msadocument.FileName;
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenId, path);
                }
                var filebyte = filebytes.Value == null ? 0 : filebytes.Value.Length;
                if (filebytes.Key == "" || filebyte == 0)
                    return null;
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDirectory Download", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
            return File(filebytes.Value, filebytes.Key);
        }
        [HttpGet]
        public async Task<IActionResult> Download(string tenId, string fileId)
        {
            try
            {
                var filebytes = new KeyValuePair<string, byte[]>();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = await commonBusiness.GetWellFileById(fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var blobSection = _configuration.GetSection("AzureBlob");
                filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], tenantId, path);
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = msadocument.FileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Download", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Well AI Phase II changes - //Open or View Operating Company Pdf Document files in PdfViewer
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fileId"></param>
        /// <param name="TenId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = db.WellFiles.FirstOrDefault(x => x.FileId == fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                string tenantId = "";
                if (msadocument != null)
                {
                    tenantId = msadocument.TenantId;
                }
                else
                {
                    tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                }
                var blobSection = _configuration.GetSection("AzureBlob");
                var filebyte = AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                         blobSection["ContainerPrefixName"], tenantId, msadocument.Category, msadocument.FileName);

                JsonResult jsonResult;
                WebClient client = new WebClient();
                byte[] arr = client.DownloadData(filebyte.Result);
                FixedDocument doc = FixedDocument.Load(arr);
                if (pageNumber == null)
                {
                    jsonResult = Json(doc.ToJson());
                }
                else
                {
                    jsonResult = Json(doc.GetPage((int)pageNumber));
                }

                return jsonResult;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory GetPdfFile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }


        [HttpGet]
        public async Task<IActionResult> DownloadForServiceCompany(string tenId, string fileId)
        {
            try
            {
                var filebytes = new KeyValuePair<string, byte[]>();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = await commonBusiness.GetWellFileById(fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                string tenantId = "";
                if (msadocument != null)
                {
                    tenantId = msadocument.TenantId;
                }
                else
                {
                    tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                }

                var blobSection = _configuration.GetSection("AzureBlob");

                filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], tenantId, path);
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = msadocument.FileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Download", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DownloadProposal(string tenId, string fileId)
        {
            try
            {
                var filebytes = new KeyValuePair<string, byte[]>();
                var auctionBusiness = new AuctionProposalBusiness(db, _userManager);
                var file = await auctionBusiness.GetServiceCompanyAttachment(tenId, fileId);
                var path = file.FilePatch + "/" + file.FileName;
                var blobSection = _configuration.GetSection("AzureBlob");
                filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                     blobSection["ContainerPrefixName"], tenId, path);
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = file.FileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory DownloadProposal", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public ActionResult Update(string pending, string expire)
        {
            try
            {
                if (pending != null)
                    Session.SetObjectAsJson("pending", pending);
                if (expire != null)
                    Session.SetObjectAsJson("expire", expire);
                return RedirectToAction("Index", new { pending, expire });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_Create([DataSourceRequest] DataSourceRequest request, ProviderProfile input)
        {
            try
            {
                if (input != null && ModelState.IsValid)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var services = new ServiceCollection();
                    services.UseServices();
                    var serviceProvider = services.BuildServiceProvider();
                    var service = serviceProvider.GetRequiredService<IPecService>();
                    //string OrganizationId = "282acaa1-5b38-4831-99ec-a83e0030ed9f";
                    var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    //var details = await service.GetOrganizationDetailsAsyc(OrganizationId, TenantId);
                    //var ranking = await service.GetOrganizationRankingAsyc(OrganizationId, TenantId);
                    var pecstatus = "";
                    //var color = ranking.overallRanking.color;
                    var color = "#33E0FF";
                    if (color == "#72a017")
                    {
                        pecstatus = "Good";
                    }
                    else if (color == "")
                    {
                        pecstatus = "Average";
                    }
                    else
                    {
                        pecstatus = "Bad";
                    }
                    var res = await operrepo.UpdateProviderDirectory(input, pecstatus, db);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory ProviderDirectory_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_Update([DataSourceRequest] DataSourceRequest request, ProviderProfile input)
        {
            try
            {
                if (input != null)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    //var services = new ServiceCollection();
                    // services.UseServices();
                    //var serviceProvider = services.BuildServiceProvider();
                    //var service = serviceProvider.GetRequiredService<IPecService>();
                    //string OrganizationId = "282acaa1-5b38-4831-99ec-a83e0030ed9f";
                    var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    //var details = await service.GetOrganizationDetailsAsyc(OrganizationId, TenantId);
                    //var ranking = await service.GetOrganizationRankingAsyc(OrganizationId, TenantId);
                    var pecstatus = "";
                    //var color = ranking.overallRanking.color;
                    var color = "#33E0FF";
                    if (color == "#72a017")
                    {
                        pecstatus = "Good";
                    }
                    else if (color == "")
                    {
                        pecstatus = "Average";
                    }
                    else
                    {
                        pecstatus = "Bad";
                    }
                    var res = await operrepo.UpdateProviderDirectory(input, pecstatus, db);                 
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_Destroy(string ProviderId, string companyId)
        {
            try
            {
                var OprTenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                if (!string.IsNullOrEmpty(companyId))
                {

                    var RigDepthPermission = db.RigsDepth_Permissions.Where(x => x.OprTenantId == OprTenantId && x.SerTenantId == companyId && x.DepthPermission == true).ToList();
                    if (RigDepthPermission.Count > 0)
                    {
                        var rig = new List<string>();
                        foreach (var dep in RigDepthPermission)
                        {
                            var rigname = db.rig_register.Where(x => x.Rig_id == dep.RigId).Select(y => y.Rig_Name);
                            rig.AddRange(rigname);
                        }

                        return Json(new { IsDepthPermission = true, RigName = string.Join(",", rig) });
                    }

                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var res = await operrepo.DeleteProviderDirectory(ProviderId);

                    var MessageQueue = db.MessageQueues.Where(x => x.EntityId == ProviderId).ToList();
                    foreach (var message in MessageQueue)
                    {
                        db.MessageQueues.Remove(message);
                        await db.SaveChangesAsync();
                    }

                    var MSADocuments = db.ProviderMSALinks.Where(x => x.OperationTenantId == OprTenantId && x.ServiceTenantId == companyId && x.IsApproved == true).ToList();

                    foreach (var MSA in MSADocuments)
                    {
                        MSA.IsApproved = false;
                        db.ProviderMSALinks.Update(MSA);
                        await db.SaveChangesAsync();
                    }

                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_Rate(string provider, int rate)
        {
            try
            {
                if (!string.IsNullOrEmpty(provider))
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var res = await operrepo.UpdateRatingProviderDirectory(provider, rate);
                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory_Rate", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Karthik-06/15/2020
        public IActionResult Providers_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var tenantIds = db.CrmCompanies.Where(x => x.TenantId != null).Select(x => x.TenantId);
                var corpProfiles = db.CorporateProfile.Where(x => tenantIds.Contains(x.TenantId)).ToList();
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                var existingList = operrepo.GetProviderDirectories();
                var existingId = existingList.Result.Select(e => e.CompanyId);
                var allVendors = corpProfiles.Select(e => e.TenantId)
                                             .Except(existingId).ToList();
                string output = String.Join(",", allVendors);
                var openVendors = corpProfiles
                                   .Where(vendor => allVendors.Contains(vendor.TenantId))
                                   .Select(cp => new CorporateProfile
                                   {
                                       ID = cp.ID,
                                       UserId = cp.UserId,
                                       Name = cp.Name,
                                       Website = cp.Website,
                                       City = cp.City,
                                       State = db.USAStates.Where(x => x.StateId == Convert.ToInt32(cp.State)).Select(y => y.Name).FirstOrDefault(),
                                       TenantId = cp.TenantId
                                   }).ToList();

                return Json(openVendors.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Providers_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveProvider([FromBody] ProviderProfile company)
        {
            CommunicationViewModel model = new CommunicationViewModel();
            // string pending="1"; string expire="1";
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var companies = await commonBusiness.GetServiceCompanies();
                var comp = companies.FirstOrDefault(cm => cm.ID == company.CompanyId);
                var companyId = "";
                if (comp != null)
                {
                    companyId = comp.TenantId;
                }
                if (companyId != "")
                {
                    company.CompanyId = companyId;
                    var pendingReview = (from ap in _tdbContext.ProviderDirectoryAppovals
                                         where ap.Name == "Pending review"
                                         select new WellAI.Advisor.Model.OperatingCompany.Models.ProviderDirectoryApproval
                                         {
                                             Id = ap.Id
                                         }
                                    ).SingleOrDefault();
                    var inactiveStatus = (from st in _tdbContext.ProviderDirectoryStatuses
                                          where st.Name == "Inactive"
                                          select new WellAI.Advisor.Model.OperatingCompany.Models.ProviderDirectoryStatus
                                          {
                                              Id = st.Id
                                          }
                                          ).SingleOrDefault();
                    company.ApprovalId = pendingReview.Id;
                    company.StatusId = inactiveStatus.Id;
                }
                //var approvaldata = (List<ProviderDirectoryApproval>)ViewData["Approvals"];
                //var statusdata = (List<ProviderDirectoryStatus>)ViewData["Statuses"];
                int result = 0;
                if (company != null)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    //var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var pecstatus = "";
                    var color = "#33E0FF";
                    if (color == "#72a017")
                    {
                        pecstatus = "Good";
                    }
                    else if (color == "")
                    {
                        pecstatus = "Average";
                    }
                    else
                    {
                        pecstatus = "Bad";
                    }
                    var res = await operrepo.UpdateProviderDirectory(company, pecstatus, db);
                    result = res;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Communication SaveProvider", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            //return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            //return View(await GetProviderDirectory(pending == "1", expire == "1"));
        }
        /// <summary>
        /// Phase II Changes - 01/19/2021 - Approve MSA Documents
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_ApproveMSA(string fileId, string approvalStatus)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileId))
                {
                    string userId = "";
                    var file = db.ProviderMSALinks.Where(x => x.FileId == fileId).FirstOrDefault();
                    var dbprefix = "serv";
                    var servguid = new Guid(file.ServiceTenantId);
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + servguid.ToString("N"));
                    var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                    var servDBContext = new TenantServiceDbContext(ti);
                    _tServdbContext = servDBContext;
                    var servdb = new ServiceTenantRepository(_tServdbContext, HttpContext, _userManager, db);
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        userId = user.Id;
                    }

                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var res = await commonBusiness.UpdateMSAApprovalStatus(fileId, approvalStatus == "true" ? true : false, userId, servdb);

                    
                    
                    //Phase II changes - 03/01/2021
                    //Welcome, Authorized and Preferred Vendor status update (1-Welcome,2-Authorized,3-Preferred)
                    var servTenantID = servguid.ToString("D");
                    var operTenantId = new Guid(file.OperationTenantId).ToString("D");
                    int statusResult = commonBusiness.UpdateVendorPreferredStatus(operTenantId, servTenantID);

                    //Phase II changes - 02/09/2021 - Remove an MSA Id at Provider Directory if the MSA is unapproved
                    if (approvalStatus == "false")
                    {
                        var provider = _operdb.ProvidersDirectory.Where(x => x.TenantId == operTenantId && x.CompanyId == servTenantID).FirstOrDefault();
                        if (provider != null)
                        {
                            provider.MSA = null;
                            _operdb.SaveChanges();
                        }
                    }
                    else if (approvalStatus == "true")
                    {
                        //MSA Permission
                        var provider = _operdb.ProvidersDirectory.Where(x => x.TenantId == operTenantId && x.CompanyId == servTenantID).FirstOrDefault();
                        if (provider != null)
                        {
                            provider.MSA = fileId;
                            _operdb.SaveChanges();
                        }
                    }

                    //Phase II changes - 05/21/2021 - Update MSA at Service
                    if (approvalStatus == "false")
                    {
                        var provider = _tServdbContext.OperatingDirectory.Where(x => x.TenantId == servTenantID && x.CompanyId == operTenantId).FirstOrDefault();
                        if (provider != null)
                        {
                            provider.MSA = null;
                            _tServdbContext.SaveChanges();
                        }
                    }
                    else if (approvalStatus == "true")
                    {
                        //MSA Permission
                        var provider = _tServdbContext.OperatingDirectory.Where(x => x.TenantId == servTenantID && x.CompanyId == operTenantId).FirstOrDefault();
                        if (provider != null)
                        {
                            provider.MSA = fileId;
                            _tServdbContext.SaveChanges();
                        }
                    }

                    if (statusResult > 0)
                    {
                        var provider = _operdb.ProvidersDirectory.Where(x => x.CompanyId == servTenantID).FirstOrDefault();
                        provider.Preferred = Convert.ToByte(statusResult);
                        _operdb.SaveChanges();
                    }
                    else if (statusResult == 0)
                    {
                        var provider = _operdb.ProvidersDirectory.Where(x => x.CompanyId == servTenantID).FirstOrDefault();
                        provider.Preferred = Convert.ToByte(1);
                        _operdb.SaveChanges();

                        var depthpermission = db.RigsDepth_Permissions.Where(x => x.OprTenantId == operTenantId && x.SerTenantId == servTenantID && x.DepthPermission == true).ToList();
                        if (depthpermission != null)
                        {
                            depthpermission.ForEach(a => a.DepthPermission = false);
                            await db.SaveChangesAsync();
                        }

                    }

                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory_ApproveMSA", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        /// <summary>
        /// Phase II Changes - GetRigs with Permission
        /// </summary>
        /// <param name="request"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public IActionResult GetRigs([DataSourceRequest] DataSourceRequest request, string CompanyId)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                byte preferredStatus = 0;
                var dbprefix = "serv";
                var servguid = new Guid(CompanyId);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var OperDBContext = new TenantServiceDbContext(ti);
                _tServdbContext = OperDBContext;

                var SubscribedRigs = _tServdbContext.subscriptionOperatorRigs.Where(x => x.CompanyId == tenantId).ToList();

                var provider = _operdb.ProvidersDirectory.Where(x => x.CompanyId == servguid.ToString("D")).FirstOrDefault();

                if (provider != null)
                {
                    preferredStatus = provider.Preferred;
                }

                var Result = (from subrig in SubscribedRigs
                              join rig in db.rig_register on subrig.RigId equals rig.Rig_id
                              join well in db.WellRegister on subrig.RigId equals well.RigID
                              join depPermission in db.RigsDepth_Permissions on well.well_id equals depPermission.WellId into dp
                              from depPermission in dp.DefaultIfEmpty()
                              where subrig.CompanyId == tenantId /*&& rig.isActive == true*/
                              select new RigsDepthPermission_Model
                              {
                                  RigName = rig.Rig_Name,
                                  WellName = well.wellname,
                                  DepthPermission = depPermission == null ? false : depPermission.DepthPermission,
                                  RigId = rig.Rig_id,
                                  WellId = well.well_id,
                                  PreferredStatus = Convert.ToInt16(preferredStatus) == 1 ? "Welcome" : "Others",
                                  WellPrediction = well.Prediction
                              }).ToList();

                return Json(Result.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory GetRigs", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]

        public async Task<IActionResult> SaveRigsDepth_Permission(string Rigid, string Wellid, string Sertenant, bool DepthPermission)
        {
            try
            {
                RigsDepth_Permission DepthValue = new RigsDepth_Permission();
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                if (ModelState.IsValid)
                {
                    DepthValue.ID = Guid.NewGuid().ToString();
                    DepthValue.RigId = Rigid;
                    DepthValue.WellId = Wellid;
                    DepthValue.DepthPermission = DepthPermission;
                    DepthValue.OprTenantId = TenantId;
                    DepthValue.SerTenantId = Sertenant;
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                var Result = await commonBusiness.CreateDepthPermission(DepthValue);

                //Phase II changes - 03/01/2021
                //Welcome, Authorized and Preferred Vendor status update (1-Welcome,2-Authorized,3-Preferred)
                int statusResult = commonBusiness.UpdateVendorPreferredStatus(TenantId, Sertenant);

                if (statusResult > 0)
                {
                    var provider = _operdb.ProvidersDirectory.Where(x => x.CompanyId == Sertenant).FirstOrDefault();
                    provider.Preferred = Convert.ToByte(statusResult);
                    _operdb.SaveChanges();
                }
                else if (statusResult == 0)
                {
                    var provider = _operdb.ProvidersDirectory.Where(x => x.CompanyId == Sertenant).FirstOrDefault();
                    provider.Preferred = Convert.ToByte(1);
                    _operdb.SaveChanges();

                    var depthpermission = db.RigsDepth_Permissions.Where(x => x.OprTenantId == TenantId && x.SerTenantId == Sertenant && x.DepthPermission == true).ToList();
                    if (depthpermission != null)
                    {
                        depthpermission.ForEach(a => a.DepthPermission = false);
                        await db.SaveChangesAsync();
                    }
                }

                return Ok(Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory SaveRigsDepth_Permission", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        //Phase II Changes - 02/25/2021
        /// <summary>
        /// Get Single Provider Profile by Profile Id
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public async Task<ProviderProfile> GetProviderProfile(string profileId)
        {
            var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var ti = HttpContext.GetMultiTenantContext().TenantInfo;
            var provider = await operrepo.GetProviderDirectoryByProfileId(profileId);
            var pecs = (from Provider in _operdb.ProvidersDirectory
                        join pec in _operdb.ProviderDirectoryPECs
                        on Provider.PEC equals pec.Id
                        where Provider.ID == profileId && pec.Name != "Good"
                        select Provider).Count();

            var msafiles = (List<MSA>)ViewData["msa"];
            var companies = (List<CorporateProfile>)ViewData["AllCompanies"];
            List<string> tenantIds = null;
            var wellIdCookie = Request.Cookies["wellfilterlayout"];
            var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
            if (companies == null)
            {
                companies = await commonBusiness.GetServiceCompanies();

                var operatingcompanies = await commonBusiness.GetOperatingCompanies();

                var servtenantIds = (from cp in db.CorporateProfile
                                     join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                     where cub.AccountType == 1
                                     select new CorporateProfile { TenantId = cp.TenantId }).ToList();

                tenantIds = servtenantIds.Select(x => x.TenantId).ToList();

                msafiles = await commonBusiness.GetMSAWellFilesFromServiceTenants(tenantIds, ti.Id, wellId);
            }
            else
                tenantIds = companies.Select(x => x.TenantId).ToList();

            var insurefiles = await commonBusiness.GetInsuranceWellFilesFromServiceTenants(tenantIds, ti.Id, wellId);
            IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);

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
            //Phase II Changes - 02/25/21
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
            var offerings = await commonBusiness.GetOperatingCompanyServices(provider.CompanyId);
            provider.ServiceOffering = offerings;
            //Phase II changes - 01/18/2021
            if (!string.IsNullOrWhiteSpace(provider.MSADocumentId))
            {
                var msafile = msafiles.Where(x => x.MsaId == provider.MSADocumentId).ToList();
                provider.Msa = msafile;
            }

            if (!string.IsNullOrWhiteSpace(provider.CompanyId))
            {
                var msafile = msafiles.Where(x => x.CompanyId == provider.CompanyId).ToList();
                provider.Msa = msafile.OrderByDescending(x => x.Expiration).ToList();
            }
            var Ins = insurefiles.Where(x => x.Value == provider.CompanyId).ToList();
            provider.Insurance = Ins;
            provider.Proposals = await auctionProposalBusiness.GetServiceCompanyActualProposals(provider.CompanyId, ti.Id, wellId);

            return provider;
        }

        //House Keeping Changes - 01/05/2022
        /// <summary>
        /// Get Provider Profiles 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProviderProfile>> GetProviderProfiles(string profileId)
        {
            var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var ti = HttpContext.GetMultiTenantContext().TenantInfo;
            //var provider = await operrepo.GetProviderDirectoryByProfileId(profileId);
            var providers = await operrepo.GetProviderDirectories();

            ProviderDirectoryModel providerDirctory = await GetProviderDirectory(false,false);
           
            if (providerDirctory.Providers.Count > 0)
            {
                return providerDirctory.Providers;
            }
            else
            {
                return new List<ProviderProfile>();
            }
            
        }

        public async Task<IActionResult> GetProviderProfilesAsync([DataSourceRequest] DataSourceRequest request, string profileId)
        {
            try
            {
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                //var provider = await operrepo.GetProviderDirectoryByProfileId(profileId);
                var providers = await operrepo.GetProviderDirectories();

                ProviderDirectoryModel providerDirctory = await GetProviderDirectory(false, false);
                var data = new List<ProviderProfile>();
                if (providerDirctory.Providers.Count > 0)
                {
                    data = providerDirctory.Providers;
                }
                else
                {
                    data = new List<ProviderProfile>();
                }
                return Json(data);
            }
            catch (Exception ex)
            {
                var data = new List<ProviderProfile>();
                return Json(data);
            }
        }

        [AcceptVerbs("POST")]
        public async Task<ActionResult> UploadInsurance(IFormCollection form, string CompanyId)
        {
            try
            {
                var blobSection = _configuration.GetSection("AzureBlob");
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                var isExits = db.WellFiles.Where(x => x.TenantId == CompanyId && x.Category == "Insurance").ToList();
                if (isExits.Count > 0)
                {

                    var FileId = isExits.Select(x => x.FileId).ToList();
                    foreach (var ids in FileId)
                    {
                        var Getpath = db.WellFiles.Where(x => x.FileId == ids).FirstOrDefault();

                        var deleted = await AzureBlobStorage.DeleteFileByPath(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                            blobSection["ContainerPrefixName"], tenantId, "Insurance" + "/" + Getpath.FileName);

                    }
                    db.WellFiles.RemoveRange(isExits);
                    db.SaveChanges();
                }

                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                var SerTenantId = form["CompanyId"];

                foreach (var file in form.Files)
                {
                    var filename = db.WellFiles.Where(x => x.FileName == file.FileName && x.Category == "Insurance").Count();
                    if (filename > 0)
                    {
                        ModelState.AddModelError("Error", "This file already exist");
                    }
                    else
                    {
                        var fileId = await SaveFile(file, "Insurance", SerTenantId);
                    }
                }

                var id = _operdb.ProvidersDirectory.Where(x => x.CompanyId == CompanyId).FirstOrDefault(); ;

                return RedirectToAction("ProviderProfile", new RouteValueDictionary(new { controller = "ProviderDirectory", action = "ProviderProfile", id = id.ID }));
                //return RedirectToAction("ProviderProfile", "ProviderDirectory");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Upload", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }


        protected async Task<object> SaveFile(IFormFile file, string pathToSave, string SerTenantId)
        {
            object result = null;
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    result = await AzureBlobStorage.UploadFileToBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, file, pathToSave);
                    System.Type type = result.GetType();
                    var docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                    var userId = _userManager.GetUserId(User);
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    await commonBusiness.CreateWellFile(new DLL.Entity.WellFile
                    {
                        FileName = file.FileName,
                        TenantId = Convert.ToString(SerTenantId),
                        Category = pathToSave,
                        UserId = userId,
                        FileId = Guid.NewGuid().ToString("D"),
                        Date = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory SaveFile", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return result;
        }

        public IActionResult Insurance_ReadForGrid([DataSourceRequest] DataSourceRequest request, string CompanyId)
        {
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                var docs = db.WellFiles.Where(x => x.TenantId == CompanyId && x.Category == "Insurance").ToList();
                var result = new List<UploadsGridFileModel>();

                foreach (var doc in docs)
                {
                    var vendor = db.CorporateProfile.FirstOrDefault(x => x.TenantId == ti.Id);

                    if (vendor != null)
                    {
                        var newItem = new UploadsGridFileModel
                        {
                            FileId = doc.FileId,
                            FileName = doc.FileName,
                            Date = (DateTime?)doc.Date.Value,
                            WellName = vendor.Name
                        };
                        result.Add(newItem);
                    }
                }
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Insurance_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return null;
            }
        }


        [HttpGet]
        public IActionResult GetInsurancefile_Validation(string FileName)
        {
            try
            {
                //var VendorDetails = Vendor.Split(":", StringSplitOptions.RemoveEmptyEntries);
                var CategoryName = "Insurance";
                int WellFiledetails1 = db.WellFiles.Where(x => x.Category == CategoryName).Count();
                if (WellFiledetails1 > 0)
                {
                    return Json(new { Status1 = "True" });
                }
                int WellFiledetails = db.WellFiles.Where(x => x.FileName == FileName && x.Category == CategoryName).Count();
                return Json(new { Status = WellFiledetails == 0 ? "False" : "True" });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Upload GetMSAfile_Validation", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insurance_Destroy(string fileId)
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                try
                {
                    bool deleted = false;
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var category = "Insurance";
                    var WellFileExisting = db.WellFiles.Where(x => x.FileId == fileId && x.Category == category).FirstOrDefault();

                    //Phase II Changes 02/08/2021- remove from ProviderLinkMSA table
                    var filename = await commonBusiness.RemoveWellFile(fileId);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                        var path = "Insurance";
                        var blobSection = _configuration.GetSection("AzureBlob");
                        deleted = await AzureBlobStorage.DeleteFileByPath(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenantId, path + "/" + filename);
                    }
                    return Json(new[] { deleted });
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "Uploads UploadsDrillPrograms_Destroy", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
            }
            return null;
        }

        public IActionResult Insurance()
        {
            return View();
        }

    }
}