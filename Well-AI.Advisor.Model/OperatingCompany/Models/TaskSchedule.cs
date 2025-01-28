using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    [Table("TaskSchedule")]
    public class TaskSchedule
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [Required]
        public DateTime? Start { get; set; }

        [StringLength(255)]
        public string StartTimezone { get; set; }

        [Required]
        public DateTime? End { get; set; }
        public string tenantId { get; set; }
        [StringLength(255)]
        public string EndTimezone { get; set; }
        [StringLength(1024)]
        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        [StringLength(255)]
        public string RecurrenceException { get; set; }
        public bool? IsAllDay { get; set; }
    }
}
