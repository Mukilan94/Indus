using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportGasLow
    {
        public DrillReportGasLow()
        {
            DrillReportGasReadingInfo = new HashSet<DrillReportGasReadingInfo>();
        }

        [Key]
        public int GasLowId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GasLow")]
        public virtual ICollection<DrillReportGasReadingInfo> DrillReportGasReadingInfo { get; set; }
    }
}
