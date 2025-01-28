using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("tenantRoles")]
    public class TenantRoles
    {
        [Key]
        [Required]
        public int TenantRoleId { get; set; }
        public string TenantId { get; set; }
        public string RoleId { get; set; }
    }
}
