using System.Collections.Generic;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.DLL.IRepository
{
    public interface IAIDataRepository
    {
        IEnumerable<AIWellDataModel> GetRIGAIResult(string wellId);

        Task<List<WellMasterDataViewModel>> GetWellMaster(string wellId, WellIdentityUser user =null);

        IEnumerable<WellDataServiceCompanyViewModel> GetServiceWellMaster(string tenantId);

        IEnumerable<WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel> GetWellAIStatusChartData(string tenantId);
        IEnumerable<WellAI.Advisor.Model.ServiceCompany.Models.WellAIRAWStatusViewModel> GetWellAIRAWStatusChartData(string tenantId);

        Task<int> AssignWellsToUser(string userId, List<string> wellIds);

        Task<int> AssignRigsToUser(string userId, List<string> RigId);

        Task<List<WellMasterDataViewModel>> GetUserAssignedWells(string userId, string tenantId);

        Task<List<WellMasterDataViewModel>> GetUserAssignedRigs(string userId, string tenantId);

        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.RigModel>> GetRigMaster(string tenantId,string RigId);

        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.PadModel>> GetPadMaster(string tenantId);
        IEnumerable<OperatingWellAIStatusViewModel> GetWellAIStatusChartDataForOpr(string tenantId);
        IEnumerable<OperatingWellAIRAWStatusViewModel> GetWellAIRAWStatusChartDataForOpr(string tenantId);
        Task<List<InDepthRigData>> GetWellDepthData(string wellId);
        Task<List<WellMasterDataViewModel>> GetWellsForOperationCompany(string tenantId);
        Task<List<WellMasterDataViewModel>> GetRigsForOperationCompany(string tenantId);


        Task<string> GetTenantIdByWelllId(string wellId);
        Task<List<InDepthRigDataGridModel>> GetWellDepthGridData(string wellId);
        Task<List<WorkstationModel>> GetWorkstationDetail(string tenantId);
        Task<List<InDepthRigData>> GetWellDepthDataAIRow(string wellId);
        Task<List<InDepthRigData>> GetWellDepthTimeChartFromTasks(string wellId);

        Task<List<WellMasterDataViewModel>> GetWellsForOperationCompanyOnServiceSite(string tenantId);
        Task<WellRegister> GetWellRegisterById(string wellId);
        Task UpdatePredictionWellRegisterById(string wellId, bool prediction);
        Task<float> GetWellCurrentDepth(string wellId);
    }
}
