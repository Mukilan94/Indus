using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmOpportunityLine
    {
        public string OpportunityLineId { get; set; }
        public string ActivityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public string OpportunityId { get; set; }
        public DateTime StartDate { get; set; }

        public virtual CrmActivity Activity { get; set; }
        public virtual CrmOpportunity Opportunity { get; set; }
    }
}
