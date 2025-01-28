using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTqOnBotMxs
    {
        public OpsReportTqOnBotMxs()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        public int TqOnBotMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TqOnBotMx")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
