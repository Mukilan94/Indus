using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Data;
using WellAI.Advisor.API.Models;
using WellAI.Advisor.API.Repository.IRepository;

namespace WellAI.Advisor.API.Repository
{
    public class TenantRepository: ITenantRepository
    {
        private readonly IConfiguration _configuration;
        
        public TenantRepository( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("WebAIAdvisorContextConnection");
            return new SqlConnection(connectionString);
        }

        public DbContextOptions<WellAIAdvisiorContext> SetDbContext(string TenantId)
        {
            var tenants = GetTenants(TenantId);
            string connectionString = tenants.Where(x=>x.tenantid==TenantId && x.isactive==true).Select(x=>x.connectionstring).FirstOrDefault();
            var optionsBuilder = new DbContextOptionsBuilder<WellAIAdvisiorContext>()
           .UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            return optionsBuilder.Options;
        }
        public IEnumerable<VTenantDetails> GetTenants(string TenantId)
        {
            using (var sc = GetSqlConnection())
                return sc.Query<VTenantDetails>(@"select Distinct(t.tenantid),(case when c.accounttype=1 then 'Serv' else 'Oper' End) AccountType,c.isactive, w.connectionstring from crmUserBasicDetail c
                            Left join tenantusers t on t.userid = c.userid
                            left join welltenants w on w.id = t.tenantid
                            where tenantid is not null", commandType: CommandType.Text);

                
        }

       
    }
    
}
