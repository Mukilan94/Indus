using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportTvdBottom
    {
        public DrillReportTvdBottom()
        {
            DrillReportCoreInfo = new HashSet<DrillReportCoreInfo>();
            DrillReportGasReadingInfo = new HashSet<DrillReportGasReadingInfo>();
            DrillReportLithShowInfo = new HashSet<DrillReportLithShowInfo>();
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
            DrillReportPerfInfo = new HashSet<DrillReportPerfInfo>();
            DrillReportWellTestInfo = new HashSet<DrillReportWellTestInfo>();
        }

        [Key]
        public int TvdBottomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdBottom")]
        public virtual ICollection<DrillReportCoreInfo> DrillReportCoreInfo { get; set; }
        [InverseProperty("TvdBottom")]
        public virtual ICollection<DrillReportGasReadingInfo> DrillReportGasReadingInfo { get; set; }
        [InverseProperty("TvdBottom")]
        public virtual ICollection<DrillReportLithShowInfo> DrillReportLithShowInfo { get; set; }
        [InverseProperty("TvdBottom")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
        [InverseProperty("TvdBottom")]
        public virtual ICollection<DrillReportPerfInfo> DrillReportPerfInfo { get; set; }
        [InverseProperty("TvdBottom")]
        public virtual ICollection<DrillReportWellTestInfo> DrillReportWellTestInfo { get; set; }
    }
}
