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
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class ProductSubscriptionSRVController : BaseController
    {
        private readonly ILogger<ProductSubscriptionSRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private TenantServiceDbContext _servicedb;

        public ProductSubscriptionSRVController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, ILogger<ProductSubscriptionSRVController> logger, TenantServiceDbContext servicedb)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _servicedb = servicedb;
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
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                 var Provider = await GetProviderDirectory();
                 ViewData["Providers"] =Provider.Providers ;
                ViewBag.CustomerSubscriptions = (from sub in db.Subscription
                                                 join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
                                                 where sub.TenantId == tenantId && sub.IsPaid == true
                                                 select new CustomerSubscriptions
                                                 {
                                                     SubscriptionName = pcg.Name,
                                                     SubscriptionStart = sub.SubStartdate,
                                                     SubscriptionEnd = sub.SubEndDate,
                                                     SubscriptionTotalAmount = sub.PackageAmount,
                                                     SubscriptionUsersCount = sub.SubscriptionCount,
                                                     IsEnableSubscription = sub.IsEnable == true ? "Active" : "Deactive",
                                                     PackageOrder=pcg.PackageOrder
                                                 }
                                                 ).FirstOrDefault();
                var result = await db.SubscriptionPackage.Where(x => x.IsActive == true && x.AccountType == 1).OrderBy(x => x.PackageOrder).ToListAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Index", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
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
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "ProductSubscription",TenantId);
            }
            else
            {
                return false;
            }
        }

        public async Task<OperatingDirectoryModel> GetProviderDirectory(bool pendingFilter = false, bool insureExpireFilter = false)
        {
            try
            {
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var providers = await operrepo.GetSubsriptionOperators();
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
                    msafiles = await commonBusiness.GetMSAWellFilesFromOperatingTenants(tenantIds, tenantId);
                }
                else
                    tenantIds = companies.Select(x => x.TenantId).ToList();
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
                        provider.Address1 = cmpprofile.Address1;
                        provider.Address2 = cmpprofile.Address2;
                    }
                    provider.MSADocument = "";
                    var msa = msafiles.FirstOrDefault(x => x.MsaId == provider.MSADocumentId);
                    if (msa != null)
                    {
                        provider.MSADocumentId = msa.MsaId;
                        provider.MSADocument = msa.Attachment;
                    }
                }
                providerdir.Providers = providers;
                return providerdir;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV GetProviderDirectory", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> Operators_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var result = await GetProviderDirectory();
                return Json(result.Providers.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> Operators_Create([DataSourceRequest] DataSourceRequest request, OperatingProviderProfile input)
        {
            try
            {
                if (input != null && ModelState.IsValid)
                {
                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await operrepo.UpdateSubsriptionOperators(input);
                    res = await operrepo.UpdateProviderDirectory(input);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> Operators_Update([DataSourceRequest] DataSourceRequest request, OperatingProviderProfile input)
        {
            try
            {
                if (input != null && ModelState.IsValid)
                {
                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await operrepo.UpdateSubsriptionOperators(input);
                    res = await operrepo.UpdateProviderDirectory(input);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> Operators_Destroy(string companyId)
        {
            try
            {
                if (!string.IsNullOrEmpty(companyId))
                {
                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await operrepo.DeleteSubsriptionOperators(companyId);
                    var res2 = await operrepo.DeleteProviderDirectoryByCompanyId(companyId);
                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        /// <summary>
        /// Phase II Changes - 12/01/2021 - Read Subscribe Rigs for Add Rigs
        /// </summary>
        /// <param name="request"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public IActionResult ReadSubscibeOPerator_Rigs([DataSourceRequest] DataSourceRequest request,string TenantId)
        {
            try
            {
                List<RigViewModel> RigList = new List<RigViewModel>();

                RigList = (from rig in db.rig_register
                               where rig.TenantID == TenantId && rig.isActive == true
                               select new RigViewModel
                               {
                                   RigName = rig.Rig_Name,
                                   RigId = rig.Rig_id
                               }).OrderBy(x => x.RigName).ToList();

                return Json(RigList.ToDataSourceResult(request));

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV ReadRigs", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }

        }
        /// <summary>
        /// Save operator rigs // phase 2
        /// </summary>
        /// <param name="SelectedRigs"></param>
        /// <returns></returns>
        [AcceptVerbs("post")]
        public async Task<IActionResult> SaveSubscibeOPerator_Rigs([FromBody] List<SubscriptionOperatorRigs> SelectedRigs)
        {

            try
            {
                int result = 0;
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var OperatorsRigsCount = (from sub in db.Subscription
                                          join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
                                          where sub.TenantId == tenantId && sub.IsPaid == true
                                          select new CustomerSubscriptions
                                          {
                                              SubscriptionUsersCount = sub.SubscriptionCount,
                                          }).FirstOrDefault();
                var Rigs = new List<string>();
                for (var i = 0; i < SelectedRigs.Count; i++)
                {
                    var RigList = SelectedRigs[i].RigId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    Rigs.AddRange(RigList);
                }
                var ExitsRig = _servicedb.subscriptionOperatorRigs.Where(x => Rigs.Contains(x.RigId)).Count();
                var RigCount = Rigs.Count - ExitsRig;
                var GetSubscribeRigs = _servicedb.subscriptionOperatorRigs.Count();
                var TotlRigCounts = RigCount + GetSubscribeRigs;

                if (TotlRigCounts > OperatorsRigsCount.SubscriptionUsersCount)
                {
                    ModelState.AddModelError(string.Empty, "Your subscription does not allow adding more operator rigs. Upgrade your subscription if you need more rigs.");
                    return Json(new { success = true });
                }

                if (ModelState.IsValid)
                {
                    var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);

                    result =await operrepo.SaveSubsciberProviderRigs(SelectedRigs);

                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV SaveRigs", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);

            }

        }
        /// <summary>
        /// Phase  II Changes // 18/01/2021 - Update Subscribe Operator Rigs
        /// </summary>
        /// <param name="SelectedRigs"></param>
        /// <returns></returns>
        [AcceptVerbs("post")]
        public async Task<IActionResult> Update_SubscibeOPerator_Rigs([FromBody] List<SubscriptionOperatorRigs> SelectedRigs)
        {

            try
            {
                int result = 0;
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var OperatorsRigsCount = (from sub in db.Subscription
                                          join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
                                          where sub.TenantId == tenantId && sub.IsPaid == true
                                          select new CustomerSubscriptions
                                          {
                                              SubscriptionUsersCount = sub.SubscriptionCount,
                                          }).FirstOrDefault();
                var Rigs = new List<string>();
                for (var i = 0; i < SelectedRigs.Count; i++)
                {
                    var RigList = SelectedRigs[i].RigId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    Rigs.AddRange(RigList);
                }

                //Changed to validate against the selected Rigs Count and not against calculated count
                if(Rigs.Count > OperatorsRigsCount.SubscriptionUsersCount)
                {
                    ModelState.AddModelError(string.Empty, "Your subscription does not allow adding more operator rigs. Upgrade your subscription if you need more rigs.");
                    return Json(new { success = true});
                }

                if (ModelState.IsValid)
                {
                    operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);

                    var CompanyId = SelectedRigs[0].CompanyId;
                    //var RigId = SelectedRigs.RigId;
                    var getRigs =await operrepo.Remove_SubsciberProviderRigs(CompanyId);

                    result = await operrepo.SaveSubsciberProviderRigs(SelectedRigs);

                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Update_SubscibeOPerator_Rigs", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);

            }

        }

        /// <summary>
        /// Phase II Chnages - 18/01/2021 - Read Subcribe Operator Rigs For Edit OPerator Rigs
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public IActionResult Read_SubscribeOPerator_Rigs(string CompanyId)
        {
            try
            {
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var result = operrepo.Get_SubsciberProviderRigs(CompanyId);
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public IActionResult Providers_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var SrvTenantIds = (from crp in db.CorporateProfile
                                    join crmp in db.CrmCompanies on crp.TenantId equals crmp.TenantId
                                    select new
                                    {
                                        TenantId = crp.TenantId
                                    }
                               ).ToList();
                var ProTenantIds = db.CorporateProfile.Where(X => !SrvTenantIds.Select(Y => Y.TenantId).Contains(X.TenantId)).Select(x => x.TenantId).ToList();
                var corpProfiles = db.CorporateProfile.Where(x => ProTenantIds.Contains(x.TenantId)).ToList();
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                //var existingList = operrepo.GetSubsriptionOperators();
                //var existingId = existingList.Result.Select(e => e.CompanyId);
                var allVendors = corpProfiles.Select(e => e.TenantId);//.Except(existingId).ToList();
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
                                       State = db.USAStates.Where(x => x.StateId == Convert.ToInt32( cp.State)).Select(s => s.Name).FirstOrDefault()
                                   }).ToList();
                return Json(openVendors.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Providers_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveProvider([FromBody] OperatingProviderProfile company)
        {
            CommunicationSRVViewModel model = new CommunicationSRVViewModel();
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
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
                                         select new OperatingDirectoryApproval
                                         {
                                             Id = ap.Id
                                         }
                                    ).SingleOrDefault();
                    var inactiveStatus = (from st in _servicedb.OperatingDirectoryStatuses
                                          where st.Name == "Inactive"
                                          select new OperatingDirectoryStatus
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
                    var res = await operrepo.UpdateSubsriptionOperators(company);
                    res = await operrepo.UpdateProviderDirectory(company);
                    result = res;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV SaveProvider", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
    }
}