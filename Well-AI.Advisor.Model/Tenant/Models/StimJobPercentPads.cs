using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPercentPads
    {
        public StimJobPercentPads()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
        }

        [Key]
        public int PercentPadId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PercentPad")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("PercentPad")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
    }
}
