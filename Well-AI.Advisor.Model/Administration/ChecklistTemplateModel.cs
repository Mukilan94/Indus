using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class ChecklistTemplateModel
    {
        [Key]
        public string TaskId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Depth { get; set; }
        public string SeletedDependency { get; set; }
        public string IsSpecialServices { get; set; }
        //public string WellType { get; set; }
        public string StageType { get; set; }
        public string StageTypeName { get; set; }
        public string CategoryName { get; set; }
        public string ServiceCategoryId { get; set; }
        public string ServiceDuration { get; set; }
        public int TaskOrder { get; set; }
        public string WellDesignId { get; set; }
        public string WellDesignName { get; set; }

        //DWOP
        [Range(0, 3, ErrorMessage = "Days must be between 0 and 3")]
        public string ServiceDurationDays { get; set; }
        public string ServiceDurationHours { get; set; }
        public string ServiceDurationMinutes { get; set; }
        //DWOP
        public bool IsActiveCategory { get; set; }
        public bool ExportToMaster { get; set; }
        public int? LeadTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsBiddable { get; set; }
        //[Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Day { get; set; }
        [Display(Name = "Schedule Time")]
        public System.TimeSpan? ScheduleTime { get; set; }
        public bool IsPreSpud { get; set; }
        public bool IsCalendar { get; set; }
        public bool IsBenchMark { get; set; }
    }

    public class Duration
    {
        public int Text { get; set; }
        public int Value { get; set; }
    }
}