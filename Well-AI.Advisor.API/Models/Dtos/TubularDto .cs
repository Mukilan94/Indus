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
	public class TubularDiaHoleAssyDto
	{
		[Key]
		public int DiaHoleAssyId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class TubularLenJointAvDto
	{
		[Key]
		public int LenJointAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class TubularOdDriftDto
	{
		[Key]
		public int OdDriftId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularTensYieldDto
	{
		[Key]
		public int TensYieldId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularTqYieldDto
	{
		[Key]
		public int TqYieldId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularStressFatigDto
	{
		[Key]
		public int StressFatigId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularLenFishneckDto
	{
		[Key]
		public int LenFishneckId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularIdFishneckDto
	{
		[Key]
		public int IdFishneckId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularOdFishneckDto
	{
		[Key]
		public int OdFishneckId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularDispDto
	{
		[Key]
		public int DispId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularPresBurstDto
	{
		[Key]
		public int PresBurstId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularPresCollapseDto
	{
		[Key]
		public int PresCollapseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularWearWallDto
	{
		[Key]
		public int WearWallId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularThickWallDto
	{
		[Key]
		public int ThickWallId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularBendStiffnessDto
	{
		[Key]
		public int Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularAxialStiffnessDto
	{
		[Key]
		public int Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularTorsionalStiffnessDto
	{
		[Key]
		public int TorsionalStiffnessId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularDoglegMxDto
	{
		[Key]
		public int DoglegMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularNameTagDto
	{
		[Key]
		public int NameTagId { get; set; }
		public string Name { get; set; }
		public string NumberingScheme { get; set; }
		public string Uid { get; set; }
	}


	public class TubularDiaPassThruDto
	{
		[Key]
		public int DiaPassThruId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularDiaPilotDto
	{
		[Key]
		public int DiaPilotId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularCostDto
	{
		[Key]
		public int CostId { get; set; }
		public string Currency { get; set; }

		public string Text { get; set; }
	}


	public class TubularAreaNozzleFlowDto
	{
		[Key]
		public int AreaNozzleFlowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularDiaNozzleDto
	{
		[Key]
		public int DiaNozzleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularIdDto
	{
		[Key]
		public int TubularIdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularOdDto
	{
		[Key]
		public int OdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularLenDto
	{
		[Key]
		public int LenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularNozzleDto
	{
		[Key]
		public int NozzleId { get; set; }
		public string Index { get; set; }
		public TubularDiaNozzleDto DiaNozzle { get; set; }
		public string TypeNozzle { get; set; }
		public TubularLenDto Len { get; set; }
		public string Uid { get; set; }
	}


	public class TubularDiaBitDto
	{
		[Key]

		public int DiaBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularWtPerLenDto
	{
		[Key]

		public int WtPerLenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularBitRecordDto
	{
		[Key]

		public int BitId { get; set; }
		public string NumBit { get; set; }
		public TubularDiaBitDto DiaBit { get; set; }
		public TubularDiaPassThruDto DiaPassThru { get; set; }
		public TubularDiaPilotDto DiaPilot { get; set; }
		public string Manufacturer { get; set; }
		public string TypeBit { get; set; }
		public TubularCostDto Cost { get; set; }
		public string CodeIADC { get; set; }
		public string CondInitInner { get; set; }
		public string CondInitOuter { get; set; }
		public string CondInitDull { get; set; }
		public string CondInitLocation { get; set; }
		public string CondInitBearing { get; set; }
		public string CondInitGauge { get; set; }
		public string CondInitOther { get; set; }
		public string CondInitReason { get; set; }
		public string CondFinalInner { get; set; }
		public string CondFinalOuter { get; set; }
		public string CondFinalDull { get; set; }
		public string CondFinalLocation { get; set; }
		public string CondFinalBearing { get; set; }
		public string CondFinalGauge { get; set; }
		public string CondFinalOther { get; set; }
		public string CondFinalReason { get; set; }
		public string Drive { get; set; }
		public string BitClass { get; set; }
		public string Uid { get; set; }
	}

	public class TubularComponentDto
	{
		[Key]
		public int TubularComponentId { get; set; }
		public string TypeTubularComp { get; set; }
		public string Sequence { get; set; }
		public string Description { get; set; }
		public TubularIdDto Id { get; set; }
		public TubularOdDto Od { get; set; }
		public TubularLenDto Len { get; set; }
		public TubularLenJointAvDto LenJointAv { get; set; }
		public string NumJointStand { get; set; }
		public TubularWtPerLenDto WtPerLen { get; set; }
		public string Grade { get; set; }
		public TubularOdDriftDto OdDrift { get; set; }
		public TubularTensYieldDto TensYield { get; set; }
		public TubularTqYieldDto TqYield { get; set; }
		public TubularStressFatigDto StressFatig { get; set; }
		public TubularLenFishneckDto LenFishneck { get; set; }
		public TubularIdFishneckDto IdFishneck { get; set; }
		public TubularOdFishneckDto OdFishneck { get; set; }
		public TubularDispDto Disp { get; set; }
		public TubularPresBurstDto PresBurst { get; set; }
		public TubularPresCollapseDto PresCollapse { get; set; }
		public string ClassService { get; set; }
		public TubularWearWallDto WearWall { get; set; }
		public TubularThickWallDto ThickWall { get; set; }
		public string ConfigCon { get; set; }
		public TubularBendStiffnessDto BendStiffness { get; set; }
		public TubularAxialStiffnessDto AxialStiffness { get; set; }
		public TubularTorsionalStiffnessDto TorsionalStiffness { get; set; }
		public string TypeMaterial { get; set; }
		public TubularDoglegMxDto DoglegMx { get; set; }
		public string Vendor { get; set; }
		public string Model { get; set; }
		public TubularNameTagDto NameTag { get; set; }
		public TubularBitRecordDto BitRecord { get; set; }
		public TubularAreaNozzleFlowDto AreaNozzleFlow { get; set; }
		public List<TubularNozzleDto> Nozzle { get; set; }
		public string Uid { get; set; }
		public TubularHoleOpenerDto HoleOpener { get; set; }
		public TubularStabilizerDto Stabilizer { get; set; }
		public TubularMotorDto Motor { get; set; }
		public TubularBendDto Bend { get; set; }
		public TubularMwdToolDto MwdTool { get; set; }
		public TubularConnectionDto Connection { get; set; }
		public TubularJarDto Jar { get; set; }
	}

	public class TubularDiaHoleOpenerDto
	{
		[Key]
		public int DiaHoleOpenerId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularHoleOpenerDto
	{
		[Key]
		public int HoleOpenerId { get; set; }
		public string TypeHoleOpener { get; set; }
		public string NumCutter { get; set; }
		public string Manufacturer { get; set; }
		public TubularDiaHoleOpenerDto DiaHoleOpener { get; set; }
	}

	public class TubularLenBladeDto
	{
		[Key]
		public int LenBladeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularOdBladeMxDto
	{
		[Key]
		public int OdBladeMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularOdBladeMnDto
	{
		[Key]
		public int OdBladeMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularDistBladeBotDto
	{
		[Key]
		public int DistBladeBotId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularStabilizerDto
	{
		[Key]
		public int StabilizerId { get; set; }
		public TubularLenBladeDto LenBlade { get; set; }
		public TubularOdBladeMxDto OdBladeMx { get; set; }
		public TubularOdBladeMnDto OdBladeMn { get; set; }
		public TubularDistBladeBotDto DistBladeBot { get; set; }
		public string ShapeBlade { get; set; }
		public string FactFric { get; set; }
		public string TypeBlade { get; set; }
		public string Uid { get; set; }
	}

	public class TubularOffsetToolDto
	{
		[Key]
		public int OffsetToolId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularFlowrateMnDto
	{
		[Key]
		public int FlowrateMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularFlowrateMxDto
	{
		[Key]
		public int FlowrateMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularDiaRotorNozzleDto
	{
		[Key]
		public int DiaRotorNozzleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularClearanceBearBoxDto
	{
		[Key]
		public int ClearanceBearBoxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularTempOpMxDto
	{
		[Key]
		public int TempOpMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularBendSettingsMnDto
	{
		[Key]
		public int BendSettingsMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularBendSettingsMxDto
	{
		[Key]
		public int BendSettingsMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularMotorDto
	{
		[Key]
		public int MotorId { get; set; }
		public TubularOffsetToolDto OffsetTool { get; set; }
		public string PresLossFact { get; set; }
		public TubularFlowrateMnDto FlowrateMn { get; set; }
		public TubularFlowrateMxDto FlowrateMx { get; set; }
		public TubularDiaRotorNozzleDto DiaRotorNozzle { get; set; }
		public TubularClearanceBearBoxDto ClearanceBearBox { get; set; }
		public string LobesRotor { get; set; }
		public string LobesStator { get; set; }
		public string TypeBearing { get; set; }
		public TubularTempOpMxDto TempOpMx { get; set; }
		public string RotorCatcher { get; set; }
		public string DumpValve { get; set; }
		public TubularDiaNozzleDto DiaNozzle { get; set; }
		public string Rotatable { get; set; }
		public TubularBendSettingsMnDto BendSettingsMn { get; set; }
		public TubularBendSettingsMxDto BendSettingsMx { get; set; }
	}

	public class TubularAngleDto
	{

		[Key]
		public int AngleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularDistBendBotDto
	{
		[Key]
		public int DistBendBotId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularBendDto
	{
		[Key]
		public int BendId { get; set; }
		public TubularAngleDto Angle { get; set; }
		public TubularDistBendBotDto DistBendBot { get; set; }
		public string Uid { get; set; }
	}

	public class TubularTempMxDto
	{
		[Key]
		public int TempMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularIdEquvDto
	{
		[Key]
		public int IdEquvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularOffsetBotDto
	{
		[Key]
		public int OffsetBotId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularSensorDto
	{
		[Key]
		public int SensorId { get; set; }
		public string TypeMeasurement { get; set; }
		public TubularOffsetBotDto OffsetBot { get; set; }
		public string Comments { get; set; }
		public string Uid { get; set; }
	}

	public class TubularMwdToolDto
	{
		[Key]
		public int MwdToolId { get; set; }
		public TubularFlowrateMnDto FlowrateMn { get; set; }
		public TubularFlowrateMxDto FlowrateMx { get; set; }
		public TubularTempMxDto TempMx { get; set; }
		public TubularIdEquvDto IdEquv { get; set; }
		public List<TubularSensorDto> Sensor { get; set; }
	}

	public class TubularSizeThreadDto
	{
		[Key]
		public int SizeThreadId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularCriticalCrossSectionDto
	{
		[Key]
		public int CriticalCrossSectionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularPresLeakDto
	{
		[Key]
		public int PresLeakId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularTqMakeupDto
	{
		[Key]
		public int TqMakeupId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularConnectionDto
	{
		[Key]
		public int ConnectionId { get; set; }
		public TubularIdDto Id { get; set; }
		public TubularOdDto Od { get; set; }
		public TubularLenDto Len { get; set; }
		public string TypeThread { get; set; }
		public TubularSizeThreadDto SizeThread { get; set; }
		public TubularTensYieldDto TensYield { get; set; }
		public TubularTqYieldDto TqYield { get; set; }
		public string Position { get; set; }
		public TubularCriticalCrossSectionDto CriticalCrossSection { get; set; }
		public TubularPresLeakDto PresLeak { get; set; }
		public TubularTqMakeupDto TqMakeup { get; set; }
		public string Uid { get; set; }
	}

	public class TubularForUpSetDto
	{
		[Key]
		public int ForUpSetId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularForDownSetDto
	{
		[Key]
		public int ForDownSetId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularForUpTripDto
	{
		[Key]
		public int ForUpTripId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularForDownTripDto
	{
		[Key]
		public int ForDownTripId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularForPmpOpenDto
	{
		[Key]
		public int ForPmpOpenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularForSealFricDto
	{
		[Key]
		public int ForSealFricId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularJarDto
	{
		[Key]
		public int JarId { get; set; }
		public TubularForUpSetDto ForUpSet { get; set; }
		public TubularForDownSetDto ForDownSet { get; set; }
		public TubularForUpTripDto ForUpTrip { get; set; }
		public TubularForDownTripDto ForDownTrip { get; set; }
		public TubularForPmpOpenDto ForPmpOpen { get; set; }
		public TubularForSealFricDto ForSealFric { get; set; }
		public string TypeJar { get; set; }
		public string JarAction { get; set; }
	}



	public class TubularDto
	{
		[Key]
		public int TubularId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string TypeTubularAssy { get; set; }
		public string ValveFloat { get; set; }
		public string SourceNuclear { get; set; }
		public TubularDiaHoleAssyDto DiaHoleAssy { get; set; }
		public List<TubularComponentDto> TubularComponent { get; set; }
		public TubularCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	public class TubularCommonDataDto
	{
		[Key]

		public int TubularyCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
 

}
