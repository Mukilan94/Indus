using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportWellTestInfo
    {
        public DrillReportWellTestInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int WellTestInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string TestType { get; set; }
        public string TestNumber { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomDrillReportMdBottomId { get; set; }
        public int? TvdTopDrillReportTvdTopId { get; set; }
        public int? TvdBottomId { get; set; }
        public int? ChokeOrificeSizeId { get; set; }
        public int? DensityOilId { get; set; }
        public int? DensityWaterId { get; set; }
        public int? DensityGasId { get; set; }
        public int? FlowRateOilId { get; set; }
        public int? FlowRateWaterId { get; set; }
        public int? FlowRateGasId { get; set; }
        public int? PresShutInId { get; set; }
        public int? PresFlowingId { get; set; }
        public int? PresBottomId { get; set; }
        public int? GasOilRatioId { get; set; }
        public int? WaterOilRatioId { get; set; }
        public int? ChlorideId { get; set; }
        public int? CarbonDioxideId { get; set; }
        public int? HydrogenSulfideId { get; set; }
        public int? VolOilTotalId { get; set; }
        public int? VolGasTotalId { get; set; }
        public int? VolWaterTotalId { get; set; }
        public int? VolOilStoredId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CarbonDioxideId))]
        [InverseProperty(nameof(DrillReportCarbonDioxide.DrillReportWellTestInfo))]
        public virtual DrillReportCarbonDioxide CarbonDioxide { get; set; }
        [ForeignKey(nameof(ChlorideId))]
        [InverseProperty(nameof(DrillReportChloride.DrillReportWellTestInfo))]
        public virtual DrillReportChloride Chloride { get; set; }
        [ForeignKey(nameof(ChokeOrificeSizeId))]
        [InverseProperty(nameof(DrillReportChokeOrificeSize.DrillReportWellTestInfo))]
        public virtual DrillReportChokeOrificeSize ChokeOrificeSize { get; set; }
        [ForeignKey(nameof(DensityGasId))]
        [InverseProperty(nameof(DrillReportDensityGas.DrillReportWellTestInfo))]
        public virtual DrillReportDensityGas DensityGas { get; set; }
        [ForeignKey(nameof(DensityOilId))]
        [InverseProperty(nameof(DrillReportDensityOil.DrillReportWellTestInfo))]
        public virtual DrillReportDensityOil DensityOil { get; set; }
        [ForeignKey(nameof(DensityWaterId))]
        [InverseProperty(nameof(DrillReportDensityWater.DrillReportWellTestInfo))]
        public virtual DrillReportDensityWater DensityWater { get; set; }
        [ForeignKey(nameof(FlowRateGasId))]
        [InverseProperty(nameof(DrillReportFlowRateGas.DrillReportWellTestInfo))]
        public virtual DrillReportFlowRateGas FlowRateGas { get; set; }
        [ForeignKey(nameof(FlowRateOilId))]
        [InverseProperty(nameof(DrillReportFlowRateOil.DrillReportWellTestInfo))]
        public virtual DrillReportFlowRateOil FlowRateOil { get; set; }
        [ForeignKey(nameof(FlowRateWaterId))]
        [InverseProperty(nameof(DrillReportFlowRateWater.DrillReportWellTestInfo))]
        public virtual DrillReportFlowRateWater FlowRateWater { get; set; }
        [ForeignKey(nameof(GasOilRatioId))]
        [InverseProperty(nameof(DrillReportGasOilRatio.DrillReportWellTestInfo))]
        public virtual DrillReportGasOilRatio GasOilRatio { get; set; }
        [ForeignKey(nameof(HydrogenSulfideId))]
        [InverseProperty(nameof(DrillReportHydrogenSulfide.DrillReportWellTestInfo))]
        public virtual DrillReportHydrogenSulfide HydrogenSulfide { get; set; }
        [ForeignKey(nameof(MdBottomDrillReportMdBottomId))]
        [InverseProperty(nameof(DrillReportMdBottom.DrillReportWellTestInfo))]
        public virtual DrillReportMdBottom MdBottomDrillReportMdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(DrillReportMdTop.DrillReportWellTestInfo))]
        public virtual DrillReportMdTop MdTop { get; set; }
        [ForeignKey(nameof(PresBottomId))]
        [InverseProperty(nameof(DrillReportPresBottom.DrillReportWellTestInfo))]
        public virtual DrillReportPresBottom PresBottom { get; set; }
        [ForeignKey(nameof(PresFlowingId))]
        [InverseProperty(nameof(DrillReportPresFlowing.DrillReportWellTestInfo))]
        public virtual DrillReportPresFlowing PresFlowing { get; set; }
        [ForeignKey(nameof(PresShutInId))]
        [InverseProperty(nameof(DrillReportPresShutIn.DrillReportWellTestInfo))]
        public virtual DrillReportPresShutIn PresShutIn { get; set; }
        [ForeignKey(nameof(TvdBottomId))]
        [InverseProperty(nameof(DrillReportTvdBottom.DrillReportWellTestInfo))]
        public virtual DrillReportTvdBottom TvdBottom { get; set; }
        [ForeignKey(nameof(TvdTopDrillReportTvdTopId))]
        [InverseProperty(nameof(DrillReportTvdTop.DrillReportWellTestInfo))]
        public virtual DrillReportTvdTop TvdTopDrillReportTvdTop { get; set; }
        [ForeignKey(nameof(VolGasTotalId))]
        [InverseProperty(nameof(DrillReportVolGasTotal.DrillReportWellTestInfo))]
        public virtual DrillReportVolGasTotal VolGasTotal { get; set; }
        [ForeignKey(nameof(VolOilStoredId))]
        [InverseProperty(nameof(DrillReportVolOilStored.DrillReportWellTestInfo))]
        public virtual DrillReportVolOilStored VolOilStored { get; set; }
        [ForeignKey(nameof(VolOilTotalId))]
        [InverseProperty(nameof(DrillReportVolOilTotal.DrillReportWellTestInfo))]
        public virtual DrillReportVolOilTotal VolOilTotal { get; set; }
        [ForeignKey(nameof(VolWaterTotalId))]
        [InverseProperty(nameof(DrillReportVolWaterTotal.DrillReportWellTestInfo))]
        public virtual DrillReportVolWaterTotal VolWaterTotal { get; set; }
        [ForeignKey(nameof(WaterOilRatioId))]
        [InverseProperty(nameof(DrillReportWaterOilRatio.DrillReportWellTestInfo))]
        public virtual DrillReportWaterOilRatio WaterOilRatio { get; set; }
        [InverseProperty("WellTestInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
