using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmCompanySubscribers
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
}
