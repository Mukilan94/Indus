using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Well_AI_Advisior.API.Authorize.Net.Model;

namespace Well_AI_Advisior.API.Authorize.Net
{
    public partial class WellAIAdvisiorApiAuthorizeContext : DbContext
    {
        public WellAIAdvisiorApiAuthorizeContext()
        {
        }

        public WellAIAdvisiorApiAuthorizeContext(DbContextOptions<WellAIAdvisiorApiAuthorizeContext> options)
            : base(options)
        {
        }


        public DbSet<ConfigurationModel> Configuration { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

              //  optionsBuilder.UseSqlServer("Server=tcp:wellai.database.windows.net,1433 ; Database=wellaidb; user id= wellaiadmin ; password = Wellaidb#;");
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["WebAIAdvisorContextConnection"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<ConfigurationModel>(entity =>
            {
                entity.HasKey(e => e.Index)
                    .HasName("PK__Configur__9A5B622801939F05");

                entity.Property(e => e.ConstantName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FriendlyName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Value).HasMaxLength(256);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
