using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("rolePermissionLinks")]
    public class RolePermissionLinks
    {
        [Key]
        [Required]
        public int RolePermissionLinkId { get; set; }
        public string RoleId { get; set; }
        public int RolePermissionId { get; set; }
        public bool IsPermitted { get; set; }
    }
}
