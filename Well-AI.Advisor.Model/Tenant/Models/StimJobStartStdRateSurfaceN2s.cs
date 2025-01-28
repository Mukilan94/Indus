using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobStartStdRateSurfaceN2s
    {
        public StimJobStartStdRateSurfaceN2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("StartStdRateSurfaceN2Id")]
        public int StartStdRateSurfaceN2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StartStdRateSurfaceN2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
