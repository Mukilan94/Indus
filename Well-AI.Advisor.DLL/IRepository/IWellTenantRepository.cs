using System.Collections.Generic;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.DLL.Repository
{
    public interface IWellTenantRepository
    {
        List<WellTenantInfo> GetAllTenants();
        bool CreateTenant(string tenantId, string connectionString, string name, string identifier, string id);
    }
}
