using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportGasReadingInfo
    {
        public DrillReportGasReadingInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int GasReadingInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string ReadingType { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomDrillReportMdBottomId { get; set; }
        public int? TvdTopDrillReportTvdTopId { get; set; }
        public int? TvdBottomId { get; set; }
        public int? GasHighId { get; set; }
        public int? GasLowId { get; set; }
        public int? MethId { get; set; }
        public int? EthId { get; set; }
        public int? PropId { get; set; }
        public int? IbutId { get; set; }
        public int? NbutId { get; set; }
        public int? IpentId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(EthId))]
        [InverseProperty(nameof(DrillReportEth.DrillReportGasReadingInfo))]
        public virtual DrillReportEth Eth { get; set; }
        [ForeignKey(nameof(GasHighId))]
        [InverseProperty(nameof(DrillReportGasHigh.DrillReportGasReadingInfo))]
        public virtual DrillReportGasHigh GasHigh { get; set; }
        [ForeignKey(nameof(GasLowId))]
        [InverseProperty(nameof(DrillReportGasLow.DrillReportGasReadingInfo))]
        public virtual DrillReportGasLow GasLow { get; set; }
        [ForeignKey(nameof(IbutId))]
        [InverseProperty(nameof(DrillReportIbut.DrillReportGasReadingInfo))]
        public virtual DrillReportIbut Ibut { get; set; }
        [ForeignKey(nameof(IpentId))]
        [InverseProperty(nameof(DrillReportIpent.DrillReportGasReadingInfo))]
        public virtual DrillReportIpent Ipent { get; set; }
        [ForeignKey(nameof(MdBottomDrillReportMdBottomId))]
        [InverseProperty(nameof(DrillReportMdBottom.DrillReportGasReadingInfo))]
        public virtual DrillReportMdBottom MdBottomDrillReportMdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(DrillReportMdTop.DrillReportGasReadingInfo))]
        public virtual DrillReportMdTop MdTop { get; set; }
        [ForeignKey(nameof(MethId))]
        [InverseProperty(nameof(DrillReportMeth.DrillReportGasReadingInfo))]
        public virtual DrillReportMeth Meth { get; set; }
        [ForeignKey(nameof(NbutId))]
        [InverseProperty(nameof(DrillReportNbut.DrillReportGasReadingInfo))]
        public virtual DrillReportNbut Nbut { get; set; }
        [ForeignKey(nameof(PropId))]
        [InverseProperty(nameof(DrillReportProp.DrillReportGasReadingInfo))]
        public virtual DrillReportProp Prop { get; set; }
        [ForeignKey(nameof(TvdBottomId))]
        [InverseProperty(nameof(DrillReportTvdBottom.DrillReportGasReadingInfo))]
        public virtual DrillReportTvdBottom TvdBottom { get; set; }
        [ForeignKey(nameof(TvdTopDrillReportTvdTopId))]
        [InverseProperty(nameof(DrillReportTvdTop.DrillReportGasReadingInfo))]
        public virtual DrillReportTvdTop TvdTopDrillReportTvdTop { get; set; }
        [InverseProperty("GasReadingInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
