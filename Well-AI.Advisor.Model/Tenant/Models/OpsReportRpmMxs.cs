using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportRpmMxs
    {
        public OpsReportRpmMxs()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        public int RpmMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RpmMx")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
