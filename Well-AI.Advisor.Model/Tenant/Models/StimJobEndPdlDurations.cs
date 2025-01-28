using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobEndPdlDurations
    {
        public StimJobEndPdlDurations()
        {
            StimJobFluidEfficiencyTests = new HashSet<StimJobFluidEfficiencyTests>();
        }

        [Key]
        public int EndPdlDurationId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndPdlDuration")]
        public virtual ICollection<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
    }
}
