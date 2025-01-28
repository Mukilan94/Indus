using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;


namespace WellAI.Advisor.BLL.Business
{
    public interface ICommonBusiness
    {
        Task<IdentityResult> Create(IdentityRole role);
        List<RoleViewModel> GetRoleList(string tenantId);
        List<RoleViewSRVModel> GetRoleSRVList(string tenantId);
        Task<List<UserViewModel>> GetUserList(string tenantId);
        Task<List<UserViewSRVModel>> GetUserSRVList(string tenantId);
        List<IdentityRole> GetRoles(string tenantId);
        Task<IList<string>> GetUserRoleNames(WellIdentityUser resultUser);
        Task<UserViewModel> GetUser(string id);
        Task<int> GetUserSubscriptionUserLeft(string tenantId);
        Task<int> AddUserCountSubscription(string tenantId);
        Task<int> RemoveUserCountSubscription(string tenantId);

        string GetCompanyName(string tenantId);
        Task<UserViewModel> GetPrimaryUser(string tenantId);
        Task<UserViewSRVModel> GetPrimaryUserSRV(string tenantId);

        Task<UserViewSRVModel> GetUserSRV(string id);
        WellIdentityUser GetUserDetail(string id);
        Task<IdentityResult> UpdateUser(WellIdentityUser resultUser);
        Task<UserViewModel> RemoveUser(string userId);
        Task<UserViewSRVModel> RemoveSRVUser(string userId);
        Task<string> GetRoleName(WellIdentityUser resultUser);
        Task<List<string>> GetRoleNames(WellIdentityUser resultUser);
        Task<IdentityResult> RemoveUserRole(WellIdentityUser resultUser, string resultRole);
        Task<bool> RemoveAllUserRoles(WellIdentityUser resultUser, IList<string> resultRoles);
        Task<IdentityResult> AddUserRole(WellIdentityUser resultUser, string resultRole);
        Task<IdentityResult> CreateUser(WellIdentityUser user, string password);
        Task<bool> CreateTenantRole(string roleId, string tenantId);
        Task<bool> CreateTenantUser(string userId, string tenantId);
        Task<string> GetTenantIdByUserId(string userId);
        Task<List<string>> GetTenantIdsByUserIds(List<string> userIds);
        List<WellTenantInfo> GetWellTenants();
        bool CreateWellTenant(string tenantId, string connectionString, string name, string identifier, string id);
        Task<bool> CreateTenantRoles(string tenantId);
        bool CreateUserBasicDetail(CrmUserBasicDetail user);
        Task<List<CorporateProfile>> GetServiceCompanies();
        Task<List<CorporateProfile>> GetServiceCompaniesByCategories(string ServiceCategoryId);
        Task<List<CorporateProfile>> GetOperatingCompanies();
        bool CreateCompanyDetail(CrmCompanies company);
        CrmUserBasicDetail GetUserBasicDetail(string userId);
        bool UpdateUserSubscription(string subscriptionId, string userId, int noOfItems);
        bool UpdateUserPagesCompleteStatus(int status, string userId);
        CrmCompanies GetCompanyDetail(string userId);
        CrmCompanies GetCompanyDetailByTenant(string tenantId);
        bool UpdateUserPaymentStatus(int status, string userId);

        Task<WellIdentityUser> GetUserByEmail(string email);
               Task<RegisterStaffViewModel> GetAdminUserByEmail (string email);
        bool UpdateUserBasicDetail(CrmUserBasicDetail user);
        Task<List<USAState>> GetUSAStates();
        Task<List<Category>> GetCategories();
        Task<bool> CreateWellFile(WellFile file);
        Task<string> RemoveWellFile(string fileId);
        Task<string> RemoveWellFiles(List<string> fileIds);
        Task<string> RemoveWellFileByName(string path, string fileName, string tenantId);
        Task<List<string>> RemoveWellFilesByName(List<KeyValuePair<string, string>> fileNamePaths, string tenantId);
        Task<WellFile> GetWellFileById(string fileId);
        Task<List<Model.OperatingCompany.Models.MSA>> GetMSAWellFilesFromServiceTenants(List<string> tenantIds, string operTenantId, string wellId);
        //MSA User Permission
        Task<List<Model.OperatingCompany.Models.MSA>> GetMSAWellFilesFromServiceTenantsForUser(List<string> tenantIds, string operTenantId, string wellId,string userId);
        Task<List<Model.OperatingCompany.Models.Insurance>> GetInsuranceWellFilesFromServiceTenants(List<string> tenantIds, string operTenantId, string wellId);
        Task<List<WellFileFolder>> GetWellFileFolders();
        Task<List<Model.ServiceCompany.Models.ServiceMSA>> GetMSAWellFilesFromOperatingTenants(List<string> tenantIds, string operTenantId);
        //Phase II Changes - 05/19/2021
        Task<List<Model.ServiceCompany.Models.ServiceInsurance>> GetInsuranceFilesFromServiceTenants(List<string> tenantIds, string serviceTenantId,string operTenantId);

