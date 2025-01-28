using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobWellheadVols
    {
        public StimJobWellheadVols()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("StdVolN2Id")]
        public int StdVolN2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("WellheadVolStdVolN2")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("WellheadVolStdVolN2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
