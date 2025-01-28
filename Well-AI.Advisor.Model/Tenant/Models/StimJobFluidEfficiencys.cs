using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFluidEfficiencys
    {
        public StimJobFluidEfficiencys()
        {
            StimJobFluidEfficiencyTests = new HashSet<StimJobFluidEfficiencyTests>();
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int FluidEfficiencyId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FluidEfficiency")]
        public virtual ICollection<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
        [InverseProperty("FluidEfficiency")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
        [InverseProperty("FluidEfficiency")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
