using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobEndStdRateSurfaceN2s
    {
        public StimJobEndStdRateSurfaceN2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("EndStdRateSurfaceN2Id")]
        public int EndStdRateSurfaceN2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndStdRateSurfaceN2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
