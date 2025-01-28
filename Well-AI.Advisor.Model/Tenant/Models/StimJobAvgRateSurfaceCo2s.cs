using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobAvgRateSurfaceCO2s")]
    public partial class StimJobAvgRateSurfaceCo2s
    {
        public StimJobAvgRateSurfaceCo2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("AvgRateSurfaceCO2Id")]
        public int AvgRateSurfaceCo2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgRateSurfaceCo2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
