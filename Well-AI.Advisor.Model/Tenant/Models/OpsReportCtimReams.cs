using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportCTimReams")]
    public partial class OpsReportCtimReams
    {
        public OpsReportCtimReams()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        [Column("CTimReamId")]
        public int CtimReamId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CtimReam")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
