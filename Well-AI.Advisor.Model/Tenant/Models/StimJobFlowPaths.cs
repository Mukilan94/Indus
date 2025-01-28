using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFlowPaths
    {
        public StimJobFlowPaths()
        {
            StimJobJobEvents = new HashSet<StimJobJobEvents>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
            StimJobTubulars = new HashSet<StimJobTubulars>();
        }

        [Key]
        public int FlowPathId { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MaxTreatmentPresId { get; set; }
        public int? MaxSlurryRateId { get; set; }
        public int? MaxWellheadRateId { get; set; }
        [Column("MaxN2StdRateId")]
        public int? MaxN2stdRateId { get; set; }
        [Column("MaxCO2LiquidRateId")]
        public int? MaxCo2liquidRateId { get; set; }
        public int? MaxGelRateId { get; set; }
        public int? MaxOilRateId { get; set; }
        public int? MaxAcidRateId { get; set; }
        public int? MaxPropConcId { get; set; }
        public int? MaxSlurryPropConcId { get; set; }
        public int? AvgTreatPresId { get; set; }
        public int? AvgBaseFluidRateId { get; set; }
        public int? AvgSlurryRateId { get; set; }
        public int? AvgWellheadRateId { get; set; }
        [Column("AvgN2StdRateId")]
        public int? AvgN2stdRateId { get; set; }
        [Column("AvgCO2LiquidRateId")]
        public int? AvgCo2liquidRateId { get; set; }
        public int? AvgGelRateId { get; set; }
        public int? AvgOilRateId { get; set; }
        public int? AvgAcidRateId { get; set; }
        public int? AvgPropConcId { get; set; }
        public int? AvgSlurryPropConcId { get; set; }
        public int? AvgTemperatureId { get; set; }
        public string AvgIntervalPhaseFraction { get; set; }
        public int? AvgBaseFluidQualityId { get; set; }
        [Column("AvgN2BaseFluidQualityId")]
        public int? AvgN2baseFluidQualityId { get; set; }
        [Column("AvgCO2BaseFluidQualityId")]
        public int? AvgCo2baseFluidQualityId { get; set; }
        public int? AvgHydraulicPowerId { get; set; }
        public int? BaseFluidVolId { get; set; }
        public int? SlurryVolId { get; set; }
        [Column("WellheadVolStdVolN2Id")]
        public int? WellheadVolStdVolN2id { get; set; }
        [Column("StdVolN2Id")]
        public int? StdVolN2id { get; set; }
        [Column("MassCO2Id")]
        public int? MassCo2id { get; set; }
        public int? GelVolId { get; set; }
        public int? OilVolId { get; set; }
        public int? AcidVolId { get; set; }
        public int? BaseFluidBypassVolId { get; set; }
        public int? PropMassId { get; set; }
        public int? MaxPmaxPacPresId { get; set; }
        public int? MaxPmaxWeaklinkPresId { get; set; }
        public int? AvgPmaxPacPresId { get; set; }
        public int? AvgPmaxWeaklinkPresId { get; set; }
        public int? ShutinPres5MinId { get; set; }
        public int? ShutinPres10MinId { get; set; }
        public int? ShutinPres15MinId { get; set; }
        public int? BreakDownPresId { get; set; }
        public int? PercentPadId { get; set; }
        public int? FractureGradientId { get; set; }
        public string PipeFrictionFactor { get; set; }
        public string StageCount { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AcidVolId))]
        [InverseProperty(nameof(StimJobAcidVols.StimJobFlowPaths))]
        public virtual StimJobAcidVols AcidVol { get; set; }
        [ForeignKey(nameof(AvgAcidRateId))]
        [InverseProperty(nameof(StimJobAvgAcidRates.StimJobFlowPaths))]
        public virtual StimJobAvgAcidRates AvgAcidRate { get; set; }
        [ForeignKey(nameof(AvgBaseFluidQualityId))]
        [InverseProperty(nameof(StimJobAvgBaseFluidQualitys.StimJobFlowPaths))]
        public virtual StimJobAvgBaseFluidQualitys AvgBaseFluidQuality { get; set; }
        [ForeignKey(nameof(AvgBaseFluidRateId))]
        [InverseProperty(nameof(StimJobAvgBaseFluidRates.StimJobFlowPaths))]
        public virtual StimJobAvgBaseFluidRates AvgBaseFluidRate { get; set; }
        [ForeignKey(nameof(AvgCo2baseFluidQualityId))]
        [InverseProperty(nameof(StimJobAvgCo2baseFluidQualitys.StimJobFlowPaths))]
        public virtual StimJobAvgCo2baseFluidQualitys AvgCo2baseFluidQuality { get; set; }
        [ForeignKey(nameof(AvgCo2liquidRateId))]
        [InverseProperty(nameof(StimJobAvgCo2liquidRates.StimJobFlowPaths))]
        public virtual StimJobAvgCo2liquidRates AvgCo2liquidRate { get; set; }
        [ForeignKey(nameof(AvgGelRateId))]
        [InverseProperty(nameof(StimJobAvgGelRates.StimJobFlowPaths))]
        public virtual StimJobAvgGelRates AvgGelRate { get; set; }
        [ForeignKey(nameof(AvgHydraulicPowerId))]
        [InverseProperty(nameof(StimJobAvgHydraulicPowers.StimJobFlowPaths))]
        public virtual StimJobAvgHydraulicPowers AvgHydraulicPower { get; set; }
        [ForeignKey(nameof(AvgN2baseFluidQualityId))]
        [InverseProperty(nameof(StimJobAvgN2baseFluidQualitys.StimJobFlowPaths))]
        public virtual StimJobAvgN2baseFluidQualitys AvgN2baseFluidQuality { get; set; }
        [ForeignKey(nameof(AvgN2stdRateId))]
        [InverseProperty(nameof(StimJobAvgN2stdRates.StimJobFlowPaths))]
        public virtual StimJobAvgN2stdRates AvgN2stdRate { get; set; }
        [ForeignKey(nameof(AvgOilRateId))]
        [InverseProperty(nameof(StimJobAvgOilRates.StimJobFlowPaths))]
        public virtual StimJobAvgOilRates AvgOilRate { get; set; }
        [ForeignKey(nameof(AvgPmaxPacPresId))]
        [InverseProperty(nameof(StimJobAvgPmaxPacPress.StimJobFlowPaths))]
        public virtual StimJobAvgPmaxPacPress AvgPmaxPacPres { get; set; }
        [ForeignKey(nameof(AvgPmaxWeaklinkPresId))]
        [InverseProperty(nameof(StimJobAvgPmaxWeaklinkPress.StimJobFlowPaths))]
        public virtual StimJobAvgPmaxWeaklinkPress AvgPmaxWeaklinkPres { get; set; }
        [ForeignKey(nameof(AvgPropConcId))]
        [InverseProperty(nameof(StimJobAvgPropConcs.StimJobFlowPaths))]
        public virtual StimJobAvgPropConcs AvgPropConc { get; set; }
        [ForeignKey(nameof(AvgSlurryPropConcId))]
        [InverseProperty(nameof(StimJobAvgSlurryPropConcs.StimJobFlowPaths))]
        public virtual StimJobAvgSlurryPropConcs AvgSlurryPropConc { get; set; }
        [ForeignKey(nameof(AvgSlurryRateId))]
        [InverseProperty(nameof(StimJobAvgSlurryRates.StimJobFlowPaths))]
        public virtual StimJobAvgSlurryRates AvgSlurryRate { get; set; }
        [ForeignKey(nameof(AvgTemperatureId))]
        [InverseProperty(nameof(StimJobAvgTemperatures.StimJobFlowPaths))]
        public virtual StimJobAvgTemperatures AvgTemperature { get; set; }
        [ForeignKey(nameof(AvgTreatPresId))]
        [InverseProperty(nameof(StimJobAvgTreatPress.StimJobFlowPaths))]
        public virtual StimJobAvgTreatPress AvgTreatPres { get; set; }
        [ForeignKey(nameof(AvgWellheadRateId))]
        [InverseProperty(nameof(StimJobAvgWellheadRates.StimJobFlowPaths))]
        public virtual StimJobAvgWellheadRates AvgWellheadRate { get; set; }
        [ForeignKey(nameof(BaseFluidBypassVolId))]
        [InverseProperty(nameof(StimJobBaseFluidBypassVols.StimJobFlowPaths))]
        public virtual StimJobBaseFluidBypassVols BaseFluidBypassVol { get; set; }
        [ForeignKey(nameof(BaseFluidVolId))]
        [InverseProperty(nameof(StimJobBaseFluidVols.StimJobFlowPaths))]
        public virtual StimJobBaseFluidVols BaseFluidVol { get; set; }
        [ForeignKey(nameof(BreakDownPresId))]
        [InverseProperty(nameof(StimJobBreakDownPress.StimJobFlowPaths))]
        public virtual StimJobBreakDownPress BreakDownPres { get; set; }
        [ForeignKey(nameof(FractureGradientId))]
        [InverseProperty(nameof(StimJobFractureGradients.StimJobFlowPaths))]
        public virtual StimJobFractureGradients FractureGradient { get; set; }
        [ForeignKey(nameof(GelVolId))]
        [InverseProperty(nameof(StimJobGelVols.StimJobFlowPaths))]
        public virtual StimJobGelVols GelVol { get; set; }
        [ForeignKey(nameof(MassCo2id))]
        [InverseProperty(nameof(StimJobMassCo2s.StimJobFlowPaths))]
        public virtual StimJobMassCo2s MassCo2 { get; set; }
        [ForeignKey(nameof(MaxAcidRateId))]
        [InverseProperty(nameof(StimJobMaxAcidRates.StimJobFlowPaths))]
        public virtual StimJobMaxAcidRates MaxAcidRate { get; set; }
        [ForeignKey(nameof(MaxCo2liquidRateId))]
        [InverseProperty(nameof(StimJobMaxCo2liquidRates.StimJobFlowPaths))]
        public virtual StimJobMaxCo2liquidRates MaxCo2liquidRate { get; set; }
        [ForeignKey(nameof(MaxGelRateId))]
        [InverseProperty(nameof(StimJobMaxGelRates.StimJobFlowPaths))]
        public virtual StimJobMaxGelRates MaxGelRate { get; set; }
        [ForeignKey(nameof(MaxN2stdRateId))]
        [InverseProperty(nameof(StimJobMaxN2stdRates.StimJobFlowPaths))]
        public virtual StimJobMaxN2stdRates MaxN2stdRate { get; set; }
        [ForeignKey(nameof(MaxOilRateId))]
        [InverseProperty(nameof(StimJobMaxOilRates.StimJobFlowPaths))]
        public virtual StimJobMaxOilRates MaxOilRate { get; set; }
        [ForeignKey(nameof(MaxPmaxPacPresId))]
        [InverseProperty(nameof(StimJobMaxPmaxPacPress.StimJobFlowPaths))]
        public virtual StimJobMaxPmaxPacPress MaxPmaxPacPres { get; set; }
        [ForeignKey(nameof(MaxPmaxWeaklinkPresId))]
        [InverseProperty(nameof(StimJobMaxPmaxWeaklinkPress.StimJobFlowPaths))]
        public virtual StimJobMaxPmaxWeaklinkPress MaxPmaxWeaklinkPres { get; set; }
        [ForeignKey(nameof(MaxPropConcId))]
        [InverseProperty(nameof(StimJobMaxPropConcs.StimJobFlowPaths))]
        public virtual StimJobMaxPropConcs MaxPropConc { get; set; }
        [ForeignKey(nameof(MaxSlurryPropConcId))]
        [InverseProperty(nameof(StimJobMaxSlurryPropConcs.StimJobFlowPaths))]
        public virtual StimJobMaxSlurryPropConcs MaxSlurryPropConc { get; set; }
        [ForeignKey(nameof(MaxSlurryRateId))]
        [InverseProperty(nameof(StimJobMaxSlurryRates.StimJobFlowPaths))]
        public virtual StimJobMaxSlurryRates MaxSlurryRate { get; set; }
        [ForeignKey(nameof(MaxTreatmentPresId))]
        [InverseProperty(nameof(StimJobMaxTreatmentPress.StimJobFlowPaths))]
        public virtual StimJobMaxTreatmentPress MaxTreatmentPres { get; set; }
        [ForeignKey(nameof(MaxWellheadRateId))]
        [InverseProperty(nameof(StimJobMaxWellheadRates.StimJobFlowPaths))]
        public virtual StimJobMaxWellheadRates MaxWellheadRate { get; set; }
        [ForeignKey(nameof(OilVolId))]
        [InverseProperty(nameof(StimJobOilVols.StimJobFlowPaths))]
        public virtual StimJobOilVols OilVol { get; set; }
        [ForeignKey(nameof(PercentPadId))]
        [InverseProperty(nameof(StimJobPercentPads.StimJobFlowPaths))]
        public virtual StimJobPercentPads PercentPad { get; set; }
        [ForeignKey(nameof(PropMassId))]
        [InverseProperty(nameof(StimJobPropMasss.StimJobFlowPaths))]
        public virtual StimJobPropMasss PropMass { get; set; }
        [ForeignKey(nameof(ShutinPres10MinId))]
        [InverseProperty(nameof(StimJobShutinPres10Mins.StimJobFlowPaths))]
        public virtual StimJobShutinPres10Mins ShutinPres10Min { get; set; }
        [ForeignKey(nameof(ShutinPres15MinId))]
        [InverseProperty(nameof(StimJobShutinPres15Mins.StimJobFlowPaths))]
        public virtual StimJobShutinPres15Mins ShutinPres15Min { get; set; }
        [ForeignKey(nameof(ShutinPres5MinId))]
        [InverseProperty(nameof(StimJobShutinPres5Mins.StimJobFlowPaths))]
        public virtual StimJobShutinPres5Mins ShutinPres5Min { get; set; }
        [ForeignKey(nameof(SlurryVolId))]
        [InverseProperty(nameof(StimJobSlurryVols.StimJobFlowPaths))]
        public virtual StimJobSlurryVols SlurryVol { get; set; }
        [ForeignKey(nameof(StdVolN2id))]
        [InverseProperty(nameof(StimJobStdVolN2s.StimJobFlowPaths))]
        public virtual StimJobStdVolN2s StdVolN2 { get; set; }
        [ForeignKey(nameof(WellheadVolStdVolN2id))]
        [InverseProperty(nameof(StimJobWellheadVols.StimJobFlowPaths))]
        public virtual StimJobWellheadVols WellheadVolStdVolN2 { get; set; }
        [InverseProperty("FlowPath")]
        public virtual ICollection<StimJobJobEvents> StimJobJobEvents { get; set; }
        [InverseProperty("FlowPath")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
        [InverseProperty("FlowPath")]
        public virtual ICollection<StimJobTubulars> StimJobTubulars { get; set; }
    }
}
