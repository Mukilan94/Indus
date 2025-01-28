using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPricePerUnits
    {
        public OpsReportPricePerUnits()
        {
            OpsReportBulks = new HashSet<OpsReportBulks>();
            OpsReportMudInventorys = new HashSet<OpsReportMudInventorys>();
        }

        [Key]
        public int PricePerUnitId { get; set; }
        public string Currency { get; set; }
        public string Text { get; set; }

        [InverseProperty("PricePerUnit")]
        public virtual ICollection<OpsReportBulks> OpsReportBulks { get; set; }
        [InverseProperty("PricePerUnit")]
        public virtual ICollection<OpsReportMudInventorys> OpsReportMudInventorys { get; set; }
    }
}
