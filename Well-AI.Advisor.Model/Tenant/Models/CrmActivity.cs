using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmActivity
    {
        public CrmActivity()
        {
            CrmOpportunityLine = new HashSet<CrmOpportunityLine>();
        }

        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ColorHex { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CrmOpportunityLine> CrmOpportunityLine { get; set; }
    }
}
