using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmChannel
    {
        public CrmChannel()
        {
            CrmLead = new HashSet<CrmLead>();
        }

        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string ColorHex { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CrmLead> CrmLead { get; set; }
    }
}
