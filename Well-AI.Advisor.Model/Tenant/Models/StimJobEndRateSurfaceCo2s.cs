using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobEndRateSurfaceCO2s")]
    public partial class StimJobEndRateSurfaceCo2s
    {
        public StimJobEndRateSurfaceCo2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("EndRateSurfaceCO2Id")]
        public int EndRateSurfaceCo2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndRateSurfaceCo2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
