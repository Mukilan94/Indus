using Finbuckle.MultiTenant;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Telerik.Web.PDF;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    [SessionTimeOut]
    public class OperatingDirectoryController : BaseController
    {
        private readonly ILogger<OperatingDirectoryController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private TenantServiceDbContext _servicedb;
        private readonly IConfiguration _configuration;
        private ISession _session;
        //Phase II Changes - 05/21/2021
        private TenantOperatingDbContext _Operdb;
        public ISession Session { get { return _session; } }
        public OperatingDirectoryController(WebAIAdvisorContext dbContext,
            SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager,
            ILogger<OperatingDirectoryController> logger,
            TenantServiceDbContext servicedb, IConfiguration configuration)
            : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _servicedb = servicedb;
            _configuration = configuration;
            _session = WellAIAppContext.Current.Session;
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
                        string returnUrl = @"/ServiceDashboard";
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

                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var user1 = await _userManager.GetUserAsync(User);
                var detail = commonBusiness.GetUserBasicDetail(user1.Id);

                var SrvTenantIds = (from crp in db.CorporateProfile                                    
                                    join crmuser in db.CrmUserBasicDetail on crp.ID equals crmuser.CorporateProfileId
                                    where crmuser.AccountType == 1|| crmuser.AccountType == 1
                                    select new
                                    {
                                        TenantId = crp.TenantId
                                    }
                               ).ToList();

                var ProTenantIds = db.CorporateProfile.Where(X => !SrvTenantIds.Select(Y => Y.TenantId).Contains(X.TenantId)).Select(x => x.TenantId).ToList();
                var corpProfiles = db.CorporateProfile.Where(x => ProTenantIds.Contains(x.TenantId)).ToList();
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var existingList = operrepo.GetProviderDirectories();
                var existingId = existingList.Result.Select(e => e.CompanyId);

                var allVendors = corpProfiles.Select(e => e.TenantId)
                                           .Except(existingId).ToList();               

                string output = String.Join(",", allVendors);
                              
                var openVendors = corpProfiles
                                   .Where(vendor => allVendors.Contains(vendor.TenantId))
                                   .Select(cp => new Model.ServiceCompany.Models.ServiceCorporateProfile
                                   {
                                       ID = cp.TenantId,
                                       UserId = cp.UserId,
                                       Name = cp.Name,
                                       Website = cp.Website,
                                       City = cp.City,
                                       State = db.USAStates.Where(x => Convert.ToString(x.StateId) == cp.State).Select(y => y.Name).FirstOrDefault(),
                                       TenantId = cp.ID
                                   }).ToList();
               
                return View(await GetProviderDirectory(pending == "1", expire == "1"));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDirectory Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task InitViewDataDicts()
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var companies = await commonBusiness.GetOperatingCompanies();
            var tenantIds = companies.Select(x => x.TenantId).ToList();
            ViewData["AllCompanies"] = companies;
            var compdat = new List<SelectListItem>();
            foreach (var company in companies)
            {
                compdat.Add(new SelectListItem
                {
                    Text = company.Name,
                    Value = company.TenantId
                });
            }
            ViewData["Companies"] = compdat;
            var ti = HttpContext.GetMultiTenantContext().TenantInfo;
            if (ti != null)
            {
                var approvals = _servicedb.OperatingDirectoryAppovals.ToList();
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
                var statuses = _servicedb.OperatingDirectoryStatuses.ToList();
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
                var pecs = _servicedb.OperatingDirectoryPECs.ToList();
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
                var msadocuments = await commonBusiness.GetMSAWellFilesFromOperatingTenants(tenantIds, ti.Id);
                ViewData["msa"] = msadocuments;

                //Phase II Changes - 05/20/2021
                var insureFiles = await commonBusiness.GetInsuranceFilesFromServiceTenants(tenantIds, ti.Id, "");               
                ViewData["ServiceInsuranceFiles"] = insureFiles;

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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "ViewDashboard", TenantId);
            }
            else
            {
                return false;
            }
        }

        public async Task<IActionResult> OperatorProfile(string id)
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                var profile = await GetOperatingProfile(id);
                //var profile = dir.Providers.FirstOrDefault(x => x.ProviderId == id);
                return View(profile);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDirectory OperatorProfile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        //TU-Phase-II Changes
        public async Task<OperatingDirectoryModel> GetProviderDirectory(bool pendingFilter = false, bool insureExpireFilter = false)
        {
            try
            {
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                var providers = await operrepo.GetProviderDirectories();
                var providerdir = new OperatingDirectoryModel
                {
                    InsExpiring90days = providers.Count(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0),
                    Pending = providers.Count(x => x.Approval == "Pending review"),
                    Records = providers.Count
                };
                //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                providerdir.PreferredProvider = providers.FirstOrDefault(x => x.Preferred);
                providerdir.SecondaryProvider = providers.FirstOrDefault(x => x.Secondary);
                if (pendingFilter)
                {
                    providers = providers.Where(x => x.Approval == "Pending review").ToList();
                }
                if (insureExpireFilter)
                {
                    providers = providers.Where(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0).ToList();
                }
                var msafiles = (List<ServiceMSA>)ViewData["msa"];
                var companies = (List<Model.OperatingCompany.Models.CorporateProfile>)ViewData["AllCompanies"];
                List<string> tenantIds = null;
                if (companies == null)
                {
                    companies = await commonBusiness.GetOperatingCompanies();
                    tenantIds = companies.Select(x => x.TenantId).ToList();
                    msafiles = await commonBusiness.GetMSAWellFilesFromOperatingTenants(tenantIds, ti.Id);
                }
                else
                    tenantIds = companies.Select(x => x.TenantId).ToList();
                //Phase II Changes - 05/19/2021
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                foreach (var provider in providers)
                {
                    var cmpprofile = db.CorporateProfile.Where(x => x.TenantId.Equals(provider.CompanyId)).FirstOrDefault();
                    var company = companies.FirstOrDefault(x => x.TenantId.ToString() == provider.CompanyId);//provider.CompanyId=tenantId of Service Company
                    if (cmpprofile != null)
                    {
                        var site = string.IsNullOrEmpty(cmpprofile.Website) || cmpprofile.Website.StartsWith("http:") ? cmpprofile.Website : "http://" + cmpprofile.Website;
                        provider.Name = cmpprofile.Name;
                        provider.CompanyId = cmpprofile.TenantId;
                        provider.Website = site;
                        provider.Phone = cmpprofile.Phone;
                        provider.User = await commonBusiness.GetPrimaryUserSRV(cmpprofile.TenantId);
                        provider.Location = string.Format("{0}{1},{2},{3},{4}", cmpprofile.Address1, string.IsNullOrEmpty(cmpprofile.Address2) ? "" : "," + cmpprofile.Address2,
                        cmpprofile.City, cmpprofile.State, cmpprofile.Zip);
                        provider.City = cmpprofile.City;
                        provider.State = db.USAStates.Where(x => x.StateId == Convert.ToInt32(cmpprofile.State)).Select(y => y.Name).FirstOrDefault();
                        provider.Zip = cmpprofile.Zip;
                        provider.Address1 = cmpprofile.Address1 + "," + cmpprofile.City + "," + provider.State + "," + cmpprofile.Phone;
                        provider.Address2 = cmpprofile.Address2;
                    }
                    msafiles = await commonBusiness.GetMSAWellFilesFromOperatingTenants(new List<string> { provider.CompanyId }, ti.Id);
                    provider.MSADocument = "";
                    var msa = msafiles.FirstOrDefault(x => x.MsaId == provider.MSADocumentId);
                    if (msa != null)
                    {
                        provider.MSADocumentId = msa.MsaId;
                        provider.MSADocument = msa.Attachment;
                    }

                    //Phase II Changes - 02/25/21 - Commented Details Retrieve on Master list load 
                    var insurefiles = await commonBusiness.GetInsuranceFilesFromServiceTenants(tenantIds, ti.Id, provider.CompanyId);

                    if (!string.IsNullOrEmpty(provider.InsuranceId))
                    {
                        var Ins = insurefiles.Where(x => x.InsId == provider.InsuranceId).FirstOrDefault();
                        if (Ins != null)
                        {
                            provider.InsuranceDocument = Ins.Attachment;
                            provider.InsuranceId = Ins.InsId;
                        }
                    }
                }
                providerdir.Providers = providers;
                return providerdir;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDirectory GetProviderDirectory", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
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
        /// <summary>
        /// Well AI Phase II changes - //Open or View Operating Company Pdf Document files in PdfViewer - 01/28/2021
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fileId"></param>
        /// <param name="TenId"></param>
        /// <returns></returns>
        //Phase-II Changes
        [HttpGet]
        public IActionResult GetPdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = db.WellFiles.FirstOrDefault(x => x.FileId == fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                var blobSection = _configuration.GetSection("AzureBlob");
                var filebyte = AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                         blobSection["ContainerPrefixName"], TenId, msadocument.Category, msadocument.FileName);

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
                customErrorHandler.WriteError(ex, "OperatingDirectoryController GetPdfFile", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
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
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var blobSection = _configuration.GetSection("AzureBlob");
                if (tenId != null)
                {
                    filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                   blobSection["ContainerPrefixName"], tenId, path);
                }
                else
                {
                    filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                   blobSection["ContainerPrefixName"], msadocument.TenantId, path);
                }
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDirectory Download", User.Identity.Name);
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
                customErrorHandler.WriteError(ex, "OperatingDirectory DownloadProposal", User.Identity.Name);
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
                customErrorHandler.WriteError(ex, "OperatingDirectory DownloadProposal", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_Create([DataSourceRequest] DataSourceRequest request, OperatingProviderProfile input)
        {
            try
            {
                if (input != null && ModelState.IsValid)
                {
                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await operrepo.UpdateProviderDirectory(input);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDirectory DownloadProposal", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_Update([DataSourceRequest] DataSourceRequest request, OperatingProviderProfile input)
        {
            try
            {              
                //Phase II Changes - 05/19/2021
                if (input != null)
                {
                    var servdb = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await servdb.UpdateProviderDirectory(input);

                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var insuranceRes = await commonBusiness.UpdateProviderInsuranceLink(Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")).ToString(), input.CompanyId, input.InsuranceId, input.InsuranceExpire);


                    var tId = Guid.Parse(input.CompanyId);
                    var dbprefix = "oper";
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + tId.ToString("N"));

                    var ti1 = new TenantInfo(input.CompanyId, input.CompanyId, input.CompanyId, connString, null);
                    var operContext = new TenantOperatingDbContext(ti1);
                    _Operdb = operContext;
                    var provider = _Operdb.ProvidersDirectory.Where(x => x.CompanyId == Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")).ToString()).FirstOrDefault();
                    provider.Insurance = Convert.ToString(input.InsuranceId);
                    provider.InsuranceExpire = input.InsuranceExpire;
                    _Operdb.SaveChanges();   
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
        public async Task<IActionResult> ProviderDirectory_Destroy(string companyId)
        {
            try
            {
                if (!string.IsNullOrEmpty(companyId))
                {
                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await operrepo.DeleteProviderDirectory(companyId);
                    var MessageQueue = db.MessageQueues.Where(x => x.EntityId == companyId).ToList();
                    foreach (var message in MessageQueue)
                    {
                        db.MessageQueues.Remove(message);
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
                string returnUrl = @"/ServiceDashboard/Error";
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
                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
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
        public async Task<IActionResult> Providers_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var user = await _userManager.GetUserAsync(User);                
                var detail = commonBusiness.GetUserBasicDetail(user.Id);
                
                var SrvTenantIds = (from crp in db.CorporateProfile                                    
                                    join crmuser in db.CrmUserBasicDetail on crp.ID equals crmuser.CorporateProfileId
                                    where crmuser.AccountType == 0 || crmuser.AccountType == 3
                                    select new
                                    {
                                        TenantId = crp.TenantId
                                    }
                               ).ToList();


                var ProTenantIds = db.CorporateProfile.Where(X => !SrvTenantIds.Select(Y => Y.TenantId).Contains(X.TenantId)).Select(x => x.TenantId).ToList();
                var corpProfiles = db.CorporateProfile.Where(x => ProTenantIds.Contains(x.TenantId)).ToList();
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var existingList = operrepo.GetProviderDirectories();
                var existingId = existingList.Result.Select(e => e.CompanyId);

                var allVendors = corpProfiles.Select(e => e.TenantId)
                                           .Except(existingId).ToList();

                //var allVendorsList = (from crp in db.CorporateProfile         
                //                      join crmuser in db.CrmUserBasicDetail on crmp.UserId equals crmuser.UserId
                //                      where crmuser.AccountType == 0
                //                      select new Model.OperatingCompany.Models.CorporateProfile
                //                      {
                //                          TenantId = crp.TenantId,
                //                      }).ToList();
                //var allVendors = allVendorsList.Select(e => e.TenantId)
                //                 .Except(existingId).ToList();      

                string output = String.Join(",", allVendors);
                var openVendors = corpProfiles
                                   .Where(vendor => allVendors.Contains(vendor.TenantId))
                                   .Select(cp => new Model.ServiceCompany.Models.ServiceCorporateProfile
                                   {
                                       ID = cp.TenantId,
                                       UserId = cp.UserId,
                                       Name = cp.Name,
                                       Website = cp.Website,
                                       City = cp.City,
                                       State = db.USAStates.Where(x => x.StateId == Convert.ToInt32(cp.State)).Select(y => y.Name).FirstOrDefault(),
                                       TenantId = cp.ID
                                   }).ToList();

                return Json(openVendors.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectory Providers_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveProvider([FromBody] OperatingProviderProfile company)
        {
            CommunicationSRVViewModel model = new CommunicationSRVViewModel();
            //string pending = "1"; string expire = "1";
            try
            {

                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var companies = await commonBusiness.GetOperatingCompanies();
                var comp = companies.FirstOrDefault(cm => cm.ID == company.CompanyId);
                var companyId = "";
                if (comp != null)
                {
                    companyId = comp.TenantId;
                }
                if (companyId != "")
                {
                    company.CompanyId = companyId;
                    var pendingReview = (from ap in _servicedb.OperatingDirectoryAppovals
                                         where ap.Name == "Pending review"
                                         select new WellAI.Advisor.Model.ServiceCompany.Models.OperatingDirectoryApproval
                                         {
                                             Id = ap.Id
                                         }
                                    ).SingleOrDefault();
                    var inactiveStatus = (from st in _servicedb.OperatingDirectoryStatuses
                                          where st.Name == "Inactive"
                                          select new WellAI.Advisor.Model.ServiceCompany.Models.OperatingDirectoryStatus
                                          {
                                              Id = st.Id
                                          }
                                          ).SingleOrDefault();
                    company.ApprovalId = pendingReview.Id;
                    company.StatusId = inactiveStatus.Id;
                }
                int result = 0;
                if (company != null)
                {
                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await operrepo.UpdateProviderDirectory(company);
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
        }
        public ActionResult GetProviderDirectoriesByTenantId()
        {
            List<OperatingProviderProfile> result /*= new List<OperatingProviderProfile>()*/;
            try
            {
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                result = operrepo.GetProviderDirectoriesByTenantId(ti.Id).Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDirectoryController GetProviderDirectoryByTenantId", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return RedirectToAction(returnUrl);
            }
            return Json(result);
        }

        //Phase-II Changes
        public async Task<OperatingProviderProfile> GetOperatingProfile(string OperatorProfileId)
        {
            try
            {
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                var provider = await operrepo.GetOperatingDirectorByProfileId(OperatorProfileId);
                var msafiles = (List<ServiceMSA>)ViewData["msa"];
                var companies = (List<Model.OperatingCompany.Models.CorporateProfile>)ViewData["AllCompanies"];
                List<string> tenantIds = null;
                if (companies == null)
                {
                    companies = await commonBusiness.GetOperatingCompanies();
                    tenantIds = companies.Select(x => x.TenantId).ToList();
                    msafiles = await commonBusiness.GetMSAWellFilesFromOperatingTenants(tenantIds, ti.Id);
                }
                else
                    tenantIds = companies.Select(x => x.TenantId).ToList();

                //Phase II Changes - 05/19/2021
                var insurefiles = await commonBusiness.GetInsuranceFilesFromServiceTenants(tenantIds, ti.Id, provider.CompanyId);
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var cmpprofile = db.CorporateProfile.Where(x => x.TenantId.Equals(provider.CompanyId)).FirstOrDefault();
                var company = companies.FirstOrDefault(x => x.TenantId.ToString() == provider.CompanyId);//provider.CompanyId=tenantId of Service Company
                if (cmpprofile != null)
                {
                    var site = string.IsNullOrEmpty(cmpprofile.Website) || cmpprofile.Website.StartsWith("http:") ? cmpprofile.Website : "http://" + cmpprofile.Website;
                    provider.Name = cmpprofile.Name;
                    provider.CompanyId = cmpprofile.TenantId;
                    provider.Website = site;
                    provider.Phone = cmpprofile.Phone;
                    provider.User = await commonBusiness.GetPrimaryUserSRV(cmpprofile.TenantId);
                    provider.Location = string.Format("{0}{1},{2},{3},{4}", cmpprofile.Address1, string.IsNullOrEmpty(cmpprofile.Address2) ? "" : "," + cmpprofile.Address2,
                    cmpprofile.City, cmpprofile.State, cmpprofile.Zip);
                    provider.City = cmpprofile.City;
                    provider.State = cmpprofile.State;
                    provider.Zip = cmpprofile.Zip;
                    provider.Address1 = cmpprofile.Address1;
                    provider.Address2 = cmpprofile.Address2;
                }
                msafiles = await commonBusiness.GetMSAWellFilesFromOperatingTenants(new List<string> { provider.CompanyId }, ti.Id);
                provider.MSADocument = "";
                var msa = msafiles.FirstOrDefault(x => x.MsaId == provider.MSADocumentId);
                if (msa != null)
                {
                    provider.MSADocumentId = msa.MsaId;
                    provider.MSADocument = msa.Attachment;
                }
                var activeProjects = await auctionProposalBusiness.GetOperatingCompanyAuctionProjects(provider.CompanyId, ti.Id, true);
                var currentActivity = new List<ServiceCurrentActivity>();
                foreach (var activeProject in activeProjects)
                {
                    currentActivity.Add(new ServiceCurrentActivity
                    {
                        CurrentActivityId = activeProject.ID,
                        Title = activeProject.ProjectTitle
                    });
                }
                provider.CurrentActivity = currentActivity;
                var notactiveProjects = await auctionProposalBusiness.GetServiceCompanyAuctionProjects(provider.CompanyId, ti.Id, false);
                var upcomeActivity = new List<ServiceUpcomingActivity>();
                foreach (var notactiveProject in notactiveProjects)
                {
                    upcomeActivity.Add(new ServiceUpcomingActivity
                    {
                        UpcomingActivityId = notactiveProject.ID,
                        Title = notactiveProject.ProjectTitle
                    });
                }
                provider.UpcomingActivity = upcomeActivity;
                var MsaFls = msafiles.Where(x => x.Value == provider.CompanyId).ToList();
                provider.Msa = MsaFls;
                List<Model.ServiceCompany.Models.ProjectAuctionModel> auctionList = new List<Model.ServiceCompany.Models.ProjectAuctionModel>();
                //changed temproraryly to check operating directory load issue
                provider.Proposals = auctionList;// await auctionProposalBusiness.GetOperatingCompanyActualProposals(provider.CompanyId, ti.Id);
                                                 //}
                                                 //providerdir.Providers = providers;
                return provider;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "OperatingDirectoryController GetOperatingProfile", User.Identity.Name);
                return null;
            }

        }
        //Phase II - 05/19/2021
        public async Task<IActionResult> Insurance_Read([DataSourceRequest] DataSourceRequest request, string companyId)
        {
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var companies = await commonBusiness.GetServiceCompanies();
                var tenantIds = companies.Select(x => x.TenantId).ToList();
                var insurefiles = await commonBusiness.GetInsuranceFilesFromServiceTenants(tenantIds, ti.Id, companyId);
                var Ins = insurefiles.ToList();
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
                        blobSection["ContainerPrefixName"], ti.Id, path);
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
    }
}