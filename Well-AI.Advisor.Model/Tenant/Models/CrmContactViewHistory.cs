using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmContactViewHistory
    {
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public DateTime ViewDate { get; set; }
    }
}
