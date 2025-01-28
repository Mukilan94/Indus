using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportMdBottom
    {
        public DrillReportMdBottom()
        {
            DrillReportCoreInfo = new HashSet<DrillReportCoreInfo>();
            DrillReportGasReadingInfo = new HashSet<DrillReportGasReadingInfo>();
            DrillReportLithShowInfo = new HashSet<DrillReportLithShowInfo>();
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
            DrillReportPerfInfo = new HashSet<DrillReportPerfInfo>();
            DrillReportWellTestInfo = new HashSet<DrillReportWellTestInfo>();
        }

        [Key]
        public int DrillReportMdBottomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdBottomDrillReportMdBottom")]
        public virtual ICollection<DrillReportCoreInfo> DrillReportCoreInfo { get; set; }
        [InverseProperty("MdBottomDrillReportMdBottom")]
        public virtual ICollection<DrillReportGasReadingInfo> DrillReportGasReadingInfo { get; set; }
        [InverseProperty("MdBottomDrillReportMdBottom")]
        public virtual ICollection<DrillReportLithShowInfo> DrillReportLithShowInfo { get; set; }
        [InverseProperty("MdBottomDrillReportMdBottom")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
        [InverseProperty("MdBottomDrillReportMdBottom")]
        public virtual ICollection<DrillReportPerfInfo> DrillReportPerfInfo { get; set; }
        [InverseProperty("MdBottomDrillReportMdBottom")]
        public virtual ICollection<DrillReportWellTestInfo> DrillReportWellTestInfo { get; set; }
    }
}
