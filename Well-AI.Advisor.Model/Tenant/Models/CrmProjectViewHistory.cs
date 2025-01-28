using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmProjectViewHistory
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public DateTime ViewDate { get; set; }
    }
}