        Task<CrmUserBasicDetail> GetMasterUserByTenantId(string tenantId);
        Task<CorporateProfile> GetCorporateProfile();
        Task<CorporateProfile> GetCorporateProfileByTenant(string tenantId);
        Task<int> UpdateCorporateProfile(CorporateProfile input, string userId, string tenantId);
        Task<int> UpdateApiConfigurationByTenant(string value, string tenantId);
        List<FieldTicketSRV> GetProjectInvoice(string ProjectID);
        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>> GetServiceOfferings(string tenantId);
        Task<List<WellAI.Advisor.Model.ServiceCompany.Models.ServiceOfferingSRV>> GetOperatingOfferings(string tenantId);
        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>> GetOperatingCompanyServices(string tenantID);

        Task<int> UpdatedUpCommingProjectsDetails(ProjectViewSRVModel input, string tenantId);
        Task<List<ProjectViewSRVModel>> GetTechnicianName(string tenantId);
        Task<List<ProjectViewModel>> GetUpCommingProjectsForOperator(WellIdentityUser user, string wellId);
        Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetWellFilesFromTenant(string tenantId, string wellCategory);
        Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetWellFilesFromTenantAndWell(string tenantId, string wellId, string wellCategory);

        Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetVendorFilesFromTenant(string tenantId, string fileCategory);
        Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetVendorFilesFromServiceTenant(string tenantId, string fileCategory);
        int GetNoOfItemsSubscribe(string userId);
        Task<List<MessageQueue>> GetNotifications(string userId);
        Task<int> AddNotifications(MessageQueue messageQueue);
        Task<bool> UpdateNotifications(string toId);
        Task<bool> NotificationExists(string entityId,string toId);
        bool GetUserAvailibilityStatus(string userId);
        void UpdateUserStatus(string userId, bool status);
        List<MessageQueue> GetUserNotificationDetails(string userId);
        List<MessageQueue> GetUserNotificationDetailsScroll(string userId, int skipCount, int takeCount);

        void UpdateNotificationStatus(int messageQueueId);
        Task<bool> CheckMSAExistFromProviderTenant(string tenantId, string userId);
        Task<bool> UpdateProviderMSALinkWellFile(string operTenantId, string fileId, string vendorId, DateTime expire, bool isActive);
        Task<bool> UpdateProviderMSALinkWellFileService(string operTenantId, string fileId, string vendorTenantId, DateTime expire, bool isActive);
        Task<bool> UpdateProviderMSALinkWellFileServiceEdit(string fileId, string tenantId, DateTime expire, bool isActive);
        Task<bool> IsUniqueWorkStation(string workstationId);
        string GetStateRegion(int stateId);
        List<CrmCompanies> GetCategorywiseCompanyDetail(string categoryId);
        //Phase II Changes - 01/19/2021 - UpdateMSAApprovalStatus
        Task<int> UpdateMSAApprovalStatus(string fileId, bool status, string userid, ServiceTenantRepository servRepo);
        //Phase II Changes - 01/19/2021 - GetApprovedMSA
        Task<List<Model.OperatingCompany.Models.MSA>> GetApprovedMSAWellFilesOfServiceTenant(List<string> tenantIds, string operTenantId, string wellId);
        //Phase II Changes - 02/08/2021
        public Task<int> DeactivateFile(string fileId);

        public Task<int> CreateDepthPermission(RigsDepth_Permission input);

        //Phase II Changes - 03/01/2021 - UpdateVendorPreferredStatus (1-Welcome,2-Authorized,3-Preferred)
        public int UpdateVendorPreferredStatus(string operTenantId, string servTenantId);
        //Phase II Changes - 03/09/2021 
        public Task<ProductSubscriptionModel> GetProductSubscription(string tenantId);

