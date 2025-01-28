using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class DrillPlandetailsViewModel
    {
        public string DrillingPlanId { get; set; }
        public string DrillingPlanName { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public bool Predictable { get; set; }
        public string WellIdList { get; set; }
        public List<string> WellId { get; set; }
        public List<DateTime>  RigRealese { get; set; }
        public List<DateTime> SpudWell { get; set; }
        public List<DateTime> LastBopTest { get; set; }
        public List<DateTime> NextBopTest { get; set; }
        public List<string> PlannedTD { get; set; }
        public List<string>  RigId{ get; set; }
    }


    public class DrillPlanWelldetails
    {
        //public string DrillingPlanId { get; set; }
        //public string DrillingPlanName { get; set; }
        //public DateTime PlanStartDate { get; set; }
        //public bool Predictable { get; set; }
        public string WellId { get; set; }
        public DateTime? RigRealese { get; set; }
        public DateTime? SpudWell { get; set; }
        public DateTime? LastBopTest { get; set; }
        public DateTime? NextBopTest { get; set; }
        public string PlannedTD { get; set; }
        public string RigId { get; set; }
    }
}
