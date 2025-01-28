using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmCustomFieldProjectValues
    {
        public int ProjectId { get; set; }
        public int FieldId { get; set; }
        public object Value { get; set; }
    }
}
