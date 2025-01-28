using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFlowBackRates
    {
        public StimJobFlowBackRates()
        {
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int FlowBackRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FlowBackRate")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
