using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobJobIntervals
    {
        public StimJobJobIntervals()
        {
            StimJobTotalProppantUsages = new HashSet<StimJobTotalProppantUsages>();
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int JobIntervalId { get; set; }
        public string UidTreatmentInterval { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        [Column("DTimStart")]
        public string DtimStart { get; set; }
        public string FormationName { get; set; }
        public int? MdFormationTopId { get; set; }
        public int? MdFormationBottomId { get; set; }
        public int? TvdFormationTopId { get; set; }
        public int? TvdFormationBottomId { get; set; }
        public string OpenHoleName { get; set; }
        public int? OpenHoleDiameterId { get; set; }
        public int? MdOpenHoleTopId { get; set; }
        public int? MdOpenHoleBottomId { get; set; }
        public int? TvdOpenHoleTopId { get; set; }
        public int? TvdOpenHoleBottomId { get; set; }
        public int? TotalFrictionPresLossId { get; set; }
        public int? TotalPumpTimeId { get; set; }
        public int? MaxPresTubingId { get; set; }
        public int? MaxPresAnnulusId { get; set; }
        public int? MaxFluidRateTubingId { get; set; }
        public int? MaxFluidRateAnnulusId { get; set; }
        public int? AvgPresTubingId { get; set; }
        public int? AvgPresCasingId { get; set; }
        public int? BreakDownPresId { get; set; }
        public int? AveragePresId { get; set; }
        public int? AvgBaseFluidReturnRateId { get; set; }
        public int? AvgSlurryReturnRateId { get; set; }
        public int? AvgBottomholeRateId { get; set; }
        public int? TotalVolumeId { get; set; }
        public int? MaxProppantConcSurfaceId { get; set; }
        public int? MaxProppantConcBottomholeId { get; set; }
        public int? AvgProppantConcSurfaceId { get; set; }
        public int? AvgProppantConcBottomholeId { get; set; }
        public int? PerfproppantConcId { get; set; }
        public int? TotalProppantMassId { get; set; }
        public int? PercentProppantPumpedId { get; set; }
        public int? WellboreProppantMassId { get; set; }
        public int? FormationProppantMassId { get; set; }
        public string PerfBallCount { get; set; }
        [Column("TotalN2StdVolumeId")]
        public int? TotalN2stdVolumeId { get; set; }
        [Column("TotalCO2MassId")]
        public int? TotalCo2massId { get; set; }
        public int? FractureGradientId { get; set; }
        public int? FinalFractureGradientId { get; set; }
        public int? InitialShutinPresId { get; set; }
        public int? ScreenOutPresId { get; set; }
        [Column("HhpOrderedCO2Id")]
        public int? HhpOrderedCo2id { get; set; }
        public int? HhpOrderedFluidId { get; set; }
        [Column("HhpUsedCO2Id")]
        public int? HhpUsedCo2id { get; set; }
        public int? HhpUsedFluidId { get; set; }
        public int? PerfBallSizeId { get; set; }
        public string ScreenedOut { get; set; }
        public int? AvgFractureWidthId { get; set; }
        public int? AvgConductivityId { get; set; }
        public int? NetPresId { get; set; }
        public int? ClosurePresId { get; set; }
        public int? ClosureDurationId { get; set; }
        public int? PdatSessionId { get; set; }
        public int? ReservoirIntervalId { get; set; }
        public int? PerforationIntervalId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AveragePresId))]
        [InverseProperty(nameof(StimJobAveragePress.StimJobJobIntervals))]
        public virtual StimJobAveragePress AveragePres { get; set; }
        [ForeignKey(nameof(AvgBaseFluidReturnRateId))]
        [InverseProperty(nameof(StimJobAvgBaseFluidReturnRates.StimJobJobIntervals))]
        public virtual StimJobAvgBaseFluidReturnRates AvgBaseFluidReturnRate { get; set; }
        [ForeignKey(nameof(AvgBottomholeRateId))]
        [InverseProperty(nameof(StimJobAvgBottomholeRates.StimJobJobIntervals))]
        public virtual StimJobAvgBottomholeRates AvgBottomholeRate { get; set; }
        [ForeignKey(nameof(AvgConductivityId))]
        [InverseProperty(nameof(StimJobAvgConductivitys.StimJobJobIntervals))]
        public virtual StimJobAvgConductivitys AvgConductivity { get; set; }
        [ForeignKey(nameof(AvgFractureWidthId))]
        [InverseProperty(nameof(StimJobAvgFractureWidths.StimJobJobIntervals))]
        public virtual StimJobAvgFractureWidths AvgFractureWidth { get; set; }
        [ForeignKey(nameof(AvgPresCasingId))]
        [InverseProperty(nameof(StimJobAvgPresCasings.StimJobJobIntervals))]
        public virtual StimJobAvgPresCasings AvgPresCasing { get; set; }
        [ForeignKey(nameof(AvgPresTubingId))]
        [InverseProperty(nameof(StimJobAvgPresTubings.StimJobJobIntervals))]
        public virtual StimJobAvgPresTubings AvgPresTubing { get; set; }
        [ForeignKey(nameof(AvgProppantConcBottomholeId))]
        [InverseProperty(nameof(StimJobAvgProppantConcBottomholes.StimJobJobIntervals))]
        public virtual StimJobAvgProppantConcBottomholes AvgProppantConcBottomhole { get; set; }
        [ForeignKey(nameof(AvgProppantConcSurfaceId))]
        [InverseProperty(nameof(StimJobAvgProppantConcSurfaces.StimJobJobIntervals))]
        public virtual StimJobAvgProppantConcSurfaces AvgProppantConcSurface { get; set; }
        [ForeignKey(nameof(AvgSlurryReturnRateId))]
        [InverseProperty(nameof(StimJobAvgSlurryReturnRates.StimJobJobIntervals))]
        public virtual StimJobAvgSlurryReturnRates AvgSlurryReturnRate { get; set; }
        [ForeignKey(nameof(BreakDownPresId))]
        [InverseProperty(nameof(StimJobBreakDownPress.StimJobJobIntervals))]
        public virtual StimJobBreakDownPress BreakDownPres { get; set; }
        [ForeignKey(nameof(ClosureDurationId))]
        [InverseProperty(nameof(StimJobClosureDurations.StimJobJobIntervals))]
        public virtual StimJobClosureDurations ClosureDuration { get; set; }
        [ForeignKey(nameof(ClosurePresId))]
        [InverseProperty(nameof(StimJobClosurePress.StimJobJobIntervals))]
        public virtual StimJobClosurePress ClosurePres { get; set; }
        [ForeignKey(nameof(FinalFractureGradientId))]
        [InverseProperty(nameof(StimJobFinalFractureGradients.StimJobJobIntervals))]
        public virtual StimJobFinalFractureGradients FinalFractureGradient { get; set; }
        [ForeignKey(nameof(FormationProppantMassId))]
        [InverseProperty(nameof(StimJobFormationProppantMasss.StimJobJobIntervals))]
        public virtual StimJobFormationProppantMasss FormationProppantMass { get; set; }
        [ForeignKey(nameof(FractureGradientId))]
        [InverseProperty(nameof(StimJobFractureGradients.StimJobJobIntervals))]
        public virtual StimJobFractureGradients FractureGradient { get; set; }
        [ForeignKey(nameof(HhpOrderedCo2id))]
        [InverseProperty(nameof(StimJobHhpOrderedCo2s.StimJobJobIntervals))]
        public virtual StimJobHhpOrderedCo2s HhpOrderedCo2 { get; set; }
        [ForeignKey(nameof(HhpOrderedFluidId))]
        [InverseProperty(nameof(StimJobHhpOrderedFluids.StimJobJobIntervals))]
        public virtual StimJobHhpOrderedFluids HhpOrderedFluid { get; set; }
        [ForeignKey(nameof(HhpUsedCo2id))]
        [InverseProperty(nameof(StimJobHhpUsedCo2s.StimJobJobIntervals))]
        public virtual StimJobHhpUsedCo2s HhpUsedCo2 { get; set; }
        [ForeignKey(nameof(HhpUsedFluidId))]
        [InverseProperty(nameof(StimJobHhpUsedFluids.StimJobJobIntervals))]
        public virtual StimJobHhpUsedFluids HhpUsedFluid { get; set; }
        [ForeignKey(nameof(InitialShutinPresId))]
        [InverseProperty(nameof(StimJobInitialShutinPress.StimJobJobIntervals))]
        public virtual StimJobInitialShutinPress InitialShutinPres { get; set; }
        [ForeignKey(nameof(MaxFluidRateAnnulusId))]
        [InverseProperty(nameof(StimJobMaxFluidRateAnnuluss.StimJobJobIntervals))]
        public virtual StimJobMaxFluidRateAnnuluss MaxFluidRateAnnulus { get; set; }
        [ForeignKey(nameof(MaxFluidRateTubingId))]
        [InverseProperty(nameof(StimJobMaxFluidRateTubings.StimJobJobIntervals))]
        public virtual StimJobMaxFluidRateTubings MaxFluidRateTubing { get; set; }
        [ForeignKey(nameof(MaxPresAnnulusId))]
        [InverseProperty(nameof(StimJobMaxPresAnnuluss.StimJobJobIntervals))]
        public virtual StimJobMaxPresAnnuluss MaxPresAnnulus { get; set; }
        [ForeignKey(nameof(MaxPresTubingId))]
        [InverseProperty(nameof(StimJobMaxPresTubings.StimJobJobIntervals))]
        public virtual StimJobMaxPresTubings MaxPresTubing { get; set; }
        [ForeignKey(nameof(MaxProppantConcBottomholeId))]
        [InverseProperty(nameof(StimJobMaxProppantConcBottomholes.StimJobJobIntervals))]
        public virtual StimJobMaxProppantConcBottomholes MaxProppantConcBottomhole { get; set; }
        [ForeignKey(nameof(MaxProppantConcSurfaceId))]
        [InverseProperty(nameof(StimJobMaxProppantConcSurfaces.StimJobJobIntervals))]
        public virtual StimJobMaxProppantConcSurfaces MaxProppantConcSurface { get; set; }
        [ForeignKey(nameof(MdFormationBottomId))]
        [InverseProperty(nameof(StimJobMdFormationBottoms.StimJobJobIntervals))]
        public virtual StimJobMdFormationBottoms MdFormationBottom { get; set; }
        [ForeignKey(nameof(MdFormationTopId))]
        [InverseProperty(nameof(StimJobMdFormationTops.StimJobJobIntervals))]
        public virtual StimJobMdFormationTops MdFormationTop { get; set; }
        [ForeignKey(nameof(MdOpenHoleBottomId))]
        [InverseProperty(nameof(StimJobMdOpenHoleBottoms.StimJobJobIntervals))]
        public virtual StimJobMdOpenHoleBottoms MdOpenHoleBottom { get; set; }
        [ForeignKey(nameof(MdOpenHoleTopId))]
        [InverseProperty(nameof(StimJobMdOpenHoleTops.StimJobJobIntervals))]
        public virtual StimJobMdOpenHoleTops MdOpenHoleTop { get; set; }
        [ForeignKey(nameof(NetPresId))]
        [InverseProperty(nameof(StimJobNetPress.StimJobJobIntervals))]
        public virtual StimJobNetPress NetPres { get; set; }
        [ForeignKey(nameof(OpenHoleDiameterId))]
        [InverseProperty(nameof(StimJobOpenHoleDiameters.StimJobJobIntervals))]
        public virtual StimJobOpenHoleDiameters OpenHoleDiameter { get; set; }
        [ForeignKey(nameof(PdatSessionId))]
        [InverseProperty(nameof(StimJobPdatSessions.StimJobJobIntervals))]
        public virtual StimJobPdatSessions PdatSession { get; set; }
        [ForeignKey(nameof(PercentProppantPumpedId))]
        [InverseProperty(nameof(StimJobPercentProppantPumpeds.StimJobJobIntervals))]
        public virtual StimJobPercentProppantPumpeds PercentProppantPumped { get; set; }
        [ForeignKey(nameof(PerfBallSizeId))]
        [InverseProperty(nameof(StimJobPerfBallSizes.StimJobJobIntervals))]
        public virtual StimJobPerfBallSizes PerfBallSize { get; set; }
        [ForeignKey(nameof(PerforationIntervalId))]
        [InverseProperty(nameof(StimJobPerforationIntervals.StimJobJobIntervals))]
        public virtual StimJobPerforationIntervals PerforationInterval { get; set; }
        [ForeignKey(nameof(PerfproppantConcId))]
        [InverseProperty(nameof(StimJobPerfproppantConcs.StimJobJobIntervals))]
        public virtual StimJobPerfproppantConcs PerfproppantConc { get; set; }
        [ForeignKey(nameof(ReservoirIntervalId))]
        [InverseProperty(nameof(StimJobReservoirIntervals.StimJobJobIntervals))]
        public virtual StimJobReservoirIntervals ReservoirInterval { get; set; }
        [ForeignKey(nameof(ScreenOutPresId))]
        [InverseProperty(nameof(StimJobScreenOutPress.StimJobJobIntervals))]
        public virtual StimJobScreenOutPress ScreenOutPres { get; set; }
        [ForeignKey(nameof(TotalCo2massId))]
        [InverseProperty(nameof(StimJobTotalCo2masss.StimJobJobIntervals))]
        public virtual StimJobTotalCo2masss TotalCo2mass { get; set; }
        [ForeignKey(nameof(TotalFrictionPresLossId))]
        [InverseProperty(nameof(StimJobTotalFrictionPresLosss.StimJobJobIntervals))]
        public virtual StimJobTotalFrictionPresLosss TotalFrictionPresLoss { get; set; }
        [ForeignKey(nameof(TotalN2stdVolumeId))]
        [InverseProperty(nameof(StimJobTotalN2stdVolumes.StimJobJobIntervals))]
        public virtual StimJobTotalN2stdVolumes TotalN2stdVolume { get; set; }
        [ForeignKey(nameof(TotalProppantMassId))]
        [InverseProperty(nameof(StimJobTotalProppantMasss.StimJobJobIntervals))]
        public virtual StimJobTotalProppantMasss TotalProppantMass { get; set; }
        [ForeignKey(nameof(TotalPumpTimeId))]
        [InverseProperty(nameof(StimJobTotalPumpTimes.StimJobJobIntervals))]
        public virtual StimJobTotalPumpTimes TotalPumpTime { get; set; }
        [ForeignKey(nameof(TotalVolumeId))]
        [InverseProperty(nameof(StimJobTotalVolumes.StimJobJobIntervals))]
        public virtual StimJobTotalVolumes TotalVolume { get; set; }
        [ForeignKey(nameof(TvdFormationBottomId))]
        [InverseProperty(nameof(StimJobTvdFormationBottoms.StimJobJobIntervals))]
        public virtual StimJobTvdFormationBottoms TvdFormationBottom { get; set; }
        [ForeignKey(nameof(TvdFormationTopId))]
        [InverseProperty(nameof(StimJobTvdFormationTops.StimJobJobIntervals))]
        public virtual StimJobTvdFormationTops TvdFormationTop { get; set; }
        [ForeignKey(nameof(TvdOpenHoleBottomId))]
        [InverseProperty(nameof(StimJobTvdOpenHoleBottoms.StimJobJobIntervals))]
        public virtual StimJobTvdOpenHoleBottoms TvdOpenHoleBottom { get; set; }
        [ForeignKey(nameof(TvdOpenHoleTopId))]
        [InverseProperty(nameof(StimJobTvdOpenHoleTops.StimJobJobIntervals))]
        public virtual StimJobTvdOpenHoleTops TvdOpenHoleTop { get; set; }
        [ForeignKey(nameof(WellboreProppantMassId))]
        [InverseProperty(nameof(StimJobWellboreProppantMasss.StimJobJobIntervals))]
        public virtual StimJobWellboreProppantMasss WellboreProppantMass { get; set; }
        [InverseProperty("JobInterval")]
        public virtual ICollection<StimJobTotalProppantUsages> StimJobTotalProppantUsages { get; set; }
        [InverseProperty("JobInterval")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
