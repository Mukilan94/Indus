using System.Collections.Generic;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Samsara.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using ServiceVehicleViewModel = WellAI.Advisor.Model.ServiceCompany.Models.ServiceVehicleViewModel;
using VechicleLocation = WellAI.Advisor.Model.ServiceCompany.Models.VechicleLocation;

namespace WellAI.Advisor.BLL.IBusiness
{
    public interface IServiceVehicleBusiness
    {
        Task<List<ServiceVehicleViewModel>> GetServiceVehicles(string tenantId);
        Task<List<ServiceVehicleViewModel>> GetActiveTechnicianAndProjectByTenantId(string tenantId);
        Task<List<ServiceVehicleViewModel>> GetActiveTechnicianAndProjectByOprTenantId(WellIdentityUser user, string wellId);
        Task<List<ServiceVehicleViewModel>> GetActiveTechnicianByProjectId(WellIdentityUser userwell, string projectId);
        Task<List<ServiceVehicleViewModel>> GetServiceVehiclesList(string tenantId);
        Task<VechicleLocation> GetLatLngOfVehicleBySamsaraId(string SamsaraId);
        Task<bool> UpdateSamsaraData(VechicleModel vehicles, string authorId, string tenantId);
        Task<TechnicianTracker> GetTechnicianTracker(string ProjectId);
    }
}
