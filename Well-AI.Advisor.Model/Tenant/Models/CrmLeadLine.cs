using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmLeadLine
    {
        public string LeadLineId { get; set; }
        public string ActivityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public string LeadId { get; set; }
        public DateTime StartDate { get; set; }

        public virtual CrmLead Lead { get; set; }
    }
}
