using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmCompanyViewHistory
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ViewDate { get; set; }
    }
}
