using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportItemVolPerUnits
    {
        public OpsReportItemVolPerUnits()
        {
            OpsReportBulks = new HashSet<OpsReportBulks>();
        }

        [Key]
        public int ItemVolPerUnitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ItemVolPerUnit")]
        public virtual ICollection<OpsReportBulks> OpsReportBulks { get; set; }
    }
}
