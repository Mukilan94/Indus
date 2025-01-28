using Finbuckle.MultiTenant;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.Tenant
{
    public class TenantConfigurationStore : IMultiTenantStore
    {
        internal const string defaultSectionName = "deprecated";
        private readonly IConfiguration _configuration;
        private ConcurrentDictionary<string, TenantInfo> tenantMap;

        public TenantConfigurationStore(IConfiguration configuration) : this(configuration, defaultSectionName)
        {
            _configuration = configuration;
        }

        public TenantConfigurationStore(IConfiguration configuration, string sectionName)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _configuration = configuration;

            UpdateTenantMap();
        }

        public void UpdateTenantMap()
        {
            var newMap = new ConcurrentDictionary<string, TenantInfo>(StringComparer.OrdinalIgnoreCase);
            var configDbConnection = _configuration.GetConnectionString("WebAIAdvisorContextConnection");

            using (SqlConnection conn = new SqlConnection(configDbConnection))
            {
                var repo = new TenantRepository(conn.ConnectionString);

                var tenants = repo.GetTenants();

                foreach (var newTenant in tenants)
                {
                    newMap.TryAdd(newTenant.Identifier, newTenant);
                }

            }
            var oldMap = tenantMap;
            tenantMap = newMap;
        }

        public Task<bool> TryAddAsync(TenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<TenantInfo> TryGetAsync(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await Task.FromResult(tenantMap.Where(kv => kv.Value.Id == id).SingleOrDefault().Value);
        }

        public async Task<TenantInfo> TryGetByIdentifierAsync(string identifier)
        {
            if (identifier is null)
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            return await Task.FromResult(tenantMap.TryGetValue(identifier, out var result) ? result : null);
        }

        public Task<bool> TryRemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryUpdateAsync(TenantInfo tenantInfo)
        {
            UpdateTenantMap();

            return Task.FromResult(true);
        }

        public class ConfigurationStoreOptions
        {
            TenantInfo Defaults { get; set; }
            public List<TenantInfo> Tenants { get; set; }
        }
    }
}
