using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportLogInfo
    {
        [Key]
        public int LogInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string RunNumber { get; set; }
        public string ServiceCompany { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomDrillReportMdBottomId { get; set; }
        public int? TvdTopDrillReportTvdTopId { get; set; }
        public int? TvdBottomId { get; set; }
        public string Tool { get; set; }
        [Column("TempBHCTId")]
        public int? TempBhctid { get; set; }
        public int? MdTempToolId { get; set; }
        public int? TvdTempToolId { get; set; }
        public string Uid { get; set; }
        [Column("TempBHSTId")]
        public int? TempBhstid { get; set; }
        [Column("ETimStaticId")]
        public int? EtimStaticId { get; set; }
        public int? DrillReportId { get; set; }

        [ForeignKey(nameof(DrillReportId))]
        [InverseProperty(nameof(DrillReports.DrillReportLogInfo))]
        public virtual DrillReports DrillReport { get; set; }
        [ForeignKey(nameof(EtimStaticId))]
        [InverseProperty(nameof(DrillReportEtimStatic.DrillReportLogInfo))]
        public virtual DrillReportEtimStatic EtimStatic { get; set; }
        [ForeignKey(nameof(MdBottomDrillReportMdBottomId))]
        [InverseProperty(nameof(DrillReportMdBottom.DrillReportLogInfo))]
        public virtual DrillReportMdBottom MdBottomDrillReportMdBottom { get; set; }
        [ForeignKey(nameof(MdTempToolId))]
        [InverseProperty(nameof(DrillReportMdTempTool.DrillReportLogInfo))]
        public virtual DrillReportMdTempTool MdTempTool { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(DrillReportMdTop.DrillReportLogInfo))]
        public virtual DrillReportMdTop MdTop { get; set; }
        [ForeignKey(nameof(TempBhctid))]
        [InverseProperty(nameof(DrillReportTempBhct.DrillReportLogInfo))]
        public virtual DrillReportTempBhct TempBhct { get; set; }
        [ForeignKey(nameof(TempBhstid))]
        [InverseProperty(nameof(DrillReportTempBhst.DrillReportLogInfo))]
        public virtual DrillReportTempBhst TempBhst { get; set; }
        [ForeignKey(nameof(TvdBottomId))]
        [InverseProperty(nameof(DrillReportTvdBottom.DrillReportLogInfo))]
        public virtual DrillReportTvdBottom TvdBottom { get; set; }
        [ForeignKey(nameof(TvdTempToolId))]
        [InverseProperty(nameof(DrillReportTvdTempTool.DrillReportLogInfo))]
        public virtual DrillReportTvdTempTool TvdTempTool { get; set; }
        [ForeignKey(nameof(TvdTopDrillReportTvdTopId))]
        [InverseProperty(nameof(DrillReportTvdTop.DrillReportLogInfo))]
        public virtual DrillReportTvdTop TvdTopDrillReportTvdTop { get; set; }
    }
}
