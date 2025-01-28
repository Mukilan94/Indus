using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAveragePresSurfaces
    {
        public StimJobAveragePresSurfaces()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int AveragePresSurfaceId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AveragePresSurface")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
