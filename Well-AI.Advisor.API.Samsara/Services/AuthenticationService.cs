using Newtonsoft.Json;
using System;
using System.Linq;
using Well_AI.Advisor.API.Samsara.Data;
using Well_AI.Advisor.API.Samsara.Models;
using Well_AI.Advisor.API.Samsara.Services.IServices;

namespace Well_AI.Advisor.API.Samsara.Services
{
    public class AuthenticationService : IAuthenticationService
    {


        public AuthenticationService()
        {

        }

        

        public Configuration AuthenticateApi()
        {
            WellAIAdvisiorApiSamsaraContext _db = new WellAIAdvisiorApiSamsaraContext();
            try
            {
                var apiConfig = _db.Configuration.Where(x => x.FriendlyName == "Samsara Api Key").FirstOrDefault();
                return apiConfig;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "AuthenticationService AuthenticateApi", null);
                throw;
            }
        }

        public TenantConfigurationSerialized AuthenticateApi(string tenantId)
        {
            WellAIAdvisiorApiSamsaraContext _db = new WellAIAdvisiorApiSamsaraContext();

            TenantConfigurationSerialized result = new TenantConfigurationSerialized();

            try
            {
                var apiConfig = _db.TenantConfigurations.FirstOrDefault(x => x.TenantId == tenantId);

                if (apiConfig!= null && apiConfig.Index > 0)
                {
                    result = JsonConvert.DeserializeObject<TenantConfigurationSerialized>(apiConfig.Value);
                }

                return result;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "AuthenticationService AuthenticateApi", null);
                throw;
            }
        }
    }
}
