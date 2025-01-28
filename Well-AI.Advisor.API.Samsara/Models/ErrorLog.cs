using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Samsara.Models
{
    [Table("errorLog")]
    public class ErrorLog
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Tenant { get; set; }
        public string ErrorMessage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string ErrorCode { get; set; }
    }
}
