using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportCTimDrillRots")]
    public partial class OpsReportCtimDrillRots
    {
        public OpsReportCtimDrillRots()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        [Column("CTimDrillRotId")]
        public int CtimDrillRotId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CtimDrillRot")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
