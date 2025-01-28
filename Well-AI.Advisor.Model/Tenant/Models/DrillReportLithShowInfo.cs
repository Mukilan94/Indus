using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportLithShowInfo
    {
        public DrillReportLithShowInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int LithShowInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomDrillReportMdBottomId { get; set; }
        public int? TvdTopDrillReportTvdTopId { get; set; }
        public int? TvdBottomId { get; set; }
        public string Show { get; set; }
        public string Lithology { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(MdBottomDrillReportMdBottomId))]
        [InverseProperty(nameof(DrillReportMdBottom.DrillReportLithShowInfo))]
        public virtual DrillReportMdBottom MdBottomDrillReportMdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(DrillReportMdTop.DrillReportLithShowInfo))]
        public virtual DrillReportMdTop MdTop { get; set; }
        [ForeignKey(nameof(TvdBottomId))]
        [InverseProperty(nameof(DrillReportTvdBottom.DrillReportLithShowInfo))]
        public virtual DrillReportTvdBottom TvdBottom { get; set; }
        [ForeignKey(nameof(TvdTopDrillReportTvdTopId))]
        [InverseProperty(nameof(DrillReportTvdTop.DrillReportLithShowInfo))]
        public virtual DrillReportTvdTop TvdTopDrillReportTvdTop { get; set; }
        [InverseProperty("LithShowInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
