using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobNetPress
    {
        public StimJobNetPress()
        {
            StimJobFluidEfficiencyTests = new HashSet<StimJobFluidEfficiencyTests>();
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int NetPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("NetPres")]
        public virtual ICollection<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
        [InverseProperty("NetPres")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
