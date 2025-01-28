using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportPerfInfo
    {
        public DrillReportPerfInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int PerfInfoId { get; set; }
        [Column("DTimOpen")]
        public string DtimOpen { get; set; }
        [Column("DTimClose")]
        public string DtimClose { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomDrillReportMdBottomId { get; set; }
        public int? TvdTopDrillReportTvdTopId { get; set; }
        public int? TvdBottomId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(MdBottomDrillReportMdBottomId))]
        [InverseProperty(nameof(DrillReportMdBottom.DrillReportPerfInfo))]
        public virtual DrillReportMdBottom MdBottomDrillReportMdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(DrillReportMdTop.DrillReportPerfInfo))]
        public virtual DrillReportMdTop MdTop { get; set; }
        [ForeignKey(nameof(TvdBottomId))]
        [InverseProperty(nameof(DrillReportTvdBottom.DrillReportPerfInfo))]
        public virtual DrillReportTvdBottom TvdBottom { get; set; }
        [ForeignKey(nameof(TvdTopDrillReportTvdTopId))]
        [InverseProperty(nameof(DrillReportTvdTop.DrillReportPerfInfo))]
        public virtual DrillReportTvdTop TvdTopDrillReportTvdTop { get; set; }
        [InverseProperty("PerfInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
