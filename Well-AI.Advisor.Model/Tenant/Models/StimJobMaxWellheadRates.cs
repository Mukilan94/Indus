using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMaxWellheadRates
    {
        public StimJobMaxWellheadRates()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int MaxWellheadRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxWellheadRate")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("MaxWellheadRate")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
