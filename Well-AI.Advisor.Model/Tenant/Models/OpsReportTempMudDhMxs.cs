using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTempMudDhMxs
    {
        public OpsReportTempMudDhMxs()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        public int TempMudDhMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempMudDhMx")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
