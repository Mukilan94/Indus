using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportCTimDrillSlids")]
    public partial class OpsReportCtimDrillSlids
    {
        public OpsReportCtimDrillSlids()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        [Column("CTimDrillSlidId")]
        public int CtimDrillSlidId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CtimDrillSlid")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
