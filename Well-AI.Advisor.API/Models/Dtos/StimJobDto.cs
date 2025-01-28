   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Dtos
{
	public class TotalPumpTimeDto
	{
		[Key]
		public int TotalPumpTimeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }

	}

	public class MaxJobPresDto
	{
		[Key]
		public int MaxJobPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxFluidRateDto
	{
		[Key]
		public int MaxFluidRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgJobPresDto
	{
		[Key]
		public int AvgJobPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TotalJobVolumeDto
	{
		[Key]
		public int TotalJobVolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TotalProppantWtDto
	{
		[Key]
		public int TotalProppantWtId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TotalN2StdVolumeDto
	{
		[Key]
		public int TotalN2StdVolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TotalCO2MassDto
	{
		[Key]
		public int TotalCO2MassId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class HhpOrderedDto
	{
		[Key]
		public int HhpOrderedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class HhpUsedDto
	{
		[Key]
		public int HhpUsedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidEfficiencyDto
	{
		[Key]
		public int FluidEfficiencyId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FlowBackPresDto
	{
		[Key]
		public int FlowBackPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FlowBackRateDto
	{
		[Key]
		public int FlowBackRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FlowBackVolumeDto
	{
		[Key]
		public int FlowBackVolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BottomholeStaticTemperatureDto
	{
		[Key]
		public int BottomholeStaticTemperatureId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TreatingBottomholeTemperatureDto
	{
		[Key]
		public int TreatingBottomholeTemperatureId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdFormationTopDto
	{
		[Key]
		public int MdFormationTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdFormationBottomDto
	{
		[Key]
		public int MdFormationBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdFormationTopDto
	{
		[Key]
		public int TvdFormationTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdFormationBottomDto
	{
		[Key]
		public int TvdFormationBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class OpenHoleDiameterDto
	{
		[Key]
		public int OpenHoleDiameterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdOpenHoleTopDto
	{
		[Key]
		public int MdOpenHoleTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdOpenHoleBottomDto
	{
		[Key]
		public int MdOpenHoleBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdOpenHoleTopDto
	{
		[Key]
		public int TvdOpenHoleTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdOpenHoleBottomDto
	{
		[Key]
		public int TvdOpenHoleBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TotalFrictionPresLossDto
	{
		[Key]
		public int TotalFrictionPresLossId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxPresTubingDto
	{
		[Key]
		public int MaxPresTubingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxPresAnnulusDto
	{
		[Key]
		public int MaxPresAnnulusId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxFluidRateTubingDto
	{
		[Key]
		public int MaxFluidRateTubingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxFluidRateAnnulusDto
	{
		[Key]
		public int MaxFluidRateAnnulusId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgPresTubingDto
	{
		[Key]
		public int AvgPresTubingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgPresCasingDto
	{
		[Key]
		public int AvgPresCasingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BreakDownPresDto
	{
		[Key]
		public int BreakDownPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AveragePresDto
	{
		[Key]
		public int AveragePresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgBaseFluidReturnRateDto
	{
		[Key]
		public int AvgBaseFluidReturnRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgSlurryReturnRateDto
	{
		[Key]
		public int AvgSlurryReturnRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgBottomholeRateDto
	{
		[Key]
		public int AvgBottomholeRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TotalVolumeDto
	{
		[Key]
		public int TotalVolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxProppantConcSurfaceDto
	{
		[Key]
		public int MaxProppantConcSurfaceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxProppantConcBottomholeDto
	{
		[Key]
		public int MaxProppantConcBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgProppantConcSurfaceDto
	{
		[Key]
		public int AvgProppantConcSurfaceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgProppantConcBottomholeDto
	{
		[Key]
		public int AvgProppantConcBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PerfproppantConcDto
	{
		[Key]
		public int PerfproppantConcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TotalProppantMassDto
	{
		[Key]
		public int TotalProppantMassId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MassDto
	{
		[Key]
		public int MassId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TotalProppantUsageDto
	{
		[Key]
		public int TotalProppantUsageId { get; set; }
		public string Name { get; set; }
		public MassDto Mass { get; set; }
		public string Uid { get; set; }
	}

	public class PercentProppantPumpedDto
	{
		[Key]
		public int PercentProppantPumpedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellboreProppantMassDto
	{
		[Key]
		public int WellboreProppantMassId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationProppantMassDto
	{
		[Key]
		public int FormationProppantMassId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FractureGradientDto
	{
		[Key]
		public int FractureGradientId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FinalFractureGradientDto
	{
		[Key]
		public int FinalFractureGradientId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class InitialShutinPresDto
	{
		[Key]
		public int InitialShutinPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PresDto
	{
		[Key]
		public int PresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TimeAfterShutinDto
	{
		[Key]
		public int TimeAfterShutinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ShutinPresDto
	{
		[Key]
		public int ShutinPresId { get; set; }
		public PresDto Pres { get; set; }
		public TimeAfterShutinDto TimeAfterShutin { get; set; }
		public string Uid { get; set; }
	}

	public class ScreenOutPresDto
	{
		[Key]
		public int ScreenOutPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class HhpOrderedCO2Dto
	{
		[Key]
		public int HhpOrderedCO2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class HhpOrderedFluidDto
	{
		[Key]
		public int HhpOrderedFluidId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class HhpUsedCO2Dto
	{
		[Key]
		public int HhpUsedCO2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class HhpUsedFluidDto
	{
		[Key]
		public int HhpUsedFluidId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PerfBallSizeDto
	{
		[Key]
		public int PerfBallSizeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgFractureWidthDto
	{
		[Key]
		public int AvgFractureWidthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgConductivityDto
	{
		[Key]
		public int AvgConductivityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class NetPresDto
	{
		[Key]
		public int NetPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ClosurePresDto
	{
		[Key]
		public int ClosurePresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ClosureDurationDto
	{
		[Key]
		public int ClosureDurationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxTreatmentPresDto
	{
		[Key]
		public int MaxTreatmentPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxSlurryRateDto
	{
		[Key]
		public int MaxSlurryRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxWellheadRateDto
	{
		[Key]
		public int MaxWellheadRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxN2StdRateDto
	{
		[Key]
		public int MaxN2StdRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxCO2LiquidRateDto
	{
		[Key]
		public int MaxCO2LiquidRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxGelRateDto
	{
		[Key]
		public int MaxGelRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxOilRateDto
	{
		[Key]
		public int MaxOilRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxAcidRateDto
	{
		[Key]
		public int MaxAcidRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxPropConcDto
	{
		[Key]
		public int MaxPropConcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxSlurryPropConcDto
	{
		[Key]
		public int MaxSlurryPropConcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgTreatPresDto
	{
		[Key]
		public int AvgTreatPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgBaseFluidRateDto
	{
		[Key]
		public int AvgBaseFluidRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgSlurryRateDto
	{
		[Key]
		public int AvgSlurryRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgWellheadRateDto
	{
		[Key]
		public int AvgWellheadRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgN2StdRateDto
	{
		[Key]
		public int AvgN2StdRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgCO2LiquidRateDto
	{
		[Key]
		public int AvgCO2LiquidRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgGelRateDto
	{
		[Key]
		public int AvgGelRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgOilRateDto
	{
		[Key]
		public int AvgOilRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgAcidRateDto
	{
		[Key]
		public int AvgAcidRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgPropConcDto
	{
		[Key]
		public int AvgPropConcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgSlurryPropConcDto
	{
		[Key]
		public int AvgSlurryPropConcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgTemperatureDto
	{
		[Key]
		public int AvgTemperatureId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgBaseFluidQualityDto
	{
		[Key]
		public int AvgBaseFluidQualityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgN2BaseFluidQualityDto
	{
		[Key]
		public int AvgN2BaseFluidQualityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgCO2BaseFluidQualityDto
	{
		[Key]
		public int AvgCO2BaseFluidQualityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgHydraulicPowerDto
	{
		[Key]
		public int AvgHydraulicPowerId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BaseFluidVolDto
	{
		[Key]
		public int BaseFluidVolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SlurryVolDto
	{
		[Key]
		public int SlurryVolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellheadVolDto
	{
		[Key]
		public int StdVolN2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StdVolN2Dto
	{
		[Key]
		public int StdVolN2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MassCO2Dto
	{
		[Key]
		public int MassCO2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class GelVolDto
	{
		[Key]
		public int GelVolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class OilVolDto
	{
		[Key]
		public int OilVolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AcidVolDto
	{
		[Key]
		public int AcidVolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BaseFluidBypassVolDto
	{
		[Key]
		public int BaseFluidBypassVolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PropMassDto
	{
		[Key]
		public int PropMassId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxPmaxPacPresDto
	{
		[Key]
		public int MaxPmaxPacPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxPmaxWeaklinkPresDto
	{
		[Key]
		public int MaxPmaxWeaklinkPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgPmaxPacPresDto
	{
		[Key]
		public int AvgPmaxPacPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgPmaxWeaklinkPresDto
	{
		[Key]
		public int AvgPmaxWeaklinkPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ShutinPres5MinDto
	{
		[Key]
		public int ShutinPres5MinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ShutinPres10MinDto
	{
		[Key]
		public int ShutinPres10MinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ShutinPres15MinDto
	{
		[Key]
		public int ShutinPres15MinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PercentPadDto
	{
		[Key]
		public int PercentPadId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class OdDtoDto
	{
		[Key]
		public int OdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	

	 
	public class VolumeFactorDto
	{
		[Key]
		public int VolumeFactorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
 

	public class PumpTimeDto
	{
		[Key]
		public int PumpTimeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartRateSurfaceLiquidDto
	{
		[Key]
		public int StartRateSurfaceLiquidId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndRateSurfaceLiquidDto
	{
		[Key]
		public int EndRateSurfaceLiquidId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgRateSurfaceLiquidDto
	{
		[Key]
		public int AvgRateSurfaceLiquidId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartRateSurfaceCO2Dto
	{
		[Key]
		public int StartRateSurfaceCO2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndRateSurfaceCO2Dto
	{
		[Key]
		public int EndRateSurfaceCO2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgRateSurfaceCO2Dto
	{
		[Key]
		public int AvgRateSurfaceCO2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartStdRateSurfaceN2Dto
	{
		[Key]
		public int StartStdRateSurfaceN2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndStdRateSurfaceN2Dto
	{
		[Key]
		public int EndStdRateSurfaceN2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgStdRateSurfaceN2Dto
	{
		[Key]
		public int AvgStdRateSurfaceN2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartPresSurfaceDto
	{
		[Key]
		public int StartPresSurfaceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndPresSurfaceDto
	{
		[Key]
		public int EndPresSurfaceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AveragePresSurfaceDto
	{
		[Key]
		public int AveragePresSurfaceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartPumpRateBottomholeDto
	{
		[Key]
		public int StartPumpRateBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndPumpRateBottomholeDto
	{
		[Key]
		public int EndPumpRateBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgPumpRateBottomholeDto
	{
		[Key]
		public int AvgPumpRateBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartPresBottomholeDto
	{
		[Key]
		public int StartPresBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndPresBottomholeDto
	{
		[Key]
		public int EndPresBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AveragePresBottomholeDto
	{
		[Key]
		public int AveragePresBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartProppantConcSurfaceDto
	{
		[Key]
		public int StartProppantConcSurfaceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndProppantConcSurfaceDto
	{
		[Key]
		public int EndProppantConcSurfaceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartProppantConcBottomholeDto
	{
		[Key]
		public int StartProppantConcBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndProppantConcBottomholeDto
	{
		[Key]
		public int EndProppantConcBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartFoamRateN2Dto
	{
		[Key]
		public int StartFoamRateN2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndFoamRateN2Dto
	{
		[Key]
		public int EndFoamRateN2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StartFoamRateCO2Dto
	{
		[Key]
		public int StartFoamRateCO2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndFoamRateCO2Dto
	{
		[Key]
		public int EndFoamRateCO2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidVolBaseDto
	{
		[Key]
		public int FluidVolBaseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidVolSlurryDto
	{
		[Key]
		public int FluidVolSlurryId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SlurryRateBeginDto
	{
		[Key]
		public int SlurryRateBeginId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SlurryRateEndDto
	{
		[Key]
		public int SlurryRateEndId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ProppantMassWellHeadDto
	{
		[Key]
		public int ProppantMassWellHeadId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ProppantMassDto
	{
		[Key]
		public int ProppantMassId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxPresDto
	{
		[Key]
		public int MaxPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgInternalPhaseFractionDto
	{
		[Key]
		public int AvgInternalPhaseFractionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgCO2RateDto
	{
		[Key]
		public int AvgCO2RateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class GelVolumeDto
	{
		[Key]
		public int GelVolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class OilVolumeDto
	{
		[Key]
		public int OilVolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AcidVolumeDto
	{
		[Key]
		public int AcidVolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidVolDto
	{
		[Key]
		public int FluidVolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class VolumeDto
	{
		[Key]
		public int VolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AdditiveDto
	{
		[Key]
		public string Uid { get; set; }
		public string Name { get; set; }
		public string Kind { get; set; }
		public VolumeDto Volume { get; set; }
		public MassDto Mass { get; set; }
		
	}

	public class ProppantDto
	{
		[Key]
		public int ProppantId { get; set; }
		public string Name { get; set; }
		public string Kind { get; set; }
		public StimJobWeightDto Weight { get; set; }
		public string SieveSize { get; set; }
	}

	public class StageFluidDto
	{
		[Key]
		public int StageFluidId { get; set; }
		public string Name { get; set; }
		public FluidVolDto FluidVol { get; set; }
		public string WaterSource { get; set; }
		public List<AdditiveDto> Additive { get; set; }
		public ProppantDto Proppant { get; set; }
	}

	public class JobStageDto
	{
		[Key]
		public int JobStageId { get; set; }
		public string Kind { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Number { get; set; }
		public string DTimStart { get; set; }
		public string DTimEnd { get; set; }
		public PumpTimeDto PumpTime { get; set; }
		public StartRateSurfaceLiquidDto StartRateSurfaceLiquid { get; set; }
		public EndRateSurfaceLiquidDto EndRateSurfaceLiquid { get; set; }
		public AvgRateSurfaceLiquidDto AvgRateSurfaceLiquid { get; set; }
		public StartRateSurfaceCO2Dto StartRateSurfaceCO2 { get; set; }
		public EndRateSurfaceCO2Dto EndRateSurfaceCO2 { get; set; }
		public AvgRateSurfaceCO2Dto AvgRateSurfaceCO2 { get; set; }
		public StartStdRateSurfaceN2Dto StartStdRateSurfaceN2 { get; set; }
		public EndStdRateSurfaceN2Dto EndStdRateSurfaceN2 { get; set; }
		public AvgStdRateSurfaceN2Dto AvgStdRateSurfaceN2 { get; set; }
		public StartPresSurfaceDto StartPresSurface { get; set; }
		public EndPresSurfaceDto EndPresSurface { get; set; }
		public AveragePresSurfaceDto AveragePresSurface { get; set; }
		public StartPumpRateBottomholeDto StartPumpRateBottomhole { get; set; }
		public EndPumpRateBottomholeDto EndPumpRateBottomhole { get; set; }
		public AvgPumpRateBottomholeDto AvgPumpRateBottomhole { get; set; }
		public StartPresBottomholeDto StartPresBottomhole { get; set; }
		public EndPresBottomholeDto EndPresBottomhole { get; set; }
		public AveragePresBottomholeDto AveragePresBottomhole { get; set; }
		public StartProppantConcSurfaceDto StartProppantConcSurface { get; set; }
		public EndProppantConcSurfaceDto EndProppantConcSurface { get; set; }
		public AvgProppantConcSurfaceDto AvgProppantConcSurface { get; set; }
		public StartProppantConcBottomholeDto StartProppantConcBottomhole { get; set; }
		public EndProppantConcBottomholeDto EndProppantConcBottomhole { get; set; }
		public AvgProppantConcBottomholeDto AvgProppantConcBottomhole { get; set; }
		public StartFoamRateN2Dto StartFoamRateN2 { get; set; }
		public EndFoamRateN2Dto EndFoamRateN2 { get; set; }
		public StartFoamRateCO2Dto StartFoamRateCO2 { get; set; }
		public EndFoamRateCO2Dto EndFoamRateCO2 { get; set; }
		public FluidVolBaseDto FluidVolBase { get; set; }
		public FluidVolSlurryDto FluidVolSlurry { get; set; }
		public SlurryRateBeginDto SlurryRateBegin { get; set; }
		public SlurryRateEndDto SlurryRateEnd { get; set; }
		public ProppantMassWellHeadDto ProppantMassWellHead { get; set; }
		public ProppantMassDto ProppantMass { get; set; }
		public MaxPresDto MaxPres { get; set; }
		public MaxSlurryRateDto MaxSlurryRate { get; set; }
		public MaxWellheadRateDto MaxWellheadRate { get; set; }
		public MaxN2StdRateDto MaxN2StdRate { get; set; }
		public MaxCO2LiquidRateDto MaxCO2LiquidRate { get; set; }
		public MaxPropConcDto MaxPropConc { get; set; }
		public MaxSlurryPropConcDto MaxSlurryPropConc { get; set; }
		public AvgPropConcDto AvgPropConc { get; set; }
		public AvgSlurryPropConcDto AvgSlurryPropConc { get; set; }
		public AvgTemperatureDto AvgTemperature { get; set; }
		public AvgInternalPhaseFractionDto AvgInternalPhaseFraction { get; set; }
		public AvgBaseFluidQualityDto AvgBaseFluidQuality { get; set; }
		public AvgN2BaseFluidQualityDto AvgN2BaseFluidQuality { get; set; }
		public AvgCO2BaseFluidQualityDto AvgCO2BaseFluidQuality { get; set; }
		public AvgHydraulicPowerDto AvgHydraulicPower { get; set; }
		public AvgBaseFluidRateDto AvgBaseFluidRate { get; set; }
		public AvgSlurryRateDto AvgSlurryRate { get; set; }
		public AvgWellheadRateDto AvgWellheadRate { get; set; }
		public AvgN2StdRateDto AvgN2StdRate { get; set; }
		public AvgCO2RateDto AvgCO2Rate { get; set; }
		public BaseFluidVolDto BaseFluidVol { get; set; }
		public SlurryVolDto SlurryVol { get; set; }
		public WellheadVolDto WellheadVol { get; set; }
		public MaxPmaxPacPresDto MaxPmaxPacPres { get; set; }
		public MaxPmaxWeaklinkPresDto MaxPmaxWeaklinkPres { get; set; }
		public MaxGelRateDto MaxGelRate { get; set; }
		public MaxOilRateDto MaxOilRate { get; set; }
		public MaxAcidRateDto MaxAcidRate { get; set; }
		public AvgGelRateDto AvgGelRate { get; set; }
		public AvgOilRateDto AvgOilRate { get; set; }
		public AvgAcidRateDto AvgAcidRate { get; set; }
		public GelVolumeDto GelVolume { get; set; }
		public OilVolumeDto OilVolume { get; set; }
		public AcidVolumeDto AcidVolume { get; set; }
		public BaseFluidBypassVolDto BaseFluidBypassVol { get; set; }
		public string FrictionFactor { get; set; }
		public StageFluidDto StageFluid { get; set; }
		public string Uid { get; set; }
	}

	public class JobEventDto
	{
		[Key]
		public int JobEventId { get; set; }
		public string Number { get; set; }
		public string DTim { get; set; }
		public string Comment { get; set; }
		public string NumStage { get; set; }
		public string Uid { get; set; }
	}

	public class StimJobIdDto
	{
		[Key]
		public int IdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobOdDto
	{
		[Key]
		public int OdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobWeightDto
	{
		[Key]
		public int WeightId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobMdTopDto
	{
		[Key]
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobMdBottomDto
	{
		[Key]
		public int MdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StimJobVolumeFactorDto
	{
		[Key]
		public int VolumeFactorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class StimJobTubularDto
	{
		[Key]
		public int TubularId { get; set; }
		public string Type { get; set; }
		public StimJobIdDto Id { get; set; }
		public StimJobOdDto Od { get; set; }
		public StimJobWeightDto Weight { get; set; }
		public StimJobMdTopDto MdTop { get; set; }
		public StimJobMdBottomDto MdBottom { get; set; }
		public StimJobVolumeFactorDto VolumeFactor { get; set; }
		public string Uid { get; set; }
	}

	public class FlowPathDto
	{
		[Key]
		public int FlowPathId { get; set; }
		public string Kind { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public MaxTreatmentPresDto MaxTreatmentPres { get; set; }
		public MaxSlurryRateDto MaxSlurryRate { get; set; }
		public MaxWellheadRateDto MaxWellheadRate { get; set; }
		public MaxN2StdRateDto MaxN2StdRate { get; set; }
		public MaxCO2LiquidRateDto MaxCO2LiquidRate { get; set; }
		public MaxGelRateDto MaxGelRate { get; set; }
		public MaxOilRateDto MaxOilRate { get; set; }
		public MaxAcidRateDto MaxAcidRate { get; set; }
		public MaxPropConcDto MaxPropConc { get; set; }
		public MaxSlurryPropConcDto MaxSlurryPropConc { get; set; }
		public AvgTreatPresDto AvgTreatPres { get; set; }
		public AvgBaseFluidRateDto AvgBaseFluidRate { get; set; }
		public AvgSlurryRateDto AvgSlurryRate { get; set; }
		public AvgWellheadRateDto AvgWellheadRate { get; set; }
		public AvgN2StdRateDto AvgN2StdRate { get; set; }
		public AvgCO2LiquidRateDto AvgCO2LiquidRate { get; set; }
		public AvgGelRateDto AvgGelRate { get; set; }
		public AvgOilRateDto AvgOilRate { get; set; }
		public AvgAcidRateDto AvgAcidRate { get; set; }
		public AvgPropConcDto AvgPropConc { get; set; }
		public AvgSlurryPropConcDto AvgSlurryPropConc { get; set; }
		public AvgTemperatureDto AvgTemperature { get; set; }
		public string AvgIntervalPhaseFraction { get; set; }
		public AvgBaseFluidQualityDto AvgBaseFluidQuality { get; set; }
		public AvgN2BaseFluidQualityDto AvgN2BaseFluidQuality { get; set; }
		public AvgCO2BaseFluidQualityDto AvgCO2BaseFluidQuality { get; set; }
		public AvgHydraulicPowerDto AvgHydraulicPower { get; set; }
		public BaseFluidVolDto BaseFluidVol { get; set; }
		public SlurryVolDto SlurryVol { get; set; }
		public WellheadVolDto WellheadVol { get; set; }
		public StdVolN2Dto StdVolN2 { get; set; }
		public MassCO2Dto MassCO2 { get; set; }
		public GelVolDto GelVol { get; set; }
		public OilVolDto OilVol { get; set; }
		public AcidVolDto AcidVol { get; set; }
		public BaseFluidBypassVolDto BaseFluidBypassVol { get; set; }
		public PropMassDto PropMass { get; set; }
		public MaxPmaxPacPresDto MaxPmaxPacPres { get; set; }
		public MaxPmaxWeaklinkPresDto MaxPmaxWeaklinkPres { get; set; }
		public AvgPmaxPacPresDto AvgPmaxPacPres { get; set; }
		public AvgPmaxWeaklinkPresDto AvgPmaxWeaklinkPres { get; set; }
		public ShutinPres5MinDto ShutinPres5Min { get; set; }
		public ShutinPres10MinDto ShutinPres10Min { get; set; }
		public ShutinPres15MinDto ShutinPres15Min { get; set; }
		public BreakDownPresDto BreakDownPres { get; set; }
		public PercentPadDto PercentPad { get; set; }
		public FractureGradientDto FractureGradient { get; set; }
		public string PipeFrictionFactor { get; set; }
		public string StageCount { get; set; }
		public List<TubularDto> Tubular { get; set; }
		public List<JobStageDto> JobStage { get; set; }
		public List<JobEventDto> JobEvent { get; set; }
		public string Uid { get; set; }
	}

	public class PumpDurationDto
	{
		[Key]
		public int PumpDurationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgBottomholeTreatmentPresDto
	{
		[Key]
		public int AvgBottomholeTreatmentPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BottomholeHydrostaticPresDto
	{
		[Key]
		public int BottomholeHydrostaticPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BubblePointPresDto
	{
		[Key]
		public int BubblePointPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FractureClosePresDto
	{
		[Key]
		public int FractureClosePresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FrictionPresDto
	{
		[Key]
		public int FrictionPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PorePresDto
	{
		[Key]
		public int PorePresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AvgBottomholeTreatmentRateDto
	{
		[Key]
		public int AvgBottomholeTreatmentRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidDensityDto
	{
		[Key]
		public int FluidDensityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellboreVolumeDto
	{
		[Key]
		public int WellboreVolumeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdSurfaceDto
	{
		[Key]
		public int MdSurfaceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdBottomholeDto
	{
		[Key]
		public int MdBottomholeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdMidPerforationDto
	{
		[Key]
		public int MdMidPerforationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdMidPerforationDto
	{
		[Key]
		public int TvdMidPerforationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SurfaceTemperatureDto
	{
		[Key]
		public int SurfaceTemperatureId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BottomholeTemperatureDto
	{
		[Key]
		public int BottomholeTemperatureId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SurfaceFluidTemperatureDto
	{
		[Key]
		public int SurfaceFluidTemperatureId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidCompressibilityDto
	{
		[Key]
		public int FluidCompressibilityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ReservoirTotalCompressibilityDto
	{
		[Key]
		public int ReservoirTotalCompressibilityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidSpecificHeatDto
	{
		[Key]
		public int FluidSpecificHeatId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidThermalConductivityDto
	{
		[Key]
		public int FluidThermalConductivityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidThermalExpansionCoefficientDto
	{
		[Key]
		public int FluidThermalExpansionCoefficientId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FoamQualityDto
	{
		[Key]
		public int FoamQualityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BottomholeRateDto
	{
		[Key]
		public int BottomholeRateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PresMeasurementDto
	{
		[Key]
		public int PresMeasurementId { get; set; }
		public PresDto Pres { get; set; }
		public BottomholeRateDto BottomholeRate { get; set; }
	}

	public class FractureExtensionPresDto
	{
		[Key]
		public int FractureExtensionPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StepRateTestDto
	{
		[Key]
		public int StepRateTestId { get; set; }
		public List<PresMeasurementDto> PresMeasurement { get; set; }
		public FractureExtensionPresDto FractureExtensionPres { get; set; }
		public string Uid { get; set; }
	}

	public class EndPdlDurationDto
	{
		[Key]
		public int EndPdlDurationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FractureCloseDurationDto
	{
		[Key]
		public int FractureCloseDurationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PseudoRadialPresDto
	{
		[Key]
		public int PseudoRadialPresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FractureLengthDto
	{
		[Key]
		public int FractureLengthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FractureWidthDto
	{
		[Key]
		public int FractureWidthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ResidualPermeabilityDto
	{
		[Key]
		public int ResidualPermeabilityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidEfficiencyTestDto
	{
		[Key]
		public int FluidEfficiencyTestId { get; set; }
		public string DTimStart { get; set; }
		public string DTimEnd { get; set; }
		public EndPdlDurationDto EndPdlDuration { get; set; }
		public FractureCloseDurationDto FractureCloseDuration { get; set; }
		public FractureClosePresDto FractureClosePres { get; set; }
		public FractureExtensionPresDto FractureExtensionPres { get; set; }
		public NetPresDto NetPres { get; set; }
		public PorePresDto PorePres { get; set; }
		public PseudoRadialPresDto PseudoRadialPres { get; set; }
		public FractureLengthDto FractureLength { get; set; }
		public FractureWidthDto FractureWidth { get; set; }
		public FluidEfficiencyDto FluidEfficiency { get; set; }
		public string PdlCoef { get; set; }
		public ResidualPermeabilityDto ResidualPermeability { get; set; }
		public string Uid { get; set; }
	}

	public class PumpFlowBackTestDto
	{
		[Key]
		public int PumpFlowBackTestId { get; set; }
		public FractureCloseDurationDto FractureCloseDuration { get; set; }
		public FractureClosePresDto FractureClosePres { get; set; }
		public string Uid { get; set; }
	}

	public class BottomholeFluidDensityDto
	{
		[Key]
		public int BottomholeFluidDensityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DiameterEntryHoleDto
	{
		[Key]
		public int PipeFrictionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PipeFrictionDto
	{
		[Key]
		public int PipeFrictionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EntryFrictionDto
	{
		[Key]
		public int EntryFrictionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PerfFrictionDto
	{
		[Key]
		public int PerfFrictionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class NearWellboreFrictionDto
	{
		[Key]
		public int NearWellboreFrictionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class StepDto
	{
		[Key]
		public int StepId { get; set; }
		public string Number { get; set; }
		public BottomholeRateDto BottomholeRate { get; set; }
		public PresDto Pres { get; set; }
		public PipeFrictionDto PipeFriction { get; set; }
		public EntryFrictionDto EntryFriction { get; set; }
		public PerfFrictionDto PerfFriction { get; set; }
		public NearWellboreFrictionDto NearWellboreFriction { get; set; }
		public string Uid { get; set; }
	}

	public class StepDownTestDto
	{
		[Key]
		public int StepDownTestId { get; set; }
		public InitialShutinPresDto InitialShutinPres { get; set; }
		public BottomholeFluidDensityDto BottomholeFluidDensity { get; set; }
		public DiameterEntryHoleDto DiameterEntryHole { get; set; }
		public string PerforationCount { get; set; }
		public string DischargeCoefficient { get; set; }
		public string EffectivePerfs { get; set; }
		public List<StepDto> Step { get; set; }
		public string Uid { get; set; }
	}

	public class PdatSessionDto
	{
		[Key]
		public int PdatSessionId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Number { get; set; }
		public string DTimPumpOn { get; set; }
		public string DTimPumpOff { get; set; }
		public string DTimWellShutin { get; set; }
		public string DTimFractureClose { get; set; }
		public PumpDurationDto PumpDuration { get; set; }
		public AvgBottomholeTreatmentPresDto AvgBottomholeTreatmentPres { get; set; }
		public BottomholeHydrostaticPresDto BottomholeHydrostaticPres { get; set; }
		public BubblePointPresDto BubblePointPres { get; set; }
		public FractureClosePresDto FractureClosePres { get; set; }
		public FrictionPresDto FrictionPres { get; set; }
		public InitialShutinPresDto InitialShutinPres { get; set; }
		public PorePresDto PorePres { get; set; }
		public AvgBottomholeTreatmentRateDto AvgBottomholeTreatmentRate { get; set; }
		public FluidDensityDto FluidDensity { get; set; }
		public BaseFluidVolDto BaseFluidVol { get; set; }
		public WellboreVolumeDto WellboreVolume { get; set; }
		public MdSurfaceDto MdSurface { get; set; }
		public MdBottomholeDto MdBottomhole { get; set; }
		public MdMidPerforationDto MdMidPerforation { get; set; }
		public TvdMidPerforationDto TvdMidPerforation { get; set; }
		public SurfaceTemperatureDto SurfaceTemperature { get; set; }
		public BottomholeTemperatureDto BottomholeTemperature { get; set; }
		public SurfaceFluidTemperatureDto SurfaceFluidTemperature { get; set; }
		public FluidCompressibilityDto FluidCompressibility { get; set; }
		public ReservoirTotalCompressibilityDto ReservoirTotalCompressibility { get; set; }
		public string FluidNprimeFactor { get; set; }
		public string FluidKprimeFactor { get; set; }
		public FluidSpecificHeatDto FluidSpecificHeat { get; set; }
		public FluidThermalConductivityDto FluidThermalConductivity { get; set; }
		public FluidThermalExpansionCoefficientDto FluidThermalExpansionCoefficient { get; set; }
		public FluidEfficiencyDto FluidEfficiency { get; set; }
		public FoamQualityDto FoamQuality { get; set; }
		public PercentPadDto PercentPad { get; set; }
		public string TemperatureCorrectionApplied { get; set; }
		public StepRateTestDto StepRateTest { get; set; }
		public FluidEfficiencyTestDto FluidEfficiencyTest { get; set; }
		public PumpFlowBackTestDto PumpFlowBackTest { get; set; }
		public StepDownTestDto StepDownTest { get; set; }
		public string Uid { get; set; }
	}

	public class MdLithTopDto
	{
		[Key]
		public int MdLithTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdLithBottomDto
	{
		[Key]
		public int MdLithBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LithFormationPermeabilityDto
	{
		[Key]
		public int LithFormationPermeabilityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LithYoungsModulusDto
	{
		[Key]
		public int LithYoungsModulusId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LithPorePresDto
	{
		[Key]
		public int LithPorePresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LithNetPayThicknessDto
	{
		[Key]
		public int LithNetPayThicknessId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdGrossPayTopDto
	{
		[Key]
		public int MdGrossPayTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdGrossPayBottomDto
	{
		[Key]
		public int MdGrossPayBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class GrossPayThicknessDto
	{
		[Key]
		public int GrossPayThicknessId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class NetPayThicknessDto
	{
		[Key]
		public int NetPayThicknessId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class NetPayPorePresDto
	{
		[Key]
		public int NetPayPorePresId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class NetPayFluidCompressibilityDto
	{
		[Key]
		public int NetPayFluidCompressibilityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class NetPayFluidViscosityDto
	{
		[Key]
		public int NetPayFluidViscosityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class NetPayFormationPermeabilityDto
	{
		[Key]
		public int NetPayFormationPermeabilityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LithPoissonsRatioDto
	{
		[Key]
		public int LithPoissonsRatioId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class NetPayFormationPorosityDto
	{
		[Key]
		public int NetPayFormationPorosityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationPermeabilityDto
	{
		[Key]
		public int FormationPermeabilityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationPorosityDto
	{
		[Key]
		public int FormationPorosityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ReservoirIntervalDto
	{
		[Key]
		public int ReservoirIntervalId { get; set; }
		public MdLithTopDto MdLithTop { get; set; }
		public MdLithBottomDto MdLithBottom { get; set; }
		public LithFormationPermeabilityDto LithFormationPermeability { get; set; }
		public LithYoungsModulusDto LithYoungsModulus { get; set; }
		public LithPorePresDto LithPorePres { get; set; }
		public LithNetPayThicknessDto LithNetPayThickness { get; set; }
		public string LithName { get; set; }
		public MdGrossPayTopDto MdGrossPayTop { get; set; }
		public MdGrossPayBottomDto MdGrossPayBottom { get; set; }
		public GrossPayThicknessDto GrossPayThickness { get; set; }
		public NetPayThicknessDto NetPayThickness { get; set; }
		public NetPayPorePresDto NetPayPorePres { get; set; }
		public NetPayFluidCompressibilityDto NetPayFluidCompressibility { get; set; }
		public NetPayFluidViscosityDto NetPayFluidViscosity { get; set; }
		public string NetPayName { get; set; }
		public NetPayFormationPermeabilityDto NetPayFormationPermeability { get; set; }
		public LithPoissonsRatioDto LithPoissonsRatio { get; set; }
		public NetPayFormationPorosityDto NetPayFormationPorosity { get; set; }
		public FormationPermeabilityDto FormationPermeability { get; set; }
		public FormationPorosityDto FormationPorosity { get; set; }
		public string NameFormation { get; set; }
		public string Uid { get; set; }
	}

	public class MdPerforationsTopDto
	{
		[Key]
		public int MdPerforationsTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdPerforationsBottomDto
	{
		[Key]
		public int MdPerforationsBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdPerforationsTopDto
	{
		[Key]
		public int TvdPerforationsTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdPerforationsBottomDto
	{
		[Key]
		public int TvdPerforationsBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SizeDto
	{
		[Key]
		public int SizeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DensityPerforationDto
	{
		[Key]
		public int DensityPerforationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PhasingPerforationDto
	{
		[Key]
		public int PhasingPerforationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PerforationIntervalDto
	{
		[Key]
		public int PerforationIntervalId { get; set; }
		public string Type { get; set; }
		public MdPerforationsTopDto MdPerforationsTop { get; set; }
		public MdPerforationsBottomDto MdPerforationsBottom { get; set; }
		public TvdPerforationsTopDto TvdPerforationsTop { get; set; }
		public TvdPerforationsBottomDto TvdPerforationsBottom { get; set; }
		public string PerforationCount { get; set; }
		public SizeDto Size { get; set; }
		public DensityPerforationDto DensityPerforation { get; set; }
		public PhasingPerforationDto PhasingPerforation { get; set; }
		public string FrictionFactor { get; set; }
		public FrictionPresDto FrictionPres { get; set; }
		public string DischargeCoefficient { get; set; }
		public string Uid { get; set; }
	}

	public class JobIntervalDto
	{
		[Key]
		public int JobIntervalId { get; set; }
		public string UidTreatmentInterval { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string DTimStart { get; set; }
		public string FormationName { get; set; }
		public MdFormationTopDto MdFormationTop { get; set; }
		public MdFormationBottomDto MdFormationBottom { get; set; }
		public TvdFormationTopDto TvdFormationTop { get; set; }
		public TvdFormationBottomDto TvdFormationBottom { get; set; }
		public string OpenHoleName { get; set; }
		public OpenHoleDiameterDto OpenHoleDiameter { get; set; }
		public MdOpenHoleTopDto MdOpenHoleTop { get; set; }
		public MdOpenHoleBottomDto MdOpenHoleBottom { get; set; }
		public TvdOpenHoleTopDto TvdOpenHoleTop { get; set; }
		public TvdOpenHoleBottomDto TvdOpenHoleBottom { get; set; }
		public TotalFrictionPresLossDto TotalFrictionPresLoss { get; set; }
		public TotalPumpTimeDto TotalPumpTime { get; set; }
		public MaxPresTubingDto MaxPresTubing { get; set; }
		public MaxPresAnnulusDto MaxPresAnnulus { get; set; }
		public MaxFluidRateTubingDto MaxFluidRateTubing { get; set; }
		public MaxFluidRateAnnulusDto MaxFluidRateAnnulus { get; set; }
		public AvgPresTubingDto AvgPresTubing { get; set; }
		public AvgPresCasingDto AvgPresCasing { get; set; }
		public BreakDownPresDto BreakDownPres { get; set; }
		public AveragePresDto AveragePres { get; set; }
		public AvgBaseFluidReturnRateDto AvgBaseFluidReturnRate { get; set; }
		public AvgSlurryReturnRateDto AvgSlurryReturnRate { get; set; }
		public AvgBottomholeRateDto AvgBottomholeRate { get; set; }
		public TotalVolumeDto TotalVolume { get; set; }
		public MaxProppantConcSurfaceDto MaxProppantConcSurface { get; set; }
		public MaxProppantConcBottomholeDto MaxProppantConcBottomhole { get; set; }
		public AvgProppantConcSurfaceDto AvgProppantConcSurface { get; set; }
		public AvgProppantConcBottomholeDto AvgProppantConcBottomhole { get; set; }
		public PerfproppantConcDto PerfproppantConc { get; set; }
		public TotalProppantMassDto TotalProppantMass { get; set; }
		public List<TotalProppantUsageDto> TotalProppantUsage { get; set; }
		public PercentProppantPumpedDto PercentProppantPumped { get; set; }
		public WellboreProppantMassDto WellboreProppantMass { get; set; }
		public List<string> ProppantName { get; set; }
		public FormationProppantMassDto FormationProppantMass { get; set; }
		public string PerfBallCount { get; set; }
		public TotalN2StdVolumeDto TotalN2StdVolume { get; set; }
		public TotalCO2MassDto TotalCO2Mass { get; set; }
		public List<string> FluidName { get; set; }
		public FractureGradientDto FractureGradient { get; set; }
		public FinalFractureGradientDto FinalFractureGradient { get; set; }
		public InitialShutinPresDto InitialShutinPres { get; set; }
		public List<ShutinPresDto> ShutinPres { get; set; }
		public ScreenOutPresDto ScreenOutPres { get; set; }
		public HhpOrderedCO2Dto HhpOrderedCO2 { get; set; }
		public HhpOrderedFluidDto HhpOrderedFluid { get; set; }
		public HhpUsedCO2Dto HhpUsedCO2 { get; set; }
		public HhpUsedFluidDto HhpUsedFluid { get; set; }
		public PerfBallSizeDto PerfBallSize { get; set; }
		public string ScreenedOut { get; set; }
		public AvgFractureWidthDto AvgFractureWidth { get; set; }
		public AvgConductivityDto AvgConductivity { get; set; }
		public NetPresDto NetPres { get; set; }
		public ClosurePresDto ClosurePres { get; set; }
		public ClosureDurationDto ClosureDuration { get; set; }
		public List<FlowPathDto> FlowPath { get; set; }
		public PdatSessionDto PdatSession { get; set; }
		public ReservoirIntervalDto ReservoirInterval { get; set; }
		public PerforationIntervalDto PerforationInterval { get; set; }
		public string Uid { get; set; }
	}

	public class StimJobDto
	{
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
		public TotalPumpTimeDto TotalPumpTime { get; set; }
		public MaxJobPresDto MaxJobPres { get; set; }
		public MaxFluidRateDto MaxFluidRate { get; set; }
		public AvgJobPresDto AvgJobPres { get; set; }
		public TotalJobVolumeDto TotalJobVolume { get; set; }
		public TotalProppantWtDto TotalProppantWt { get; set; }
		public string ProppantName { get; set; }
		public string PerfBallCount { get; set; }
		public TotalN2StdVolumeDto TotalN2StdVolume { get; set; }
		public TotalCO2MassDto TotalCO2Mass { get; set; }
		public HhpOrderedDto HhpOrdered { get; set; }
		public HhpUsedDto HhpUsed { get; set; }
		public string TreatmentCount { get; set; }
		public FluidEfficiencyDto FluidEfficiency { get; set; }
		public FlowBackPresDto FlowBackPres { get; set; }
		public FlowBackRateDto FlowBackRate { get; set; }
		public FlowBackVolumeDto FlowBackVolume { get; set; }
		public string TreatmentIntervalCount { get; set; }
		public BottomholeStaticTemperatureDto BottomholeStaticTemperature { get; set; }
		public TreatingBottomholeTemperatureDto TreatingBottomholeTemperature { get; set; }
		public JobIntervalDto JobInterval { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class StimJobs
	{
		[Key]
		public int StimJobsId { get; set; }
		public StimJobDto StimJob { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xmlns { get; set; }
		public string Xsi { get; set; }
	}

}
