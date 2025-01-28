using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class TaskModel
    {
        [ScaffoldColumn(false)]
        public string TaskId { get; set; }
        [Required]
        public string Name { get; set; }
        //[Required]
        public string Description { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Day { get; set; }
        [Display(Name = "Schedule Time")]
        public System.TimeSpan? ScheduleTime { get; set; }
        //[Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Duration { get; set; }
        //[Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Depth { get; set; }
        //[Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? LeadTime { get; set; }
        [Display(Name = "Dependency")]
        public string SeletedDependency { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string IsSpecialServices { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsBiddable { get; set; }
        public string StageType { get; set; }
        public string StageTypeName { get; set; }
        [Required(ErrorMessage ="The service phase field is required")]
        public string ServiceCategoryId { get; set; }

        public string CategoryName { get; set; }

        //DWOP
        [Range(0, 3, ErrorMessage = "Days must be between 0 and 3")]
        public string ServiceDurationDays { get; set; }
        public string ServiceDurationHours { get; set; }
        public string ServiceDurationMinutes { get; set; }
        //public string ServiceDuration { get; set; }

        public string ServiceDuration { get; set; }
        public bool IsActiveCategory { get; set; }
        public bool ExportToMaster { get; set; }
        public int TaskOrder { get; set; }

        public bool IsCalendar { get; set; }
        public bool IsPreSpud { get; set; }
        public bool IsBenchMark { get; set; }

        public string Dependency { get; set; }
       
    }

    public class ServiceDuration
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
