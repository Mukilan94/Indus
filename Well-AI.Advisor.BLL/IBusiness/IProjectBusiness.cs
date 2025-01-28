using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.BLL.IBusiness
{
    public interface IProjectBusiness
    {
        public Task<MessageToQueue> GetProposalCreatorOperatorAndUserEmail(string projectId, string senderUserId,string tid);
        public Task<ProjectDashboardOperViewModel> ProjectDashboardOperTenantId(string tenantid, WellIdentityUser user, string wellId);
        public Task<ProjectDashboardSerViewModel> ProjectDashboardSerTenantId(string tenantid, string operId);
        Task<ProjectViewSRVModel> GetUpCommingProjectsDetailsByTenantIdForSRV(string tenantId, string projectId);
        Task<ProjectViewModel> GetUpCommingProjectsDetailsByTenantIdForOperator(string tenantid, string projectId);
        Task<List<AuctionProposalAttachmentViewModel>> GetProjectProposalAttachments(string tenantId, string proposalId, string projectId);
        Task<List<TechnicianViewModel>> GetAssignedTechnicianByProjectId(string projectId);
        Task<TechnicianViewModel> AddTechnicianOnProject(TechnicianViewModel input);
        Task<List<TechnicianViewModel>> GetTechnicianByTenantid(string tenantId);
        Task<int> UpdateUpCommingProjectsDetails(ProjectViewSRVModel input);
        Task<int> UpdateUpCommingProjectsDetailsForOperator(ProjectViewModel input);
        Task<int> UpdateUpComingProjectsDetailsForOperator_V1(ProjectViewModel input, string rigId);
        Task<bool> RemoveTechUserIdFromProject(string projectTechId);
        Task<List<ProjectViewModel>> GetUpCommingProjectsForOperator(string tenantId);
        Task<List<ProjectViewSRVModel>> GetUpCommingProjectsSRV(string tenantId, string operId);
        List<ProjectViewSRVModel> GetUpComingProjectsSRV_Chat(string tenantId, string operId);
        List<ProjectViewModel> GetUpComingProjects_Chat(string tenantId, string serviceTenantId);
        public Task<List<WellCheckListDetailModel>> EnsureAndGetWellCheckListForProject(string tenantId, string wellId);
        public Task<List<ProjectWellCheckListModel>> GetWellCheckListForProjects(string tenantId, string wellId);
        public Task<string> UpdateWellCheckStatusListForProject(string tenantId, string wellId, List<string> checkIds);
        public Task<string> UpdateWellCheckStatusListForProjects(string tenantId, List<string> checkIds);
        public Task<List<ProjectWellCheckListModel>> UpdateWellCheckStatusListForProjects(string tenantId, IEnumerable<ProjectWellCheckListModel> checkListModels);

        Task<List<ProjectNote>> GetProjectNotes(string projectId);
        Task<ProjectNote> CreateNewProjectNote(ProjectNote newNote);
        Task<List<ProjectViewSRVModel>> GetProjectsByWellId(string tenantId, string wellId);
        Task<List<ProjectViewSRVModel>> GetProjectsByWellIdSRV(string tenantId, string wellId);
        Task RemoveCheckListItem(string tenantId, string wellId, string wellCheckListId, string taskId);
        Task CreateCheckListItem(string tenantId, CheckListTaskModel input);

        Task<List<StagingChartModel>> GetStagingData(string tenantId, string wellId);
        Task<StagingTasksModel> GetCasingStringAndCurrentStage(string tenantId, string wellId);
    }
}
