using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportItemWtPerUnits
    {
        public OpsReportItemWtPerUnits()
        {
            OpsReportMudInventorys = new HashSet<OpsReportMudInventorys>();
        }

        [Key]
        public int ItemWtPerUnitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ItemWtPerUnit")]
        public virtual ICollection<OpsReportMudInventorys> OpsReportMudInventorys { get; set; }
    }
}
