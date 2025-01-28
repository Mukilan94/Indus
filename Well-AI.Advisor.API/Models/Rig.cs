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


	public class RigRatingDrillDepth
	{
		[Key]
		public int RatingDrillDepthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingWaterDepth
	{
		[Key]
		public int RatingWaterDepthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigAirGap
	{
		[Key]
		public int AirGapId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSizeConnectionBop
	{
		[Key]
		public int SizeConnectionBopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class RigSizeBopSys
	{
		[Key]
		public int SizeBopSysId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdBoosterLine
	{
		[Key]
		public int IdBoosterLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdBoosterLine
	{
		[Key]
		public int OdBoosterLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenBoosterLine
	{
		[Key]
		public int LenBoosterLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdSurfLine
	{
		[Key]
		public int IdSurfLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdSurfLine
	{
		[Key]
		public int OdSurfLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenSurfLine
	{
		[Key]
		public int LenSurfLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdChkLine
	{
		[Key]
		public int IdChkLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdChkLine
	{
		[Key]
		public int OdChkLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenChkLine
	{
		[Key]
		public int LenChkLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdKillLine
	{
		[Key]
		public int IdKillLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdKillLine
	{
		[Key]
		public int OdKillLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenKillLine
	{
		[Key]
		public int LenKillLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdPassThru
	{
		[Key]
		public int IdPassThruId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresWork
	{
		[Key]
		public int PresWorkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigDiaCloseMn
	{
		[Key]
		public int DiaCloseMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigDiaCloseMx
	{
		[Key]
		public int DiaCloseMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigBopComponent
	{
		[Key]
		public int BopComponentId { get; set; }
		public string TypeBopComp { get; set; }
		public string DescComp { get; set; }
		public RigIdPassThru IdPassThru { get; set; }
		public RigPresWork PresWork { get; set; }
		public RigDiaCloseMn DiaCloseMn { get; set; }
		public RigDiaCloseMx DiaCloseMx { get; set; }
		public string Nomenclature { get; set; }
		public string IsVariable { get; set; }
		public string Uid { get; set; }
	}

	public class RigDiaDiverter
	{
		[Key]
		public int DiaDiverterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresWorkDiverter
	{
		[Key]
		public int PresWorkDiverterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapAccFluid
	{
		[Key]
		public int CapAccFluidId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresAccPreCharge
	{
		[Key]
		public int PresAccPreChargeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigVolAccPreCharge
	{
		[Key]
		public int VolAccPreChargeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresAccOpRating
	{
		[Key]
		public int PresAccOpRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresChokeManifold
	{
		[Key]
		public int PresChokeManifoldId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresBopRating
	{
		[Key]
		public int PresBopRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigBop
	{
		[Key]
		public int BopId { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string TypeConnectionBop { get; set; }
		public RigSizeConnectionBop SizeConnectionBop { get; set; }
		public RigPresBopRating PresBopRating { get; set; }
		public RigSizeBopSys SizeBopSys { get; set; }
		public string RotBop { get; set; }
		public RigIdBoosterLine IdBoosterLine { get; set; }
		public RigOdBoosterLine OdBoosterLine { get; set; }
		public RigLenBoosterLine LenBoosterLine { get; set; }
		public RigIdSurfLine IdSurfLine { get; set; }
		public RigOdSurfLine OdSurfLine { get; set; }
		public RigLenSurfLine LenSurfLine { get; set; }
		public RigIdChkLine IdChkLine { get; set; }
		public RigOdChkLine OdChkLine { get; set; }
		public RigLenChkLine LenChkLine { get; set; }
		public RigIdKillLine IdKillLine { get; set; }
		public RigOdKillLine OdKillLine { get; set; }
		public RigLenKillLine LenKillLine { get; set; }
		public List<RigBopComponent> BopComponent { get; set; }
		public string TypeDiverter { get; set; }
		public RigDiaDiverter DiaDiverter { get; set; }
		public RigPresWorkDiverter PresWorkDiverter { get; set; }
		public string Accumulator { get; set; }
		public RigCapAccFluid CapAccFluid { get; set; }
		public RigPresAccPreCharge PresAccPreCharge { get; set; }
		public RigVolAccPreCharge VolAccPreCharge { get; set; }
		public RigPresAccOpRating PresAccOpRating { get; set; }
		public string TypeControlManifold { get; set; }
		public string DescControlManifold { get; set; }
		public string TypeChokeManifold { get; set; }
		public RigPresChokeManifold PresChokeManifold { get; set; }
	}

	public class RigCapMx
	{
		[Key]
		public int CapMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class RigOdRod
	{
		[Key]
		public int OdRodId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class RigEff
	{
		[Key]
		public int EffId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class RigPresMx
	{
		[Key]
		public int PresMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPowHydMx
	{
		[Key]
		public int PowHydMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSpmMx
	{
		[Key]
		public int SpmMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigDisplacement
	{
		[Key]
		public int DisplacementId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresDamp
	{
		[Key]
		public int PresDampId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigVolDamp
	{
		[Key]
		public int VolDampId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPowMechMx
	{
		[Key]
		public int PowMechMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class RigCapFlow
	{
		[Key]
		public int CapFlowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSizeMeshMn
	{
		[Key]
		public int SizeMeshMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class RigCentrifuge
	{
		[Key]
		public int CentrifugeId { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string Type { get; set; }
		public RigCapFlow CapFlow { get; set; }
		public string Owner { get; set; }
		public string Uid { get; set; }
	}

	public class RigHydrocyclone
	{
		[Key]
		public int HydrocycloneId { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string Type { get; set; }
		public string DescCone { get; set; }
		public string Owner { get; set; }
		public string Uid { get; set; }
	}

	public class RigHeight
	{
		[Key]
		public int HeightId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLen
	{
		[Key]
		public int LenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigId
	{
		[Key]
		public int UniqueId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigAreaSeparatorFlow
	{
		[Key]
		public int AreaSeparatorFlowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtMudSeal
	{
		[Key]
		public int HtMudSealId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdInlet
	{
		[Key]
		public int IdInletId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdVentLine
	{
		[Key]
		public int IdVentLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenVentLine
	{
		[Key]
		public int LenVentLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapGasSep
	{
		[Key]
		public int CapGasSepId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapBlowdown
	{
		[Key]
		public int CapBlowdownId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresRating
	{
		[Key]
		public int PresRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigTempRating
	{
		[Key]
		public int TempRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigDegasser
	{
		[Key]
		public int DegasserId { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string Type { get; set; }
		public string Owner { get; set; }
		public RigHeight Height { get; set; }
		public RigLen Len { get; set; }
		public RigId Id { get; set; }
		public RigCapFlow CapFlow { get; set; }
		public RigAreaSeparatorFlow AreaSeparatorFlow { get; set; }
		public RigHtMudSeal HtMudSeal { get; set; }
		public RigIdInlet IdInlet { get; set; }
		public RigIdVentLine IdVentLine { get; set; }
		public RigLenVentLine LenVentLine { get; set; }
		public RigCapGasSep CapGasSep { get; set; }
		public RigCapBlowdown CapBlowdown { get; set; }
		public RigPresRating PresRating { get; set; }
		public RigTempRating TempRating { get; set; }
		public string Uid { get; set; }
	}

	public class RigIdStandpipe
	{
		[Key]
		public int IdStandpipeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenStandpipe
	{
		[Key]
		public int LenStandpipeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdHose
	{
		[Key]
		public int IdHoseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenHose
	{
		[Key]
		public int LenHoseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdSwivel
	{
		[Key]
		public int IdSwivelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenSwivel
	{
		[Key]
		public int LenSwivelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdKelly
	{
		[Key]
		public int IdKellyId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenKelly
	{
		[Key]
		public int LenKellyId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdDischargeLine
	{
		[Key]
		public int IdDischargeLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenDischargeLine
	{
		[Key]
		public int LenDischargeLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdReel
	{
		[Key]
		public int OdReelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdCore
	{
		[Key]
		public int OdCoreId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigWidReelWrap
	{
		[Key]
		public int WidReelWrapId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenReel
	{
		[Key]
		public int LenReelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtInjStk
	{
		[Key]
		public int HtInjStkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdUmbilical
	{
		[Key]
		public int OdUmbilicalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenUmbilical
	{
		[Key]
		public int LenUmbilicalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdTopStk
	{
		[Key]
		public int IdTopStkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtTopStk
	{
		[Key]
		public int HtTopStkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtFlange
	{
		[Key]
		public int HtFlangeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSurfaceEquipment
	{
		[Key]
		public int SurfaceEquipmentId { get; set; }
		public string Description { get; set; }
		public RigPresRating PresRating { get; set; }
		public string TypeSurfEquip { get; set; }
		public string UsePumpDischarge { get; set; }
		public string UseStandpipe { get; set; }
		public string UseHose { get; set; }
		public string UseSwivel { get; set; }
		public string UseKelly { get; set; }
		public string UseTopStack { get; set; }
		public string UseInjStack { get; set; }
		public string UseSurfaceIron { get; set; }
		public RigIdStandpipe IdStandpipe { get; set; }
		public RigLenStandpipe LenStandpipe { get; set; }
		public RigIdHose IdHose { get; set; }
		public RigLenHose LenHose { get; set; }
		public RigIdSwivel IdSwivel { get; set; }
		public RigLenSwivel LenSwivel { get; set; }
		public RigIdKelly IdKelly { get; set; }
		public RigLenKelly LenKelly { get; set; }
		public RigIdDischargeLine IdDischargeLine { get; set; }
		public RigLenDischargeLine LenDischargeLine { get; set; }
		public string CtWrapType { get; set; }
		public RigOdReel OdReel { get; set; }
		public RigOdCore OdCore { get; set; }
		public RigWidReelWrap WidReelWrap { get; set; }
		public RigLenReel LenReel { get; set; }
		public string InjStkUp { get; set; }
		public RigHtInjStk HtInjStk { get; set; }
		public string UmbInside { get; set; }
		public RigOdUmbilical OdUmbilical { get; set; }
		public RigLenUmbilical LenUmbilical { get; set; }
		public RigIdTopStk IdTopStk { get; set; }
		public RigHtTopStk HtTopStk { get; set; }
		public RigHtFlange HtFlange { get; set; }
	}

	public class RigRatingDerrick
	{
		[Key]
		public int RatingDerrickId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtDerrick
	{
		[Key]
		public int HtDerrickId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingHkld
	{
		[Key]
		public int RatingHkldId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapWindDerrick
	{
		[Key]
		public int CapWindDerrickId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigWtBlock
	{
		[Key]
		public int WtBlockId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingBlock
	{
		[Key]
		public int RatingBlockId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingHook
	{
		[Key]
		public int RatingHookId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSizeDrillLine
	{
		[Key]
		public int SizeDrillLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPowerDrawWorks
	{
		[Key]
		public int PowerDrawWorksId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingDrawWorks
	{
		[Key]
		public int RatingDrawWorksId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingSwivel
	{
		[Key]
		public int RatingSwivelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingTqRotSys
	{
		[Key]
		public int RatingTqRotSysId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRotSizeOpening
	{
		[Key]
		public int RotSizeOpeningId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingRotSystem
	{
		[Key]
		public int RatingRotSystemId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapBulkMud
	{
		[Key]
		public int CapBulkMudId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapLiquidMud
	{
		[Key]
		public int CapLiquidMudId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapDrillWater
	{
		[Key]
		public int CapDrillWaterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapPotableWater
	{
		[Key]
		public int CapPotableWaterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapFuel
	{
		[Key]
		public int CapFuelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapBulkCement
	{
		[Key]
		public int CapBulkCementId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigVarDeckLdMx
	{
		[Key]
		public int VarDeckLdMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigVdlStorm
	{
		[Key]
		public int VdlStormId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigMotionCompensationMn
	{
		[Key]
		public int MotionCompensationMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigMotionCompensationMx
	{
		[Key]
		public int MotionCompensationMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigStrokeMotionCompensation
	{
		[Key]
		public int StrokeMotionCompensationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRiserAngleLimit
	{
		[Key]
		public int RiserAngleLimitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHeaveMx
	{
		[Key]
		public int HeaveMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPit
	{
		[Key]
		public string Uid { get; set; }
		public string Index { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public RigCapMx CapMx { get; set; }
		public string Owner { get; set; }
		public string TypePit { get; set; }
		public string IsActive { get; set; }
		
	}


	public class RigIdLiner
	{
		[Key]
		public int IdLinerId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenStroke
	{
		[Key]
		public int LenStrokeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPump
	{
		[Key]
		public string Uid { get; set; }
		public string Index { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string Owner { get; set; }
		public string TypePump { get; set; }
		public string NumCyl { get; set; }
		public RigOdRod OdRod { get; set; }
		public RigIdLiner IdLiner { get; set; }
		public string PumpAction { get; set; }
		public RigEff Eff { get; set; }
		public RigLenStroke LenStroke { get; set; }
		public RigPresMx PresMx { get; set; }
		public RigPowHydMx PowHydMx { get; set; }
		public RigSpmMx SpmMx { get; set; }
		public RigDisplacement Displacement { get; set; }
		public RigPresDamp PresDamp { get; set; }
		public RigVolDamp VolDamp { get; set; }
		public RigPowMechMx PowMechMx { get; set; }
		
	}

	public class RigShaker
	{
		[Key]
		public string Uid { get; set; }
		public string Name { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string Type { get; set; }
		public string LocationShaker { get; set; }
		public string NumDecks { get; set; }
		public string NumCascLevel { get; set; }
		public string MudCleaner { get; set; }
		public RigCapFlow CapFlow { get; set; }
		public string Owner { get; set; }
		public RigSizeMeshMn SizeMeshMn { get; set; }
		
	}
	public class Rig
	{
		[Key]
		public int RigId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string Owner { get; set; }
		public string TypeRig { get; set; }
		public string Manufacturer { get; set; }
		public string YearEntService { get; set; }
		public string ClassRig { get; set; }
		public string Approvals { get; set; }
		public string Registration { get; set; }
		public string TelNumber { get; set; }
		public string FaxNumber { get; set; }
		public string EmailAddress { get; set; }
		public string NameContact { get; set; }
		public RigRatingDrillDepth RatingDrillDepth { get; set; }
		public RigRatingWaterDepth RatingWaterDepth { get; set; }
		public string IsOffshore { get; set; }
		public RigAirGap AirGap { get; set; }
		public string DTimStartOp { get; set; }
		public string DTimEndOp { get; set; }
		public RigBop Bop { get; set; }
		public RigPit Pit { get; set; }
		public RigPump Pump { get; set; }
		public RigShaker Shaker { get; set; }
		public RigCentrifuge Centrifuge { get; set; }
		public RigHydrocyclone Hydrocyclone { get; set; }
		public RigDegasser Degasser { get; set; }
		public RigSurfaceEquipment SurfaceEquipment { get; set; }
		public string NumDerricks { get; set; }
		public string TypeDerrick { get; set; }
		public RigRatingDerrick RatingDerrick { get; set; }
		public RigHtDerrick HtDerrick { get; set; }
		public RigRatingHkld RatingHkld { get; set; }
		public RigCapWindDerrick CapWindDerrick { get; set; }
		public RigWtBlock WtBlock { get; set; }
		public RigRatingBlock RatingBlock { get; set; }
		public string NumBlockLines { get; set; }
		public string TypeHook { get; set; }
		public RigRatingHook RatingHook { get; set; }
		public RigSizeDrillLine SizeDrillLine { get; set; }
		public string TypeDrawWorks { get; set; }
		public RigPowerDrawWorks PowerDrawWorks { get; set; }
		public RigRatingDrawWorks RatingDrawWorks { get; set; }
		public string MotorDrawWorks { get; set; }
		public string DescBrake { get; set; }
		public string TypeSwivel { get; set; }
		public RigRatingSwivel RatingSwivel { get; set; }
		public string RotSystem { get; set; }
		public string DescRotSystem { get; set; }
		public RigRatingTqRotSys RatingTqRotSys { get; set; }
		public RigRotSizeOpening RotSizeOpening { get; set; }
		public RigRatingRotSystem RatingRotSystem { get; set; }
		public string ScrSystem { get; set; }
		public string PipeHandlingSystem { get; set; }
		public RigCapBulkMud CapBulkMud { get; set; }
		public RigCapLiquidMud CapLiquidMud { get; set; }
		public RigCapDrillWater CapDrillWater { get; set; }
		public RigCapPotableWater CapPotableWater { get; set; }
		public RigCapFuel CapFuel { get; set; }
		public RigCapBulkCement CapBulkCement { get; set; }
		public string MainEngine { get; set; }
		public string Generator { get; set; }
		public string CementUnit { get; set; }
		public string NumBunks { get; set; }
		public string BunksPerRoom { get; set; }
		public string NumCranes { get; set; }
		public string NumAnch { get; set; }
		public string MoorType { get; set; }
		public string NumGuideTens { get; set; }
		public string NumRiserTens { get; set; }
		public RigVarDeckLdMx VarDeckLdMx { get; set; }
		public RigVdlStorm VdlStorm { get; set; }
		public string NumThrusters { get; set; }
		public string Azimuthing { get; set; }
		public RigMotionCompensationMn MotionCompensationMn { get; set; }
		public RigMotionCompensationMx MotionCompensationMx { get; set; }
		public RigStrokeMotionCompensation StrokeMotionCompensation { get; set; }
		public RigRiserAngleLimit RiserAngleLimit { get; set; }
		public RigHeaveMx HeaveMx { get; set; }
		public string Gantry { get; set; }
		public string Flares { get; set; }
		public RigCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class RigCommonData
	{
		[Key]
		public int RigCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	 

}
