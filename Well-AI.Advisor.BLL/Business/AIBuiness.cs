using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.IRepository;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.BLL.Business
{
    public class AIBuiness:IAIBusiness
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;

        public AIBuiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public IEnumerable<AIWellDataModel> GetRIGAIResult(string wellId)
        {
            List<AIWellDataModel> AIAssociateTasksList = new List<AIWellDataModel>();

            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetRIGAIResult(wellId);
        }

        public async Task<List<WellMasterDataViewModel>> GetWellMaster(string wellId, WellIdentityUser user = null)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellMaster(wellId, user);
        }
        public async Task<string> GetTenantIdByWelllId(string wellId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetTenantIdByWelllId(wellId);
        }

        public IEnumerable<WellDataServiceCompanyViewModel> GetServiceWellMaster(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetServiceWellMaster(tenantId);
        }

        public IEnumerable<WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel> GetWellAIStatusChartData(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetWellAIStatusChartData(tenantId);
        }

        public IEnumerable<WellAI.Advisor.Model.ServiceCompany.Models.WellAIRAWStatusViewModel> GetWellAIRAWStatusChartData(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetWellAIRAWStatusChartData(tenantId);
        }
        public async Task<int> AssignWellsToUser(string userId, List<string> wellIds)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.AssignWellsToUser(userId, wellIds);
        }

        public async Task<int> AssignRigsToUser(string userId, List<string> RigId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.AssignRigsToUser(userId, RigId);
        }

        public async Task<List<WellMasterDataViewModel>> GetUserAssignedWells(string userId,string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetUserAssignedWells(userId,tenantId);
        }


        public async Task<List<WellMasterDataViewModel>> GetUserAssignedRigs(string userId, string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetUserAssignedRigs(userId, tenantId);
        }

        public async Task<List<WellMasterDataViewModel>> GetWellsForOperationCompany(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellsForOperationCompany(tenantId);
        }

        public async Task<List<WellMasterDataViewModel>> GetWellsForOperationCompanyOnServiceSite(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellsForOperationCompanyOnServiceSite(tenantId);
        }
        
        public async Task UpdatePredictionWellRegisterById(string wellId, bool prediction)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            await aIDataRepository.UpdatePredictionWellRegisterById(wellId, prediction);
        }

        public async Task<WellRegister> GetWellRegisterById(string wellId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellRegisterById(wellId);
        }
        public Task<List<WellAI.Advisor.Model.OperatingCompany.Models.RigModel>> GetRigMaster(string tenantId,string RigId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetRigMaster(tenantId, RigId);
        }

        public Task<List<WellAI.Advisor.Model.OperatingCompany.Models.PadModel>> GetPadMaster(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetPadMaster(tenantId);
        }

        public Task<List<WorkstationModel>> GetWorkstationDetail(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetWorkstationDetail(tenantId);
        }

        public IEnumerable<OperatingWellAIStatusViewModel> GetWellAIStatusChartDataForOpr(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetWellAIStatusChartDataForOpr(tenantId);
        }

        public IEnumerable<OperatingWellAIRAWStatusViewModel> GetWellAIRAWStatusChartDataForOpr(string tenantId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return aIDataRepository.GetWellAIRAWStatusChartDataForOpr(tenantId);
        }
        
        public async Task<List<InDepthRigData>> GetWellDepthData(string wellId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellDepthData(wellId);
        }

        public async Task<List<InDepthRigData>> GetWellDepthDataAIRow(string wellId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellDepthDataAIRow(wellId);
        }
        public async Task<List<InDepthRigData>> GetWellDepthTimeChartFromTasks(string wellId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellDepthTimeChartFromTasks(wellId);
        }

        public async Task<float> GetWellCurrentDepth(string wellId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellCurrentDepth(wellId);
        }

        public async Task<List<InDepthRigDataGridModel>> GetWellDepthGridData(string wellId)
        {
            IAIDataRepository aIDataRepository = new AIDataRepository(db, _roleManager, _userManager);
            return await aIDataRepository.GetWellDepthGridData(wellId);
        }
    }
}
