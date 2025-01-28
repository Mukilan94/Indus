using System;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public List<RolePermissionModel> RolePermissions { get; set; }
    }
}
