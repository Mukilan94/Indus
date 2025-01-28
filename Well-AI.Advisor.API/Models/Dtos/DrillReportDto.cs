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
	

	public class DrillReportWellAliasDto
	{
		[Key]
		
		public int WellAliasId { get; set; }
		public string Name { get; set; }
		public string NamingSystem { get; set; }
	}

	public class DrillReportWellboreAliasDto
	{
		[Key]
		
		public string Uid { get; set; }
		public string Name { get; set; }
		public string NamingSystem { get; set; }

	}

	public class DrillReportElevationDto
	{
		[Key]
		
		public int ElevationId { get; set; }
		public string Datum { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportWellDatumDto
	{
		[Key]
		
		public string Uid { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public DrillReportElevationDto Elevation { get; set; }
		
	}


	public class DrillReportWtMudDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class DrillReportGeodeticCRSDto
	{
		[Key]
		
		public int GeodeticCRSId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	

	public class DrillReportWellCRSDto
	{
		[Key]
		
		public string Uid { get; set; }
		public string Name { get; set; }
		public DrillReportGeodeticCRSDto GeodeticCRS { get; set; }
		public string Description { get; set; }
		
	}

	public class DrillReportRigAliasDto
	{
		[Key]
		
		public string Uid { get; set; }
		public int RigAliasId { get; set; }
		public string Name { get; set; }
		public string NamingSystem { get; set; }
		
	}

	public class DrillReportWellboreInfoDto
	{
		[Key]
		
		public int WellboreInfoId { get; set; }
		public string DTimSpud { get; set; }
		public string DTimPreSpud { get; set; }
		public string Operator { get; set; }
		public List<DrillReportRigAliasDto> RigAlias { get; set; }
	}

	public class DrillReportMdDto
	{
		[Key]
		
		public int MdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTvdDto
	{
		[Key]
		
		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdPlugTopDto
	{
		[Key]
		
		public int MdPlugTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDiaHoleDto
	{
		[Key]
		
		public int DiaHoleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdDiaHoleStartDto
	{
		[Key]
		
		public int MdDiaHoleStartId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDiaPilotDto
	{
		[Key]
		
		public int DiaPilotId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdDiaPilotPlanDto
	{
		[Key]
		
		public int MdDiaPilotPlanId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdKickoffDto
	{
		[Key]
		
		public int MdKickoffId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportStrengthFormDto
	{
		[Key]
		
		public int StrengthFormId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdStrengthFormDto
	{
		[Key]
		
		public int MdStrengthFormId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDiaCsgLastDto
	{
		[Key]
		
		public int DiaCsgLastId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdCsgLastDto
	{
		[Key]
		
		public int MdCsgLastId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDistDrillDto
	{
		[Key]
		
		public int DistDrillId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportRopCurrentDto
	{
		[Key]
		
		public int RopCurrentId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportStatusInfoDto
	{
		[Key]
		
		public int StatusInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdDto Md { get; set; }
		public DrillReportTvdDto Tvd { get; set; }
		public DrillReportMdPlugTopDto MdPlugTop { get; set; }
		public DrillReportDiaHoleDto DiaHole { get; set; }
		public DrillReportMdDiaHoleStartDto MdDiaHoleStart { get; set; }
		public DrillReportDiaPilotDto DiaPilot { get; set; }
		public DrillReportMdDiaPilotPlanDto MdDiaPilotPlan { get; set; }
		public DrillReportMdKickoffDto MdKickoff { get; set; }
		public DrillReportStrengthFormDto StrengthForm { get; set; }
		public DrillReportMdStrengthFormDto MdStrengthForm { get; set; }
		public DrillReportDiaCsgLastDto DiaCsgLast { get; set; }
		public DrillReportMdCsgLastDto MdCsgLast { get; set; }
		public string PresTestType { get; set; }
		public DrillReportDistDrillDto DistDrill { get; set; }
		public string Sum24Hr { get; set; }
		public string Forecast24Hr { get; set; }
		public DrillReportRopCurrentDto RopCurrent { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportDiaBitDto
	{
		[Key]
		
		public int DiaBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportBitRecordDto
	{
		[Key]
		
		public int Id { get; set; }
		public string NumBit { get; set; }
		public DrillReportDiaBitDto DiaBit { get; set; }
		public string Manufacturer { get; set; }
		public string CodeMfg { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportPresBopRatingDto
	{
		[Key]
		
		public int PresBopRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityDto
	{
		[Key]
		
		public int DensityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTempVisDto
	{
		[Key]
		
		public int TempVisId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPvDto
	{
		[Key]
		
		public int PvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFluidDto
	{
		[Key]
		
		public int FluidId { get; set; }
		public string Type { get; set; }
		public string DTim { get; set; }
		public DrillReportMdDto Md { get; set; }
		public DrillReportTvdDto Tvd { get; set; }
		public DrillReportPresBopRatingDto PresBopRating { get; set; }
		public string MudClass { get; set; }
		public DrillReportDensityDto Density { get; set; }
		public DrillReportTempVisDto TempVis { get; set; }
		public DrillReportPvDto Pv { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportEquivalentMudWeightDto
	{
		[Key]
		
		public int EquivalentMudWeightId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPorePressureDto
	{
		[Key]
		
		public int PorePressureId { get; set; }
		public string ReadingKind { get; set; }
		public DrillReportEquivalentMudWeightDto EquivalentMudWeight { get; set; }
		public string DTim { get; set; }
		public DrillReportMdDto Md { get; set; }
		public DrillReportTvdDto Tvd { get; set; }
		public string Uid { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportExtendedReportDto
	{
		[Key]
		
		public int ExtendedReportId { get; set; }
		public string DTim { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportInclDto
	{
		[Key]
		
		public int InclId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportAziDto
	{
		[Key]
		
		public int AziId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportSurveyStationDto
	{
		[Key]
		
		public int SurveyStationId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdDto Md { get; set; }
		public DrillReportTvdDto Tvd { get; set; }
		public DrillReportInclDto Incl { get; set; }
		public DrillReportAziDto Azi { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportActivityDto
	{
		[Key]
		
		public int ActivityId { get; set; }
		public string DTimStart { get; set; }
		public string DTimEnd { get; set; }
		public DrillReportMdDto Md { get; set; }
		public DrillReportTvdDto Tvd { get; set; }
		public string Phase { get; set; }
		public string ActivityCode { get; set; }
		public string DetailActivity { get; set; }
		public string State { get; set; }
		public string StateDetailActivity { get; set; }
		public string Comments { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportMdTopDto
	{
		[Key]
		
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class DrillReportTvdBottomDto
	{
		[Key]
		
		public int TvdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTempBHCTDto
	{
		[Key]
		
		public int TempBHCTId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdTempToolDto
	{
		[Key]
		
		public int MdTempToolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTvdTempToolDto
	{
		[Key]
		
		public int TvdTempToolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportLogInfoDto
	{
		[Key]
		
		public int LogInfoId { get; set; }
		public string DTim { get; set; }
		public string RunNumber { get; set; }
		public string ServiceCompany { get; set; }
		public DrillReportMdTopDto MdTop { get; set; }
		public DrillReportMdBottomDto MdBottom { get; set; }
		public DrillReportTvdTopDto TvdTop { get; set; }
		public DrillReportTvdBottomDto TvdBottom { get; set; }
		public string Tool { get; set; }
		public DrillReportTempBHCTDto TempBHCT { get; set; }
		public DrillReportMdTempToolDto MdTempTool { get; set; }
		public DrillReportTvdTempToolDto TvdTempTool { get; set; }
		public string Uid { get; set; }
		public DrillReportTempBHSTDto TempBHST { get; set; }
		public DrillReportETimStaticDto ETimStatic { get; set; }
	}

	public class DrillReportTempBHSTDto
	{
		[Key]
		
		public int TempBHSTId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportETimStaticDto
	{
		[Key]
		
		public int ETimStaticId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportLenRecoveredDto
	{
		[Key]
		
		public int LenRecoveredId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportRecoverPcDto
	{
		[Key]
		
		public int RecoverPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportLenBarrelDto
	{
		[Key]
		public int LenBarrelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportCoreInfoDto
	{
		[Key]
		
		public int CoreInfoId { get; set; }
		public string DTim { get; set; }
		public string CoreNumber { get; set; }
		public DrillReportMdTopDto MdTop { get; set; }
		public DrillReportMdBottomDto MdBottom { get; set; }
		public DrillReportTvdTopDto TvdTop { get; set; }
		public DrillReportTvdBottomDto TvdBottom { get; set; }
		public DrillReportLenRecoveredDto LenRecovered { get; set; }
		public DrillReportRecoverPcDto RecoverPc { get; set; }
		public DrillReportLenBarrelDto LenBarrel { get; set; }
		public string InnerBarrelType { get; set; }
		public string CoreDescription { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportChokeOrificeSizeDto
	{
		[Key]
		
		public int ChokeOrificeSizeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityOilDto
	{
		[Key]
		
		public int DensityOilId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityWaterDto
	{
		[Key]
		
		public int DensityWaterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityGasDto
	{
		[Key]
		
		public int DensityGasId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFlowRateOilDto
	{
		[Key]
		
		public int FlowRateOilId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFlowRateWaterDto
	{
		[Key]
		
		public int FlowRateWaterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFlowRateGasDto
	{
		[Key]
		
		public int FlowRateGasId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresShutInDto
	{
		[Key]
		
		public int PresShutInId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresFlowingDto
	{
		[Key]
		
		public int PresFlowingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresBottomDto
	{
		[Key]
		
		public int PresBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportGasOilRatioDto
	{
		[Key]
		
		public int GasOilRatioId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportWaterOilRatioDto
	{
		[Key]
		
		public int WaterOilRatioId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportChlorideDto
	{
		[Key]
		
		public int ChlorideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportCarbonDioxideDto
	{
		[Key]
		
		public int CarbonDioxideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportHydrogenSulfideDto
	{
		[Key]
		
		public int HydrogenSulfideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolOilTotalDto
	{
		[Key]
		
		public int VolOilTotalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolGasTotalDto
	{
		[Key]
		
		public int VolGasTotalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolWaterTotalDto
	{
		[Key]
		
		public int VolWaterTotalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolOilStoredDto
	{
		[Key]
		
		public int VolOilStoredId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportWellTestInfoDto
	{
		[Key]
		
		public int WellTestInfoId { get; set; }
		public string DTim { get; set; }
		public string TestType { get; set; }
		public string TestNumber { get; set; }
		public DrillReportMdTopDto MdTop { get; set; }
		public DrillReportMdBottomDto MdBottom { get; set; }
		public DrillReportTvdTopDto TvdTop { get; set; }
		public DrillReportTvdBottomDto TvdBottom { get; set; }
		public DrillReportChokeOrificeSizeDto ChokeOrificeSize { get; set; }
		public DrillReportDensityOilDto DensityOil { get; set; }
		public DrillReportDensityWaterDto DensityWater { get; set; }
		public DrillReportDensityGasDto DensityGas { get; set; }
		public DrillReportFlowRateOilDto FlowRateOil { get; set; }
		public DrillReportFlowRateWaterDto FlowRateWater { get; set; }
		public DrillReportFlowRateGasDto FlowRateGas { get; set; }
		public DrillReportPresShutInDto PresShutIn { get; set; }
		public DrillReportPresFlowingDto PresFlowing { get; set; }
		public DrillReportPresBottomDto PresBottom { get; set; }
		public DrillReportGasOilRatioDto GasOilRatio { get; set; }
		public DrillReportWaterOilRatioDto WaterOilRatio { get; set; }
		public DrillReportChlorideDto Chloride { get; set; }
		public DrillReportCarbonDioxideDto CarbonDioxide { get; set; }
		public DrillReportHydrogenSulfideDto HydrogenSulfide { get; set; }
		public DrillReportVolOilTotalDto VolOilTotal { get; set; }
		public DrillReportVolGasTotalDto VolGasTotal { get; set; }
		public DrillReportVolWaterTotalDto VolWaterTotal { get; set; }
		public DrillReportVolOilStoredDto VolOilStored { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportPresPoreDto
	{
		[Key]
		
		public int PresPoreId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdSampleDto
	{
		[Key]
		
		public int MdSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityHCDto
	{
		[Key]
		
		public int DensityHCId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolumeSampleDto
	{
		[Key]
		
		public int VolumeSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFormTestInfoDto
	{
		[Key]
		
		public int FormTestInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdDto Md { get; set; }
		public DrillReportTvdDto Tvd { get; set; }
		public DrillReportPresPoreDto PresPore { get; set; }
		public string GoodSeal { get; set; }
		public DrillReportMdSampleDto MdSample { get; set; }
		public string DominateComponent { get; set; }
		public DrillReportDensityHCDto DensityHC { get; set; }
		public DrillReportVolumeSampleDto VolumeSample { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportLithShowInfoDto
	{
		[Key]
		
		public int LithShowInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdTopDto MdTop { get; set; }
		public DrillReportMdBottomDto MdBottom { get; set; }
		public DrillReportTvdTopDto TvdTop { get; set; }
		public DrillReportTvdBottomDto TvdBottom { get; set; }
		public string Show { get; set; }
		public string Lithology { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportETimMissProductionDto
	{
		[Key]
		
		public int ETimMissProductionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportEquipFailureInfoDto
	{
		[Key]
		
		public int EquipFailureInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdDto Md { get; set; }
		public string EquipClass { get; set; }
		public DrillReportETimMissProductionDto ETimMissProduction { get; set; }
		public string DTimRepair { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportMdInflowDto
	{
		[Key]
		
		public int MdInflowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTvdInflowDto
	{
		[Key]
		
		public int TvdInflowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportETimLostDto
	{
		[Key]
		
		public int ETimLostId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdBitDto
	{
		[Key]
		
		public int MdBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class DrillReportVolMudGainedDto
	{
		[Key]
		
		public int VolMudGainedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresShutInCasingDto
	{
		[Key]
		
		public int PresShutInCasingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresShutInDrillDto
	{
		[Key]
		
		public int PresShutInDrillId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTempBottomDto
	{
		[Key]
		
		public int TempBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresMaxChokeDto
	{
		[Key]
		
		public int PresMaxChokeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportControlIncidentInfoDto
	{
		[Key]
		
		public int ControlIncidentInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdInflowDto MdInflow { get; set; }
		public DrillReportTvdInflowDto TvdInflow { get; set; }
		public string Phase { get; set; }
		public string ActivityCode { get; set; }
		public string DetailActivity { get; set; }
		public DrillReportETimLostDto ETimLost { get; set; }
		public string DTimRegained { get; set; }
		public DrillReportDiaBitDto DiaBit { get; set; }
		public DrillReportMdBitDto MdBit { get; set; }
		public DrillReportWtMudDto WtMud { get; set; }
		public DrillReportPorePressureDto PorePressure { get; set; }
		public DrillReportDiaCsgLastDto DiaCsgLast { get; set; }
		public DrillReportMdCsgLastDto MdCsgLast { get; set; }
		public DrillReportVolMudGainedDto VolMudGained { get; set; }
		public DrillReportPresShutInCasingDto PresShutInCasing { get; set; }
		public DrillReportPresShutInDrillDto PresShutInDrill { get; set; }
		public string IncidentType { get; set; }
		public string KillingType { get; set; }
		public string Formation { get; set; }
		public DrillReportTempBottomDto TempBottom { get; set; }
		public DrillReportPresMaxChokeDto PresMaxChoke { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportStratInfoDto
	{
		[Key]
		
		public int StratInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdTopDto MdTop { get; set; }
		public DrillReportTvdTopDto TvdTop { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportPerfInfoDto
	{
		[Key]
		
		public int PerfInfoId { get; set; }
		public string DTimOpen { get; set; }
		public string DTimClose { get; set; }
		public DrillReportMdTopDto MdTop { get; set; }
		public DrillReportMdBottomDto MdBottom { get; set; }
		public DrillReportTvdTopDto TvdTop { get; set; }
		public DrillReportTvdBottomDto TvdBottom { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportGasHighDto
	{
		[Key]
		
		public int GasHighId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportGasLowDto
	{
		[Key]
		
		public int GasLowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMethDto
	{
		[Key]
		
		public int MethId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportEthDto
	{
		[Key]
		
		public int EthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPropDto
	{
		[Key]
		
		public int PropId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportIbutDto
	{
		[Key]
		
		public int IbutId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportNbutDto
	{
		[Key]
		
		public int NbutId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportIpentDto
	{
		[Key]
		
		public int IpentId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdBottomDto
	{
		[Key]
		
		public int DrillReportMdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTvdTopDto
	{
		[Key]
		
		public int DrillReportTvdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportGasReadingInfoDto
	{
		[Key]
		
		public int GasReadingInfoId { get; set; }
		public string DTim { get; set; }
		public string ReadingType { get; set; }
		public DrillReportMdTopDto MdTop { get; set; }
		public DrillReportMdBottomDto MdBottom { get; set; }
		public DrillReportTvdTopDto TvdTop { get; set; }
		public DrillReportTvdBottomDto TvdBottom { get; set; }
		public DrillReportGasHighDto GasHigh { get; set; }
		public DrillReportGasLowDto GasLow { get; set; }
		public DrillReportMethDto Meth { get; set; }
		public DrillReportEthDto Eth { get; set; }
		public DrillReportPropDto Prop { get; set; }
		public DrillReportIbutDto Ibut { get; set; }
		public DrillReportNbutDto Nbut { get; set; }
		public DrillReportIpentDto Ipent { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportDefaultDatumDto
	{
		[Key]
		
		public int DefaultDatumId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}


	public class DrillReportCommonDataDto
	{
		[Key]
		public int CommonDataId { get; set; }
		public DrillReportDefaultDatumDto DefaultDatum { get; set; }
	}

	public class DrillReportDrillReportFluidDto
	{
		[Key]
		public string Uid { get; set; }
		public string Type { get; set; }
		public string DTim { get; set; }
		public DrillReportMdDto Md { get; set; }
		public DrillReportTvdDto Tvd { get; set; }
		public DrillReportPresBopRatingDto PresBopRating { get; set; }
		public string MudClass { get; set; }
		public DrillReportDensityDto Density { get; set; }
		public DrillReportTempVisDto TempVis { get; set; }
		public DrillReportPvDto Pv { get; set; }
		
	}
	public class DrillReportDto
	{
		[Key]
		
		public int DrillReportId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string DTimStart { get; set; }
		public string DTimEnd { get; set; }
		public string VersionKind { get; set; }
		public string CreateDate { get; set; }
		public DrillReportWellAliasDto WellAlias { get; set; }
		public List<DrillReportWellboreAliasDto> WellboreAlias { get; set; }
		public List<DrillReportWellDatumDto> WellDatum { get; set; }
		public DrillReportWellCRSDto WellCRS { get; set; }
		public DrillReportWellboreInfoDto WellboreInfo { get; set; }
		public DrillReportStatusInfoDto StatusInfo { get; set; }
		public DrillReportBitRecordDto BitRecord { get; set; }
		public List<DrillReportFluidDto> Fluid { get; set; }
		public List<DrillReportPorePressureDto> PorePressure { get; set; }
		public DrillReportExtendedReportDto ExtendedReport { get; set; }
		public DrillReportSurveyStationDto SurveyStation { get; set; }
		public List<DrillReportActivityDto> Activity { get; set; }
		public List<DrillReportLogInfoDto> LogInfo { get; set; }
		public DrillReportCoreInfoDto CoreInfo { get; set; }
		public DrillReportWellTestInfoDto WellTestInfo { get; set; }
		public DrillReportFormTestInfoDto FormTestInfo { get; set; }
		public DrillReportLithShowInfoDto LithShowInfo { get; set; }
		public DrillReportEquipFailureInfoDto EquipFailureInfo { get; set; }
		public DrillReportControlIncidentInfoDto ControlIncidentInfo { get; set; }
		public DrillReportStratInfoDto StratInfo { get; set; }
		public DrillReportPerfInfoDto PerfInfo { get; set; }
		public DrillReportGasReadingInfoDto GasReadingInfo { get; set; }
		public DrillReportCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class DrillReportsDto
	{
		
		public DrillReportDto DrillReport { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}

}
