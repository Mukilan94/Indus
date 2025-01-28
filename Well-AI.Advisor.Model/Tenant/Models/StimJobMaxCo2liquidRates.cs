using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobMaxCO2LiquidRates")]
    public partial class StimJobMaxCo2liquidRates
    {
        public StimJobMaxCo2liquidRates()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("MaxCO2LiquidRateId")]
        public int MaxCo2liquidRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxCo2liquidRate")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("MaxCo2liquidRate")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
