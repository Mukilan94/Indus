using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFractureCloseDurations
    {
        public StimJobFractureCloseDurations()
        {
            StimJobFluidEfficiencyTests = new HashSet<StimJobFluidEfficiencyTests>();
            StimJobPumpFlowBackTests = new HashSet<StimJobPumpFlowBackTests>();
        }

        [Key]
        public int FractureCloseDurationId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FractureCloseDuration")]
        public virtual ICollection<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
        [InverseProperty("FractureCloseDuration")]
        public virtual ICollection<StimJobPumpFlowBackTests> StimJobPumpFlowBackTests { get; set; }
    }
}
