using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmCustomFields
    {
        public int FieldId { get; set; }
        public int InstanceId { get; set; }
        public int Type { get; set; }
        public string FieldName { get; set; }
        public bool Mandatory { get; set; }
        public int OrderByNumber { get; set; }
        public bool UsedForCompanies { get; set; }
        public bool UsedForContacts { get; set; }
        public bool UsedForProjects { get; set; }
    }
}
