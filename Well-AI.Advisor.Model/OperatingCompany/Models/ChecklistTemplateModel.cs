using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    //DWOP

    public class ChecklistTemplateModel
    {
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TenantId { get; set; }
        public string Checklist { get; set; }
        public bool IsDefault { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUser { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string WellTypeId { get; set; }
        public string WellType { get; set; }
        public int? TaskCount { get; set; }
        public int? BopFrequency { get; set; }
        public string BOPFrequencyPermissionValue { get; set; }
    }

    public class ChecklistTaskTemplateModel
    {
        public string TaskId { get; set; }
        public string CategoryName { get; set; }
        public string ServiceCategoryId { get; set; }
        public int? Depth { get; set; }
        public string Description { get; set; }
        public string IsSpecialServices { get; set; }
        public string Name { get; set; }
        public string SeletedDependency { get; set; }
      
        public string ServiceDuration { get; set; }
        public string StageTypeName { get; set; }
        public string StageType { get; set; }
        public int TaskOrder { get; set; }
        public int? LeadTime { get; set; }
        public bool IsBiddable { get; set; }
        public int? Day { get; set; }
        public string ScheduleTime { get; set; }
        public bool? IsActive { get; set; }
        public bool ExportToMaster { get; set; }
        public bool IsPreSpud { get; set; }
        public bool IsBenchMark { get; set; }

        [Range(0, 3, ErrorMessage = "Days must be between 0 and 3")]
        public string ServiceDurationDays { get; set; }
        public string ServiceDurationHours { get; set; }
        public string ServiceDurationMinutes { get; set; }

        public bool IsActiveCategory { get; set; }
    }


    public class ChecklistModel
    {
        [Key]
        public string TaskId { get; set; }
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
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? Day { get; set; }
        public string? ScheduleTime { get; set; }
        public bool IsPreSpud { get; set; }
        public bool IsBenchMark { get; set; }
    }



    public class ChecklistTemplate
    {
        public string ChecklistTemplateId { get; set; }
        public string ChecklistTemplateName { get; set; }
        public string WellTypeId { get; set; }
        public bool IsDefault { get; set; }
        public List<string> DeletedTasks { get; set; }
        public List<ChecklistTaskTemplateModel> Checklist { get; set; }
        public int BopFrequency { get; set; }
    }


}
