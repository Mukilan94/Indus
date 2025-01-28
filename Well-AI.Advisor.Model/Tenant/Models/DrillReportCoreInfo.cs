using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportCoreInfo
    {
        public DrillReportCoreInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int CoreInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string CoreNumber { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomDrillReportMdBottomId { get; set; }
        public int? TvdTopDrillReportTvdTopId { get; set; }
        public int? TvdBottomId { get; set; }
        public int? LenRecoveredId { get; set; }
        public int? RecoverPcId { get; set; }
        public int? LenBarrelId { get; set; }
        public string InnerBarrelType { get; set; }
        public string CoreDescription { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(LenBarrelId))]
        [InverseProperty(nameof(DrillReportLenBarrel.DrillReportCoreInfo))]
        public virtual DrillReportLenBarrel LenBarrel { get; set; }
        [ForeignKey(nameof(LenRecoveredId))]
        [InverseProperty(nameof(DrillReportLenRecovered.DrillReportCoreInfo))]
        public virtual DrillReportLenRecovered LenRecovered { get; set; }
        [ForeignKey(nameof(MdBottomDrillReportMdBottomId))]
        [InverseProperty(nameof(DrillReportMdBottom.DrillReportCoreInfo))]
        public virtual DrillReportMdBottom MdBottomDrillReportMdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(DrillReportMdTop.DrillReportCoreInfo))]
        public virtual DrillReportMdTop MdTop { get; set; }
        [ForeignKey(nameof(RecoverPcId))]
        [InverseProperty(nameof(DrillReportRecoverPc.DrillReportCoreInfo))]
        public virtual DrillReportRecoverPc RecoverPc { get; set; }
        [ForeignKey(nameof(TvdBottomId))]
        [InverseProperty(nameof(DrillReportTvdBottom.DrillReportCoreInfo))]
        public virtual DrillReportTvdBottom TvdBottom { get; set; }
        [ForeignKey(nameof(TvdTopDrillReportTvdTopId))]
        [InverseProperty(nameof(DrillReportTvdTop.DrillReportCoreInfo))]
        public virtual DrillReportTvdTop TvdTopDrillReportTvdTop { get; set; }
        [InverseProperty("CoreInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