        public Task<int> SaveUsersession(UserSessions user);
        public Task<int> UpdateUsersession(UserSessions user, string email);
        public Task<int> DeleteUsersession(string Email);
        //Phase II Changes - 05/19/2021
        public Task<int> UpdateProviderInsuranceLink(string serviceTenantId, string operTenantId, string fileId, DateTime? expire);

        public Task<int> CreateWellChecklist(string TenantId, string WellId);

        Task<List<ChecklistTaskTemplateModel>> ChecklistTemplateFordrillplan(string welltype, string wellid);

        //DWOP
        Task<List<ChecklistTaskTemplateModel>> ReadChecklistTemplate(string welltype);
        //DWOP
        Task<List<Model.OperatingCompany.Models.ChecklistTemplateModel>> ReadChecklistTemplateList(string operTenantId);

        Task<List<ChecklistTaskTemplateModel>> GetChecklistTemplate(string CheckListId,string TenantId);

        Task<List<TaskModel>> GetTasks();

        Task<int> ChangeChecklistDefaultForTenant(string templatId, string tenantId,string wellTypeId, bool IsDefault);

        Task<string> SaveChecklistTemplate(ChecklistTemplate ChecklistTemplate, string TenantId,string userId);

        Task<List<ServiceDuration>> GetServiceHours();
        Task<List<ServiceDuration>> GetServiceMinutes();
        List<ServiceStageModel> GetServiceStage();

        List<Model.Administration.ServiceCategoryModel> GetCategoriesList();

        List<TaskModel> GetTaskDependencyList(string taskId);
        /// <summary>
        /// Get Template List for a Well Design and Operator Tenant
        /// </summary>
        /// <param name="wellDesign"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel>> GetChecklistTemplateList(string wellDesign, string tenantId);

        /// <summary>
        /// Get Well Register for Operator Tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<List<WellMasterDataViewModel>> GetWellRegister(string tenantId,string rigId,bool checkWellFilter);

        Task<int> DeleteChecklistTemplate(string templateId);

        Task<float> CalculateHours(int days, int hours, int minutes);

        public Task<DrillPlanWellViewModel> DrillingPlanTabContent(string wellid, string DrillingPlanId, string tenantId);

        Task<int> SaveDrillplanHeader(IFormCollection form,DrillPlandetailsViewModel Input,string TenantId,string UserId);

        Task<int> SaveUpdatePlandetails(PlanDetailsModel PlanDetails, string Tenantid);

        Task<List<PlannedTasksModel>> GetPlanDetailsTasks(string wellId, string drillPlanId, string TenantId);

        Task<List<wellmodel>> GetDrillPlanWells(string drillPlanId, string tenantId);

        Task<List<DrillPlanModel>> GetDrillPlanList(string tenantId, string rigId);

        Task<int> DeleteDrillPlan(string planId,string tenantId);

        Task<List<TaskModel>> GetTasksList();

        Task<int> ImportTaskForDrillingPlan(string wellId,string drillPlanId, string TenantId);

        bool GetComponentPermission(string componentName,string userId,string operTenantId);

        public Task<int> GetUserNotificationCount(string userId, string notificationType);
        Task<UserViewModel> EnableUserDetails(string userId);
        Task<UserViewSRVModel> EnableUserDetailsSRV(string userId);
        //Dispatch Routes
        Task<WellIdentityUser> GetUserById(string UserId);
        //Dispatch Routes
        Task<List<DispatchRoutesModel>> GetDispatchRoutes(string UserId,bool isForApi);

        bool CreateDispatchRoute(DispatchRoutes dispatch);
        List<WellIdentityUser> GetComponentPermissionUsers(string componentName, string tenantId);
        Task<DispatchNotification> GetActiveDispatchRoutes(string userId, string dispatchNotes);
        Task<DispatchNotification> GetActiveDispatchRoutes_V2(string userId, string dispatchNotes);
        
        string GetActiveUserNotes(string userId);

Task<List<DispatchRoutesModel>> GetDispatchRoutesForOperator(List<AuctionProposalViewModel> auctionActiveList,string operatorId);
        Task<bool> UpdateDispatchRouteOrders(DispatchRouteOrderModel dispatch);

        Task<CorporateProfile> GetCorporateProfileByUserId(string userId);

        Task<bool> CreateDispatchRoutesHistory(DispatchRoutesHistoryModel destinationChanges);

        int GetStateId(string companyState);
        //Task<List<UserViewSRVModel>> GetUserSRVListByProfileId(string profileId);

        //List<IdentityRole> GetRolesByProfileId(string profileId);
    }
}