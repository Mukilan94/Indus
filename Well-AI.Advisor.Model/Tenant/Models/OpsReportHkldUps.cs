using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportHkldUps
    {
        public OpsReportHkldUps()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        public int HkldUpId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("HkldUp")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
