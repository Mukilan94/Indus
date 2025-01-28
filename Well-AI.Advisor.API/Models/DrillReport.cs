   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Models
{
		public class DrillReportWellAlias 
	{
		[Key]
		
		public int WellAliasId { get; set; }
		public string Name { get; set; }
		public string NamingSystem { get; set; }
	}

	public class DrillReportWellboreAlias 
	{
		[Key]
		
		public string Uid { get; set; }
		public string Name { get; set; }
		public string NamingSystem { get; set; }

	}

	public class DrillReportElevation 
	{
		[Key]
		
		public int ElevationId { get; set; }
		public string Datum { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportWellDatum 
	{
		[Key]
		
		public string Uid { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public DrillReportElevation Elevation { get; set; }
		
	}


	public class DrillReportWtMud 
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class DrillReportGeodeticCRS 
	{
		[Key]
		
		public int GeodeticCRSId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	

	public class DrillReportWellCRS 
	{
		[Key]
		
		public string Uid { get; set; }
		public string Name { get; set; }
		public DrillReportGeodeticCRS GeodeticCRS { get; set; }
		public string Description { get; set; }
		
	}

	public class DrillReportRigAlias 
	{
		[Key]
		
		public string Uid { get; set; }
		public int RigAliasId { get; set; }
		public string Name { get; set; }
		public string NamingSystem { get; set; }
		
	}

	public class DrillReportWellboreInfo 
	{
		[Key]
		
		public int WellboreInfoId { get; set; }
		public string DTimSpud { get; set; }
		public string DTimPreSpud { get; set; }
		public string Operator { get; set; }
		public List<DrillReportRigAlias > RigAlias { get; set; }
	}

	public class DrillReportMd 
	{
		[Key]
		
		public int MdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTvd 
	{
		[Key]
		
		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdPlugTop 
	{
		[Key]
		
		public int MdPlugTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDiaHole 
	{
		[Key]
		
		public int DiaHoleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdDiaHoleStart 
	{
		[Key]
		
		public int MdDiaHoleStartId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDiaPilot 
	{
		[Key]
		
		public int DiaPilotId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdDiaPilotPlan 
	{
		[Key]
		
		public int MdDiaPilotPlanId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdKickoff 
	{
		[Key]
		
		public int MdKickoffId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportStrengthForm 
	{
		[Key]
		
		public int StrengthFormId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdStrengthForm 
	{
		[Key]
		
		public int MdStrengthFormId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDiaCsgLast 
	{
		[Key]
		
		public int DiaCsgLastId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdCsgLast 
	{
		[Key]
		
		public int MdCsgLastId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDistDrill 
	{
		[Key]
		
		public int DistDrillId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportRopCurrent 
	{
		[Key]
		
		public int RopCurrentId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportStatusInfo 
	{
		[Key]
		
		public int StatusInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMd Md { get; set; }
		public DrillReportTvd Tvd { get; set; }
		public DrillReportMdPlugTop MdPlugTop { get; set; }
		public DrillReportDiaHole DiaHole { get; set; }
		public DrillReportMdDiaHoleStart MdDiaHoleStart { get; set; }
		public DrillReportDiaPilot DiaPilot { get; set; }
		public DrillReportMdDiaPilotPlan MdDiaPilotPlan { get; set; }
		public DrillReportMdKickoff MdKickoff { get; set; }
		public DrillReportStrengthForm StrengthForm { get; set; }
		public DrillReportMdStrengthForm MdStrengthForm { get; set; }
		public DrillReportDiaCsgLast DiaCsgLast { get; set; }
		public DrillReportMdCsgLast MdCsgLast { get; set; }
		public string PresTestType { get; set; }
		public DrillReportDistDrill DistDrill { get; set; }
		public string Sum24Hr { get; set; }
		public string Forecast24Hr { get; set; }
		public DrillReportRopCurrent RopCurrent { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportDiaBit 
	{
		[Key]
		
		public int DiaBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportBitRecord 
	{
		[Key]
		
		public int Id { get; set; }
		public string NumBit { get; set; }
		public DrillReportDiaBit DiaBit { get; set; }
		public string Manufacturer { get; set; }
		public string CodeMfg { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportPresBopRating 
	{
		[Key]
		
		public int PresBopRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensity 
	{
		[Key]
		
		public int DensityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTempVis 
	{
		[Key]
		
		public int TempVisId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPv 
	{
		[Key]
		
		public int PvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFluid 
	{
		[Key]
		
		public int FluidId { get; set; }
		public string Type { get; set; }
		public string DTim { get; set; }
		public DrillReportMd Md { get; set; }
		public DrillReportTvd Tvd { get; set; }
		public DrillReportPresBopRating PresBopRating { get; set; }
		public string MudClass { get; set; }
		public DrillReportDensity Density { get; set; }
		public DrillReportTempVis TempVis { get; set; }
		public DrillReportPv Pv { get; set; }
		public string Uid { get; set; }
	}



	public class DrillReportEquivalentMudWeight 
	{
		[Key]
		
		public int EquivalentMudWeightId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPorePressure 
	{
		[Key]
		
		public int PorePressureId { get; set; }
		public string ReadingKind { get; set; }
		public DrillReportEquivalentMudWeight EquivalentMudWeight { get; set; }
		public string DTim { get; set; }
		public DrillReportMd Md { get; set; }
		public DrillReportTvd Tvd { get; set; }
		public string Uid { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportExtendedReport 
	{
		[Key]
		
		public int ExtendedReportId { get; set; }
		public string DTim { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportIncl 
	{
		[Key]
		
		public int InclId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportAzi 
	{
		[Key]
		
		public int AziId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportSurveyStation 
	{
		[Key]
		
		public int SurveyStationId { get; set; }
		public string DTim { get; set; }
		public DrillReportMd Md { get; set; }
		public DrillReportTvd Tvd { get; set; }
		public DrillReportIncl Incl { get; set; }
		public DrillReportAzi Azi { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportActivity 
	{
		[Key]
		
		public int ActivityId { get; set; }
		public string DTimStart { get; set; }
		public string DTimEnd { get; set; }
		public DrillReportMd Md { get; set; }
		public DrillReportTvd Tvd { get; set; }
		public string Phase { get; set; }
		public string ActivityCode { get; set; }
		public string DetailActivity { get; set; }
		public string State { get; set; }
		public string StateDetailActivity { get; set; }
		public string Comments { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportMdTop 
	{
		[Key]
		
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class DrillReportTvdBottom 
	{
		[Key]
		
		public int TvdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTempBHCT 
	{
		[Key]
		
		public int TempBHCTId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdTempTool 
	{
		[Key]
		
		public int MdTempToolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTvdTempTool 
	{
		[Key]
		
		public int TvdTempToolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportLogInfo 
	{
		[Key]
		
		public int LogInfoId { get; set; }
		public string DTim { get; set; }
		public string RunNumber { get; set; }
		public string ServiceCompany { get; set; }
		public DrillReportMdTop MdTop { get; set; }
		public DrillReportMdBottom MdBottom { get; set; }
		public DrillReportTvdTop TvdTop { get; set; }
		public DrillReportTvdBottom TvdBottom { get; set; }
		public string Tool { get; set; }
		public DrillReportTempBHCT TempBHCT { get; set; }
		public DrillReportMdTempTool MdTempTool { get; set; }
		public DrillReportTvdTempTool TvdTempTool { get; set; }
		public string Uid { get; set; }
		public DrillReportTempBHST TempBHST { get; set; }
		public DrillReportETimStatic ETimStatic { get; set; }
	}

	public class DrillReportTempBHST 
	{
		[Key]
		
		public int TempBHSTId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportETimStatic 
	{
		[Key]
		
		public int ETimStaticId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportLenRecovered 
	{
		[Key]
		
		public int LenRecoveredId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportRecoverPc 
	{
		[Key]
		
		public int RecoverPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportLenBarrel 
	{
		[Key]
		public int LenBarrelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportCoreInfo 
	{
		[Key]
		
		public int CoreInfoId { get; set; }
		public string DTim { get; set; }
		public string CoreNumber { get; set; }
		public DrillReportMdTop MdTop { get; set; }
		public DrillReportMdBottom MdBottom { get; set; }
		public DrillReportTvdTop TvdTop { get; set; }
		public DrillReportTvdBottom TvdBottom { get; set; }
		public DrillReportLenRecovered LenRecovered { get; set; }
		public DrillReportRecoverPc RecoverPc { get; set; }
		public DrillReportLenBarrel LenBarrel { get; set; }
		public string InnerBarrelType { get; set; }
		public string CoreDescription { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportChokeOrificeSize 
	{
		[Key]
		
		public int ChokeOrificeSizeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityOil 
	{
		[Key]
		
		public int DensityOilId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityWater 
	{
		[Key]
		
		public int DensityWaterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityGas 
	{
		[Key]
		
		public int DensityGasId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFlowRateOil 
	{
		[Key]
		
		public int FlowRateOilId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFlowRateWater 
	{
		[Key]
		
		public int FlowRateWaterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFlowRateGas 
	{
		[Key]
		
		public int FlowRateGasId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresShutIn 
	{
		[Key]
		
		public int PresShutInId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresFlowing 
	{
		[Key]
		
		public int PresFlowingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresBottom 
	{
		[Key]
		
		public int PresBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportGasOilRatio 
	{
		[Key]
		
		public int GasOilRatioId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportWaterOilRatio 
	{
		[Key]
		
		public int WaterOilRatioId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportChloride 
	{
		[Key]
		
		public int ChlorideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportCarbonDioxide 
	{
		[Key]
		
		public int CarbonDioxideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportHydrogenSulfide 
	{
		[Key]
		
		public int HydrogenSulfideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolOilTotal 
	{
		[Key]
		
		public int VolOilTotalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolGasTotal 
	{
		[Key]
		
		public int VolGasTotalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolWaterTotal 
	{
		[Key]
		
		public int VolWaterTotalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolOilStored 
	{
		[Key]
		
		public int VolOilStoredId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportWellTestInfo 
	{
		[Key]
		
		public int WellTestInfoId { get; set; }
		public string DTim { get; set; }
		public string TestType { get; set; }
		public string TestNumber { get; set; }
		public DrillReportMdTop MdTop { get; set; }
		public DrillReportMdBottom MdBottom { get; set; }
		public DrillReportTvdTop TvdTop { get; set; }
		public DrillReportTvdBottom TvdBottom { get; set; }
		public DrillReportChokeOrificeSize ChokeOrificeSize { get; set; }
		public DrillReportDensityOil DensityOil { get; set; }
		public DrillReportDensityWater DensityWater { get; set; }
		public DrillReportDensityGas DensityGas { get; set; }
		public DrillReportFlowRateOil FlowRateOil { get; set; }
		public DrillReportFlowRateWater FlowRateWater { get; set; }
		public DrillReportFlowRateGas FlowRateGas { get; set; }
		public DrillReportPresShutIn PresShutIn { get; set; }
		public DrillReportPresFlowing PresFlowing { get; set; }
		public DrillReportPresBottom PresBottom { get; set; }
		public DrillReportGasOilRatio GasOilRatio { get; set; }
		public DrillReportWaterOilRatio WaterOilRatio { get; set; }
		public DrillReportChloride Chloride { get; set; }
		public DrillReportCarbonDioxide CarbonDioxide { get; set; }
		public DrillReportHydrogenSulfide HydrogenSulfide { get; set; }
		public DrillReportVolOilTotal VolOilTotal { get; set; }
		public DrillReportVolGasTotal VolGasTotal { get; set; }
		public DrillReportVolWaterTotal VolWaterTotal { get; set; }
		public DrillReportVolOilStored VolOilStored { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportPresPore 
	{
		[Key]
		
		public int PresPoreId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdSample 
	{
		[Key]
		
		public int MdSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportDensityHC 
	{
		[Key]
		
		public int DensityHCId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportVolumeSample 
	{
		[Key]
		
		public int VolumeSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportFormTestInfo 
	{
		[Key]
		
		public int FormTestInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMd Md { get; set; }
		public DrillReportTvd Tvd { get; set; }
		public DrillReportPresPore PresPore { get; set; }
		public string GoodSeal { get; set; }
		public DrillReportMdSample MdSample { get; set; }
		public string DominateComponent { get; set; }
		public DrillReportDensityHC DensityHC { get; set; }
		public DrillReportVolumeSample VolumeSample { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportLithShowInfo 
	{
		[Key]
		
		public int LithShowInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdTop MdTop { get; set; }
		public DrillReportMdBottom MdBottom { get; set; }
		public DrillReportTvdTop TvdTop { get; set; }
		public DrillReportTvdBottom TvdBottom { get; set; }
		public string Show { get; set; }
		public string Lithology { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportETimMissProduction 
	{
		[Key]
		
		public int ETimMissProductionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportEquipFailureInfo 
	{
		[Key]
		
		public int EquipFailureInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMd Md { get; set; }
		public string EquipClass { get; set; }
		public DrillReportETimMissProduction ETimMissProduction { get; set; }
		public string DTimRepair { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportMdInflow 
	{
		[Key]
		
		public int MdInflowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTvdInflow 
	{
		[Key]
		
		public int TvdInflowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportETimLost 
	{
		[Key]
		
		public int ETimLostId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdBit 
	{
		[Key]
		
		public int MdBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class DrillReportVolMudGained 
	{
		[Key]
		
		public int VolMudGainedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresShutInCasing 
	{
		[Key]
		
		public int PresShutInCasingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresShutInDrill 
	{
		[Key]
		
		public int PresShutInDrillId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTempBottom 
	{
		[Key]
		
		public int TempBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportPresMaxChoke 
	{
		[Key]
		
		public int PresMaxChokeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportControlIncidentInfo 
	{
		[Key]
		
		public int ControlIncidentInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdInflow MdInflow { get; set; }
		public DrillReportTvdInflow TvdInflow { get; set; }
		public string Phase { get; set; }
		public string ActivityCode { get; set; }
		public string DetailActivity { get; set; }
		public DrillReportETimLost ETimLost { get; set; }
		public string DTimRegained { get; set; }
		public DrillReportDiaBit DiaBit { get; set; }
		public DrillReportMdBit MdBit { get; set; }
		public DrillReportWtMud WtMud { get; set; }
		public DrillReportPorePressure PorePressure { get; set; }
		public DrillReportDiaCsgLast DiaCsgLast { get; set; }
		public DrillReportMdCsgLast MdCsgLast { get; set; }
		public DrillReportVolMudGained VolMudGained { get; set; }
		public DrillReportPresShutInCasing PresShutInCasing { get; set; }
		public DrillReportPresShutInDrill PresShutInDrill { get; set; }
		public string IncidentType { get; set; }
		public string KillingType { get; set; }
		public string Formation { get; set; }
		public DrillReportTempBottom TempBottom { get; set; }
		public DrillReportPresMaxChoke PresMaxChoke { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportStratInfo 
	{
		[Key]
		
		public int StratInfoId { get; set; }
		public string DTim { get; set; }
		public DrillReportMdTop MdTop { get; set; }
		public DrillReportTvdTop TvdTop { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportPerfInfo 
	{
		[Key]
		
		public int PerfInfoId { get; set; }
		public string DTimOpen { get; set; }
		public string DTimClose { get; set; }
		public DrillReportMdTop MdTop { get; set; }
		public DrillReportMdBottom MdBottom { get; set; }
		public DrillReportTvdTop TvdTop { get; set; }
		public DrillReportTvdBottom TvdBottom { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportGasHigh 
	{
		[Key]
		
		public int GasHighId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportGasLow 
	{
		[Key]
		
		public int GasLowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMeth 
	{
		[Key]
		
		public int MethId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportEth 
	{
		[Key]
		
		public int EthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportProp 
	{
		[Key]
		
		public int PropId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportIbut 
	{
		[Key]
		
		public int IbutId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportNbut 
	{
		[Key]
		
		public int NbutId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportIpent 
	{
		[Key]
		
		public int IpentId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportMdBottom 
	{
		[Key]
		
		public int DrillReportMdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportTvdTop
	{
		[Key]
		
		public int DrillReportTvdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DrillReportGasReadingInfo 
	{
		[Key]
		
		public int GasReadingInfoId { get; set; }
		public string DTim { get; set; }
		public string ReadingType { get; set; }
		public DrillReportMdTop MdTop { get; set; }
		public DrillReportMdBottom MdBottom { get; set; }
		public DrillReportTvdTop TvdTop { get; set; }
		public DrillReportTvdBottom TvdBottom { get; set; }
		public DrillReportGasHigh GasHigh { get; set; }
		public DrillReportGasLow GasLow { get; set; }
		public DrillReportMeth Meth { get; set; }
		public DrillReportEth Eth { get; set; }
		public DrillReportProp Prop { get; set; }
		public DrillReportIbut Ibut { get; set; }
		public DrillReportNbut Nbut { get; set; }
		public DrillReportIpent Ipent { get; set; }
		public string Uid { get; set; }
	}

	public class DrillReportDefaultDatum 
	{
		[Key]
		
		public int DefaultDatumId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}


	public class DrillReportCommonData 
	{
		[Key]
		public int CommonDataId { get; set; }
		public DrillReportDefaultDatum DefaultDatum { get; set; }
	}

	
	public class DrillReport 
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
		public DrillReportWellAlias WellAlias { get; set; }
		public List<DrillReportWellboreAlias > WellboreAlias { get; set; }
		public List<DrillReportWellDatum > WellDatum { get; set; }
		public DrillReportWellCRS WellCRS { get; set; }
		public DrillReportWellboreInfo WellboreInfo { get; set; }
		public DrillReportStatusInfo StatusInfo { get; set; }
		public DrillReportBitRecord BitRecord { get; set; }
		public List<DrillReportFluid > Fluid { get; set; }
		public List<DrillReportPorePressure > PorePressure { get; set; }
		public DrillReportExtendedReport ExtendedReport { get; set; }
		public DrillReportSurveyStation SurveyStation { get; set; }
		public List<DrillReportActivity > Activity { get; set; }
		public List<DrillReportLogInfo > LogInfo { get; set; }
		public DrillReportCoreInfo CoreInfo { get; set; }
		public DrillReportWellTestInfo WellTestInfo { get; set; }
		public DrillReportFormTestInfo FormTestInfo { get; set; }
		public DrillReportLithShowInfo LithShowInfo { get; set; }
		public DrillReportEquipFailureInfo EquipFailureInfo { get; set; }
		public DrillReportControlIncidentInfo ControlIncidentInfo { get; set; }
		public DrillReportStratInfo StratInfo { get; set; }
		public DrillReportPerfInfo PerfInfo { get; set; }
		public DrillReportGasReadingInfo GasReadingInfo { get; set; }
		public DrillReportCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class DrillReports
	{
		
		public DrillReport DrillReport { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}

}
