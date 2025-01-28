using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobShutinPres10Mins
    {
        public StimJobShutinPres10Mins()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
        }

        [Key]
        public int ShutinPres10MinId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ShutinPres10Min")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
    }
}
