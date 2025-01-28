using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("tenantUsers")]
    public class TenantUsers
    {
        [Key]
        [Required]
        public int TenantUserId { get; set; }
        public string TenantId { get; set; }
        public string UserId { get; set; }
    }
}
