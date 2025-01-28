using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmOpportunity
    {
        public CrmOpportunity()
        {
            CrmOpportunityLine = new HashSet<CrmOpportunityLine>();
        }

        public string OpportunityId { get; set; }
        public string HasChild { get; set; }
        public string AccountExecutiveId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CustomerId { get; set; }
        public string Description { get; set; }
        public DateTime EstimatedClosingDate { get; set; }
        public decimal EstimatedRevenue { get; set; }
        public string OpportunityName { get; set; }
        public int Probability { get; set; }
        public string RatingId { get; set; }
        public string StageId { get; set; }

        public virtual CrmAccountExecutive AccountExecutive { get; set; }
        public virtual CrmRating Rating { get; set; }
        public virtual ICollection<CrmOpportunityLine> CrmOpportunityLine { get; set; }
    }
}
