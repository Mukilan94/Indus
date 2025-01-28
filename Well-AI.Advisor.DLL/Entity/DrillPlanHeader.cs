using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("DrillPlanHeader")]
    public class DrillPlanHeader
    {
        [Key]
        public string DrillPlanId { get; set; }
        public string DrillPlanName { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanCompleteDate { get; set; }
        public DateTime? PlanCreateDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualCompletedDate { get; set; }
        public DateTime? PlanLastModifyDate { get; set; }
        public string PlanApprovedBy { get; set; }
        public string TenantId { get; set; }
        public bool Prediction { get; set;}
    }
}
