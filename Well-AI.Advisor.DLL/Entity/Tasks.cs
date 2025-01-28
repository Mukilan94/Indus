using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        public string TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Day { get; set; }
        public System.TimeSpan? ScheduleTime { get; set; }
        public int? Duration { get; set; }
        public int? Depth { get; set; }
        public int? LeadTime { get; set; }
        public string Dependency { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int IsSpecialServices { get; set; }
        public bool IsBiddable { get; set; }
        public string StageType { get; set; }
        public string ServiceCategoryId { get; set; }
        //DWOP
        public string ServiceDuration { get; set; }
        public bool IsCalendar { get; set; }
        public bool IsPreSpud { get; set; }
        public bool IsBenchMark { get; set; }
    }
}
