   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Well_AI.Advisor.API.Models
{
	public class TotalPumpTime {
		[Key]
		public int TotalPumpTimeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }

	}

	public class MaxJobPres {
		[Key]
		public int MaxJobPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxFluidRate {
		[Key]
		public int MaxFluidRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgJobPres {
		[Key]
		public int AvgJobPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TotalJobVolume {
		[Key]
		public int TotalJobVolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TotalProppantWt {
		[Key]
		public int TotalProppantWtId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TotalN2StdVolume {
		[Key]
		public int TotalN2StdVolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TotalCO2Mass {
		[Key]
		public int TotalCO2MassId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class HhpOrdered {
		[Key]
		public int HhpOrderedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class HhpUsed {
		[Key]
		public int HhpUsedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidEfficiency {
		[Key]
		public int FluidEfficiencyId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FlowBackPres {
		[Key]
		public int FlowBackPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FlowBackRate {
		[Key]
		public int FlowBackRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FlowBackVolume {
		[Key]
		public int FlowBackVolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class BottomholeStaticTemperature {
		[Key]
		public int BottomholeStaticTemperatureId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TreatingBottomholeTemperature {
		[Key]
		public int TreatingBottomholeTemperatureId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdFormationTop {
		[Key]
		public int MdFormationTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdFormationBottom {
		[Key]
		public int MdFormationBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdFormationTop {
		[Key]
		public int TvdFormationTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdFormationBottom {
		[Key]
		public int TvdFormationBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class OpenHoleDiameter {
		[Key]
		public int OpenHoleDiameterId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdOpenHoleTop {
		[Key]
		public int MdOpenHoleTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdOpenHoleBottom {
		[Key]
		public int MdOpenHoleBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdOpenHoleTop {
		[Key]
		public int TvdOpenHoleTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdOpenHoleBottom {
		[Key]
		public int TvdOpenHoleBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TotalFrictionPresLoss {
		[Key]
		public int TotalFrictionPresLossId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxPresTubing {
		[Key]
		public int MaxPresTubingId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxPresAnnulus {
		[Key]
		public int MaxPresAnnulusId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxFluidRateTubing {
		[Key]
		public int MaxFluidRateTubingId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxFluidRateAnnulus {
		[Key]
		public int MaxFluidRateAnnulusId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgPresTubing {
		[Key]
		public int AvgPresTubingId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgPresCasing {
		[Key]
		public int AvgPresCasingId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class BreakDownPres {
		[Key]
		public int BreakDownPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AveragePres {
		[Key]
		public int AveragePresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgBaseFluidReturnRate {
		[Key]
		public int AvgBaseFluidReturnRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgSlurryReturnRate {
		[Key]
		public int AvgSlurryReturnRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgBottomholeRate {
		[Key]
		public int AvgBottomholeRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TotalVolume {
		[Key]
		public int TotalVolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxProppantConcSurface {
		[Key]
		public int MaxProppantConcSurfaceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxProppantConcBottomhole {
		[Key]
		public int MaxProppantConcBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgProppantConcSurface {
		[Key]
		public int AvgProppantConcSurfaceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgProppantConcBottomhole {
		[Key]
		public int AvgProppantConcBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PerfproppantConc {
		[Key]
		public int PerfproppantConcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TotalProppantMass {
		[Key]
		public int TotalProppantMassId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class Mass {
		[Key]
		public int MassId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TotalProppantUsage {
		[Key]
		public int TotalProppantUsageId { get; set; }
		public string Name { get; set; }
		public Mass Mass { get; set; }
		public string Uid { get; set; }
	}

	public class PercentProppantPumped {
		[Key]
		public int PercentProppantPumpedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WellboreProppantMass {
		[Key]
		public int WellboreProppantMassId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FormationProppantMass {
		[Key]
		public int FormationProppantMassId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FractureGradient {
		[Key]
		public int FractureGradientId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FinalFractureGradient {
		[Key]
		public int FinalFractureGradientId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class InitialShutinPres {
		[Key]
		public int InitialShutinPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class Pres {
		[Key]
		public int PresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TimeAfterShutin {
		[Key]
		public int TimeAfterShutinId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ShutinPres {
		[Key]
		public int ShutinPresId { get; set; }
		public Pres Pres { get; set; }
		public TimeAfterShutin TimeAfterShutin { get; set; }
		public string Uid { get; set; }
	}

	public class ScreenOutPres {
		[Key]
		public int ScreenOutPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class HhpOrderedCO2 {
		[Key]
		public int HhpOrderedCO2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class HhpOrderedFluid {
		[Key]
		public int HhpOrderedFluidId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class HhpUsedCO2 {
		[Key]
		public int HhpUsedCO2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class HhpUsedFluid {
		[Key]
		public int HhpUsedFluidId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PerfBallSize {
		[Key]
		public int PerfBallSizeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgFractureWidth {
		[Key]
		public int AvgFractureWidthId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgConductivity {
		[Key]
		public int AvgConductivityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class NetPres {
		[Key]
		public int NetPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ClosurePres {
		[Key]
		public int ClosurePresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ClosureDuration {
		[Key]
		public int ClosureDurationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxTreatmentPres {
		[Key]
		public int MaxTreatmentPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxSlurryRate {
		[Key]
		public int MaxSlurryRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxWellheadRate {
		[Key]
		public int MaxWellheadRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxN2StdRate {
		[Key]
		public int MaxN2StdRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxCO2LiquidRate {
		[Key]
		public int MaxCO2LiquidRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxGelRate {
		[Key]
		public int MaxGelRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxOilRate {
		[Key]
		public int MaxOilRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxAcidRate {
		[Key]
		public int MaxAcidRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxPropConc {
		[Key]
		public int MaxPropConcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxSlurryPropConc {
		[Key]
		public int MaxSlurryPropConcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgTreatPres {
		[Key]
		public int AvgTreatPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgBaseFluidRate {
		[Key]
		public int AvgBaseFluidRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgSlurryRate {
		[Key]
		public int AvgSlurryRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgWellheadRate {
		[Key]
		public int AvgWellheadRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgN2StdRate {
		[Key]
		public int AvgN2StdRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgCO2LiquidRate {
		[Key]
		public int AvgCO2LiquidRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgGelRate {
		[Key]
		public int AvgGelRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgOilRate {
		[Key]
		public int AvgOilRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgAcidRate {
		[Key]
		public int AvgAcidRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgPropConc {
		[Key]
		public int AvgPropConcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgSlurryPropConc {
		[Key]
		public int AvgSlurryPropConcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgTemperature {
		[Key]
		public int AvgTemperatureId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgBaseFluidQuality {
		[Key]
		public int AvgBaseFluidQualityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgN2BaseFluidQuality {
		[Key]
		public int AvgN2BaseFluidQualityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgCO2BaseFluidQuality {
		[Key]
		public int AvgCO2BaseFluidQualityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgHydraulicPower {
		[Key]
		public int AvgHydraulicPowerId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class BaseFluidVol {
		[Key]
		public int BaseFluidVolId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class SlurryVol {
		[Key]
		public int SlurryVolId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WellheadVol {
		[Key]
		public int StdVolN2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StdVolN2 {
		[Key]
		public int StdVolN2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MassCO2 {
		[Key]
		public int MassCO2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class GelVol {
		[Key]
		public int GelVolId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class OilVol {
		[Key]
		public int OilVolId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AcidVol {
		[Key]
		public int AcidVolId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class BaseFluidBypassVol {
		[Key]
		public int BaseFluidBypassVolId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PropMass {
		[Key]
		public int PropMassId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxPmaxPacPres {
		[Key]
		public int MaxPmaxPacPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxPmaxWeaklinkPres {
		[Key]
		public int MaxPmaxWeaklinkPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgPmaxPacPres {
		[Key]
		public int AvgPmaxPacPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgPmaxWeaklinkPres {
		[Key]
		public int AvgPmaxWeaklinkPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ShutinPres5Min {
		[Key]
		public int ShutinPres5MinId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ShutinPres10Min {
		[Key]
		public int ShutinPres10MinId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ShutinPres15Min {
		[Key]
		public int ShutinPres15MinId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PercentPad {
		[Key]
		public int PercentPadId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	

	public class Od {
		[Key]
		public int OdId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	

	//public class MdTop {
	//	public string Uom { get; set; }
		
	//	public string Text { get; set; }
	//}

	//public class MdBottom {
	//	public string Uom { get; set; }
		
	//	public string Text { get; set; }
	//}

	public class VolumeFactor {
		[Key]
		public int VolumeFactorId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	

	public class PumpTime {
		[Key]
		public int PumpTimeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartRateSurfaceLiquid {
		[Key]
		public int StartRateSurfaceLiquidId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndRateSurfaceLiquid {
		[Key]
		public int EndRateSurfaceLiquidId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgRateSurfaceLiquid {
		[Key]
		public int AvgRateSurfaceLiquidId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartRateSurfaceCO2 {
		[Key]
		public int StartRateSurfaceCO2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndRateSurfaceCO2 {
		[Key]
		public int EndRateSurfaceCO2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgRateSurfaceCO2 {
		[Key]
		public int AvgRateSurfaceCO2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartStdRateSurfaceN2 {
		[Key]
		public int StartStdRateSurfaceN2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndStdRateSurfaceN2 {
		[Key]
		public int EndStdRateSurfaceN2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgStdRateSurfaceN2 {
		[Key]
		public int AvgStdRateSurfaceN2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartPresSurface {
		[Key]
		public int StartPresSurfaceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndPresSurface {
		[Key]
		public int EndPresSurfaceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AveragePresSurface {
		[Key]
		public int AveragePresSurfaceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartPumpRateBottomhole {
		[Key]
		public int StartPumpRateBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndPumpRateBottomhole {
		[Key]
		public int EndPumpRateBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgPumpRateBottomhole {
		[Key]
		public int AvgPumpRateBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartPresBottomhole {
		[Key]
		public int StartPresBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndPresBottomhole {
		[Key]
		public int EndPresBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AveragePresBottomhole {
		[Key]
		public int AveragePresBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartProppantConcSurface {
		[Key]
		public int StartProppantConcSurfaceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndProppantConcSurface {
		[Key]
		public int EndProppantConcSurfaceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartProppantConcBottomhole {
		[Key]
		public int StartProppantConcBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndProppantConcBottomhole {
		[Key]
		public int EndProppantConcBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartFoamRateN2 {
		[Key]
		public int StartFoamRateN2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndFoamRateN2 {
		[Key]
		public int EndFoamRateN2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StartFoamRateCO2 {
		[Key]
		public int StartFoamRateCO2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EndFoamRateCO2 {
		[Key]
		public int EndFoamRateCO2Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidVolBase {
		[Key]
		public int FluidVolBaseId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidVolSlurry {
		[Key]
		public int FluidVolSlurryId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class SlurryRateBegin {
		[Key]
		public int SlurryRateBeginId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class SlurryRateEnd {
		[Key]
		public int SlurryRateEndId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ProppantMassWellHead {
		[Key]
		public int ProppantMassWellHeadId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ProppantMass {
		[Key]
		public int ProppantMassId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MaxPres {
		[Key]
		public int MaxPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgInternalPhaseFraction {
		[Key]
		public int AvgInternalPhaseFractionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgCO2Rate {
		[Key]
		public int AvgCO2RateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class GelVolume {
		[Key]
		public int GelVolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class OilVolume {
		[Key]
		public int OilVolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AcidVolume {
		[Key]
		public int AcidVolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidVol {
		[Key]
		public int FluidVolId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class Volume {
		[Key]
		public int VolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	

	public class Proppant {
		[Key]
		public int ProppantId { get; set; }
		public string Name { get; set; }
		public string Kind { get; set; }
		public StimJobWeight Weight { get; set; }
		public string SieveSize { get; set; }
	}

	public class StimJobAdditive
	{
		[Key]
		public int AdditiveId { get; set; }
		public string Name { get; set; }
		public string Kind { get; set; }
		public Volume Volume { get; set; }
		public Mass Mass { get; set; }
		public string Uid { get; set; }
	}

	public class StageFluid {
		[Key]
		public int StageFluidId { get; set; }
		public string Name { get; set; }
		public FluidVol FluidVol { get; set; }
		public string WaterSource { get; set; }
		public List<StimJobAdditive> Additive { get; set; }
		public Proppant Proppant { get; set; }
	}

	public class JobStage {
		[Key]
		public int JobStageId { get; set; }
		public string Kind { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Number { get; set; }
		public string DTimStart { get; set; }
		public string DTimEnd { get; set; }
		public PumpTime PumpTime { get; set; }
		public StartRateSurfaceLiquid StartRateSurfaceLiquid { get; set; }
		public EndRateSurfaceLiquid EndRateSurfaceLiquid { get; set; }
		public AvgRateSurfaceLiquid AvgRateSurfaceLiquid { get; set; }
		public StartRateSurfaceCO2 StartRateSurfaceCO2 { get; set; }
		public EndRateSurfaceCO2 EndRateSurfaceCO2 { get; set; }
		public AvgRateSurfaceCO2 AvgRateSurfaceCO2 { get; set; }
		public StartStdRateSurfaceN2 StartStdRateSurfaceN2 { get; set; }
		public EndStdRateSurfaceN2 EndStdRateSurfaceN2 { get; set; }
		public AvgStdRateSurfaceN2 AvgStdRateSurfaceN2 { get; set; }
		public StartPresSurface StartPresSurface { get; set; }
		public EndPresSurface EndPresSurface { get; set; }
		public AveragePresSurface AveragePresSurface { get; set; }
		public StartPumpRateBottomhole StartPumpRateBottomhole { get; set; }
		public EndPumpRateBottomhole EndPumpRateBottomhole { get; set; }
		public AvgPumpRateBottomhole AvgPumpRateBottomhole { get; set; }
		public StartPresBottomhole StartPresBottomhole { get; set; }
		public EndPresBottomhole EndPresBottomhole { get; set; }
		public AveragePresBottomhole AveragePresBottomhole { get; set; }
		public StartProppantConcSurface StartProppantConcSurface { get; set; }
		public EndProppantConcSurface EndProppantConcSurface { get; set; }
		public AvgProppantConcSurface AvgProppantConcSurface { get; set; }
		public StartProppantConcBottomhole StartProppantConcBottomhole { get; set; }
		public EndProppantConcBottomhole EndProppantConcBottomhole { get; set; }
		public AvgProppantConcBottomhole AvgProppantConcBottomhole { get; set; }
		public StartFoamRateN2 StartFoamRateN2 { get; set; }
		public EndFoamRateN2 EndFoamRateN2 { get; set; }
		public StartFoamRateCO2 StartFoamRateCO2 { get; set; }
		public EndFoamRateCO2 EndFoamRateCO2 { get; set; }
		public FluidVolBase FluidVolBase { get; set; }
		public FluidVolSlurry FluidVolSlurry { get; set; }
		public SlurryRateBegin SlurryRateBegin { get; set; }
		public SlurryRateEnd SlurryRateEnd { get; set; }
		public ProppantMassWellHead ProppantMassWellHead { get; set; }
		public ProppantMass ProppantMass { get; set; }
		public MaxPres MaxPres { get; set; }
		public MaxSlurryRate MaxSlurryRate { get; set; }
		public MaxWellheadRate MaxWellheadRate { get; set; }
		public MaxN2StdRate MaxN2StdRate { get; set; }
		public MaxCO2LiquidRate MaxCO2LiquidRate { get; set; }
		public MaxPropConc MaxPropConc { get; set; }
		public MaxSlurryPropConc MaxSlurryPropConc { get; set; }
		public AvgPropConc AvgPropConc { get; set; }
		public AvgSlurryPropConc AvgSlurryPropConc { get; set; }
		public AvgTemperature AvgTemperature { get; set; }
		public AvgInternalPhaseFraction AvgInternalPhaseFraction { get; set; }
		public AvgBaseFluidQuality AvgBaseFluidQuality { get; set; }
		public AvgN2BaseFluidQuality AvgN2BaseFluidQuality { get; set; }
		public AvgCO2BaseFluidQuality AvgCO2BaseFluidQuality { get; set; }
		public AvgHydraulicPower AvgHydraulicPower { get; set; }
		public AvgBaseFluidRate AvgBaseFluidRate { get; set; }
		public AvgSlurryRate AvgSlurryRate { get; set; }
		public AvgWellheadRate AvgWellheadRate { get; set; }
		public AvgN2StdRate AvgN2StdRate { get; set; }
		public AvgCO2Rate AvgCO2Rate { get; set; }
		public BaseFluidVol BaseFluidVol { get; set; }
		public SlurryVol SlurryVol { get; set; }
		public WellheadVol WellheadVol { get; set; }
		public MaxPmaxPacPres MaxPmaxPacPres { get; set; }
		public MaxPmaxWeaklinkPres MaxPmaxWeaklinkPres { get; set; }
		public MaxGelRate MaxGelRate { get; set; }
		public MaxOilRate MaxOilRate { get; set; }
		public MaxAcidRate MaxAcidRate { get; set; }
		public AvgGelRate AvgGelRate { get; set; }
		public AvgOilRate AvgOilRate { get; set; }
		public AvgAcidRate AvgAcidRate { get; set; }
		public GelVolume GelVolume { get; set; }
		public OilVolume OilVolume { get; set; }
		public AcidVolume AcidVolume { get; set; }
		public BaseFluidBypassVol BaseFluidBypassVol { get; set; }
		public string FrictionFactor { get; set; }
		public StageFluid StageFluid { get; set; }
		public string Uid { get; set; }
	}

	public class JobEvent {
		[Key]
		public int JobEventId { get; set; }
		public string Number { get; set; }
		public string DTim { get; set; }
		public string Comment { get; set; }
		public string NumStage { get; set; }
		public string Uid { get; set; }
	}

	public class StimJobId
	{
		[Key]
		public int IdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobOd
	{
		[Key]
		public int OdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobWeight
	{
		[Key]
		public int WeightId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobMdTop
	{
		[Key]
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobMdBottom
	{
		[Key]
		public int MdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobVolumeFactor
	{
		[Key]
		public int VolumeFactorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class StimJobTubular
	{
		[Key]
		public int TubularId { get; set; }
		public string Type { get; set; }
		public StimJobId Id { get; set; }
		public StimJobOd Od { get; set; }
		public StimJobWeight Weight { get; set; }
		public StimJobMdTop MdTop { get; set; }
		public StimJobMdBottom MdBottom { get; set; }
		public StimJobVolumeFactor VolumeFactor { get; set; }
		public string Uid { get; set; }
	}

	public class FlowPath {
		[Key]
		public int FlowPathId { get; set; }
		public string Kind { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public MaxTreatmentPres MaxTreatmentPres { get; set; }
		public MaxSlurryRate MaxSlurryRate { get; set; }
		public MaxWellheadRate MaxWellheadRate { get; set; }
		public MaxN2StdRate MaxN2StdRate { get; set; }
		public MaxCO2LiquidRate MaxCO2LiquidRate { get; set; }
		public MaxGelRate MaxGelRate { get; set; }
		public MaxOilRate MaxOilRate { get; set; }
		public MaxAcidRate MaxAcidRate { get; set; }
		public MaxPropConc MaxPropConc { get; set; }
		public MaxSlurryPropConc MaxSlurryPropConc { get; set; }
		public AvgTreatPres AvgTreatPres { get; set; }
		public AvgBaseFluidRate AvgBaseFluidRate { get; set; }
		public AvgSlurryRate AvgSlurryRate { get; set; }
		public AvgWellheadRate AvgWellheadRate { get; set; }
		public AvgN2StdRate AvgN2StdRate { get; set; }
		public AvgCO2LiquidRate AvgCO2LiquidRate { get; set; }
		public AvgGelRate AvgGelRate { get; set; }
		public AvgOilRate AvgOilRate { get; set; }
		public AvgAcidRate AvgAcidRate { get; set; }
		public AvgPropConc AvgPropConc { get; set; }
		public AvgSlurryPropConc AvgSlurryPropConc { get; set; }
		public AvgTemperature AvgTemperature { get; set; }
		public string AvgIntervalPhaseFraction { get; set; }
		public AvgBaseFluidQuality AvgBaseFluidQuality { get; set; }
		public AvgN2BaseFluidQuality AvgN2BaseFluidQuality { get; set; }
		public AvgCO2BaseFluidQuality AvgCO2BaseFluidQuality { get; set; }
		public AvgHydraulicPower AvgHydraulicPower { get; set; }
		public BaseFluidVol BaseFluidVol { get; set; }
		public SlurryVol SlurryVol { get; set; }
		public WellheadVol WellheadVol { get; set; }
		public StdVolN2 StdVolN2 { get; set; }
		public MassCO2 MassCO2 { get; set; }
		public GelVol GelVol { get; set; }
		public OilVol OilVol { get; set; }
		public AcidVol AcidVol { get; set; }
		public BaseFluidBypassVol BaseFluidBypassVol { get; set; }
		public PropMass PropMass { get; set; }
		public MaxPmaxPacPres MaxPmaxPacPres { get; set; }
		public MaxPmaxWeaklinkPres MaxPmaxWeaklinkPres { get; set; }
		public AvgPmaxPacPres AvgPmaxPacPres { get; set; }
		public AvgPmaxWeaklinkPres AvgPmaxWeaklinkPres { get; set; }
		public ShutinPres5Min ShutinPres5Min { get; set; }
		public ShutinPres10Min ShutinPres10Min { get; set; }
		public ShutinPres15Min ShutinPres15Min { get; set; }
		public BreakDownPres BreakDownPres { get; set; }
		public PercentPad PercentPad { get; set; }
		public FractureGradient FractureGradient { get; set; }
		public string PipeFrictionFactor { get; set; }
		public string StageCount { get; set; }
		public List<StimJobTubular> Tubular { get; set; }
		public List<JobStage> JobStage { get; set; }
		public List<JobEvent> JobEvent { get; set; }
		public string Uid { get; set; }
	}

	public class PumpDuration {
		[Key]
		public int PumpDurationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgBottomholeTreatmentPres {
		[Key]
		public int AvgBottomholeTreatmentPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class BottomholeHydrostaticPres {
		[Key]
		public int BottomholeHydrostaticPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class BubblePointPres {
		[Key]
		public int BubblePointPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FractureClosePres {
		[Key]
		public int FractureClosePresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FrictionPres {
		[Key]
		public int FrictionPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PorePres {
		[Key]
		public int PorePresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AvgBottomholeTreatmentRate {
		[Key]
		public int AvgBottomholeTreatmentRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidDensity {
		[Key]
		public int FluidDensityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WellboreVolume {
		[Key]
		public int WellboreVolumeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdSurface {
		[Key]
		public int MdSurfaceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdBottomhole {
		[Key]
		public int MdBottomholeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdMidPerforation {
		[Key]
		public int MdMidPerforationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdMidPerforation {
		[Key]
		public int TvdMidPerforationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class SurfaceTemperature {
		[Key]
		public int SurfaceTemperatureId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class BottomholeTemperature {
		[Key]
		public int BottomholeTemperatureId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class SurfaceFluidTemperature {
		[Key]
		public int SurfaceFluidTemperatureId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidCompressibility {
		[Key]
		public int FluidCompressibilityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ReservoirTotalCompressibility {
		[Key]
		public int ReservoirTotalCompressibilityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidSpecificHeat {
		[Key]
		public int FluidSpecificHeatId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidThermalConductivity {
		[Key]
		public int FluidThermalConductivityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidThermalExpansionCoefficient {
		[Key]
		public int FluidThermalExpansionCoefficientId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FoamQuality {
		[Key]
		public int FoamQualityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class BottomholeRate {
		[Key]
		public int BottomholeRateId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PresMeasurement {
		[Key]
		public int PresMeasurementId { get; set; }
		public Pres Pres { get; set; }
		public BottomholeRate BottomholeRate { get; set; }
	}

	public class FractureExtensionPres {
		[Key]
		public int FractureExtensionPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class StepRateTest {
		[Key]
		public int StepRateTestId { get; set; }
		public List<PresMeasurement> PresMeasurement { get; set; }
		public FractureExtensionPres FractureExtensionPres { get; set; }
		public string Uid { get; set; }
	}

	public class EndPdlDuration {
		[Key]
		public int EndPdlDurationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FractureCloseDuration {
		[Key]
		public int FractureCloseDurationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PseudoRadialPres {
		[Key]
		public int PseudoRadialPresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FractureLength {
		[Key]
		public int FractureLengthId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FractureWidth {
		[Key]
		public int FractureWidthId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ResidualPermeability {
		[Key]
		public int ResidualPermeabilityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FluidEfficiencyTest {
		[Key]
		public int FluidEfficiencyTestId { get; set; }
		public string DTimStart { get; set; }
		public string DTimEnd { get; set; }
		public EndPdlDuration EndPdlDuration { get; set; }
		public FractureCloseDuration FractureCloseDuration { get; set; }
		public FractureClosePres FractureClosePres { get; set; }
		public FractureExtensionPres FractureExtensionPres { get; set; }
		public NetPres NetPres { get; set; }
		public PorePres PorePres { get; set; }
		public PseudoRadialPres PseudoRadialPres { get; set; }
		public FractureLength FractureLength { get; set; }
		public FractureWidth FractureWidth { get; set; }
		public FluidEfficiency FluidEfficiency { get; set; }
		public string PdlCoef { get; set; }
		public ResidualPermeability ResidualPermeability { get; set; }
		public string Uid { get; set; }
	}

	public class PumpFlowBackTest {
		[Key]
		public int PumpFlowBackTestId { get; set; }
		public FractureCloseDuration FractureCloseDuration { get; set; }
		public FractureClosePres FractureClosePres { get; set; }
		public string Uid { get; set; }
	}

	public class BottomholeFluidDensity {
		[Key]
		public int BottomholeFluidDensityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DiameterEntryHole {
		[Key]
		public int PipeFrictionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PipeFriction {
		[Key]
		public int PipeFrictionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class EntryFriction {
		[Key]
		public int EntryFrictionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PerfFriction {
		[Key]
		public int PerfFrictionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class NearWellboreFriction {
		[Key]
		public int NearWellboreFrictionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class Step {
		[Key]
		public int StepId { get; set; }
		public string Number { get; set; }
		public BottomholeRate BottomholeRate { get; set; }
		public Pres Pres { get; set; }
		public PipeFriction PipeFriction { get; set; }
		public EntryFriction EntryFriction { get; set; }
		public PerfFriction PerfFriction { get; set; }
		public NearWellboreFriction NearWellboreFriction { get; set; }
		public string Uid { get; set; }
	}

	public class StepDownTest {
		[Key]
		public int StepDownTestId { get; set; }
		public InitialShutinPres InitialShutinPres { get; set; }
		public BottomholeFluidDensity BottomholeFluidDensity { get; set; }
		public DiameterEntryHole DiameterEntryHole { get; set; }
		public string PerforationCount { get; set; }
		public string DischargeCoefficient { get; set; }
		public string EffectivePerfs { get; set; }
		public List<Step> Step { get; set; }
		public string Uid { get; set; }
	}

	public class PdatSession {
		[Key]
		public int PdatSessionId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Number { get; set; }
		public string DTimPumpOn { get; set; }
		public string DTimPumpOff { get; set; }
		public string DTimWellShutin { get; set; }
		public string DTimFractureClose { get; set; }
		public PumpDuration PumpDuration { get; set; }
		public AvgBottomholeTreatmentPres AvgBottomholeTreatmentPres { get; set; }
		public BottomholeHydrostaticPres BottomholeHydrostaticPres { get; set; }
		public BubblePointPres BubblePointPres { get; set; }
		public FractureClosePres FractureClosePres { get; set; }
		public FrictionPres FrictionPres { get; set; }
		public InitialShutinPres InitialShutinPres { get; set; }
		public PorePres PorePres { get; set; }
		public AvgBottomholeTreatmentRate AvgBottomholeTreatmentRate { get; set; }
		public FluidDensity FluidDensity { get; set; }
		public BaseFluidVol BaseFluidVol { get; set; }
		public WellboreVolume WellboreVolume { get; set; }
		public MdSurface MdSurface { get; set; }
		public MdBottomhole MdBottomhole { get; set; }
		public MdMidPerforation MdMidPerforation { get; set; }
		public TvdMidPerforation TvdMidPerforation { get; set; }
		public SurfaceTemperature SurfaceTemperature { get; set; }
		public BottomholeTemperature BottomholeTemperature { get; set; }
		public SurfaceFluidTemperature SurfaceFluidTemperature { get; set; }
		public FluidCompressibility FluidCompressibility { get; set; }
		public ReservoirTotalCompressibility ReservoirTotalCompressibility { get; set; }
		public string FluidNprimeFactor { get; set; }
		public string FluidKprimeFactor { get; set; }
		public FluidSpecificHeat FluidSpecificHeat { get; set; }
		public FluidThermalConductivity FluidThermalConductivity { get; set; }
		public FluidThermalExpansionCoefficient FluidThermalExpansionCoefficient { get; set; }
		public FluidEfficiency FluidEfficiency { get; set; }
		public FoamQuality FoamQuality { get; set; }
		public PercentPad PercentPad { get; set; }
		public string TemperatureCorrectionApplied { get; set; }
		public StepRateTest StepRateTest { get; set; }
		public FluidEfficiencyTest FluidEfficiencyTest { get; set; }
		public PumpFlowBackTest PumpFlowBackTest { get; set; }
		public StepDownTest StepDownTest { get; set; }
		public string Uid { get; set; }
	}

	public class MdLithTop {
		[Key]
		public int MdLithTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdLithBottom {
		[Key]
		public int MdLithBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class LithFormationPermeability {
		[Key]
		public int LithFormationPermeabilityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class LithYoungsModulus {
		[Key]
		public int LithYoungsModulusId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class LithPorePres {
		[Key]
		public int LithPorePresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class LithNetPayThickness {
		[Key]
		public int LithNetPayThicknessId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdGrossPayTop {
		[Key]
		public int MdGrossPayTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdGrossPayBottom {
		[Key]
		public int MdGrossPayBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class GrossPayThickness {
		[Key]
		public int GrossPayThicknessId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class NetPayThickness {
		[Key]
		public int NetPayThicknessId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class NetPayPorePres {
		[Key]
		public int NetPayPorePresId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class NetPayFluidCompressibility {
		[Key]
		public int NetPayFluidCompressibilityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class NetPayFluidViscosity {
		[Key]
		public int NetPayFluidViscosityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class NetPayFormationPermeability {
		[Key]
		public int NetPayFormationPermeabilityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class LithPoissonsRatio {
		[Key]
		public int LithPoissonsRatioId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class NetPayFormationPorosity {
		[Key]
		public int NetPayFormationPorosityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FormationPermeability {
		[Key]
		public int FormationPermeabilityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FormationPorosity {
		[Key]
		public int FormationPorosityId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ReservoirInterval {
		[Key]
		public int ReservoirIntervalId { get; set; }
		public MdLithTop MdLithTop { get; set; }
		public MdLithBottom MdLithBottom { get; set; }
		public LithFormationPermeability LithFormationPermeability { get; set; }
		public LithYoungsModulus LithYoungsModulus { get; set; }
		public LithPorePres LithPorePres { get; set; }
		public LithNetPayThickness LithNetPayThickness { get; set; }
		public string LithName { get; set; }
		public MdGrossPayTop MdGrossPayTop { get; set; }
		public MdGrossPayBottom MdGrossPayBottom { get; set; }
		public GrossPayThickness GrossPayThickness { get; set; }
		public NetPayThickness NetPayThickness { get; set; }
		public NetPayPorePres NetPayPorePres { get; set; }
		public NetPayFluidCompressibility NetPayFluidCompressibility { get; set; }
		public NetPayFluidViscosity NetPayFluidViscosity { get; set; }
		public string NetPayName { get; set; }
		public NetPayFormationPermeability NetPayFormationPermeability { get; set; }
		public LithPoissonsRatio LithPoissonsRatio { get; set; }
		public NetPayFormationPorosity NetPayFormationPorosity { get; set; }
		public FormationPermeability FormationPermeability { get; set; }
		public FormationPorosity FormationPorosity { get; set; }
		public string NameFormation { get; set; }
		public string Uid { get; set; }
	}

	public class MdPerforationsTop {
		[Key]
		public int MdPerforationsTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdPerforationsBottom {
		[Key]
		public int MdPerforationsBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdPerforationsTop {
		[Key]
		public int TvdPerforationsTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdPerforationsBottom {
		[Key]
		public int TvdPerforationsBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class Size {
		[Key]
		public int SizeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DensityPerforation {
		[Key]
		public int DensityPerforationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PhasingPerforation {
		[Key]
		public int PhasingPerforationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PerforationInterval {
		[Key]
		public int PerforationIntervalId { get; set; }
		public string Type { get; set; }
		public MdPerforationsTop MdPerforationsTop { get; set; }
		public MdPerforationsBottom MdPerforationsBottom { get; set; }
		public TvdPerforationsTop TvdPerforationsTop { get; set; }
		public TvdPerforationsBottom TvdPerforationsBottom { get; set; }
		public string PerforationCount { get; set; }
		public Size Size { get; set; }
		public DensityPerforation DensityPerforation { get; set; }
		public PhasingPerforation PhasingPerforation { get; set; }
		public string FrictionFactor { get; set; }
		public FrictionPres FrictionPres { get; set; }
		public string DischargeCoefficient { get; set; }
		public string Uid { get; set; }
	}

	public class JobInterval {
		[Key]
		public int JobIntervalId { get; set; }
		public string UidTreatmentInterval { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string DTimStart { get; set; }
		public string FormationName { get; set; }
		public MdFormationTop MdFormationTop { get; set; }
		public MdFormationBottom MdFormationBottom { get; set; }
		public TvdFormationTop TvdFormationTop { get; set; }
		public TvdFormationBottom TvdFormationBottom { get; set; }
		public string OpenHoleName { get; set; }
		public OpenHoleDiameter OpenHoleDiameter { get; set; }
		public MdOpenHoleTop MdOpenHoleTop { get; set; }
		public MdOpenHoleBottom MdOpenHoleBottom { get; set; }
		public TvdOpenHoleTop TvdOpenHoleTop { get; set; }
		public TvdOpenHoleBottom TvdOpenHoleBottom { get; set; }
		public TotalFrictionPresLoss TotalFrictionPresLoss { get; set; }
		public TotalPumpTime TotalPumpTime { get; set; }
		public MaxPresTubing MaxPresTubing { get; set; }
		public MaxPresAnnulus MaxPresAnnulus { get; set; }
		public MaxFluidRateTubing MaxFluidRateTubing { get; set; }
		public MaxFluidRateAnnulus MaxFluidRateAnnulus { get; set; }
		public AvgPresTubing AvgPresTubing { get; set; }
		public AvgPresCasing AvgPresCasing { get; set; }
		public BreakDownPres BreakDownPres { get; set; }
		public AveragePres AveragePres { get; set; }
		public AvgBaseFluidReturnRate AvgBaseFluidReturnRate { get; set; }
		public AvgSlurryReturnRate AvgSlurryReturnRate { get; set; }
		public AvgBottomholeRate AvgBottomholeRate { get; set; }
		public TotalVolume TotalVolume { get; set; }
		public MaxProppantConcSurface MaxProppantConcSurface { get; set; }
		public MaxProppantConcBottomhole MaxProppantConcBottomhole { get; set; }
		public AvgProppantConcSurface AvgProppantConcSurface { get; set; }
		public AvgProppantConcBottomhole AvgProppantConcBottomhole { get; set; }
		public PerfproppantConc PerfproppantConc { get; set; }
		public TotalProppantMass TotalProppantMass { get; set; }
		public List<TotalProppantUsage> TotalProppantUsage { get; set; }
		public PercentProppantPumped PercentProppantPumped { get; set; }
		public WellboreProppantMass WellboreProppantMass { get; set; }
		[NotMapped]
		public List<string> ProppantName { get; set; }
		public FormationProppantMass FormationProppantMass { get; set; }
		public string PerfBallCount { get; set; }
		public TotalN2StdVolume TotalN2StdVolume { get; set; }
		public TotalCO2Mass TotalCO2Mass { get; set; }

		[NotMapped]
		public List<string> FluidName { get; set; }
		public FractureGradient FractureGradient { get; set; }
		public FinalFractureGradient FinalFractureGradient { get; set; }
		public InitialShutinPres InitialShutinPres { get; set; }
		[NotMapped]
		public List<ShutinPres> ShutinPres { get; set; }
		public ScreenOutPres ScreenOutPres { get; set; }
		public HhpOrderedCO2 HhpOrderedCO2 { get; set; }
		public HhpOrderedFluid HhpOrderedFluid { get; set; }
		public HhpUsedCO2 HhpUsedCO2 { get; set; }
		public HhpUsedFluid HhpUsedFluid { get; set; }
		public PerfBallSize PerfBallSize { get; set; }
		public string ScreenedOut { get; set; }
		public AvgFractureWidth AvgFractureWidth { get; set; }
		public AvgConductivity AvgConductivity { get; set; }
		public NetPres NetPres { get; set; }
		public ClosurePres ClosurePres { get; set; }
		public ClosureDuration ClosureDuration { get; set; }
		[NotMapped]
		public List<FlowPath> FlowPath { get; set; }
		public PdatSession PdatSession { get; set; }
		public ReservoirInterval ReservoirInterval { get; set; }
		public PerforationInterval PerforationInterval { get; set; }
		public string Uid { get; set; }
	}

	public class StimJob {
		[Key]
		public int StimJobId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string Kind { get; set; }
		public string CommodityCode { get; set; }
		public string ServiceCompany { get; set; }
		public string Supervisor { get; set; }
		public string ApiNumber { get; set; }
		public string CustomerName { get; set; }
		public string DTimArrival { get; set; }
		public string DTimStart { get; set; }
		public TotalPumpTime TotalPumpTime { get; set; }
		public MaxJobPres MaxJobPres { get; set; }
		public MaxFluidRate MaxFluidRate { get; set; }
		public AvgJobPres AvgJobPres { get; set; }
		public TotalJobVolume TotalJobVolume { get; set; }
		public TotalProppantWt TotalProppantWt { get; set; }
		public string ProppantName { get; set; }
		public string PerfBallCount { get; set; }
		public TotalN2StdVolume TotalN2StdVolume { get; set; }
		public TotalCO2Mass TotalCO2Mass { get; set; }
		public HhpOrdered HhpOrdered { get; set; }
		public HhpUsed HhpUsed { get; set; }
		public string TreatmentCount { get; set; }
		public FluidEfficiency FluidEfficiency { get; set; }
		public FlowBackPres FlowBackPres { get; set; }
		public FlowBackRate FlowBackRate { get; set; }
		public FlowBackVolume FlowBackVolume { get; set; }
		public string TreatmentIntervalCount { get; set; }
		public BottomholeStaticTemperature BottomholeStaticTemperature { get; set; }
		public TreatingBottomholeTemperature TreatingBottomholeTemperature { get; set; }
		public JobInterval JobInterval { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class StimJobs {
		
		public StimJob StimJob { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xmlns { get; set; }
		public string Xsi { get; set; }
	}

}
