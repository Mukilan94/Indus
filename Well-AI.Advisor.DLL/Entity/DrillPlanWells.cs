using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("DrillPlanWells")]
    public class DrillPlanWells
    {
        [Key]
        public string DrillPlanWellsId { get; set; }
        public string DrillPlanId { get; set; }
        public string Wellid { get; set; }
        public DateTime? RigRealese { get; set; }
        public DateTime? SPUDWell { get; set; }
        public DateTime? LastBOPTest { get; set; }
        public DateTime? NextBOPTest { get; set; }
        public string PlannedTD { get; set; }
        public string RigId { get; set; }

    }
}
