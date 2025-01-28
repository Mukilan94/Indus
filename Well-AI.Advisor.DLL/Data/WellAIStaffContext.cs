using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.DLL.Data
{
    public class WellAIStaffContext: IdentityDbContext<StaffWellIdentityUser, IdentityRole,string, 
        IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>//, IdentityUserClaim<string>>//, 
    {
        public WellAIStaffContext(DbContextOptions<WellAIStaffContext> options)
            : base(options)
        {
        }
        public DbSet<StaffWellIdentityUser> StaffWellIdentityUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserRole<string>>(userRole =>
            {
                userRole.HasKey(ur => new {ur.UserId,ur.RoleId });
            });

            modelBuilder.Entity<StaffWellIdentityUser>().ToTable("StaffUsers", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("StaffNetRoles", "dbo");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("StaffNetUserRoles", "dbo");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("StaffNetRoleClaims", "dbo");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("StaffNetUserClaims", "dbo");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("staffUserLogins", "dbo");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("StaffNetUserTokens", "dbo");
            //Phase II Changes - 03/16/2021
            modelBuilder.Entity<StaffUserSessions>().ToTable("StaffUserSessions", "dbo");
        }
    }

    public class StaffWellIdentityUser: IdentityUser
    {
        public bool IsCustomer { get; set; } = true;
        public bool IsSuperAdmin { get; set; } = true;
        public bool IsSupportAgent { get; set; }
        public bool IsSupportEngineer { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }


    }

    public class StaffUserSessions 
    {
        [Key]
        [StringLength(40)]
        public string SessionId { get; set; }
        [StringLength(40)]
        public string UserId { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        public DateTime SessionTimeStamp { get; set; }

    }
}
