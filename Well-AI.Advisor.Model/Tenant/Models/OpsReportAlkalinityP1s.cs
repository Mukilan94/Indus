using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportAlkalinityP1s
    {
        public OpsReportAlkalinityP1s()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        [Column("AlkalinityP1Id")]
        public int AlkalinityP1id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AlkalinityP1")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
