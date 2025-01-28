using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMudInventorys
    {
        public OpsReportMudInventorys()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int MudInventoryId { get; set; }
        public string Name { get; set; }
        public int? ItemWtPerUnitId { get; set; }
        public int? PricePerUnitId { get; set; }
        public string QtyStart { get; set; }
        public string QtyAdjustment { get; set; }
        public string QtyReceived { get; set; }
        public string QtyReturned { get; set; }
        public string QtyUsed { get; set; }
        public int? CostItemId { get; set; }
        public string QtyOnLocation { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CostItemId))]
        [InverseProperty(nameof(OpsReportCostItems.OpsReportMudInventorys))]
        public virtual OpsReportCostItems CostItem { get; set; }
        [ForeignKey(nameof(ItemWtPerUnitId))]
        [InverseProperty(nameof(OpsReportItemWtPerUnits.OpsReportMudInventorys))]
        public virtual OpsReportItemWtPerUnits ItemWtPerUnit { get; set; }
        [ForeignKey(nameof(PricePerUnitId))]
        [InverseProperty(nameof(OpsReportPricePerUnits.OpsReportMudInventorys))]
        public virtual OpsReportPricePerUnits PricePerUnit { get; set; }
        [InverseProperty("MudInventory")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
