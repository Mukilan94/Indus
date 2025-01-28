using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportWaterOilRatio
    {
        public DrillReportWaterOilRatio()
        {
            DrillReportWellTestInfo = new HashSet<DrillReportWellTestInfo>();
        }

        [Key]
        public int WaterOilRatioId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("WaterOilRatio")]
        public virtual ICollection<DrillReportWellTestInfo> DrillReportWellTestInfo { get; set; }
    }
}
