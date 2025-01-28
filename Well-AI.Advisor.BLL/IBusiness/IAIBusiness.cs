using System.Collections.Generic;
using System.Threading.Tasks;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.BLL.IBusiness
{
    public interface IAIBusiness
    {
        IEnumerable<AIWellDataModel> GetRIGAIResult(string wellId);

        Task<List<WellMasterDataViewModel>> GetWellMaster(string wellId, WellIdentityUser user = null);

        IEnumerable<WellDataServiceCompanyViewModel> GetServiceWellMaster(string tenantId);

        IEnumerable<WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel> GetWellAIStatusChartData(string tenantId);

        IEnumerable<WellAI.Advisor.Model.ServiceCompany.Models.WellAIRAWStatusViewModel> GetWellAIRAWStatusChartData(string tenantId);

        Task<int> AssignWellsToUser(string userId, List<string> wellIds);
        Task<List<WellMasterDataViewModel>> GetUserAssignedWells(string userId,string tenantId);

        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.RigModel>> GetRigMaster(string tenantId,string RigId);

        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.PadModel>> GetPadMaster(string tenantId);

        Task<List<WorkstationModel>> GetWorkstationDetail(string tenantId);

        IEnumerable<OperatingWellAIStatusViewModel> GetWellAIStatusChartDataForOpr(string tenantId);

        IEnumerable<OperatingWellAIRAWStatusViewModel> GetWellAIRAWStatusChartDataForOpr(string tenantId);

        Task<List<InDepthRigData>> GetWellDepthData(string wellId);
        Task<List<InDepthRigData>> GetWellDepthTimeChartFromTasks(string wellId);
        Task<string> GetTenantIdByWelllId(string wellId);
        Task<List<InDepthRigDataGridModel>> GetWellDepthGridData(string wellId);
        Task<List<WellMasterDataViewModel>> GetWellsForOperationCompany(string tenantId);
        Task<List<WellMasterDataViewModel>> GetWellsForOperationCompanyOnServiceSite(string tenantId);

        Task<float> GetWellCurrentDepth(string wellId);
    }
}
