using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text;
using Well_AI.Advisor.API.PEC.Models;
using System.Configuration;

namespace Well_AI.Advisor.API.PEC.Data
{
   public partial class WellAIAdvisiorApiPecContext:DbContext
    {

        public WellAIAdvisiorApiPecContext()
    {
    }

    public WellAIAdvisiorApiPecContext(DbContextOptions<WellAIAdvisiorApiPecContext> options)
         : base(options)
    {
    }

        public DbSet<TenantConfiguration> vw_PecConfiguration { get; set; }
        public DbSet<userDetailConfiguration> vw_PecuserDetailConfiguration { get; set; }
        public DbSet<DbCrmCompanies> vw_PecdbCrmCompanies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
             optionsBuilder.UseSqlServer("Server=tcp:wellai.database.windows.net,1433;Initial Catalog=wellaidb;Persist Security Info=False;User ID=wellaiadmin;Password=Wellaidb#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;MultipleActiveResultSets=true;Timeout=300");
           
        }
    }

}
}
