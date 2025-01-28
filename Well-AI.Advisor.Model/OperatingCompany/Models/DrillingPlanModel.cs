using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class DrillingPlanModel
    {
        public string DrillingPlanId { get; set; }
        public string DrillingPlanName { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanCompletedDate { get; set; }
        public DateTime PlanCreatedDate { get; set; }
        public DateTime PlanLastModified { get; set; }
        public DateTime PlanCreatedBy { get; set; }
        public List<PlanDetails> PlanDetails { get; set; }
        public string RigId { get; set; }
        public string TenantId { get; set; }
    }

    public class PlannedTasks
    {
        public string TaskId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string EmployeeId { get; set; }
    }


    public class PlannedTasksModel
    {
        [Required(ErrorMessage ="The Task Name field is required")]
        public string TaskName { get; set; }

        public string TaskId { get; set; }
        public bool? IsBiddable { get; set; }
        public decimal OperationDays { get; set; }
        public decimal OperationHours { get; set; }
        public decimal OperationDisplayHours { get; set; }
        public decimal? AccumulatedDays { get; set; }
        public string? EmployeeId { get; set; }
        public string? VendorName { get; set; }
        public string? Vendor { get; set; }
        public string? CategoryName { get; set; }
        public string? ServiceCategoryId { get; set; }
        public int? Depth { get; set; }
        public string? Description { get; set; }
        public byte? IsSpecialServices { get; set; }
        public string? SeletedDependency { get; set; }
        public string? ServiceDuration { get; set; }
        public string? StageTypeName { get; set; }
        public string? StageType { get; set; }
        public int? LeadTime { get; set; }
        public int? Day { get; set; }
        public bool IsActive { get; set; }
        public bool ExportToMaster { get; set; }
        //DWOP
        [Range(0, 3, ErrorMessage = "Days must be between 0 and 3")]
        public string? ServiceDurationDays { get; set; }
        public string? ServiceDurationHours { get; set; }
        public string? ServiceDurationMinutes { get; set; }

        public DateTime? PlanStart { get; set; }
        public DateTime? PlanFinishedDate { get; set; }
        public DateTime? ActualPlanStart { get; set; }
        public DateTime? ActualPlanFinishedDate { get; set; }
        public string? EmployeeName { get; set; }
        public string? commands { get; set; }
        public string? Serviceoperator { get; set; }
        public string? Category { get; set; }
        public string? stage { get; set; }
        public string? StageName { get; set; }
        public bool IsPlanTask { get; set; }
        public System.TimeSpan? ScheduleTimePicker { get; set; }
        public string? ScheduleTime { get; set; }
        public string? Dependency { get; set; }
        public int? TaskOrder { get; set; }
        public int? existingTaskOrder { get; set; }
        public bool? IsRowModified { get; set; }
        public bool IsPreSpud { get; set; }
        public bool IsBenchMark { get; set; }
        public string? PhoneNumber { get; set; }
        public int InsertIndex { get; set; }
       
    }

    public class Employeelist
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class Stagelist
    {
        public string stage { get; set; }
        public string StageName { get; set; }
    }

    public class Categorylist
    {
        public string Category { get; set; }
        public string CategoryName { get; set; }
    }

    public class PlanDetails
    {
        public string WellId { get; set; }
        public List<PlannedTasks> PlannedTasks { get; set; }

    }

    public class DrillPlanTasks
    {
        public List<PlanDetails> PlanDetails { get; set; }
    }

    public class wellmodel
    {
        public string wellId { get; set; }
        public string wellName { get; set; }
    }



    public class DrillingPlanList
    {
        public string DrillingPlanId { get; set; }
        [Required(ErrorMessage ="Please Enter Plan Name.")]
        public string DrillingPlanName { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanCompletedDate { get; set; }
        public DateTime? PlanCreatedDate { get; set; }
        public DateTime? PlanLastModified { get; set; }
        public DateTime? PlanCreatedBy { get; set; }
        public string RigId { get; set; }
        public string TenantId { get; set; }
        public string TaskId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string EmployeeId { get; set; }
        public string WellId { get; set; }
        public string WellName { get; set; }
        public int WellCounts { get; set; }
        public bool IsActive { get; set; }
        public DateTime? RigRelease { get; set; }
        public DateTime? SpudWell { get; set; }
        public DateTime? LastBopTest { get; set; }
        public DateTime? NextBopTest { get; set; }
        public string RigName { get; set; }
        public string PlannedTD { get; set; }
        public List<wellmodel> WellIds { get; set; }
        public List<string> WellNames { get; set; }
        public bool Predictable { get; set; }
        public string WellIdList { get; set; }
    }


    public class PlanDetailsModel
    {
        public string DrillingPlanId { get; set; }
        public string DrillingPlanName { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public bool? Prediction { get; set; }
        public string WellId { get; set; }
        public DateTime? RigRealese { get; set; }
        public DateTime? SpudWell { get; set; }
        public DateTime? LastBopTest { get; set; }
        public DateTime? NextBopTest { get; set; }
        public string PlannedTD { get; set; }
        public string RigId { get; set; }
        public List<PlannedTasksModel> drillPlanTasks { get; set; }
        public List<string> DeleteTasks { get; set; }
        public string TenantId { get; set; }
        public bool isStageUpdate { get; set; }
    }

    public class StageChartData
    {
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanFinishedDate { get; set; }
        public DateTime? ActualPlanStart { get; set; }
        public DateTime? ActualFinished { get; set; }
        public double? PlanFinishedOperationHours { get; set; }
        public double? ActualOperationFinishedHours { get; set; }
        public string TaskName { get; set; }
        public string CategoryName { get; set; }
        public string StageTypeName { get; set; }
    }

    public class ChecklistReview : WellAI.Advisor.Model.Administration.ChecklistTemplateModel
    {
        public string PlanId { get; set; }
        public string WellId { get; set; }
        public string WellTypeId { get; set; }
        public string ChecklistTemplateId { get; set; }
        public string PlanWellsId { get; set; }
    }
}
