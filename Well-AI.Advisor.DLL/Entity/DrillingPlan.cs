using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("DrillingPlan")]
   public class DrillingPlan
    {
        [Key]
        public string DrillingPlanId { get; set; }
        public string DrillingPlanName { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanCompletedDate { get; set; }
        public DateTime PlanCreatedDate { get; set; }
        public DateTime PlanLastModified { get; set; }
        public string PlanCreatedBy { get; set; }
        public string PlanDetails { get; set; }
        public string RigId { get; set; }
        public string TenantId { get; set; }
    }
}
