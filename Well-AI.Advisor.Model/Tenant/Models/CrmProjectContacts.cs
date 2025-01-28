using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmProjectContacts
    {
        public int ProjectId { get; set; }
        public int ContactId { get; set; }
    }
}
