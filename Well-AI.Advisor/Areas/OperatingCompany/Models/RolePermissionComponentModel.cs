using System;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class RolePermissionComponentModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public bool? IsPermitted { get; set; }
    }
}
