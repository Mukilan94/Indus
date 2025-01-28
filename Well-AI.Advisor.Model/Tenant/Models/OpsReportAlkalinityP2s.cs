using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportAlkalinityP2s
    {
        public OpsReportAlkalinityP2s()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        [Column("AlkalinityP2Id")]
        public int AlkalinityP2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AlkalinityP2")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
