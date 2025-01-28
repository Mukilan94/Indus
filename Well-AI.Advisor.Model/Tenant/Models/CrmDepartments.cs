using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmDepartments
    {
        public int DepartmentId { get; set; }
        public int InstanceId { get; set; }
        public string Name { get; set; }
    }
}
