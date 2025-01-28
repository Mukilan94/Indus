using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportDayCosts
    {
        [Key]
        public int DayCostId { get; set; }
        [Column("NumAFE")]
        public string NumAfe { get; set; }
        public string CostGroup { get; set; }
        public string CostClass { get; set; }
        public string CostCode { get; set; }
        public string CostSubCode { get; set; }
        public string CostItemDescription { get; set; }
        public int? CostPerItemId { get; set; }
        public string ItemKind { get; set; }
        public string ItemSize { get; set; }
        public string QtyItem { get; set; }
        public int? CostAmountId { get; set; }
        public string NumInvoice { get; set; }
        [Column("NumPO")]
        public string NumPo { get; set; }
        public string NumTicket { get; set; }
        public string IsCarryOver { get; set; }
        public string IsRental { get; set; }
        public string NumSerial { get; set; }
        public string NameVendor { get; set; }
        public string NumVendor { get; set; }
        public string Pool { get; set; }
        public string Estimated { get; set; }
        public string Uid { get; set; }
        public int? OpsReportId { get; set; }

        [ForeignKey(nameof(CostAmountId))]
        [InverseProperty(nameof(OpsReportCostAmounts.OpsReportDayCosts))]
        public virtual OpsReportCostAmounts CostAmount { get; set; }
        [ForeignKey(nameof(CostPerItemId))]
        [InverseProperty(nameof(OpsReportCostPerItems.OpsReportDayCosts))]
        public virtual OpsReportCostPerItems CostPerItem { get; set; }
        [ForeignKey(nameof(OpsReportId))]
        [InverseProperty(nameof(OpsReports.OpsReportDayCosts))]
        public virtual OpsReports OpsReport { get; set; }
    }
}
