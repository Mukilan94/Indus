using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
   public class ActiveDrillPlanModel
    {
        public string DrillingPlanId { get; set; }
        public string DrillingPlanName { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanCompletedDate { get; set; }
        public DateTime? PlanCreatedDate { get; set; }
        public DateTime? PlanLastModified { get; set; }
        public DateTime? PlanCreatedBy { get; set; }
        public string RigId { get; set; }
        public string TenantId { get; set; }
        public string TaskId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string EmployeeId { get; set; }
        public string WellId { get; set; }
        public string WellName { get; set; }
        public int WellCounts { get; set; }
        public bool IsActive { get; set; }
        public DateTime? RegRelease { get; set; }
        public DateTime? SpudWell { get; set; }
        public DateTime? LastBopTest { get; set; }
        public DateTime? NextBopTest { get; set; }
        public string RigName { get; set; }
        public string PlannedTD { get; set; }
        public List<string> WellIds { get; set; }
        public List<string> WellNames { get; set; }
        public bool Predictable { get; set; }
        public AddAuctionProposalViewModel AddAuction { get; set; }
    }

    public class ActiveDrilPlanTasks
    {
        public string TaskName { get; set; }
        public string TaskId { get; set; }
        public bool IsBiddable { get; set; }
        public double OperationDays { get; set; }
        public decimal? OperationHours { get; set; }
        public int? AccumulatedDays { get; set; }
        public string EmployeeId { get; set; }
        public string VendorName { get; set; }
        public string Vendor { get; set; }
        public string CategoryName { get; set; }
        public string ServiceCategoryId { get; set; }
        public int? Depth { get; set; }
        public string Description { get; set; }
        public byte? IsSpecialServices { get; set; }
        [Required]
        public string SeletedDependency { get; set; }
        public string ServiceDuration { get; set; }
        public string StageTypeName { get; set; }
        public string StageType { get; set; }
        public int? LeadTime { get; set; }
        public int? Day { get; set; }
        public bool IsActive { get; set; }
        public bool ExportToMaster { get; set; }
        public string ServiceDurationDays { get; set; }
        public string ServiceDurationHours { get; set; }
        public string ServiceDurationMinutes { get; set; }
        public DateTime? PlanStart { get; set; }
        public DateTime? PlanFinishedDate { get; set; }
        public DateTime? ActualPlanStart { get; set; }
        public DateTime? ActualPlanFinishedDate { get; set; }     
        public string EmployeeName { get; set; }
        public string commands { get; set; }
        public string Serviceoperator { get; set; }
        public string Category { get; set; }
        public string stage { get; set; }
        public string StageName { get; set; }
        public bool IsPlanTask { get; set; }
        public string ScheduleTime { get; set; }
        public string Dependency { get; set; }
        public int? TaskOrder { get; set; }
        public bool IsPreSpud { get; set; }
        public bool IsBenchMark { get; set; }
    }


    public class ActiveDrilPlanTasksViewModel
    {
        public string DrillPlanWellsId { get; set; }
        public string DrillPlanId { get; set; }
        public string Wellid { get; set; }
        public DateTime RigRealese { get; set; }
        public DateTime SPUDWell { get; set; }
        public DateTime LastBOPTest { get; set; }
        public DateTime NextBOPTest { get; set; }
        public string PlannedTD { get; set; }
        public string RigId { get; set; }
        public string Rigname { get; set; }
        public string WellName { get; set; }
        public List<ActiveDrilPlanTasks> ActiveDrilPlanTasks { get; set; }
    }
}
