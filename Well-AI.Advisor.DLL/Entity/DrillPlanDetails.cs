using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("DrillPlanDetails")]
   public class DrillPlanDetails
    {
        [Key]
        public string DrillPlanDetailsId { get; set; }
        public string DrillPlanWellsId { get; set; }
        public string DrillPlanId { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public Decimal? OperationHours { get; set; }
        public DateTime? PlanFinishedDate { get; set; }
        public DateTime? ActualFinishedDate { get; set; }
        public string? EmployeeId { get; set; }
        public string? Comments { get; set; }
        public string ServiceOperatorId { get; set; }
        public int? TaskOrder { get; set; }
        public string? CategoryId { get; set; }
        public string? StageId { get; set; }
        public bool IsPlanTask { get; set; }
        public string? Description { get; set; }
        public int? Day { get; set; }
        public System.TimeSpan? ScheduleTime { get; set; }
        public int? Depth { get; set; }
        public int? LeadTime { get; set; }
        public string? Dependency { get; set; }
        public string? ServiceDuration { get; set; }
        public byte? IsSpecialServices { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsBiddable { get; set; }
        public bool? IsPreSpud { get; set; }
        public bool? IsBenchMark { get; set; }
    }
}
