using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmTags
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public int InstanceId { get; set; }
    }
}
