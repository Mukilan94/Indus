using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class DrillPlanWellViewModel
    {
        public string DrillPlanWellsId { get; set; }
        public string DrillPlanId { get; set; }
        public string DrillPlanName { get; set; }
        public string Wellid { get; set; }
        public DateTime? RigRealese { get; set; }
        public DateTime? SPUDWell { get; set; }
        public DateTime? LastBOPTest { get; set; }
        public DateTime? NextBOPTest { get; set; }
        public string PlannedTD { get; set; }
        public string RigId { get; set; }
        public string Rigname { get; set; }
        public string WellName { get; set; }
        //Azure function
        public string TenantId { get; set; }
        public int BopFrequency { get; set; }
        public int CurrentRowIndex { get; set; }
    }
}
