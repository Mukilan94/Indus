using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using src.Models;
using Well_AI.Advisor.Helpdesk.Models;
using Well_AI.Helpdesk.Models.ManageViewModels;

namespace src.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("StaffUsers");
            builder.Entity<Customer>().ToTable("hdCustomer");
            builder.Entity<Product>().ToTable("hdProduct");
            builder.Entity<Contact>().ToTable("hdContact");
            builder.Entity<SupportAgent>().ToTable("hdSupportAgent");
            builder.Entity<SupportEngineer>().ToTable("hdSupportEngineer");
            builder.Entity<Ticket>().ToTable("hdTicket");
            builder.Entity<CorporateProfile>().ToTable("CorporateProfile");
            builder.Entity<hdStaffs>().ToTable("AspNetUsers");
            
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().ToTable("StaffNetRoles");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().ToTable("StaffNetRoleClaims");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>().ToTable("StaffNetUserClaims");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>().ToTable("StaffNetUserLogins");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().ToTable("StaffNetUserRoles");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>().ToTable("StaffNetUserTokens");
        }

        public DbSet<src.Models.Organization> Organization { get; set; }

        public DbSet<src.Models.Product> Product { get; set; }

        public DbSet<src.Models.Customer> Customer { get; set; }

        public DbSet<src.Models.Contact> Contact { get; set; }

        public DbSet<src.Models.SupportAgent> SupportAgent { get; set; }

        public DbSet<src.Models.SupportEngineer> SupportEngineer { get; set; }

        public DbSet<src.Models.Ticket> Ticket { get; set; }

        public DbSet<src.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Well_AI.Helpdesk.Models.ManageViewModels.CorporateProfile> CorporateProfile { get; set; }
        public DbSet<Well_AI.Helpdesk.Models.hdIssues> hdIssues { get; set; }
        public DbSet<Well_AI.Helpdesk.Models.hdStatus> hdStatus { get; set; }
        public DbSet<Well_AI.Helpdesk.Models.hdComments> hdComments { get; set; }

        public DbSet<Well_AI.Advisor.Helpdesk.Models.hdStaffs> hdStaffs { get; set; }
    }
}
