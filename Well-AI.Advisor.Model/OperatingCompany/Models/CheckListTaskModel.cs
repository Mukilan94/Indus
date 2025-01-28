using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class CheckListTaskModel
    {
        [Required]
        public string WellId { get; set; }
        public string TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Day { get; set; }
        [Display(Name = "Schedule Time")]
        public System.TimeSpan? ScheduleTime { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Duration { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Depth { get; set; }
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

        [Required]
        public string RigId { get; set;}
        public string StageName { get; set; }
        public string CategoryName { get; set; }
    }
}
