using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WellFile")]
    public class WellFile
    {
        [Key]
        [Required]
        [StringLength(40)]
        public string FileId { get; set; }
        
        [StringLength(254)]
        public string FileName { get; set; }

        [StringLength(254)]
        public string Category { get; set; }

        [StringLength(40)]
        public string UserId { get; set; }

        [StringLength(40)]
        public string TenantId { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(40)]
        public string WellId { get; set; }

        [StringLength(40)]
        public string VendorId { get; set; }
    }
}
