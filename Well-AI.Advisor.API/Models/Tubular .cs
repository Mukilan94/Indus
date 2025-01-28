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
	
	public class TubularDiaHoleAssy {
		[Key]
		public int DiaHoleAssyId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	
	public class TubularLenJointAv {
		[Key]
		public int LenJointAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	
	public class TubularOdDrift {
		[Key]
		public int OdDriftId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularTensYield {
		[Key]
		public int TensYieldId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularTqYield {
		[Key]
		public int TqYieldId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularStressFatig {
		[Key]
		public int StressFatigId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularLenFishneck {
		[Key]
		public int LenFishneckId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularIdFishneck {
		[Key]
		public int IdFishneckId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularOdFishneck {
		[Key]
		public int OdFishneckId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularDisp {
		[Key]
		public int DispId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularPresBurst {
		[Key]
		public int PresBurstId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularPresCollapse {
		[Key]
		public int PresCollapseId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularWearWall {
		[Key]
		public int WearWallId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularThickWall {
		[Key]
		public int ThickWallId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularBendStiffness {
		[Key]
		public int Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularAxialStiffness {
		[Key]
		public int Id { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularTorsionalStiffness {
		[Key]
		public int TorsionalStiffnessId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularDoglegMx {
		[Key]
		public int DoglegMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularNameTag {
		[Key]
		public int NameTagId { get; set; }
		public string Name { get; set; }
		public string NumberingScheme { get; set; }
		public string Uid { get; set; }
	}

	
	public class TubularDiaPassThru {
		[Key]
		public int DiaPassThruId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularDiaPilot
	{
		[Key]
		public int DiaPilotId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularCost {
		[Key]
		public int CostId { get; set; }
		public string Currency { get; set; }
		
		public string Text { get; set; }
	}

	
	public class TubularAreaNozzleFlow {
		[Key]
		public int AreaNozzleFlowId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularDiaNozzle {
		[Key]
		public int DiaNozzleId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularId
	{
		[Key]
		public int TubularIdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularOd
	{
		[Key]
		public int OdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularLen
	{
		[Key]
		public int LenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularNozzle {
		[Key]
		public int NozzleId { get; set; }
		public string Index { get; set; }
		public TubularDiaNozzle DiaNozzle { get; set; }
		public string TypeNozzle { get; set; }
		public TubularLen Len { get; set; }
		public string Uid { get; set; }
	}


	public class TubularDiaBit
	{
		[Key]

		public int DiaBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularWtPerLen
	{
		[Key]

		public int WtPerLenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TubularBitRecord
	{
		[Key]

		public int BitId { get; set; }
		public string NumBit { get; set; }
		public TubularDiaBit DiaBit { get; set; }
		public TubularDiaPassThru DiaPassThru { get; set; }
		public TubularDiaPilot DiaPilot { get; set; }
		public string Manufacturer { get; set; }
		public string TypeBit { get; set; }
		public TubularCost Cost { get; set; }
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

	public class TubularComponent {
		[Key]
		public int TubularComponentId { get; set; }
		public string TypeTubularComp { get; set; }
		public string Sequence { get; set; }
		public string Description { get; set; }
		public TubularId Id { get; set; }
		public TubularOd Od { get; set; }
		public TubularLen Len { get; set; }
		public TubularLenJointAv LenJointAv { get; set; }
		public string NumJointStand { get; set; }
		public TubularWtPerLen WtPerLen { get; set; }
		public string Grade { get; set; }
		public TubularOdDrift OdDrift { get; set; }
		public TubularTensYield TensYield { get; set; }
		public TubularTqYield TqYield { get; set; }
		public TubularStressFatig StressFatig { get; set; }
		public TubularLenFishneck LenFishneck { get; set; }
		public TubularIdFishneck IdFishneck { get; set; }
		public TubularOdFishneck OdFishneck { get; set; }
		public TubularDisp Disp { get; set; }
		public TubularPresBurst PresBurst { get; set; }
		public TubularPresCollapse PresCollapse { get; set; }
		public string ClassService { get; set; }
		public TubularWearWall WearWall { get; set; }
		public TubularThickWall ThickWall { get; set; }
		public string ConfigCon { get; set; }
		public TubularBendStiffness BendStiffness { get; set; }
		public TubularAxialStiffness AxialStiffness { get; set; }
		public TubularTorsionalStiffness TorsionalStiffness { get; set; }
		public string TypeMaterial { get; set; }
		public TubularDoglegMx DoglegMx { get; set; }
		public string Vendor { get; set; }
		public string Model { get; set; }
		public TubularNameTag NameTag { get; set; }
		public TubularBitRecord BitRecord { get; set; }
		public TubularAreaNozzleFlow AreaNozzleFlow { get; set; }
		public List<TubularNozzle> Nozzle { get; set; }
		public string Uid { get; set; }
		public TubularHoleOpener HoleOpener { get; set; }
		public TubularStabilizer Stabilizer { get; set; }
		public TubularMotor Motor { get; set; }
		public TubularBend Bend { get; set; }
		public TubularMwdTool MwdTool { get; set; }
		public TubularConnection Connection { get; set; }
		public TubularJar Jar { get; set; }
	}

	public class TubularDiaHoleOpener {
		[Key]
		public int DiaHoleOpenerId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularHoleOpener {
		[Key]
		public int HoleOpenerId { get; set; }
		public string TypeHoleOpener { get; set; }
		public string NumCutter { get; set; }
		public string Manufacturer { get; set; }
		public TubularDiaHoleOpener DiaHoleOpener { get; set; }
	}

	public class TubularLenBlade {
		[Key]
		public int LenBladeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularOdBladeMx {
		[Key]
		public int OdBladeMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularOdBladeMn {
		[Key]
		public int OdBladeMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularDistBladeBot {
		[Key]
		public int DistBladeBotId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularStabilizer {
		[Key]
		public int StabilizerId { get; set; }
		public TubularLenBlade LenBlade { get; set; }
		public TubularOdBladeMx OdBladeMx { get; set; }
		public TubularOdBladeMn OdBladeMn { get; set; }
		public TubularDistBladeBot DistBladeBot { get; set; }
		public string ShapeBlade { get; set; }
		public string FactFric { get; set; }
		public string TypeBlade { get; set; }
		public string Uid { get; set; }
	}

	public class TubularOffsetTool {
		[Key]
		public int OffsetToolId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularFlowrateMn {
		[Key]
		public int FlowrateMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularFlowrateMx {
		[Key]
		public int FlowrateMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularDiaRotorNozzle {
		[Key]
		public int DiaRotorNozzleId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularClearanceBearBox {
		[Key]
		public int ClearanceBearBoxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularTempOpMx {
		[Key]
		public int TempOpMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularBendSettingsMn {
		[Key]
		public int BendSettingsMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularBendSettingsMx {
		[Key]
		public int BendSettingsMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularMotor {
		[Key]
		public int MotorId { get; set; }
		public TubularOffsetTool OffsetTool { get; set; }
		public string PresLossFact { get; set; }
		public TubularFlowrateMn FlowrateMn { get; set; }
		public TubularFlowrateMx FlowrateMx { get; set; }
		public TubularDiaRotorNozzle DiaRotorNozzle { get; set; }
		public TubularClearanceBearBox ClearanceBearBox { get; set; }
		public string LobesRotor { get; set; }
		public string LobesStator { get; set; }
		public string TypeBearing { get; set; }
		public TubularTempOpMx TempOpMx { get; set; }
		public string RotorCatcher { get; set; }
		public string DumpValve { get; set; }
		public TubularDiaNozzle DiaNozzle { get; set; }
		public string Rotatable { get; set; }
		public TubularBendSettingsMn BendSettingsMn { get; set; }
		public TubularBendSettingsMx BendSettingsMx { get; set; }
	}

	public class TubularAngle {

		[Key]
		public int AngleId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularDistBendBot {
		[Key]
		public int DistBendBotId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularBend {
		[Key]
		public int BendId { get; set; }
		public TubularAngle Angle { get; set; }
		public TubularDistBendBot DistBendBot { get; set; }
		public string Uid { get; set; }
	}

	public class TubularTempMx {
		[Key]
		public int TempMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularIdEquv {
		[Key]
		public int IdEquvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularOffsetBot {
		[Key]
		public int OffsetBotId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularSensor {
		[Key]
		public int SensorId { get; set; }
		public string TypeMeasurement { get; set; }
		public TubularOffsetBot OffsetBot { get; set; }
		public string Comments { get; set; }
		public string Uid { get; set; }
	}

	public class TubularMwdTool {
		[Key]
		public int MwdToolId { get; set; }
		public TubularFlowrateMn FlowrateMn { get; set; }
		public TubularFlowrateMx FlowrateMx { get; set; }
		public TubularTempMx TempMx { get; set; }
		public TubularIdEquv IdEquv { get; set; }
		public List<TubularSensor> Sensor { get; set; }
	}

	public class TubularSizeThread {
		[Key]
		public int SizeThreadId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularCriticalCrossSection {
		[Key]
		public int CriticalCrossSectionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularPresLeak {
		[Key]
		public int PresLeakId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularTqMakeup {
		[Key]
		public int TqMakeupId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularConnection {
		[Key]
		public int ConnectionId { get; set; }
		public TubularId Id { get; set; }
		public TubularOd Od { get; set; }
		public TubularLen Len { get; set; }
		public string TypeThread { get; set; }
		public TubularSizeThread SizeThread { get; set; }
		public TubularTensYield TensYield { get; set; }
		public TubularTqYield TqYield { get; set; }
		public string Position { get; set; }
		public TubularCriticalCrossSection CriticalCrossSection { get; set; }
		public TubularPresLeak PresLeak { get; set; }
		public TubularTqMakeup TqMakeup { get; set; }
		public string Uid { get; set; }
	}

	public class TubularForUpSet {
		[Key]
		public int ForUpSetId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularForDownSet {
		[Key]
		public int ForDownSetId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularForUpTrip {
		[Key]
		public int ForUpTripId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularForDownTrip {
		[Key]
		public int ForDownTripId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularForPmpOpen {
		[Key]
		public int ForPmpOpenId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularForSealFric {
		[Key]
		public int ForSealFricId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TubularJar {
		[Key]
		public int JarId { get; set; }
		public TubularForUpSet ForUpSet { get; set; }
		public TubularForDownSet ForDownSet { get; set; }
		public TubularForUpTrip ForUpTrip { get; set; }
		public TubularForDownTrip ForDownTrip { get; set; }
		public TubularForPmpOpen ForPmpOpen { get; set; }
		public TubularForSealFric ForSealFric { get; set; }
		public string TypeJar { get; set; }
		public string JarAction { get; set; }
	}

	

	public class Tubular {
		[Key]
		public int TubularId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string TypeTubularAssy { get; set; }
		public string ValveFloat { get; set; }
		public string SourceNuclear { get; set; }
		public TubularDiaHoleAssy DiaHoleAssy { get; set; }
		public List<TubularComponent> TubularComponent { get; set; }
		public TubularCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	public class TubularCommonData
	{
		[Key]

		public int TubularyCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	 

}
