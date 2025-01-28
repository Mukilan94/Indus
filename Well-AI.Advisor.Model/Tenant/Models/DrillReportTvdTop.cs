using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportTvdTop
    {
        public DrillReportTvdTop()
        {
            DrillReportCoreInfo = new HashSet<DrillReportCoreInfo>();
            DrillReportGasReadingInfo = new HashSet<DrillReportGasReadingInfo>();
            DrillReportLithShowInfo = new HashSet<DrillReportLithShowInfo>();
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
            DrillReportPerfInfo = new HashSet<DrillReportPerfInfo>();
            DrillReportStratInfo = new HashSet<DrillReportStratInfo>();
            DrillReportWellTestInfo = new HashSet<DrillReportWellTestInfo>();
        }

        [Key]
        public int DrillReportTvdTopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdTopDrillReportTvdTop")]
        public virtual ICollection<DrillReportCoreInfo> DrillReportCoreInfo { get; set; }
        [InverseProperty("TvdTopDrillReportTvdTop")]
        public virtual ICollection<DrillReportGasReadingInfo> DrillReportGasReadingInfo { get; set; }
        [InverseProperty("TvdTopDrillReportTvdTop")]
        public virtual ICollection<DrillReportLithShowInfo> DrillReportLithShowInfo { get; set; }
        [InverseProperty("TvdTopDrillReportTvdTop")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
        [InverseProperty("TvdTopDrillReportTvdTop")]
        public virtual ICollection<DrillReportPerfInfo> DrillReportPerfInfo { get; set; }
        [InverseProperty("TvdTopDrillReportTvdTop")]
        public virtual ICollection<DrillReportStratInfo> DrillReportStratInfo { get; set; }
        [InverseProperty("TvdTopDrillReportTvdTop")]
        public virtual ICollection<DrillReportWellTestInfo> DrillReportWellTestInfo { get; set; }
    }
}
