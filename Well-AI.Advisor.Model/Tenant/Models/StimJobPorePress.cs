using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPorePress
    {
        public StimJobPorePress()
        {
            StimJobFluidEfficiencyTests = new HashSet<StimJobFluidEfficiencyTests>();
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
        }

        [Key]
        public int PorePresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PorePres")]
        public virtual ICollection<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
        [InverseProperty("PorePres")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
    }
}
