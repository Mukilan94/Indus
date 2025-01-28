using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmCustomFieldOptions
    {
        public int OptionId { get; set; }
        public int FieldId { get; set; }
        public string OptionValue { get; set; }
    }
}
