using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobShutinPres5Mins
    {
        public StimJobShutinPres5Mins()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
        }

        [Key]
        public int ShutinPres5MinId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ShutinPres5Min")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
    }
}
