using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WellAI.Advisor.DLL.ServiceEntity
{
    [Table("rolePermissions")]
    public class SRVRolePermissions
    {
        [Key]
        [Required]
        public int RolePermissionId { get; set; }
        public string RolePermissionName { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
