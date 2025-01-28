using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAvgPmaxPacPress
    {
        public StimJobAvgPmaxPacPress()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
        }

        [Key]
        public int AvgPmaxPacPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgPmaxPacPres")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
    }
}
