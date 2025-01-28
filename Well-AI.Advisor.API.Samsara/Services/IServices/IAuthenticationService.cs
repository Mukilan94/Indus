
using Well_AI.Advisor.API.Samsara.Data;
using Well_AI.Advisor.API.Samsara.Models;

namespace Well_AI.Advisor.API.Samsara.Services.IServices
{
    
   public interface IAuthenticationService
    {
        Configuration AuthenticateApi();
        TenantConfigurationSerialized AuthenticateApi(string tenantId);
        
            

    }
}
