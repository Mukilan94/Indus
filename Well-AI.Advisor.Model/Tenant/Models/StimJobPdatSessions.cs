using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPdatSessions
    {
        public StimJobPdatSessions()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int PdatSessionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        [Column("DTimPumpOn")]
        public string DtimPumpOn { get; set; }
        [Column("DTimPumpOff")]
        public string DtimPumpOff { get; set; }
        [Column("DTimWellShutin")]
        public string DtimWellShutin { get; set; }
        [Column("DTimFractureClose")]
        public string DtimFractureClose { get; set; }
        public int? PumpDurationId { get; set; }
        public int? AvgBottomholeTreatmentPresId { get; set; }
        public int? BottomholeHydrostaticPresId { get; set; }
        public int? BubblePointPresId { get; set; }
        public int? FractureClosePresId { get; set; }
        public int? FrictionPresId { get; set; }
        public int? InitialShutinPresId { get; set; }
        public int? PorePresId { get; set; }
        public int? AvgBottomholeTreatmentRateId { get; set; }
        public int? FluidDensityId { get; set; }
        public int? BaseFluidVolId { get; set; }
        public int? WellboreVolumeId { get; set; }
        public int? MdSurfaceId { get; set; }
        public int? MdBottomholeId { get; set; }
        public int? MdMidPerforationId { get; set; }
        public int? TvdMidPerforationId { get; set; }
        public int? SurfaceTemperatureId { get; set; }
        public int? BottomholeTemperatureId { get; set; }
        public int? SurfaceFluidTemperatureId { get; set; }
        public int? FluidCompressibilityId { get; set; }
        public int? ReservoirTotalCompressibilityId { get; set; }
        public string FluidNprimeFactor { get; set; }
        public string FluidKprimeFactor { get; set; }
        public int? FluidSpecificHeatId { get; set; }
        public int? FluidThermalConductivityId { get; set; }
        public int? FluidThermalExpansionCoefficientId { get; set; }
        public int? FluidEfficiencyId { get; set; }
        public int? FoamQualityId { get; set; }
        public int? PercentPadId { get; set; }
        public string TemperatureCorrectionApplied { get; set; }
        public int? StepRateTestId { get; set; }
        public int? FluidEfficiencyTestId { get; set; }
        public int? PumpFlowBackTestId { get; set; }
        public int? StepDownTestId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AvgBottomholeTreatmentPresId))]
        [InverseProperty(nameof(StimJobAvgBottomholeTreatmentPress.StimJobPdatSessions))]
        public virtual StimJobAvgBottomholeTreatmentPress AvgBottomholeTreatmentPres { get; set; }
        [ForeignKey(nameof(AvgBottomholeTreatmentRateId))]
        [InverseProperty(nameof(StimJobAvgBottomholeTreatmentRates.StimJobPdatSessions))]
        public virtual StimJobAvgBottomholeTreatmentRates AvgBottomholeTreatmentRate { get; set; }
        [ForeignKey(nameof(BaseFluidVolId))]
        [InverseProperty(nameof(StimJobBaseFluidVols.StimJobPdatSessions))]
        public virtual StimJobBaseFluidVols BaseFluidVol { get; set; }
        [ForeignKey(nameof(BottomholeHydrostaticPresId))]
        [InverseProperty(nameof(StimJobBottomholeHydrostaticPress.StimJobPdatSessions))]
        public virtual StimJobBottomholeHydrostaticPress BottomholeHydrostaticPres { get; set; }
        [ForeignKey(nameof(BottomholeTemperatureId))]
        [InverseProperty(nameof(StimJobBottomholeTemperatures.StimJobPdatSessions))]
        public virtual StimJobBottomholeTemperatures BottomholeTemperature { get; set; }
        [ForeignKey(nameof(BubblePointPresId))]
        [InverseProperty(nameof(StimJobBubblePointPress.StimJobPdatSessions))]
        public virtual StimJobBubblePointPress BubblePointPres { get; set; }
        [ForeignKey(nameof(FluidCompressibilityId))]
        [InverseProperty(nameof(StimJobFluidCompressibilitys.StimJobPdatSessions))]
        public virtual StimJobFluidCompressibilitys FluidCompressibility { get; set; }
        [ForeignKey(nameof(FluidDensityId))]
        [InverseProperty(nameof(StimJobFluidDensitys.StimJobPdatSessions))]
        public virtual StimJobFluidDensitys FluidDensity { get; set; }
        [ForeignKey(nameof(FluidEfficiencyId))]
        [InverseProperty(nameof(StimJobFluidEfficiencys.StimJobPdatSessions))]
        public virtual StimJobFluidEfficiencys FluidEfficiency { get; set; }
        [ForeignKey(nameof(FluidEfficiencyTestId))]
        [InverseProperty(nameof(StimJobFluidEfficiencyTests.StimJobPdatSessions))]
        public virtual StimJobFluidEfficiencyTests FluidEfficiencyTest { get; set; }
        [ForeignKey(nameof(FluidSpecificHeatId))]
        [InverseProperty(nameof(StimJobFluidSpecificHeats.StimJobPdatSessions))]
        public virtual StimJobFluidSpecificHeats FluidSpecificHeat { get; set; }
        [ForeignKey(nameof(FluidThermalConductivityId))]
        [InverseProperty(nameof(StimJobFluidThermalConductivitys.StimJobPdatSessions))]
        public virtual StimJobFluidThermalConductivitys FluidThermalConductivity { get; set; }
        [ForeignKey(nameof(FluidThermalExpansionCoefficientId))]
        [InverseProperty(nameof(StimJobFluidThermalExpansionCoefficients.StimJobPdatSessions))]
        public virtual StimJobFluidThermalExpansionCoefficients FluidThermalExpansionCoefficient { get; set; }
        [ForeignKey(nameof(FoamQualityId))]
        [InverseProperty(nameof(StimJobFoamQualitys.StimJobPdatSessions))]
        public virtual StimJobFoamQualitys FoamQuality { get; set; }
        [ForeignKey(nameof(FractureClosePresId))]
        [InverseProperty(nameof(StimJobFractureClosePress.StimJobPdatSessions))]
        public virtual StimJobFractureClosePress FractureClosePres { get; set; }
        [ForeignKey(nameof(FrictionPresId))]
        [InverseProperty(nameof(StimJobFrictionPress.StimJobPdatSessions))]
        public virtual StimJobFrictionPress FrictionPres { get; set; }
        [ForeignKey(nameof(InitialShutinPresId))]
        [InverseProperty(nameof(StimJobInitialShutinPress.StimJobPdatSessions))]
        public virtual StimJobInitialShutinPress InitialShutinPres { get; set; }
        [ForeignKey(nameof(MdBottomholeId))]
        [InverseProperty(nameof(StimJobMdBottomholes.StimJobPdatSessions))]
        public virtual StimJobMdBottomholes MdBottomhole { get; set; }
        [ForeignKey(nameof(MdMidPerforationId))]
        [InverseProperty(nameof(StimJobMdMidPerforations.StimJobPdatSessions))]
        public virtual StimJobMdMidPerforations MdMidPerforation { get; set; }
        [ForeignKey(nameof(MdSurfaceId))]
        [InverseProperty(nameof(StimJobMdSurfaces.StimJobPdatSessions))]
        public virtual StimJobMdSurfaces MdSurface { get; set; }
        [ForeignKey(nameof(PercentPadId))]
        [InverseProperty(nameof(StimJobPercentPads.StimJobPdatSessions))]
        public virtual StimJobPercentPads PercentPad { get; set; }
        [ForeignKey(nameof(PorePresId))]
        [InverseProperty(nameof(StimJobPorePress.StimJobPdatSessions))]
        public virtual StimJobPorePress PorePres { get; set; }
        [ForeignKey(nameof(PumpDurationId))]
        [InverseProperty(nameof(StimJobPumpDurations.StimJobPdatSessions))]
        public virtual StimJobPumpDurations PumpDuration { get; set; }
        [ForeignKey(nameof(PumpFlowBackTestId))]
        [InverseProperty(nameof(StimJobPumpFlowBackTests.StimJobPdatSessions))]
        public virtual StimJobPumpFlowBackTests PumpFlowBackTest { get; set; }
        [ForeignKey(nameof(ReservoirTotalCompressibilityId))]
        [InverseProperty(nameof(StimJobReservoirTotalCompressibilitys.StimJobPdatSessions))]
        public virtual StimJobReservoirTotalCompressibilitys ReservoirTotalCompressibility { get; set; }
        [ForeignKey(nameof(StepDownTestId))]
        [InverseProperty(nameof(StimJobStepDownTests.StimJobPdatSessions))]
        public virtual StimJobStepDownTests StepDownTest { get; set; }
        [ForeignKey(nameof(StepRateTestId))]
        [InverseProperty(nameof(StimJobStepRateTests.StimJobPdatSessions))]
        public virtual StimJobStepRateTests StepRateTest { get; set; }
        [ForeignKey(nameof(SurfaceFluidTemperatureId))]
        [InverseProperty(nameof(StimJobSurfaceFluidTemperatures.StimJobPdatSessions))]
        public virtual StimJobSurfaceFluidTemperatures SurfaceFluidTemperature { get; set; }
        [ForeignKey(nameof(SurfaceTemperatureId))]
        [InverseProperty(nameof(StimJobSurfaceTemperatures.StimJobPdatSessions))]
        public virtual StimJobSurfaceTemperatures SurfaceTemperature { get; set; }
        [ForeignKey(nameof(TvdMidPerforationId))]
        [InverseProperty(nameof(StimJobTvdMidPerforations.StimJobPdatSessions))]
        public virtual StimJobTvdMidPerforations TvdMidPerforation { get; set; }
        [ForeignKey(nameof(WellboreVolumeId))]
        [InverseProperty(nameof(StimJobWellboreVolumes.StimJobPdatSessions))]
        public virtual StimJobWellboreVolumes WellboreVolume { get; set; }
        [InverseProperty("PdatSession")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
