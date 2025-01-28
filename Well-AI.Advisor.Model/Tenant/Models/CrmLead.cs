using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmLead
    {
        public CrmLead()
        {
            CrmLeadLine = new HashSet<CrmLeadLine>();
        }

        public string LeadId { get; set; }
        public string HasChild { get; set; }
        public string AccountExecutiveId { get; set; }
        public string ChannelId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CustomerId { get; set; }
        public string Description { get; set; }
        public bool IsConverted { get; set; }
        public bool IsQualified { get; set; }
        public string LeadName { get; set; }
        public string Province { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }

        public virtual CrmAccountExecutive AccountExecutive { get; set; }
        public virtual CrmChannel Channel { get; set; }
        public virtual ICollection<CrmLeadLine> CrmLeadLine { get; set; }
    }
}
