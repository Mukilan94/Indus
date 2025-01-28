using System;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class RolePermissionComponentSRVModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public bool IsPermitted { get; set; }
    }
}
