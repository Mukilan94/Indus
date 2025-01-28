using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Well_AI.Advisor.API.Samsara.Models;

namespace Well_AI.Advisor.API.Samsara.Data
{
    public partial class WellAIAdvisiorApiSamsaraContext:DbContext
    {
        public WellAIAdvisiorApiSamsaraContext()
        {
        }

        public WellAIAdvisiorApiSamsaraContext(DbContextOptions<WellAIAdvisiorApiSamsaraContext> options)
          : base(options)
        {
        }

        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<TenantConfiguration> TenantConfigurations { get; set; }
        public DbSet<ErrorLog> errorLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=tcp:wellai.database.windows.net,1433 ; Database=wellaidb; user id= wellaiadmin ; password = Wellaidb#;");
                
            }
        }

    }
}
