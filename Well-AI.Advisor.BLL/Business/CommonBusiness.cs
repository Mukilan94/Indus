using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Common;
using Finbuckle.MultiTenant;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.Model.Administration;
using ChecklistTemplateModel = WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.BLL.Business
{
    public class CommonBusiness : ICommonBusiness
    {
        private readonly WebAIAdvisorContext db;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public CommonBusiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, IConfiguration configuration = null)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> Create(IdentityRole role)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.Create(role);
        }

        public List<RoleViewModel> GetRoleList(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetRoleList(tenantId);
        }

        public List<IdentityRole> GetRoles(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetRoles(tenantId);
        }

        public async Task<IList<string>> GetUserRoleNames(WellIdentityUser resultUser)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUserRoleNames(resultUser);
        }

        public async Task<List<UserViewModel>> GetUserList(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUserList(tenantId);
        }

        public async Task<int> GetUserSubscriptionUserLeft(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUserSubscriptionUserLeft(tenantId);
        }

        public async Task<int> AddUserCountSubscription(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.AddUserCountSubscription(tenantId);
        }

        public async Task<int> RemoveUserCountSubscription(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.RemoveUserCountSubscription(tenantId);
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUser(id);
        }

        public async Task<UserViewModel> GetPrimaryUser(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetPrimaryUser(tenantId);
        }

        public async Task<WellIdentityUser> GetUserByEmail(string email)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUserByEmail(email);
        }

        //
        public async Task<RegisterStaffViewModel> GetAdminUserByEmail(string email)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetAdminUserByEmail(email);
        }

        public WellIdentityUser GetUserDetail(string id)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetUserDetail(id);
        }

        public async Task<IdentityResult> UpdateUser(WellIdentityUser resultUser)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateUser(resultUser);
        }

        public async Task<UserViewModel> RemoveUser(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);

            var resdisable = commonRepository.DisableUserBasicDetail(userId);

            var resuser = await GetUser(userId);
            resuser.IsActive = resdisable.IsActive;
            resuser.IsMaster = resdisable.IsMaster;
            return resuser;
        }

        public async Task<UserViewModel> EnableUserDetails(string userId)
        {
            try
            {
                ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
                var result = commonRepository.EnableUser(userId);
                var resuser = await GetUser(userId);
                resuser.IsActive = result.Result.IsActive;
                resuser.IsMaster = result.Result.IsMaster;
                return resuser;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<string> GetRoleName(WellIdentityUser resultUser)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetRoleName(resultUser);
        }

        public async Task<List<string>> GetRoleNames(WellIdentityUser resultUser)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var roles = await commonRepository.GetUserRoleNames(resultUser);

            return roles.ToList();
        }

        public async Task<IdentityResult> RemoveUserRole(WellIdentityUser resultUser, string resultRole)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.RemoveUserRole(resultUser, resultRole);
        }

        public async Task<bool> RemoveAllUserRoles(WellIdentityUser resultUser, IList<string> resultRoles)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            await commonRepository.RemoveAllUserRoles(resultUser, resultRoles);

            return true;
        }

        public async Task<IdentityResult> AddUserRole(WellIdentityUser resultUser, string resultRole)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.AddUserRole(resultUser, resultRole);
        }

        public async Task<IdentityResult> CreateUser(WellIdentityUser user, string password)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.CreateUser(user, password);
        }

        public bool CreateUserBasicDetail(CrmUserBasicDetail user)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.CreateUserBasicDetail(user);
        }

        public bool UpdateUserBasicDetail(CrmUserBasicDetail user)
        {          
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.UpdateUserBasicDetail(user);
        }

        public async Task<List<CorporateProfile>> GetServiceCompanies()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetServiceCompanies();
        }

        public async Task<List<CorporateProfile>> GetServiceCompaniesByCategories(string ServiceCategoryId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetServiceCompaniesByCategories(ServiceCategoryId);
        }

        public bool CreateCompanyDetail(CrmCompanies company)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.CreateCompanyDetail(company);
        }

        public bool UpdateCompanyCategories(string category, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.UpdateCompanyCategories(category, tenantId);
        }

        public async Task<bool> CreateTenantUser(string userId, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            TenantUsers user = new TenantUsers();
            user.UserId = userId;
            user.TenantId = tenantId;
            return await commonRepository.CreateTenantUser(user);
        }

        public async Task<string> GetTenantIdByUserId(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetTenantUser(userId);
        }

        public async Task<List<string>> GetTenantIdsByUserIds(List<string> userIds)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetTenantUserIds(userIds);
        }

        public List<WellTenantInfo> GetWellTenants()
        {
            var wellrepo = new WellTenantRepository(db);
            return wellrepo.GetAllTenants();
        }

        public bool CreateWellTenant(string tenantId, string connectionString, string name, string identifier, string id)
        {
            var wellrepo = new WellTenantRepository(db);
            return wellrepo.CreateTenant(tenantId, connectionString, name, identifier, id);
        }

        public async Task<bool> CreateTenantRoles(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await Task.FromResult(true);
        }

        public async Task<bool> CreateTenantRole(string roleId, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            TenantRoles role = new TenantRoles();
            role.RoleId = roleId;
            role.TenantId = tenantId;
            return await commonRepository.CreateTenantRole(role);
        }

        public CrmUserBasicDetail GetUserBasicDetail(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetUserBasicDetail(userId);
        }

        public async Task<CrmUserBasicDetail> GetMasterUserByTenantId(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetMasterUserByTenantId(tenantId);
        }

        public CrmCompanies GetCompanyDetail(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetCompanyDetail(userId);
        }

        public CrmCompanies GetCompanyDetailByTenant(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetCompanyDetailByTenant(tenantId);
        }

        public string GetStateRegion(int stateId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetStateRegion(stateId);
        }

        public bool UpdateUserSubscription(string subscriptionId, string userId, int noOfItems)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.UpdateUserSubscription(subscriptionId, userId, noOfItems);
        }

        public bool UpdateUserPagesCompleteStatus(int status, string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.UpdateUserPagesCompleteStatus(status, userId);
        }

        public bool UpdateUserPaymentStatus(int status, string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.UpdateUserPaymentStatus(status, userId);
        }

        public List<RoleViewSRVModel> GetRoleSRVList(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetRoleSRVList(tenantId);
        }

        public async Task<List<UserViewSRVModel>> GetUserSRVList(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUserSRVList(tenantId);
        }

        public async Task<UserViewSRVModel> GetUserSRV(string id)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUserSRV(id);
        }

        public async Task<List<USAState>> GetUSAStates()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUSAStates();
        }

        public async Task<List<Category>> GetCategories()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetCategories();
        }

        public async Task<UserViewSRVModel> RemoveSRVUser(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);

            var resdisable = commonRepository.DisableUserBasicDetail(userId);

            var resuser = await GetUserSRV(userId);
            resuser.IsActive = resdisable.IsActive;
            resuser.IsMaster = resdisable.IsMaster;
            return resuser;
        }

        public async Task<UserViewSRVModel> EnableUserDetailsSRV(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);

            var resdisable = commonRepository.EnableUser(userId);

            var resuser = await GetUserSRV(userId);
            resuser.IsActive = resdisable.Result.IsActive;
            resuser.IsMaster = resdisable.Result.IsMaster;
            return resuser;
        }

        public async Task<bool> CreateWellFile(WellFile file)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.CreateWellFile(file);
        }

        public async Task<string> RemoveWellFileByName(string path, string fileName, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.RemoveWellFileByName(path, fileName, tenantId);
        }

        public async Task<List<string>> RemoveWellFilesByName(List<KeyValuePair<string, string>> fileNamePaths, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.RemoveWellFilesByName(fileNamePaths, tenantId);
        }

        public async Task<string> RemoveWellFile(string fileId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.RemoveWellFile(fileId);
        }

        public async Task<string> RemoveWellFiles(List<string> fileIds)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.RemoveWellFiles(fileIds);
        }

        public async Task<WellFile> GetWellFileById(string fileId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetWellFileById(fileId);
        }

        public async Task<List<Model.OperatingCompany.Models.MSA>> GetMSAWellFilesFromServiceTenants(List<string> tenantIds, string operTenantId, string wellId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var alldocs = await commonRepository.GetMSAWellFilesFromTenants(tenantIds, operTenantId, wellId);

            return alldocs;
        }

        //Phase II Changes
        //MSA Permission
        public async Task<List<Model.OperatingCompany.Models.MSA>> GetMSAWellFilesFromServiceTenantsForUser(List<string> tenantIds, string operTenantId, string wellId, string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var alldocs = await commonRepository.GetMSAWellFilesFromTenantsForUser(tenantIds, operTenantId, wellId, userId);

            return alldocs;
        }

        public async Task<bool> UpdateProviderMSALinkWellFile(string operTenantId, string fileId, string vendorId, DateTime expire, bool isActive)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var success = await commonRepository.UpdateProviderMSALinkWellFile(operTenantId, fileId, vendorId, expire, isActive);

            return success;
        }

        public async Task<bool> UpdateProviderMSALinkWellFileServiceEdit(string fileId, string tenantId, DateTime expire, bool isActive)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var success = await commonRepository.UpdateProviderMSALinkWellFileServiceEdit(fileId, tenantId, expire, isActive);

            return success;
        }

        public async Task<bool> UpdateProviderMSALinkWellFileService(string operTenantId, string fileId, string vendorTenantId, DateTime expire, bool isActive)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var success = await commonRepository.UpdateProviderMSALinkWellFileService(operTenantId, fileId, vendorTenantId, expire, isActive);

            return success;
        }

        public async Task<List<Model.OperatingCompany.Models.Insurance>> GetInsuranceWellFilesFromServiceTenants(List<string> tenantIds, string operTenantId, string wellId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var alldocs = await commonRepository.GetInsuranceWellFilesFromServiceTenants(tenantIds, operTenantId, wellId);

            return alldocs;
        }

        //Phase II Changes - 05/19/2021
        public async Task<List<Model.ServiceCompany.Models.ServiceInsurance>> GetInsuranceFilesFromServiceTenants(List<string> tenantIds, string serviceTenantId, string operTenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var alldocs = await commonRepository.GetInsuranceFilesFromServiceTenants(tenantIds, serviceTenantId, operTenantId);

            return alldocs;
        }

        public async Task<List<WellFileFolder>> GetWellFileFolders()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetWellFileFolders();
        }

        public async Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetWellFilesFromTenant(string tenantId, string wellCategory)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetWellFilesFromTenant(tenantId, wellCategory);
        }

        public async Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetWellFilesFromTenantAndWell(string tenantId, string wellId, string wellCategory)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetWellFilesFromTenantAndWell(tenantId, wellId, wellCategory);
        }

        public async Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetVendorFilesFromTenant(string tenantId, string fileCategory)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetVendorFilesFromTenant(tenantId, fileCategory);
        }

        public async Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetVendorFilesFromServiceTenant(string tenantId, string fileCategory)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetVendorFilesFromServiceTenant(tenantId, fileCategory);
        }

        public async Task<CorporateProfile> GetCorporateProfile()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetCorporateProfile();
        }

        public async Task<CorporateProfile> GetCorporateProfileByTenant(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetCorporateProfileByTenant(tenantId);
        }

        public async Task<TenantConfiguration> GetApiConfigurationByTenant(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetApiConfigurationByTenant(tenantId);
        }

        public async Task<int> UpdateCorporateProfile(CorporateProfile input, string userId, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateCorporateProfile(input, userId, tenantId);
        }

        public async Task<int> UpdateApiConfigurationByTenant(string value, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateApiConfigurationByTenant(value, tenantId);
        }

        public List<FieldTicketSRV> GetProjectInvoice(string ProjectID)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetProjectInvoice(ProjectID);
        }

        public async Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>> GetServiceOfferings(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetServiceOfferings(tenantId);
        }

        public async Task<List<ProjectViewSRVModel>> GetUpCommingProjects(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUpCommingProjects(tenantId);
        }

        public async Task<List<ProjectViewModel>> GetUpCommingProjectsForOperator(WellIdentityUser user, string RigId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetUpCommingProjectsForOperator(user, RigId);
        }

        public async Task<int> UpdatedUpCommingProjectsDetails(ProjectViewSRVModel input, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdatedUpCommingProjectsDetails(input, tenantId);
        }

        public async Task<List<ProjectViewSRVModel>> GetTechnicianName(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetTechnicianName(tenantId);
        }

        public async Task<ServiceCorporateProfile> GetServiceCorporateProfile()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetServiceCorporateProfile();
        }

        public async Task<ServiceCorporateProfile> GetServiceCorporateProfileByTenant(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetServiceCorporateProfileByTenant(tenantId);
        }

        public async Task<int> UpdateServiceCorporateProfile(ServiceCorporateProfile input, string userId, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateServiceCorporateProfile(input, userId, tenantId);
        }
        public async Task<int> UpdateCustomerCorporateProfile(CorporateProfileAdmin input, string userId, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateCustomerCorporateProfile(input, userId, tenantId);
        }
        public async Task<List<Model.ServiceCompany.Models.ServiceMSA>> GetMSAWellFilesFromOperatingTenants(List<string> tenantIds, string ServiceTenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var alldocs = await commonRepository.GetMSAWellFilesFromOperatingTenants(tenantIds, ServiceTenantId);
            return alldocs;
        }

        public async Task<List<Model.ServiceCompany.Models.ServiceInsurance>> GetInsuranceWellFilesFromOperatingTenants(List<string> tenantIds, string ServiceTenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var alldocs = await commonRepository.GetInsuranceWellFilesFromOperatingTenants(tenantIds, ServiceTenantId);
            return alldocs;
        }

        public async Task<UserViewSRVModel> GetPrimaryUserSRV(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetPrimaryUserSRV(tenantId);
        }

        public async Task<List<Model.ServiceCompany.Models.ServiceOfferingSRV>> GetOperatingOfferings(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetOperatingOfferings(tenantId);
        }

        public async Task<List<Model.OperatingCompany.Models.ServiceOffering>> GetOperatingCompanyServices(string TenantID)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetOperatingCompanyServices(TenantID);
        }

        public async Task<List<CorporateProfile>> GetOperatingCompanies()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetOperatingCompanies();
        }

        public int GetNoOfItemsSubscribe(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return Convert.ToInt32(commonRepository.GetUserBasicDetail(userId).NoOfItems);
        }

        public async Task<List<MessageQueue>> GetNotifications(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetNotifications(userId);
        }

        public async Task<int> AddNotifications(MessageQueue messageQueue)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await Task.FromResult(commonRepository.AddNotifications(messageQueue));
        }

        public async Task<bool> UpdateNotifications(string toId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await Task.FromResult(commonRepository.UpdateNotifications(toId));
        }

        public async Task<bool> NotificationExists(string entityId, string toId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await Task.FromResult(commonRepository.NotificationExists(entityId, toId));
        }

        public bool GetUserAvailibilityStatus(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetUserAvailibilityStatus(userId);
        }

        public void UpdateUserStatus(string userId, bool status)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            commonRepository.UpdateUserStatus(userId, status);
        }

        //public List<MessageQueue> GetUserNotificationDetails(string userId)
        //{
        //    ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
        //    return commonRepository.GetUserNotificationDetails(userId);
        //}

       

        public List<MessageQueue> GetUserNotificationDetailsScroll(string userId, int skipCount, int takeCount)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetUserNotificationDetailsScroll(userId, skipCount, takeCount);
        }


        public void UpdateNotificationStatus(int messageQueueId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            commonRepository.UpdateNotificationStatus(messageQueueId);
        }

        public async Task<bool> CheckMSAExistFromProviderTenant(string tenantId, string userId)
        {
            var result = false;

            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var detail = commonRepository.GetUserBasicDetail(userId);

            var dbprefix = "oper";
            if(detail != null)
            {
                if (detail.AccountType == 1)
                    dbprefix = "serv";
            }
           

            var tId = Guid.Parse(tenantId);

            var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                   dbprefix + "db_" + tId.ToString("N"));

            var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);

            var operContext = new TenantOperatingDbContext(ti);
            var provider = operContext.ProvidersDirectory.FirstOrDefault(x => x.TenantId == tenantId);

            if (provider != null)
            {
                if (!string.IsNullOrEmpty(provider.MSA))
                    result = true;
            }

            return await Task.FromResult(result);
        }

        public async Task<bool> IsUniqueWorkStation(string workStationId)
        {
            var isUnique = false;
            var customer = db.WorkstationRegister.SingleOrDefault(x => x.WorkstationIdentifier == workStationId && x.IsActive == true);
            if (customer == null)
            {
                isUnique = true;
            }
            return await Task.FromResult(isUnique);
        }

        public List<CrmCompanies> GetCategorywiseCompanyDetail(string categoryId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetCategorywiseCompanyDetail(categoryId);
        }

        /// <summary>
        /// Phase II Changes - 01/19/2021 - UpdateMSAApprovalStatus
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task<int> UpdateMSAApprovalStatus(string fileId, bool status, string userid, ServiceTenantRepository servRepo)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.UpdateMSAApprovalStatus(fileId, status, userid, servRepo);
        }

        /// <summary>
        /// Phase II Changes - 01/19/2021
        /// </summary>
        /// <param name="tenantIds"></param>
        /// <param name="operTenantId"></param>
        /// <param name="wellId"></param>
        /// <returns></returns>
        public async Task<List<Model.OperatingCompany.Models.MSA>> GetApprovedMSAWellFilesOfServiceTenant(List<string> tenantIds, string operTenantId, string wellId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            var alldocs = await commonRepository.GetApprovedMSAWellFilesOfServiceTenant(tenantIds, operTenantId, wellId);

            return alldocs;
        }

        /// <summary>
        ///  Phase II Changes - 02/08/2021
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<int> DeactivateFile(string fileId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.DeactivateFile(fileId);
        }

        public async Task<int> CreateDepthPermission(RigsDepth_Permission DepthValue)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.CreateDepthPermission(DepthValue);
        }

        /// <summary>
        /// Phase II Changes - Update Vendor Preferred Status
        /// </summary>
        /// <param name="servTenantId"></param>
        /// <param name="userid"></param>
        /// <param name="servRepo"></param>
        /// <returns></returns>
        public int UpdateVendorPreferredStatus(string operTenantId, string servTenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.UpdateVendorPreferredStatus(operTenantId, servTenantId);
        }

        /// <summary>
        /// Phase II Changes - Get Product Subscription
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<ProductSubscriptionModel> GetProductSubscription(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetProductSubscription(tenantId);
        }

        //Phase II Changes - 03/16/2021
        public async Task<int> SaveUsersession(UserSessions user)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.SaveUsersession(user);
        }
        public async Task<int> UpdateUsersession(UserSessions user, string email)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateUsersession(user, email);
        }
        //Phase II Changes - 03/16/2021
        public async Task<int> DeleteUsersession(string Email)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.DeleteUsersession(Email);
        }

        //Phase II Changes - 05/19/2021
        //Phase II Changes - 05/19/2021
        public async Task<int> UpdateProviderInsuranceLink(string serviceTenantId, string operTenantId, string fileId, DateTime? expire)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateProviderInsuranceLink(serviceTenantId, operTenantId, fileId, expire);
        }

        //Changed to async Task<int> from Task<int> CreateWellChecklist
        public async Task<int> CreateWellChecklist(string TenantId, string WellId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.CreateWellChecklist(TenantId, WellId);
        }

        //DWOP
        public async Task<List<Model.OperatingCompany.Models.ChecklistTemplateModel>> ReadChecklistTemplateList(string operTenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.ReadChecklistTemplateList(operTenantId);
        }

        public async Task<List<ChecklistTaskTemplateModel>> GetChecklistTemplate(string CheckListId, string TenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetChecklistTemplate(CheckListId,TenantId);
        }

        public async Task<List<TaskModel>> GetTasks()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetTasks();
        }
        //DWOP
        public async Task<List<ChecklistTaskTemplateModel>> ReadChecklistTemplate(string WellType)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.ReadChecklistTemplate(WellType);
        }


        public async Task<List<ChecklistTaskTemplateModel>> ChecklistTemplateFordrillplan(string welltype,string wellid)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.ChecklistTemplateFordrillplan(welltype, wellid);
        }

        public async Task<int> ChangeChecklistDefaultForTenant(string templatId,string tenantId,string wellTypeId,bool IsDefault)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.ChangeChecklistDefaultForTenant(templatId, tenantId,wellTypeId, IsDefault);
        }


        public async Task<string> SaveChecklistTemplate(ChecklistTemplate ChecklistTemplate, string TenantId,string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.SaveChecklistTemplate(ChecklistTemplate, TenantId, userId);
        }

        //DWOP
        public async Task<List<ServiceDuration>> GetServiceHours()
        {
                List<ServiceDuration> Hours = new List<ServiceDuration>();

                for (var i = 0; i < 23; i++)
                {
                    var value = Convert.ToString(i + 1);
                    if (value.Length < 2)
                    {
                        value = "0" + i;
                    }
                    var result = new ServiceDuration
                    {
                        Text = Convert.ToString(value),
                        Value = Convert.ToString(value)
                    };

                    Hours.Add(result);
                }

                return Hours;          
        }
        //DWOP
        public async Task<List<ServiceDuration>> GetServiceMinutes()
        {
                List<ServiceDuration> Minutes = new List<ServiceDuration>();

                for (var i = 0; i < 45; i++)
                {
                    var value = "";
                    var Text = "";

                    if (i != 0)
                    {
                        Text = Convert.ToString(15 + i - 1);
                        i = Convert.ToInt16(Text);
                        value = Text == "15" ? "15" : Text == "30" ? "30" : "45";
                    }
                    if (Text.Length < 2)
                    {
                        Text = "0" + i;
                        value = "00";
                    }
                    var result = new ServiceDuration
                    {
                        Text = Convert.ToString(Text),
                        Value = Convert.ToString(value)
                    };

                    Minutes.Add(result);
                }

                return Minutes;         
        }

        public List<ServiceStageModel> GetServiceStage()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetServiceStage();
        }

        public List<Model.Administration.ServiceCategoryModel> GetCategoriesList()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetCategoriesList();
        }

        public List<TaskModel> GetTaskDependencyList(string taskId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetTaskDependencyList(taskId);
        }

        public Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel>> GetChecklistTemplateList(string wellDesign, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetChecklistTemplateList(wellDesign, tenantId);
        }

        public Task<List<WellMasterDataViewModel>> GetWellRegister(string tenantId, string rigId, bool checkWellFilter)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetWellRegister(tenantId, rigId, checkWellFilter);
        }

        public Task<int> DeleteChecklistTemplate(string templateId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.DeleteChecklistTemplate(templateId);
        }

        public Task<float> CalculateHours(int days, int hours, int minutes)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.CalculateHours(days, hours, minutes);
        }

        public Task<DrillPlanWellViewModel> DrillingPlanTabContent(string wellid, string DrillingPlanId, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.DrillingPlanTabContent(wellid, DrillingPlanId, tenantId);
        }
        public Task<int> SaveDrillplanHeader(IFormCollection form, DrillPlandetailsViewModel Input, string TenantId, string UserId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.SaveDrillplanHeader(form, Input, TenantId, UserId);
        }
        public Task<int> SaveUpdatePlandetails(PlanDetailsModel PlanDetails, string Tenantid)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.SaveUpdatePlandetails(PlanDetails, Tenantid);
        }

        public Task<List<PlannedTasksModel>> GetPlanDetailsTasks(string wellId, string drillPlanId, string TenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetPlanDetailsTasks(wellId, drillPlanId, TenantId);
        }

        public Task<List<wellmodel>> GetDrillPlanWells(string drillPlanId, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetDrillPlanWells(drillPlanId, tenantId);
        }

        public Task<List<DrillPlanModel>> GetDrillPlanList(string tenantId, string rigId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetDrillPlanList(tenantId, rigId);
        }

        public Task<int> DeleteDrillPlan(string planId, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.DeleteDrillPlan(planId, tenantId);
        }
      
        public Task<List<TaskModel>> GetTasksList()
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetTasksList();
        }

        public Task<int> ImportTaskForDrillingPlan(string wellId, string drillPlanId, string TenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.ImportTaskForDrillingPlan(wellId, drillPlanId, TenantId);
        }
        public bool GetComponentPermission(string componentName, string userId, string operTenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetComponentPermission(componentName, userId, operTenantId);
        }
        public Task<int> GetUserNotificationCount(string userId, string notificationType)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetUserNotificationCount(userId, notificationType);
        }
        //Dispatch Routes
        public Task<WellIdentityUser> GetUserById(string UserId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetUserById(UserId);
        }

        public Task<List<DispatchRoutesModel>> GetDispatchRoutes(string UserId, bool isForApi)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetDispatchRoutes(UserId, isForApi);
        }
        public Task<List<DispatchRoutesModel>> GetDispatchRoutes_Preview(string UserId, bool isForApi)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetDispatchRoutes_Preview(UserId, isForApi);
        }
        public Task<List<DispatchRoutesModel>> GetDispatchRoutes_V2(string userId, bool isForApi, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetDispatchRoutes_V2(userId, isForApi, tenantId);
        }

     
        //dispatch save
        public bool CreateDispatchRoute(DispatchRoutes dispatch)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.CreateDispatchRoute(dispatch);
        }

        public async Task<List<DispatchRoutesHistoryDetailsModel>> CreateDispatchRoute_V2(List<DispatchRoutes> dispatch, List<DispatchRoutesHistoryDetailsModel> dispatch2)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.CreateDispatchRoute_V2(dispatch, dispatch2);
        }
        public async Task<bool> UpdateRouterStatus(DispatchRoutes dispatch)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateRouterStatus(dispatch);
        }

        public async Task<bool> UpdateOperatorId(List<DispatchRoutes> dispatch)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateOperatorId(dispatch);
        }
        public async Task<List<DispatchRoutesHistoryDetailsModel>> DeleteDispatchRoute_V2(List<DispatchRoutes> dispatchRoutes, List<DispatchRoutesHistoryDetailsModel>  routesList2)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.DeleteDispatchRoute_V2(dispatchRoutes, routesList2);
        }

        
        public List<WellIdentityUser> GetComponentPermissionUsers(string componentName, string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetComponentPermissionUsers(componentName, tenantId);

        }
        public bool UpdateDispatch(DispatchRoutes dispatch)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.UpdateDispatch(dispatch);
        }

        public Task<DispatchNotification> GetActiveDispatchRoutes(string userId, string dispatchNotes)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetActiveDispatchRoutes(userId, dispatchNotes);
        }

        public Task<DispatchNotification> GetActiveDispatchRoutes_V2(string userId, string dispatchNotes)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetActiveDispatchRoutes_V2(userId, dispatchNotes);
        }
        public string GetActiveUserNotes(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetActiveUserNotes(userId);
        }
        public Task<List<DispatchRoutesModel>> GetDispatchRoutesForOperator(List<AuctionProposalViewModel> auctionActiveList, string operatorId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetDispatchRoutesForOperator(auctionActiveList, operatorId);
        }

        public Task<List<DispatchRoutesModel>> GetOperatorsharedetails(List<AuctionProposalViewModel> auctionActiveList, string operatorId,string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetOperatorsharedetails(auctionActiveList, operatorId, userId);
        }
        

        public async Task<bool> UpdateDispatchRouteOrders(DispatchRouteOrderModel dispatch)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.UpdateDispatchRouteOrders(dispatch);
        }
        public async Task<IdentityResult> EditProfilePasswordChange(WellIdentityUser resultUser)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.EditProfilePasswordChange(resultUser);
        }

        public async Task<CorporateProfile> GetCorporateProfileByUserId(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.GetCorporateProfileByUserId(userId);
        }
        public async Task<bool> CreateDispatchRoutesHistory(DispatchRoutesHistoryModel destinationChanges)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.CreateDispatchRoutesHistory(destinationChanges);
        }
        public async Task<bool> CreateDispatchRoutesHistory_Deleted(DispatchRoutesHistoryModel destinationChanges)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return await commonRepository.CreateDispatchRoutesHistory_Deleted(destinationChanges);
        }

        public List<MessageQueue> GetUserNotificationDetails(string userId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetUserNotificationDetails(userId);
        }
               
        int ICommonBusiness.GetStateId(string companyState)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetStateId(companyState);
        }

        public string GetCompanyName(string tenantId)
        {
            ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
            return commonRepository.GetCompanyName(tenantId);
        }

        //public async Task<List<UserViewSRVModel>> GetUserSRVListByProfileId(string profileId)
        //{
        //    ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
        //    return await commonRepository.GetUserSRVListByProfileId(profileId);
        //}
        //public List<IdentityRole> GetRolesByProfileId(string profileId)
        //{
        //    ICommonRepository commonRepository = new CommonRepository(db, _roleManager, _userManager);
        //    return  commonRepository.GetRolesByProfileId(profileId);
        //}
    }
}