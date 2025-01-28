using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WellTenants")]
    public class WellTenantInfo
    {
        [Column("TenId")]
        [Key]
        [Required]
        public string TenId { get; set; }

        [Column("Id")]
        [Required]
        [StringLength(1024)]
        public string Id { get; set; }

        [Column("Identifier")]
        [Required]
        [StringLength(1024)]
        public string Identifier { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(1024)]
        public string Name { get; set; }

        [Column("ConnectionString")]
        [Required]
        [StringLength(1024)]
        public string ConnectionString { get; set; }
    }
}
