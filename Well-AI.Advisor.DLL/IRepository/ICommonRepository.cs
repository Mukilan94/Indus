using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;


namespace WellAI.Advisor.DLL.Repository
{
    public interface ICommonRepository
    {
        Task<IdentityResult> Create(IdentityRole role);
        List<RoleViewModel> GetRoleList(string tenantId);
        List<RoleViewSRVModel> GetRoleSRVList(string tenantId);
        Task<List<UserViewModel>> GetUserList(string tenantId);
        Task<List<UserViewSRVModel>> GetUserSRVList(string tenantId);
        List<IdentityRole> GetRoles(string tenantId);
        Task<IList<string>> GetUserRoleNames(WellIdentityUser resultUser);
        List<IdentityRole> GetRolesSet();
        Task<UserViewModel> GetUser(string id);
        Task<int> GetUserSubscriptionUserLeft(string tenantId);
        Task<int> AddUserCountSubscription(string tenantId);
        Task<int> RemoveUserCountSubscription(string tenantId);
        Task<UserViewModel> GetPrimaryUser(string tenantId);
        Task<UserViewSRVModel> GetUserSRV(string id);
        WellIdentityUser GetUserDetail(string id);
        Task<IdentityResult> UpdateUser(WellIdentityUser resultUser);
        Task<string> GetRoleName(WellIdentityUser resultUser);

        string GetCompanyName(string tenantId);
        Task<IdentityResult> RemoveUserRole(WellIdentityUser resultUser, string resultRole);
        Task<IdentityResult> AddUserRole(WellIdentityUser resultUser, string resultRole);
        Task<IdentityResult> CreateUser(WellIdentityUser user, string password);
        Task<IdentityResult> RemoveUser(WellIdentityUser user);
        Task<bool> RemoveAllUserRoles(WellIdentityUser resultUser, IList<string> resultRoles);
        Task<bool> CreateTenantUser(TenantUsers user);
        Task<string> GetTenantUser(string userId);
        Task<List<string>> GetTenantUserIds(List<string> userIds);
        bool RemoveTenantUser(string userId);
        Task<bool> CreateTenantRole(TenantRoles role);
        bool CreateUserBasicDetail(CrmUserBasicDetail user);
        bool UpdateUserBasicDetail(CrmUserBasicDetail user);
        Task<List<CorporateProfile>> GetServiceCompanies();
        Task<List<CorporateProfile>> GetServiceCompaniesByCategories(string ServiceCategoryId);
        CrmUserBasicDetail DisableUserBasicDetail(string userId);
        bool CreateCompanyDetail(CrmCompanies company);
        bool UpdateCompanyCategories(string category, string tenantId);
        CrmUserBasicDetail GetUserBasicDetail(string userId);
        Task<CrmUserBasicDetail> GetMasterUserByTenantId(string tenantId);
        bool UpdateUserSubscription(string subscriptionId, string userId, int noOfItems);
        bool UpdateUserPagesCompleteStatus(int status, string userId);
        CrmCompanies GetCompanyDetail(string userId);
        CrmCompanies GetCompanyDetailByTenant(string tenantId);
        bool UpdateUserPaymentStatus(int status, string userId);
        Task<WellIdentityUser> GetUserByEmail(string email);
       Task <RegisterStaffViewModel> GetAdminUserByEmail (string email);
        string GetUserBasicDetailByEmail(string email);
        Task<List<USAState>> GetUSAStates();
        Task<List<Category>> GetCategories();
        Task<bool> CreateWellFile(WellFile file);
        Task<string> RemoveWellFile(string fileId);
        Task<string> RemoveWellFiles(List<string> fileIds);
        Task<string> RemoveWellFileByName(string path, string fileName, string tenantId);
        Task<List<string>> RemoveWellFilesByName(List<KeyValuePair<string, string>> fileNamePaths, string tenantId);
        Task<List<Model.OperatingCompany.Models.MSA>> GetMSAWellFilesFromTenants(List<string> tenantIds, string operTenantId, string wellId);
        //Phase II Changes
        //MSA Permission
        Task<List<Model.OperatingCompany.Models.MSA>> GetMSAWellFilesFromTenantsForUser(List<string> tenantIds, string operTenantId, string wellId,string userId);
        Task<List<Model.OperatingCompany.Models.Insurance>> GetInsuranceWellFilesFromServiceTenants(List<string> tenantIds, string operTenantId, string wellId);

