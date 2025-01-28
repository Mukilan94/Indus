using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportCTimHolds")]
    public partial class OpsReportCtimHolds
    {
        public OpsReportCtimHolds()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        [Column("CTimHoldId")]
        public int CtimHoldId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CtimHold")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
