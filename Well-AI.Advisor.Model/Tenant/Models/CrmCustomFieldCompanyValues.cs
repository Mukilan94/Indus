using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmCustomFieldCompanyValues
    {
        public int CompanyId { get; set; }
        public int FieldId { get; set; }
        public object Value { get; set; }
    }
}
