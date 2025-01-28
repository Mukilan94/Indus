using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    [Table("ProjectNote")]
    public class ProjectNote
    {
        [Key]
        [StringLength(40)]
        public string ID { get; set; }
        [StringLength(40)]
        public string ProjectId { get; set; }

        [StringLength(254)]
        [Required]
        public string Title { get; set; }

        [StringLength(1024)]
        [Required]
        public string Body { get; set; }

        [StringLength(40)]
        public string Author { get; set; }
        public DateTime? Created { get; set; }
    }
}