        Task<WellFile> GetWellFileById(string fileId);
        Task<List<WellFileFolder>> GetWellFileFolders();
        Task<CorporateProfile> GetCorporateProfile();
        Task<CorporateProfile> GetCorporateProfileByTenant(string tenantId);
        Task<TenantConfiguration> GetApiConfigurationByTenant(string tenantId);
        Task<int> UpdateCorporateProfile(CorporateProfile input, string userId, string tenantId);
        Task<int> UpdateApiConfigurationByTenant(string value, string tenantId);
        List<FieldTicketSRV> GetProjectInvoice(string ProjectID);
        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>> GetServiceOfferings(string tenantId);
        Task<ServiceCorporateProfile> GetServiceCorporateProfile();
        Task<ServiceCorporateProfile> GetServiceCorporateProfileByTenant(string tenantId);
        Task<int> UpdateServiceCorporateProfile(ServiceCorporateProfile input, string userId, string tenantId);
        Task<int> UpdatedUpCommingProjectsDetails(ProjectViewSRVModel input, string tenantId);
        Task<List<ProjectViewSRVModel>> GetTechnicianName(string tenantId);
        Task<List<ProjectViewModel>> GetUpCommingProjectsForOperator(WellIdentityUser user, string welID);
        Task<List<ProjectViewSRVModel>> GetUpCommingProjects(string tenantId);
        Task<UserViewSRVModel> GetPrimaryUserSRV(string tenantId);
        Task<List<CorporateProfile>> GetOperatingCompanies();
        Task<CrmUserBasicDetail> EnableUser(string userId);
        Task<List<Model.ServiceCompany.Models.ServiceOfferingSRV>> GetOperatingOfferings(string tenantId);
        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>> GetOperatingCompanyServices(string TenantID);

        Task<List<Model.ServiceCompany.Models.ServiceMSA>> GetMSAWellFilesFromOperatingTenants(List<string> tenantIds, string operTenantId);
        Task<List<Model.ServiceCompany.Models.ServiceInsurance>> GetInsuranceWellFilesFromOperatingTenants(List<string> tenantIds, string operTenantId);
        //Phase II Changes - 05/19/2021
        Task<List<Model.ServiceCompany.Models.ServiceInsurance>> GetInsuranceFilesFromServiceTenants(List<string> tenantIds, string serviceTenantId, string operTenantId);


        Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetWellFilesFromTenant(string tenantId, string wellCategory);
        Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetWellFilesFromTenantAndWell(string tenantId, string wellId, string wellCategory);
        Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetVendorFilesFromTenant(string tenantId, string fileCategory);
        Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetVendorFilesFromServiceTenant(string tenantId, string fileCategory);
        Task<List<MessageQueue>> GetNotifications(string userId);
        int AddNotifications(MessageQueue messageQueue);
        bool UpdateNotifications(string toId);
        bool GetUserAvailibilityStatus(string userId);
        void UpdateUserStatus(string userId, bool status);
        List<MessageQueue> GetUserNotificationDetails(string userId);
        List<MessageQueue> GetUserNotificationDetailsScroll(string userId, int skip, int take);

        Task<bool> UpdateProviderMSALinkWellFile(string operTenantId, string fileId, string vendorId, DateTime expire, bool isActive);
        Task<bool> UpdateProviderMSALinkWellFileService(string operTenantId, string fileId, string vendorTenantId, DateTime expire, bool isActive);
        Task<bool> UpdateProviderMSALinkWellFileServiceEdit(string fileId, string tenantId, DateTime expire, bool isActive);
        string GetStateRegion(int stateId);
        void UpdateNotificationStatus(int messageQueueId);
        List<CrmCompanies> GetCategorywiseCompanyDetail(string categoryId);
        bool NotificationExists(string entityId, string toId);
        //Phase II Changes - 01/19/2021 - UpdateMSAApprovalStatus
        Task<int> UpdateMSAApprovalStatus(string fileId, bool status, string userid, ServiceTenantRepository servRepo);
        Task<List<Model.OperatingCompany.Models.MSA>> GetApprovedMSAWellFilesOfServiceTenant(List<string> tenantIds, string operTenantId, string wellId);
        //Phase II Changes - 02/08/2021
        public Task<int> DeactivateFile(string fileId);

        public Task<int> CreateDepthPermission(RigsDepth_Permission input);

        //Phase II Changes - 03/01/2021 - UpdateVendorPreferredStatus (1-Welcome,2-Authorized,3-Preferred)
        public int UpdateVendorPreferredStatus(string operTenantId, string servTenantId);

        //Phase II Changes - 03/09/2021
        public Task<ProductSubscriptionModel> GetProductSubscription(string tenantId);

        public Task<int> SaveUsersession(UserSessions user);

        public Task<int> DeleteUsersession(string Email);

        //Phase II Changes - 05/16/2021
        public Task<int> UpdateProviderInsuranceLink(string serviceTenantId, string operTenantId, string fileId, DateTime? expire);

        public Task<int> CreateWellChecklist(string TenantId, string WellId);
        Task<List<ChecklistTaskTemplateModel>> ChecklistTemplateFordrillplan(string welltype,string wellid);

