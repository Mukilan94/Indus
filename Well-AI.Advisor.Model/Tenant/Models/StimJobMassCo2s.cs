using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobMassCO2s")]
    public partial class StimJobMassCo2s
    {
        public StimJobMassCo2s()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
        }

        [Key]
        [Column("MassCO2Id")]
        public int MassCo2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MassCo2")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
    }
}
