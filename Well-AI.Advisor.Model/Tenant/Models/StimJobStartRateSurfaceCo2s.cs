using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobStartRateSurfaceCO2s")]
    public partial class StimJobStartRateSurfaceCo2s
    {
        public StimJobStartRateSurfaceCo2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("StartRateSurfaceCO2Id")]
        public int StartRateSurfaceCo2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StartRateSurfaceCo2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
