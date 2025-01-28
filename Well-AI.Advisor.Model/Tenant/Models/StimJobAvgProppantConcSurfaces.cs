using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAvgProppantConcSurfaces
    {
        public StimJobAvgProppantConcSurfaces()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int AvgProppantConcSurfaceId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgProppantConcSurface")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
        [InverseProperty("AvgProppantConcSurface")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
