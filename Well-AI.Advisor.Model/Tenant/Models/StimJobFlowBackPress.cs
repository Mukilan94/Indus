using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFlowBackPress
    {
        public StimJobFlowBackPress()
        {
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int FlowBackPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FlowBackPres")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