        //DWOP
        Task<List<ChecklistTaskTemplateModel>> ReadChecklistTemplate(string welltype);
        Task<List<Model.OperatingCompany.Models.ChecklistTemplateModel>> ReadChecklistTemplateList(string operTenantId);
        Task<List<ChecklistTaskTemplateModel>> GetChecklistTemplate(string CheckListId, string TenantId);
        Task<List<TaskModel>> GetTasks();
        public Task<int> ChangeChecklistDefaultForTenant(string templatId, string tenantId,string wellTypeId,bool IsDefault);

        Task<string> SaveChecklistTemplate(ChecklistTemplate ChecklistTemplate, string TenantId, string userId);

        //DWOP
        public List<ServiceStageModel> GetServiceStage();

        public List<Model.Administration.ServiceCategoryModel> GetCategoriesList();

        public List<TaskModel> GetTaskDependencyList(string taskId);
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
        public Task<List<WellMasterDataViewModel>> GetWellRegister(string tenantId, string rigId, bool checkWellFilter);

        Task<int> DeleteChecklistTemplate(string templateId);
        Task<float> CalculateHours(int days, int hours, int minutes);
        public Task<DrillPlanWellViewModel> DrillingPlanTabContent(string wellid, string DrillingPlanId, string tenantId);
        Task<int> SaveDrillplanHeader(IFormCollection form,DrillPlandetailsViewModel Input, string TenantId, string UserId);
        Task<int> SaveUpdatePlandetails(PlanDetailsModel PlanDetails, string Tenantid);
        Task<List<PlannedTasksModel>> GetPlanDetailsTasks(string wellId, string drillPlanId, string TenantId);
        Task<List<wellmodel>> GetDrillPlanWells(string drillPlanId, string tenantId);
        Task<List<DrillPlanModel>> GetDrillPlanList(string tenantId, string rigId);
        Task<int> DeleteDrillPlan(string planId, string tenantId);
        Task<List<TaskModel>> GetTasksList();
        Task<int> ImportTaskForDrillingPlan(string wellId, string drillPlanId, string TenantId);
        bool GetComponentPermission(string componentName, string userId, string operTenantId);

        public Task<int> GetUserNotificationCount(string userId, string notificationType);
        //Dispatch Routes
        public Task<WellIdentityUser> GetUserById(string email);
        public Task<List<DispatchRoutesModel>> GetDispatchRoutes(string UserId,bool isForApi);
        public Task<List<DispatchRoutesModel>> GetDispatchRoutes_Preview(string UserId, bool isForApi);
        public Task<List<DispatchRoutesModel>> GetDispatchRoutes_V2(string userId, bool isForApi, string tenantId);

      
        Task<DispatchNotification> GetActiveDispatchRoutes_V2(string userId, string dispatchNotes);

        
        public Task<List<DispatchRoutesModel>> GetDispatchRoutesForOperator(List<AuctionProposalViewModel> auctionActiveList, string operatorId);


        public Task<List<DispatchRoutesModel>> GetOperatorsharedetails(List<AuctionProposalViewModel> auctionActiveList, string operatorId, string userId);

        bool CreateDispatchRoute(DispatchRoutes Dispatch);

        int GetStateId(string statename);
        bool CreateDispatchRoute_RefreshRout(DispatchRoutes Dispatch);
        bool CreateDispatchRoute_RefreshRout_Update(DispatchRoutes Dispatch);

        Task<List<DispatchRoutesHistoryDetailsModel>> CreateDispatchRoute_V2(List<DispatchRoutes> Dispatch, List<DispatchRoutesHistoryDetailsModel> Dispatch2);

        Task<bool> UpdateRouterStatus(DispatchRoutes Dispatch);

        Task<bool> UpdateOperatorId(List<DispatchRoutes> Dispatch);


        //bool CreateDispatchRoutesHistoryHead_V2(DispatchRoutesHistoryHead Historyhead);      
        Task<List<DispatchRoutesHistoryDetailsModel>> DeleteDispatchRoute_V2(List<DispatchRoutes> dispatchRoutes, List<DispatchRoutesHistoryDetailsModel> routesList2);
        public List<WellIdentityUser> GetComponentPermissionUsers(string componentName, string tenantId);

        bool UpdateDispatch(DispatchRoutes Dispatch);
        Task<DispatchNotification> GetActiveDispatchRoutes(string userId,string dispatchNotes);
        string GetActiveUserNotes(string userId);
        Task<bool> UpdateDispatchRouteOrders(DispatchRouteOrderModel dispatch);


        Task<IdentityResult> EditProfilePasswordChange(WellIdentityUser resultUser);

        Task<CorporateProfile> GetCorporateProfileByUserId(string userId);
         Task<bool> CreateDispatchRoutesHistory(DispatchRoutesHistoryModel destinationChanges);
        Task<bool> CreateDispatchRoutesHistory_Deleted(DispatchRoutesHistoryModel destinationChanges);
        Task<int> UpdateCustomerCorporateProfile(CorporateProfileAdmin input, string userId, string tenantId);
        Task<int> UpdateUsersession(UserSessions user, string email);
    }
}
