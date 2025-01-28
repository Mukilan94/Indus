using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobBreakDownPress
    {
        public StimJobBreakDownPress()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int BreakDownPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BreakDownPres")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("BreakDownPres")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
