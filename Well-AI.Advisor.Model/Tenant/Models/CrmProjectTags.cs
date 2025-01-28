using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmProjectTags
    {
        public int TagId { get; set; }
        public int ProjectId { get; set; }
    }
}
