using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmAccountExecutive
    {
        public CrmAccountExecutive()
        {
            CrmLead = new HashSet<CrmLead>();
            CrmOpportunity = new HashSet<CrmOpportunity>();
        }

        public string AccountExecutiveId { get; set; }
        public string AccountExecutiveName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string SystemUserId { get; set; }

        public virtual ICollection<CrmLead> CrmLead { get; set; }
        public virtual ICollection<CrmOpportunity> CrmOpportunity { get; set; }
    }
}
