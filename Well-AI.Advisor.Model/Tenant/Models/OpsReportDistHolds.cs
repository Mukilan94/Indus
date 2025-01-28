using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportDistHolds
    {
        public OpsReportDistHolds()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int DistHoldId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DistHold")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
        [InverseProperty("DistHold")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
