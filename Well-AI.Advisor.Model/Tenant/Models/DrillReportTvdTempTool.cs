using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportTvdTempTool
    {
        public DrillReportTvdTempTool()
        {
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
        }

        [Key]
        public int TvdTempToolId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdTempTool")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
    }
}
