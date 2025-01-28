using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobMaxN2StdRates")]
    public partial class StimJobMaxN2stdRates
    {
        public StimJobMaxN2stdRates()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("MaxN2StdRateId")]
        public int MaxN2stdRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxN2stdRate")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("MaxN2stdRate")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
