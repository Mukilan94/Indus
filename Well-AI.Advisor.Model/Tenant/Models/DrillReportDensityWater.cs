using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportDensityWater
    {
        public DrillReportDensityWater()
        {
            DrillReportWellTestInfo = new HashSet<DrillReportWellTestInfo>();
        }

        [Key]
        public int DensityWaterId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DensityWater")]
        public virtual ICollection<DrillReportWellTestInfo> DrillReportWellTestInfo { get; set; }
    }
}
