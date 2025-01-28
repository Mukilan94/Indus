using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WellAI.Advisor
{
    public class TenantStoreDbContext : EFCoreStoreDbContext
    {
        private readonly IConfiguration _configuration;
        public TenantStoreDbContext(DbContextOptions<TenantStoreDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Phase II Changes - 03/29/2021
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WebAIAdvisorContextConnection"), options => options.EnableRetryOnFailure());
            base.OnConfiguring(optionsBuilder);
        }
    }
}
