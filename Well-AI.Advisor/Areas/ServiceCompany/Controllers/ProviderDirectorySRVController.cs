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
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class ProviderDirectorySRVController : BaseController
    {
        private readonly ILogger<ProviderDirectorySRVController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        public ProviderDirectorySRVController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager, 
                                              RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, 
                                              ILogger<ProviderDirectorySRVController> logger)
            : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
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
                //checking invalid user//
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }                
                return View(GetProviderDirectory());
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDirectorySRV Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        //Phase II - 05/19/2021
        private async Task InitViewDataDicts()
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var companies = await commonBusiness.GetOperatingCompanies();
            var tenantIds = companies.Select(x => x.TenantId).ToList();
            ViewData["AllOperatingCompanies"] = companies;
            var compdat = new List<Model.OperatingCompany.Models.ProviderCompany>();
            foreach (var company in companies)
            {
                compdat.Add(new Model.OperatingCompany.Models.ProviderCompany
                {
                    Name = company.Name,
                    CompanyId = company.TenantId
                });
            }
            ViewData["OperatingCompanies"] = compdat;         
        }
        private bool GetComponentsBasedOnRole()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            string roleName = roles.FirstOrDefault().Value;
            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRoleByName(roleName);
            return rolePermissionBusiness.GetComponentBasedOnRole(roleResult.Id, "ViewDashboard");
        }
        public async Task<IActionResult> ProviderProfile(int id)
        {
            ProviderProfile profile = new ProviderProfile();
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                var dir = await GetProviderDirectory();
                profile = dir.Providers.FirstOrDefault(x => x.ProviderId == id);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ProviderDiretorySRV ProviderProfile", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
            return View(profile);
        }
        public async Task<ProviderDirectorySRVModel> GetProviderDirectory()
        {
            var providerdir = new ProviderDirectorySRVModel {
                InsExpiring90days = 1,
                Pending = 5,
                Records = 11
            };
            var providers = new List<ProviderProfile> { 
                new ProviderProfile{
                    ProviderId = 1, Name= "Petro Mechanical Services", Approval="Approved", Status="Active",PecStatus="green",Rating=5,
                    User=new UserViewSRVModel{
                        FirstName="Hannah",
                        LastName="Bair",
                        JobTitle="Head of Sales"
                    },
                    MSADocument = "PetroMS_MSA.pdf",Website="PetroMS.com",Phone="800.727.1398",Location="2451 N. FM 1936, Odessa, Texas 79763",
                    CurrentActivity = new List<CurrentActivity>{ 
                        new CurrentActivity { CurrentActivityId=1, Title= "Order Diesel" },
                        new CurrentActivity { CurrentActivityId=2, Title= "Order Production Casing" },
                    },
                    UpcomingActivity = new List<UpcomingActivity>{
                        new UpcomingActivity { UpcomingActivityId=1, Title= "Order Diesel" },
                        new UpcomingActivity { UpcomingActivityId=2, Title= "Order Production Casing" },
                    },
                    ServiceOffering = new List<ServiceOffering>{
                        new ServiceOffering { ServiceOfferId=1, Title= "Order Diesel" },
                        new ServiceOffering { ServiceOfferId=2, Title= "Order Production Casing" },
                        new ServiceOffering { ServiceOfferId=3, Title= "Casing Clean & Drift" },
                        new ServiceOffering { ServiceOfferId=4, Title= "Order Float Equipment" },
                    },
                    Msa = new List<MSA>{ new MSA{ MsaId=1, Expiration = new System.DateTime(2022, 1, 2), Status="Active", Attachment="XTO_PetroMS_MSA.pdf"} },
                    Insurance = new List<Insurance>{ new Insurance{ InsId=1, Expiration = new System.DateTime(2022, 1, 2), Status="Active", Attachment="PetroMS_Insurance.pdf"} },
                    Proposals = new List<ProjectAuctionModel>{
                        new ProjectAuctionModel{ AuctionID="1", Name="Order Float Equipment" , Status="Awarded", Attachment="PetroMS_CasingCrew.pdf",OpenDate=new System.DateTime(2020,2,18,11, 30, 0), Description = "Drilling Service Company", Location="Washington"},
                        new ProjectAuctionModel{ AuctionID="2", Name="Casing Crew", Status="Unread", Attachment="PetroMS_CasingCrew.pdf", Seller="EOG Nabors",OpenDate=new System.DateTime(2020,1,19,10, 30, 0), Description = "EOG Resources Company", Location="New York", CloseDate=new System.DateTime(2020,2,24,10, 30, 0)},
                        new ProjectAuctionModel{ AuctionID="3", Name="Cementing Service", Status="Pending", Attachment="PetroMS_CasingCrew.pdf", Seller="XTO ENSIGN",OpenDate=new System.DateTime(2020,2,21,1, 13, 30, 0), Description = "Drilling Service Company", Location="Washington"},
                    }
                },
                new ProviderProfile{
                    ProviderId = 2, Name= "Abbot Group", Approval="Pending Review", Status="Active",PecStatus="yellow",Rating=5,
                    User=new UserViewSRVModel{ 
                        FirstName="Hannah",
                        LastName="Bair",
                        JobTitle="Head of Sales"
                    },
                    MSADocument = "AbbotGroup_MSA.pdf",Website="AbbotGroup.com",Phone="800.727.1298",Location="2451 N. FM 1936, Odessa, Texas 79763",
                    CurrentActivity = new List<CurrentActivity>{
                        new CurrentActivity { CurrentActivityId=1, Title= "Order Diesel" },
                        new CurrentActivity { CurrentActivityId=2, Title= "Order Production Casing" },
                    },
                    UpcomingActivity = new List<UpcomingActivity>{
                        new UpcomingActivity { UpcomingActivityId=1, Title= "Order Diesel" },
                        new UpcomingActivity { UpcomingActivityId=2, Title= "Order Production Casing" },
                    },
                    ServiceOffering = new List<ServiceOffering>{
                        new ServiceOffering { ServiceOfferId=1, Title= "Order Diesel" },
                        new ServiceOffering { ServiceOfferId=2, Title= "Order Production Casing" },
                        new ServiceOffering { ServiceOfferId=3, Title= "Casing Clean & Drift" },
                        new ServiceOffering { ServiceOfferId=4, Title= "Order Float Equipment" },
                    },
                    Msa = new List<MSA>{ new MSA{ MsaId=1, Expiration = new System.DateTime(2022, 1, 2), Status="Active", Attachment="AbbotGroup_MSA.pdf"} },
                    Insurance = new List<Insurance>{ new Insurance{ InsId=1, Expiration = new System.DateTime(2022, 1, 2), Status="Active", Attachment="PetroMS_Insurance.pdf"} },
                },
                new ProviderProfile{
                    ProviderId = 3, Name= "Permian Services Group", Approval="Approved", Status="Active",PecStatus="green",Rating=5,
                    User=new UserViewSRVModel{
                        FirstName="Hannah",
                        LastName="Bair",
                        JobTitle="Head of Sales"
                    },
                    MSADocument = "PSG_MSA.pdf",Website="PermianServicesGroup.com",Phone="800.727.1398",Location="2451 N. FM 1936, Odessa, Texas 79763",
                    CurrentActivity = new List<CurrentActivity>{
                        new CurrentActivity { CurrentActivityId=1, Title= "Order Diesel" },
                        new CurrentActivity { CurrentActivityId=2, Title= "Order Production Casing" },
                    },
                    UpcomingActivity = new List<UpcomingActivity>{
                        new UpcomingActivity { UpcomingActivityId=1, Title= "Order Diesel" },
                        new UpcomingActivity { UpcomingActivityId=2, Title= "Order Production Casing" },
                    },
                    ServiceOffering = new List<ServiceOffering>{
                        new ServiceOffering { ServiceOfferId=1, Title= "Order Diesel" },
                        new ServiceOffering { ServiceOfferId=2, Title= "Order Production Casing" },
                        new ServiceOffering { ServiceOfferId=3, Title= "Casing Clean & Drift" },
                        new ServiceOffering { ServiceOfferId=4, Title= "Order Float Equipment" },
                    },
                    Msa = new List<MSA>{ new MSA{ MsaId=1, Expiration = new System.DateTime(2022, 1, 2), Status="Active", Attachment="PSG_MSA.pdf"} },
                    Insurance = new List<Insurance>{ new Insurance{ InsId=1, Expiration = new System.DateTime(2022, 1, 2), Status="Active", Attachment="PetroMS_Insurance.pdf"} },
                },
            };
            providerdir.Providers = providers;
            providerdir.PreferredProvider = providers[0];
            providerdir.SecondaryProvider = providers[1];
            return providerdir;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}