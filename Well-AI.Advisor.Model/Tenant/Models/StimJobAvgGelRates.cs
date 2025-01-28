using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAvgGelRates
    {
        public StimJobAvgGelRates()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int AvgGelRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgGelRate")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("AvgGelRate")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
