using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCostPerItems
    {
        public OpsReportCostPerItems()
        {
            OpsReportDayCosts = new HashSet<OpsReportDayCosts>();
        }

        [Key]
        public int CostPerItemId { get; set; }
        public string Currency { get; set; }
        public string Text { get; set; }

        [InverseProperty("CostPerItem")]
        public virtual ICollection<OpsReportDayCosts> OpsReportDayCosts { get; set; }
    }
}
