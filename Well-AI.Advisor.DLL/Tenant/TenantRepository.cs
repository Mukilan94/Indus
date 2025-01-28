using Finbuckle.MultiTenant;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace WellAI.Advisor.Tenant
{
    public class TenantRepository
    {
        private readonly string _connectionString;

        public TenantRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public IEnumerable<TenantInfo> GetTenants()
        {
            IEnumerable<TenantInfo> result = null;

            using (var sc = GetSqlConnection())
            {
                
                result = sc.Query<TenantInfo>("select [Id],[Identifier],[Name],[ConnectionString] from [WellTenants]", commandType: CommandType.Text);

                sc.Close();
            }
            return result;
        }

        public async Task<int> CreateTenantDB(string dbName)
        {
            int result = -1;

            using (var sc = GetSqlConnection())
            {
                result = await sc.ExecuteAsync("CREATE DATABASE [" + dbName + "] ( EDITION = 'standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 10 GB ) ;",
                    commandType: CommandType.Text, commandTimeout: 300);

                sc.Close();
            }

            return result;
        }
    }
}
