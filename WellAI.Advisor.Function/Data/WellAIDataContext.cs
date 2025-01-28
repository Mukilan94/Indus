using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.Function.MailQueue.Models;

namespace WellAI.Advisor.Function.MailQueue.Data
{
    public  class WellAIDataContext : DbContext
    {
        public WellAIDataContext(DbContextOptions<WellAIDataContext> options) : base(options)
        {

        }
        public DbSet<UserSessionModel> UserSession { get; set; }
        public DbSet<StaffUserSessionsModel> StaffUserSession { get; set; }

    }
}
