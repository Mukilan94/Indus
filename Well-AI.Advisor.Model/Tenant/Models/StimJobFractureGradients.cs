using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFractureGradients
    {
        public StimJobFractureGradients()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int FractureGradientId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FractureGradient")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("FractureGradient")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
