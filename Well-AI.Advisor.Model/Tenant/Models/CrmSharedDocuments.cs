using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmSharedDocuments
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
    }
}
