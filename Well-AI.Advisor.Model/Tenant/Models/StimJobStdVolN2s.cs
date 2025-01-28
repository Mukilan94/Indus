using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobStdVolN2s
    {
        public StimJobStdVolN2s()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
        }

        [Key]
        [Column("StdVolN2Id")]
        public int StdVolN2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StdVolN2")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
    }
}
