using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportCTimSteerings")]
    public partial class OpsReportCtimSteerings
    {
        public OpsReportCtimSteerings()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        [Column("CTimSteeringId")]
        public int CtimSteeringId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CtimSteering")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
