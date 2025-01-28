using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobJobStages
    {
        [Key]
        public int JobStageId { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        [Column("DTimStart")]
        public string DtimStart { get; set; }
        [Column("DTimEnd")]
        public string DtimEnd { get; set; }
        public int? PumpTimeId { get; set; }
        public int? StartRateSurfaceLiquidId { get; set; }
        public int? EndRateSurfaceLiquidId { get; set; }
        public int? AvgRateSurfaceLiquidId { get; set; }
        [Column("StartRateSurfaceCO2Id")]
        public int? StartRateSurfaceCo2id { get; set; }
        [Column("EndRateSurfaceCO2Id")]
        public int? EndRateSurfaceCo2id { get; set; }
        [Column("AvgRateSurfaceCO2Id")]
        public int? AvgRateSurfaceCo2id { get; set; }
        [Column("StartStdRateSurfaceN2Id")]
        public int? StartStdRateSurfaceN2id { get; set; }
        [Column("EndStdRateSurfaceN2Id")]
        public int? EndStdRateSurfaceN2id { get; set; }
        [Column("AvgStdRateSurfaceN2Id")]
        public int? AvgStdRateSurfaceN2id { get; set; }
        public int? StartPresSurfaceId { get; set; }
        public int? EndPresSurfaceId { get; set; }
        public int? AveragePresSurfaceId { get; set; }
        public int? StartPumpRateBottomholeId { get; set; }
        public int? EndPumpRateBottomholeId { get; set; }
        public int? AvgPumpRateBottomholeId { get; set; }
        public int? StartPresBottomholeId { get; set; }
        public int? EndPresBottomholeId { get; set; }
        public int? AveragePresBottomholeId { get; set; }
        public int? StartProppantConcSurfaceId { get; set; }
        public int? EndProppantConcSurfaceId { get; set; }
        public int? AvgProppantConcSurfaceId { get; set; }
        public int? StartProppantConcBottomholeId { get; set; }
        public int? EndProppantConcBottomholeId { get; set; }
        public int? AvgProppantConcBottomholeId { get; set; }
        [Column("StartFoamRateN2Id")]
        public int? StartFoamRateN2id { get; set; }
        [Column("EndFoamRateN2Id")]
        public int? EndFoamRateN2id { get; set; }
        [Column("StartFoamRateCO2Id")]
        public int? StartFoamRateCo2id { get; set; }
        [Column("EndFoamRateCO2Id")]
        public int? EndFoamRateCo2id { get; set; }
        public int? FluidVolBaseId { get; set; }
        public int? FluidVolSlurryId { get; set; }
        public int? SlurryRateBeginId { get; set; }
        public int? SlurryRateEndId { get; set; }
        public int? ProppantMassWellHeadId { get; set; }
        public int? ProppantMassId { get; set; }
        public int? MaxPresId { get; set; }
        public int? MaxSlurryRateId { get; set; }
        public int? MaxWellheadRateId { get; set; }
        [Column("MaxN2StdRateId")]
        public int? MaxN2stdRateId { get; set; }
        [Column("MaxCO2LiquidRateId")]
        public int? MaxCo2liquidRateId { get; set; }
        public int? MaxPropConcId { get; set; }
        public int? MaxSlurryPropConcId { get; set; }
        public int? AvgPropConcId { get; set; }
        public int? AvgSlurryPropConcId { get; set; }
        public int? AvgTemperatureId { get; set; }
        public int? AvgInternalPhaseFractionId { get; set; }
        public int? AvgBaseFluidQualityId { get; set; }
        [Column("AvgN2BaseFluidQualityId")]
        public int? AvgN2baseFluidQualityId { get; set; }
        [Column("AvgCO2BaseFluidQualityId")]
        public int? AvgCo2baseFluidQualityId { get; set; }
        public int? AvgHydraulicPowerId { get; set; }
        public int? AvgBaseFluidRateId { get; set; }
        public int? AvgSlurryRateId { get; set; }
        public int? AvgWellheadRateId { get; set; }
        [Column("AvgN2StdRateId")]
        public int? AvgN2stdRateId { get; set; }
        [Column("AvgCO2RateId")]
        public int? AvgCo2rateId { get; set; }
        public int? BaseFluidVolId { get; set; }
        public int? SlurryVolId { get; set; }
        [Column("WellheadVolStdVolN2Id")]
        public int? WellheadVolStdVolN2id { get; set; }
        public int? MaxPmaxPacPresId { get; set; }
        public int? MaxPmaxWeaklinkPresId { get; set; }
        public int? MaxGelRateId { get; set; }
        public int? MaxOilRateId { get; set; }
        public int? MaxAcidRateId { get; set; }
        public int? AvgGelRateId { get; set; }
        public int? AvgOilRateId { get; set; }
        public int? AvgAcidRateId { get; set; }
        public int? GelVolumeId { get; set; }
        public int? OilVolumeId { get; set; }
        public int? AcidVolumeId { get; set; }
        public int? BaseFluidBypassVolId { get; set; }
        public string FrictionFactor { get; set; }
        public int? StageFluidId { get; set; }
        public string Uid { get; set; }
        public int? FlowPathId { get; set; }

        [ForeignKey(nameof(AcidVolumeId))]
        [InverseProperty(nameof(StimJobAcidVolumes.StimJobJobStages))]
        public virtual StimJobAcidVolumes AcidVolume { get; set; }
        [ForeignKey(nameof(AveragePresBottomholeId))]
        [InverseProperty(nameof(StimJobAveragePresBottomholes.StimJobJobStages))]
        public virtual StimJobAveragePresBottomholes AveragePresBottomhole { get; set; }
        [ForeignKey(nameof(AveragePresSurfaceId))]
        [InverseProperty(nameof(StimJobAveragePresSurfaces.StimJobJobStages))]
        public virtual StimJobAveragePresSurfaces AveragePresSurface { get; set; }
        [ForeignKey(nameof(AvgAcidRateId))]
        [InverseProperty(nameof(StimJobAvgAcidRates.StimJobJobStages))]
        public virtual StimJobAvgAcidRates AvgAcidRate { get; set; }
        [ForeignKey(nameof(AvgBaseFluidQualityId))]
        [InverseProperty(nameof(StimJobAvgBaseFluidQualitys.StimJobJobStages))]
        public virtual StimJobAvgBaseFluidQualitys AvgBaseFluidQuality { get; set; }
        [ForeignKey(nameof(AvgBaseFluidRateId))]
        [InverseProperty(nameof(StimJobAvgBaseFluidRates.StimJobJobStages))]
        public virtual StimJobAvgBaseFluidRates AvgBaseFluidRate { get; set; }
        [ForeignKey(nameof(AvgCo2baseFluidQualityId))]
        [InverseProperty(nameof(StimJobAvgCo2baseFluidQualitys.StimJobJobStages))]
        public virtual StimJobAvgCo2baseFluidQualitys AvgCo2baseFluidQuality { get; set; }
        [ForeignKey(nameof(AvgCo2rateId))]
        [InverseProperty(nameof(StimJobAvgCo2rates.StimJobJobStages))]
        public virtual StimJobAvgCo2rates AvgCo2rate { get; set; }
        [ForeignKey(nameof(AvgGelRateId))]
        [InverseProperty(nameof(StimJobAvgGelRates.StimJobJobStages))]
        public virtual StimJobAvgGelRates AvgGelRate { get; set; }
        [ForeignKey(nameof(AvgHydraulicPowerId))]
        [InverseProperty(nameof(StimJobAvgHydraulicPowers.StimJobJobStages))]
        public virtual StimJobAvgHydraulicPowers AvgHydraulicPower { get; set; }
        [ForeignKey(nameof(AvgInternalPhaseFractionId))]
        [InverseProperty(nameof(StimJobAvgInternalPhaseFractions.StimJobJobStages))]
        public virtual StimJobAvgInternalPhaseFractions AvgInternalPhaseFraction { get; set; }
        [ForeignKey(nameof(AvgN2baseFluidQualityId))]
        [InverseProperty(nameof(StimJobAvgN2baseFluidQualitys.StimJobJobStages))]
        public virtual StimJobAvgN2baseFluidQualitys AvgN2baseFluidQuality { get; set; }
        [ForeignKey(nameof(AvgN2stdRateId))]
        [InverseProperty(nameof(StimJobAvgN2stdRates.StimJobJobStages))]
        public virtual StimJobAvgN2stdRates AvgN2stdRate { get; set; }
        [ForeignKey(nameof(AvgOilRateId))]
        [InverseProperty(nameof(StimJobAvgOilRates.StimJobJobStages))]
        public virtual StimJobAvgOilRates AvgOilRate { get; set; }
        [ForeignKey(nameof(AvgPropConcId))]
        [InverseProperty(nameof(StimJobAvgPropConcs.StimJobJobStages))]
        public virtual StimJobAvgPropConcs AvgPropConc { get; set; }
        [ForeignKey(nameof(AvgProppantConcBottomholeId))]
        [InverseProperty(nameof(StimJobAvgProppantConcBottomholes.StimJobJobStages))]
        public virtual StimJobAvgProppantConcBottomholes AvgProppantConcBottomhole { get; set; }
        [ForeignKey(nameof(AvgProppantConcSurfaceId))]
        [InverseProperty(nameof(StimJobAvgProppantConcSurfaces.StimJobJobStages))]
        public virtual StimJobAvgProppantConcSurfaces AvgProppantConcSurface { get; set; }
        [ForeignKey(nameof(AvgPumpRateBottomholeId))]
        [InverseProperty(nameof(StimJobAvgPumpRateBottomholes.StimJobJobStages))]
        public virtual StimJobAvgPumpRateBottomholes AvgPumpRateBottomhole { get; set; }
        [ForeignKey(nameof(AvgRateSurfaceCo2id))]
        [InverseProperty(nameof(StimJobAvgRateSurfaceCo2s.StimJobJobStages))]
        public virtual StimJobAvgRateSurfaceCo2s AvgRateSurfaceCo2 { get; set; }
        [ForeignKey(nameof(AvgRateSurfaceLiquidId))]
        [InverseProperty(nameof(StimJobAvgRateSurfaceLiquids.StimJobJobStages))]
        public virtual StimJobAvgRateSurfaceLiquids AvgRateSurfaceLiquid { get; set; }
        [ForeignKey(nameof(AvgSlurryPropConcId))]
        [InverseProperty(nameof(StimJobAvgSlurryPropConcs.StimJobJobStages))]
        public virtual StimJobAvgSlurryPropConcs AvgSlurryPropConc { get; set; }
        [ForeignKey(nameof(AvgSlurryRateId))]
        [InverseProperty(nameof(StimJobAvgSlurryRates.StimJobJobStages))]
        public virtual StimJobAvgSlurryRates AvgSlurryRate { get; set; }
        [ForeignKey(nameof(AvgStdRateSurfaceN2id))]
        [InverseProperty(nameof(StimJobAvgStdRateSurfaceN2s.StimJobJobStages))]
        public virtual StimJobAvgStdRateSurfaceN2s AvgStdRateSurfaceN2 { get; set; }
        [ForeignKey(nameof(AvgTemperatureId))]
        [InverseProperty(nameof(StimJobAvgTemperatures.StimJobJobStages))]
        public virtual StimJobAvgTemperatures AvgTemperature { get; set; }
        [ForeignKey(nameof(AvgWellheadRateId))]
        [InverseProperty(nameof(StimJobAvgWellheadRates.StimJobJobStages))]
        public virtual StimJobAvgWellheadRates AvgWellheadRate { get; set; }
        [ForeignKey(nameof(BaseFluidBypassVolId))]
        [InverseProperty(nameof(StimJobBaseFluidBypassVols.StimJobJobStages))]
        public virtual StimJobBaseFluidBypassVols BaseFluidBypassVol { get; set; }
        [ForeignKey(nameof(BaseFluidVolId))]
        [InverseProperty(nameof(StimJobBaseFluidVols.StimJobJobStages))]
        public virtual StimJobBaseFluidVols BaseFluidVol { get; set; }
        [ForeignKey(nameof(EndFoamRateCo2id))]
        [InverseProperty(nameof(StimJobEndFoamRateCo2s.StimJobJobStages))]
        public virtual StimJobEndFoamRateCo2s EndFoamRateCo2 { get; set; }
        [ForeignKey(nameof(EndFoamRateN2id))]
        [InverseProperty(nameof(StimJobEndFoamRateN2s.StimJobJobStages))]
        public virtual StimJobEndFoamRateN2s EndFoamRateN2 { get; set; }
        [ForeignKey(nameof(EndPresBottomholeId))]
        [InverseProperty(nameof(StimJobEndPresBottomholes.StimJobJobStages))]
        public virtual StimJobEndPresBottomholes EndPresBottomhole { get; set; }
        [ForeignKey(nameof(EndPresSurfaceId))]
        [InverseProperty(nameof(StimJobEndPresSurfaces.StimJobJobStages))]
        public virtual StimJobEndPresSurfaces EndPresSurface { get; set; }
        [ForeignKey(nameof(EndProppantConcBottomholeId))]
        [InverseProperty(nameof(StimJobEndProppantConcBottomholes.StimJobJobStages))]
        public virtual StimJobEndProppantConcBottomholes EndProppantConcBottomhole { get; set; }
        [ForeignKey(nameof(EndProppantConcSurfaceId))]
        [InverseProperty(nameof(StimJobEndProppantConcSurfaces.StimJobJobStages))]
        public virtual StimJobEndProppantConcSurfaces EndProppantConcSurface { get; set; }
        [ForeignKey(nameof(EndPumpRateBottomholeId))]
        [InverseProperty(nameof(StimJobEndPumpRateBottomholes.StimJobJobStages))]
        public virtual StimJobEndPumpRateBottomholes EndPumpRateBottomhole { get; set; }
        [ForeignKey(nameof(EndRateSurfaceCo2id))]
        [InverseProperty(nameof(StimJobEndRateSurfaceCo2s.StimJobJobStages))]
        public virtual StimJobEndRateSurfaceCo2s EndRateSurfaceCo2 { get; set; }
        [ForeignKey(nameof(EndRateSurfaceLiquidId))]
        [InverseProperty(nameof(StimJobEndRateSurfaceLiquids.StimJobJobStages))]
        public virtual StimJobEndRateSurfaceLiquids EndRateSurfaceLiquid { get; set; }
        [ForeignKey(nameof(EndStdRateSurfaceN2id))]
        [InverseProperty(nameof(StimJobEndStdRateSurfaceN2s.StimJobJobStages))]
        public virtual StimJobEndStdRateSurfaceN2s EndStdRateSurfaceN2 { get; set; }
        [ForeignKey(nameof(FlowPathId))]
        [InverseProperty(nameof(StimJobFlowPaths.StimJobJobStages))]
        public virtual StimJobFlowPaths FlowPath { get; set; }
        [ForeignKey(nameof(FluidVolBaseId))]
        [InverseProperty(nameof(StimJobFluidVolBases.StimJobJobStages))]
        public virtual StimJobFluidVolBases FluidVolBase { get; set; }
        [ForeignKey(nameof(FluidVolSlurryId))]
        [InverseProperty(nameof(StimJobFluidVolSlurrys.StimJobJobStages))]
        public virtual StimJobFluidVolSlurrys FluidVolSlurry { get; set; }
        [ForeignKey(nameof(GelVolumeId))]
        [InverseProperty(nameof(StimJobGelVolumes.StimJobJobStages))]
        public virtual StimJobGelVolumes GelVolume { get; set; }
        [ForeignKey(nameof(MaxAcidRateId))]
        [InverseProperty(nameof(StimJobMaxAcidRates.StimJobJobStages))]
        public virtual StimJobMaxAcidRates MaxAcidRate { get; set; }
        [ForeignKey(nameof(MaxCo2liquidRateId))]
        [InverseProperty(nameof(StimJobMaxCo2liquidRates.StimJobJobStages))]
        public virtual StimJobMaxCo2liquidRates MaxCo2liquidRate { get; set; }
        [ForeignKey(nameof(MaxGelRateId))]
        [InverseProperty(nameof(StimJobMaxGelRates.StimJobJobStages))]
        public virtual StimJobMaxGelRates MaxGelRate { get; set; }
        [ForeignKey(nameof(MaxN2stdRateId))]
        [InverseProperty(nameof(StimJobMaxN2stdRates.StimJobJobStages))]
        public virtual StimJobMaxN2stdRates MaxN2stdRate { get; set; }
        [ForeignKey(nameof(MaxOilRateId))]
        [InverseProperty(nameof(StimJobMaxOilRates.StimJobJobStages))]
        public virtual StimJobMaxOilRates MaxOilRate { get; set; }
        [ForeignKey(nameof(MaxPmaxPacPresId))]
        [InverseProperty(nameof(StimJobMaxPmaxPacPress.StimJobJobStages))]
        public virtual StimJobMaxPmaxPacPress MaxPmaxPacPres { get; set; }
        [ForeignKey(nameof(MaxPmaxWeaklinkPresId))]
        [InverseProperty(nameof(StimJobMaxPmaxWeaklinkPress.StimJobJobStages))]
        public virtual StimJobMaxPmaxWeaklinkPress MaxPmaxWeaklinkPres { get; set; }
        [ForeignKey(nameof(MaxPresId))]
        [InverseProperty(nameof(StimJobMaxPress.StimJobJobStages))]
        public virtual StimJobMaxPress MaxPres { get; set; }
        [ForeignKey(nameof(MaxPropConcId))]
        [InverseProperty(nameof(StimJobMaxPropConcs.StimJobJobStages))]
        public virtual StimJobMaxPropConcs MaxPropConc { get; set; }
        [ForeignKey(nameof(MaxSlurryPropConcId))]
        [InverseProperty(nameof(StimJobMaxSlurryPropConcs.StimJobJobStages))]
        public virtual StimJobMaxSlurryPropConcs MaxSlurryPropConc { get; set; }
        [ForeignKey(nameof(MaxSlurryRateId))]
        [InverseProperty(nameof(StimJobMaxSlurryRates.StimJobJobStages))]
        public virtual StimJobMaxSlurryRates MaxSlurryRate { get; set; }
        [ForeignKey(nameof(MaxWellheadRateId))]
        [InverseProperty(nameof(StimJobMaxWellheadRates.StimJobJobStages))]
        public virtual StimJobMaxWellheadRates MaxWellheadRate { get; set; }
        [ForeignKey(nameof(OilVolumeId))]
        [InverseProperty(nameof(StimJobOilVolumes.StimJobJobStages))]
        public virtual StimJobOilVolumes OilVolume { get; set; }
        [ForeignKey(nameof(ProppantMassId))]
        [InverseProperty(nameof(StimJobProppantMasss.StimJobJobStages))]
        public virtual StimJobProppantMasss ProppantMass { get; set; }
        [ForeignKey(nameof(ProppantMassWellHeadId))]
        [InverseProperty(nameof(StimJobProppantMassWellHeads.StimJobJobStages))]
        public virtual StimJobProppantMassWellHeads ProppantMassWellHead { get; set; }
        [ForeignKey(nameof(PumpTimeId))]
        [InverseProperty(nameof(StimJobPumpTimes.StimJobJobStages))]
        public virtual StimJobPumpTimes PumpTime { get; set; }
        [ForeignKey(nameof(SlurryRateBeginId))]
        [InverseProperty(nameof(StimJobSlurryRateBegins.StimJobJobStages))]
        public virtual StimJobSlurryRateBegins SlurryRateBegin { get; set; }
        [ForeignKey(nameof(SlurryRateEndId))]
        [InverseProperty(nameof(StimJobSlurryRateEnds.StimJobJobStages))]
        public virtual StimJobSlurryRateEnds SlurryRateEnd { get; set; }
        [ForeignKey(nameof(SlurryVolId))]
        [InverseProperty(nameof(StimJobSlurryVols.StimJobJobStages))]
        public virtual StimJobSlurryVols SlurryVol { get; set; }
        [ForeignKey(nameof(StageFluidId))]
        [InverseProperty(nameof(StimJobStageFluids.StimJobJobStages))]
        public virtual StimJobStageFluids StageFluid { get; set; }
        [ForeignKey(nameof(StartFoamRateCo2id))]
        [InverseProperty(nameof(StimJobStartFoamRateCo2s.StimJobJobStages))]
        public virtual StimJobStartFoamRateCo2s StartFoamRateCo2 { get; set; }
        [ForeignKey(nameof(StartFoamRateN2id))]
        [InverseProperty(nameof(StimJobStartFoamRateN2s.StimJobJobStages))]
        public virtual StimJobStartFoamRateN2s StartFoamRateN2 { get; set; }
        [ForeignKey(nameof(StartPresBottomholeId))]
        [InverseProperty(nameof(StimJobStartPresBottomholes.StimJobJobStages))]
        public virtual StimJobStartPresBottomholes StartPresBottomhole { get; set; }
        [ForeignKey(nameof(StartPresSurfaceId))]
        [InverseProperty(nameof(StimJobStartPresSurfaces.StimJobJobStages))]
        public virtual StimJobStartPresSurfaces StartPresSurface { get; set; }
        [ForeignKey(nameof(StartProppantConcBottomholeId))]
        [InverseProperty(nameof(StimJobStartProppantConcBottomholes.StimJobJobStages))]
        public virtual StimJobStartProppantConcBottomholes StartProppantConcBottomhole { get; set; }
        [ForeignKey(nameof(StartProppantConcSurfaceId))]
        [InverseProperty(nameof(StimJobStartProppantConcSurfaces.StimJobJobStages))]
        public virtual StimJobStartProppantConcSurfaces StartProppantConcSurface { get; set; }
        [ForeignKey(nameof(StartPumpRateBottomholeId))]
        [InverseProperty(nameof(StimJobStartPumpRateBottomholes.StimJobJobStages))]
        public virtual StimJobStartPumpRateBottomholes StartPumpRateBottomhole { get; set; }
        [ForeignKey(nameof(StartRateSurfaceCo2id))]
        [InverseProperty(nameof(StimJobStartRateSurfaceCo2s.StimJobJobStages))]
        public virtual StimJobStartRateSurfaceCo2s StartRateSurfaceCo2 { get; set; }
        [ForeignKey(nameof(StartRateSurfaceLiquidId))]
        [InverseProperty(nameof(StimJobStartRateSurfaceLiquids.StimJobJobStages))]
        public virtual StimJobStartRateSurfaceLiquids StartRateSurfaceLiquid { get; set; }
        [ForeignKey(nameof(StartStdRateSurfaceN2id))]
        [InverseProperty(nameof(StimJobStartStdRateSurfaceN2s.StimJobJobStages))]
        public virtual StimJobStartStdRateSurfaceN2s StartStdRateSurfaceN2 { get; set; }
        [ForeignKey(nameof(WellheadVolStdVolN2id))]
        [InverseProperty(nameof(StimJobWellheadVols.StimJobJobStages))]
        public virtual StimJobWellheadVols WellheadVolStdVolN2 { get; set; }
    }
}
