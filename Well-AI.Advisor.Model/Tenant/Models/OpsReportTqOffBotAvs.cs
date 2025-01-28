using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTqOffBotAvs
    {
        public OpsReportTqOffBotAvs()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        public int TqOffBotAvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TqOffBotAv")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
