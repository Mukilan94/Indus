using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCostItems
    {
        public OpsReportCostItems()
        {
            OpsReportBulks = new HashSet<OpsReportBulks>();
            OpsReportMudInventorys = new HashSet<OpsReportMudInventorys>();
        }

        [Key]
        public int CostItemId { get; set; }
        public string Currency { get; set; }
        public string Text { get; set; }

        [InverseProperty("CostItem")]
        public virtual ICollection<OpsReportBulks> OpsReportBulks { get; set; }
        [InverseProperty("CostItem")]
        public virtual ICollection<OpsReportMudInventorys> OpsReportMudInventorys { get; set; }
    }
}
