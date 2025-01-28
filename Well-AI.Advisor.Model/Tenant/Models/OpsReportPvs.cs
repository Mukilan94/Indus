using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPvs
    {
        public OpsReportPvs()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        public int ReportPvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PvReportPv")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
