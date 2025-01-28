using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCostAmounts
    {
        public OpsReportCostAmounts()
        {
            OpsReportDayCosts = new HashSet<OpsReportDayCosts>();
        }

        [Key]
        public int CostAmountId { get; set; }
        public string Currency { get; set; }
        public string Text { get; set; }

        [InverseProperty("CostAmount")]
        public virtual ICollection<OpsReportDayCosts> OpsReportDayCosts { get; set; }
    }
}
