using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMaxAcidRates
    {
        public StimJobMaxAcidRates()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int MaxAcidRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxAcidRate")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("MaxAcidRate")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
