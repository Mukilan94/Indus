using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
   public class DrillPlanDetailsModel
    {
        public int SlNum { get; set; }
        public string DrillPlanDetailsId { get; set; }
        public string DrillPlanWellsId { get; set; }
        public string DrillPlanId { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public decimal? OperationHours { get; set; }
        public decimal? OperationDays { get; set; }
        public decimal? AccumDays { get; set; }
        public DateTime? PlanFinishedDate { get; set; }
        public string EmployeeId { get; set; }
        public string Comments { get; set; }
        public string ServiceOperatorId { get; set; }
        public string BidId { get; set; }
        public int? TaskOrder { get; set; }
        public DateTime? ActualFinisheDate { get; set; }
    }
}
