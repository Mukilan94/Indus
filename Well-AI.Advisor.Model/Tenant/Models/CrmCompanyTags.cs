using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmCompanyTags
    {
        public int TagId { get; set; }
        public int CompanyId { get; set; }
    }
}
