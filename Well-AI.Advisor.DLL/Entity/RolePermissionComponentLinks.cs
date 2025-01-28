using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("rolePermissionComponentLinks")]
    public class RolePermissionComponentLinks
    {
        [Key]
        [Required]
        public int RolePermissionComponentLinkId { get; set; }
        public int RolePermissionId { get; set; }
        public int ComponentId { get; set; }
        public bool IsPermitted { get; set; }
    }
}
