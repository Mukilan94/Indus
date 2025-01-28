using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMaxGelRates
    {
        public StimJobMaxGelRates()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int MaxGelRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxGelRate")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("MaxGelRate")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
