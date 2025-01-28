using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAvgStdRateSurfaceN2s
    {
        public StimJobAvgStdRateSurfaceN2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("AvgStdRateSurfaceN2Id")]
        public int AvgStdRateSurfaceN2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgStdRateSurfaceN2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
