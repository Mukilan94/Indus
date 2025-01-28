using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportBulks
    {
        public OpsReportBulks()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int BulkId { get; set; }
        public string Name { get; set; }
        public int? ItemVolPerUnitId { get; set; }
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
        [InverseProperty(nameof(OpsReportCostItems.OpsReportBulks))]
        public virtual OpsReportCostItems CostItem { get; set; }
        [ForeignKey(nameof(ItemVolPerUnitId))]
        [InverseProperty(nameof(OpsReportItemVolPerUnits.OpsReportBulks))]
        public virtual OpsReportItemVolPerUnits ItemVolPerUnit { get; set; }
        [ForeignKey(nameof(PricePerUnitId))]
        [InverseProperty(nameof(OpsReportPricePerUnits.OpsReportBulks))]
        public virtual OpsReportPricePerUnits PricePerUnit { get; set; }
        [InverseProperty("Bulk")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
