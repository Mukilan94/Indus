using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportIbut
    {
        public DrillReportIbut()
        {
            DrillReportGasReadingInfo = new HashSet<DrillReportGasReadingInfo>();
        }

        [Key]
        public int IbutId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Ibut")]
        public virtual ICollection<DrillReportGasReadingInfo> DrillReportGasReadingInfo { get; set; }
    }
}
