using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportChokeOrificeSize
    {
        public DrillReportChokeOrificeSize()
        {
            DrillReportWellTestInfo = new HashSet<DrillReportWellTestInfo>();
        }

        [Key]
        public int ChokeOrificeSizeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ChokeOrificeSize")]
        public virtual ICollection<DrillReportWellTestInfo> DrillReportWellTestInfo { get; set; }
    }
}
