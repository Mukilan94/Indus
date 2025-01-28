using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("AuditLog")]
    public class Audit
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Activity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
