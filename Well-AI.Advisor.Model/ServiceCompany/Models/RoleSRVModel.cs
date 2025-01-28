using System;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class RoleSRVModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public List<RolePermissionSRVModel> RolePermissions { get; set; }
    }
}
