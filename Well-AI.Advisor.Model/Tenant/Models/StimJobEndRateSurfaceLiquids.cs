using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobEndRateSurfaceLiquids
    {
        public StimJobEndRateSurfaceLiquids()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int EndRateSurfaceLiquidId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndRateSurfaceLiquid")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
