using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.Function.BidExpirationScheduler.Models;

namespace WellAI.Advisor.Function.BidExpirationScheduler.Data
{
    public  class WellAIDataContext :DbContext
    {
        public WellAIDataContext(DbContextOptions<WellAIDataContext> options) : base(options)
        {

        }
        public DbSet<MessageQueue> MessageQueues { get; set; }
        public DbSet<AuctionProposalViewModel> AuctionProposalView { get; set; }
        public DbSet<CrmCompanies> CrmCompanies { get; set; }
      



    }
}
