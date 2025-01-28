using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFractureClosePress
    {
        public StimJobFractureClosePress()
        {
            StimJobFluidEfficiencyTests = new HashSet<StimJobFluidEfficiencyTests>();
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
            StimJobPumpFlowBackTests = new HashSet<StimJobPumpFlowBackTests>();
        }

        [Key]
        public int FractureClosePresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FractureClosePres")]
        public virtual ICollection<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
        [InverseProperty("FractureClosePres")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
        [InverseProperty("FractureClosePres")]
        public virtual ICollection<StimJobPumpFlowBackTests> StimJobPumpFlowBackTests { get; set; }
    }
}
