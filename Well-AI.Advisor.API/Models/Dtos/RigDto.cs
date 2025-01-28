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


	public class RigRatingDrillDepthDto
	{
		[Key]
		public int RatingDrillDepthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingWaterDepthDto
	{
		[Key]
		public int RatingWaterDepthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigAirGapDto
	{
		[Key]
		public int AirGapId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSizeConnectionBopDto
	{
		[Key]
		public int SizeConnectionBopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class RigSizeBopSysDto
	{
		[Key]
		public int SizeBopSysId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdBoosterLineDto
	{
		[Key]
		public int IdBoosterLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdBoosterLineDto
	{
		[Key]
		public int OdBoosterLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenBoosterLineDto
	{
		[Key]
		public int LenBoosterLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdSurfLineDto
	{
		[Key]
		public int IdSurfLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdSurfLineDto
	{
		[Key]
		public int OdSurfLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenSurfLineDto
	{
		[Key]
		public int LenSurfLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdChkLineDto
	{
		[Key]
		public int IdChkLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdChkLineDto
	{
		[Key]
		public int OdChkLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenChkLineDto
	{
		[Key]
		public int LenChkLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdKillLineDto
	{
		[Key]
		public int IdKillLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdKillLineDto
	{
		[Key]
		public int OdKillLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenKillLineDto
	{
		[Key]
		public int LenKillLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdPassThruDto
	{
		[Key]
		public int IdPassThruId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresWorkDto
	{
		[Key]
		public int PresWorkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigDiaCloseMnDto
	{
		[Key]
		public int DiaCloseMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigDiaCloseMxDto
	{
		[Key]
		public int DiaCloseMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigBopComponentDto
	{
		[Key]
		public int BopComponentId { get; set; }
		public string TypeBopComp { get; set; }
		public string DescComp { get; set; }
		public RigIdPassThruDto IdPassThru { get; set; }
		public RigPresWorkDto PresWork { get; set; }
		public RigDiaCloseMnDto DiaCloseMn { get; set; }
		public RigDiaCloseMxDto DiaCloseMx { get; set; }
		public string Nomenclature { get; set; }
		public string IsVariable { get; set; }
		public string Uid { get; set; }
	}

	public class RigDiaDiverterDto
	{
		[Key]
		public int DiaDiverterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresWorkDiverterDto
	{
		[Key]
		public int PresWorkDiverterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapAccFluidDto
	{
		[Key]
		public int CapAccFluidId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresAccPreChargeDto
	{
		[Key]
		public int PresAccPreChargeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigVolAccPreChargeDto
	{
		[Key]
		public int VolAccPreChargeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresAccOpRatingDto
	{
		[Key]
		public int PresAccOpRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresChokeManifoldDto
	{
		[Key]
		public int PresChokeManifoldId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresBopRatingDto
	{
		[Key]
		public int PresBopRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigBopDto
	{
		[Key]
		public int BopId { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string TypeConnectionBop { get; set; }
		public RigSizeConnectionBopDto SizeConnectionBop { get; set; }
		public RigPresBopRatingDto PresBopRating { get; set; }
		public RigSizeBopSysDto SizeBopSys { get; set; }
		public string RotBop { get; set; }
		public RigIdBoosterLineDto IdBoosterLine { get; set; }
		public RigOdBoosterLineDto OdBoosterLine { get; set; }
		public RigLenBoosterLineDto LenBoosterLine { get; set; }
		public RigIdSurfLineDto IdSurfLine { get; set; }
		public RigOdSurfLineDto OdSurfLine { get; set; }
		public RigLenSurfLineDto LenSurfLine { get; set; }
		public RigIdChkLineDto IdChkLine { get; set; }
		public RigOdChkLineDto OdChkLine { get; set; }
		public RigLenChkLineDto LenChkLine { get; set; }
		public RigIdKillLineDto IdKillLine { get; set; }
		public RigOdKillLineDto OdKillLine { get; set; }
		public RigLenKillLineDto LenKillLine { get; set; }
		public List<RigBopComponentDto> BopComponent { get; set; }
		public string TypeDiverter { get; set; }
		public RigDiaDiverterDto DiaDiverter { get; set; }
		public RigPresWorkDiverterDto PresWorkDiverter { get; set; }
		public string Accumulator { get; set; }
		public RigCapAccFluidDto CapAccFluid { get; set; }
		public RigPresAccPreChargeDto PresAccPreCharge { get; set; }
		public RigVolAccPreChargeDto VolAccPreCharge { get; set; }
		public RigPresAccOpRatingDto PresAccOpRating { get; set; }
		public string TypeControlManifold { get; set; }
		public string DescControlManifold { get; set; }
		public string TypeChokeManifold { get; set; }
		public RigPresChokeManifoldDto PresChokeManifold { get; set; }
	}

	public class RigCapMxDto
	{
		[Key]
		public int CapMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class RigOdRodDto
	{
		[Key]
		public int OdRodId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class RigEffDto
	{
		[Key]
		public int EffId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class RigPresMxDto
	{
		[Key]
		public int PresMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPowHydMxDto
	{
		[Key]
		public int PowHydMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSpmMxDto
	{
		[Key]
		public int SpmMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigDisplacementDto
	{
		[Key]
		public int DisplacementId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresDampDto
	{
		[Key]
		public int PresDampId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigVolDampDto
	{
		[Key]
		public int VolDampId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPowMechMxDto
	{
		[Key]
		public int PowMechMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class RigCapFlowDto
	{
		[Key]
		public int CapFlowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSizeMeshMnDto
	{
		[Key]
		public int SizeMeshMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class RigCentrifugeDto
	{
		[Key]
		public int CentrifugeId { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string Type { get; set; }
		public RigCapFlowDto CapFlow { get; set; }
		public string Owner { get; set; }
		public string Uid { get; set; }
	}

	public class RigHydrocycloneDto
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

	public class RigHeightDto
	{
		[Key]
		public int HeightId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenDto
	{
		[Key]
		public int LenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdDto
	{
		[Key]
		public int UniqueId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigAreaSeparatorFlowDto
	{
		[Key]
		public int AreaSeparatorFlowId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtMudSealDto
	{
		[Key]
		public int HtMudSealId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdInletDto
	{
		[Key]
		public int IdInletId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdVentLineDto
	{
		[Key]
		public int IdVentLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenVentLineDto
	{
		[Key]
		public int LenVentLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapGasSepDto
	{
		[Key]
		public int CapGasSepId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapBlowdownDto
	{
		[Key]
		public int CapBlowdownId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPresRatingDto
	{
		[Key]
		public int PresRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigTempRatingDto
	{
		[Key]
		public int TempRatingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigDegasserDto
	{
		[Key]
		public int DegasserId { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public string Type { get; set; }
		public string Owner { get; set; }
		public RigHeightDto Height { get; set; }
		public RigLenDto Len { get; set; }
		public RigIdDto Id { get; set; }
		public RigCapFlowDto CapFlow { get; set; }
		public RigAreaSeparatorFlowDto AreaSeparatorFlow { get; set; }
		public RigHtMudSealDto HtMudSeal { get; set; }
		public RigIdInletDto IdInlet { get; set; }
		public RigIdVentLineDto IdVentLine { get; set; }
		public RigLenVentLineDto LenVentLine { get; set; }
		public RigCapGasSepDto CapGasSep { get; set; }
		public RigCapBlowdownDto CapBlowdown { get; set; }
		public RigPresRatingDto PresRating { get; set; }
		public RigTempRatingDto TempRating { get; set; }
		public string Uid { get; set; }
	}

	public class RigIdStandpipeDto
	{
		[Key]
		public int IdStandpipeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenStandpipeDto
	{
		[Key]
		public int LenStandpipeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdHoseDto
	{
		[Key]
		public int IdHoseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenHoseDto
	{
		[Key]
		public int LenHoseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdSwivelDto
	{
		[Key]
		public int IdSwivelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenSwivelDto
	{
		[Key]
		public int LenSwivelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdKellyDto
	{
		[Key]
		public int IdKellyId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenKellyDto
	{
		[Key]
		public int LenKellyId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdDischargeLineDto
	{
		[Key]
		public int IdDischargeLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenDischargeLineDto
	{
		[Key]
		public int LenDischargeLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdReelDto
	{
		[Key]
		public int OdReelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdCoreDto
	{
		[Key]
		public int OdCoreId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigWidReelWrapDto
	{
		[Key]
		public int WidReelWrapId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenReelDto
	{
		[Key]
		public int LenReelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtInjStkDto
	{
		[Key]
		public int HtInjStkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigOdUmbilicalDto
	{
		[Key]
		public int OdUmbilicalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenUmbilicalDto
	{
		[Key]
		public int LenUmbilicalId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigIdTopStkDto
	{
		[Key]
		public int IdTopStkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtTopStkDto
	{
		[Key]
		public int HtTopStkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtFlangeDto
	{
		[Key]
		public int HtFlangeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSurfaceEquipmentDto
	{
		[Key]
		public int SurfaceEquipmentId { get; set; }
		public string Description { get; set; }
		public RigPresRatingDto PresRating { get; set; }
		public string TypeSurfEquip { get; set; }
		public string UsePumpDischarge { get; set; }
		public string UseStandpipe { get; set; }
		public string UseHose { get; set; }
		public string UseSwivel { get; set; }
		public string UseKelly { get; set; }
		public string UseTopStack { get; set; }
		public string UseInjStack { get; set; }
		public string UseSurfaceIron { get; set; }
		public RigIdStandpipeDto IdStandpipe { get; set; }
		public RigLenStandpipeDto LenStandpipe { get; set; }
		public RigIdHoseDto IdHose { get; set; }
		public RigLenHoseDto LenHose { get; set; }
		public RigIdSwivelDto IdSwivel { get; set; }
		public RigLenSwivelDto LenSwivel { get; set; }
		public RigIdKellyDto IdKelly { get; set; }
		public RigLenKellyDto LenKelly { get; set; }
		public RigIdDischargeLineDto IdDischargeLine { get; set; }
		public RigLenDischargeLineDto LenDischargeLine { get; set; }
		public string CtWrapType { get; set; }
		public RigOdReelDto OdReel { get; set; }
		public RigOdCoreDto OdCore { get; set; }
		public RigWidReelWrapDto WidReelWrap { get; set; }
		public RigLenReelDto LenReel { get; set; }
		public string InjStkUp { get; set; }
		public RigHtInjStkDto HtInjStk { get; set; }
		public string UmbInside { get; set; }
		public RigOdUmbilicalDto OdUmbilical { get; set; }
		public RigLenUmbilicalDto LenUmbilical { get; set; }
		public RigIdTopStkDto IdTopStk { get; set; }
		public RigHtTopStkDto HtTopStk { get; set; }
		public RigHtFlangeDto HtFlange { get; set; }
	}

	public class RigRatingDerrickDto
	{
		[Key]
		public int RatingDerrickId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHtDerrickDto
	{
		[Key]
		public int HtDerrickId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingHkldDto
	{
		[Key]
		public int RatingHkldId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapWindDerrickDto
	{
		[Key]
		public int CapWindDerrickId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigWtBlockDto
	{
		[Key]
		public int WtBlockId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingBlockDto
	{
		[Key]
		public int RatingBlockId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingHookDto
	{
		[Key]
		public int RatingHookId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigSizeDrillLineDto
	{
		[Key]
		public int SizeDrillLineId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPowerDrawWorksDto
	{
		[Key]
		public int PowerDrawWorksId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingDrawWorksDto
	{
		[Key]
		public int RatingDrawWorksId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingSwivelDto
	{
		[Key]
		public int RatingSwivelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingTqRotSysDto
	{
		[Key]
		public int RatingTqRotSysId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRotSizeOpeningDto
	{
		[Key]
		public int RotSizeOpeningId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRatingRotSystemDto
	{
		[Key]
		public int RatingRotSystemId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapBulkMudDto
	{
		[Key]
		public int CapBulkMudId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapLiquidMudDto
	{
		[Key]
		public int CapLiquidMudId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapDrillWaterDto
	{
		[Key]
		public int CapDrillWaterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapPotableWaterDto
	{
		[Key]
		public int CapPotableWaterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapFuelDto
	{
		[Key]
		public int CapFuelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigCapBulkCementDto
	{
		[Key]
		public int CapBulkCementId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigVarDeckLdMxDto
	{
		[Key]
		public int VarDeckLdMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigVdlStormDto
	{
		[Key]
		public int VdlStormId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigMotionCompensationMnDto
	{
		[Key]
		public int MotionCompensationMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigMotionCompensationMxDto
	{
		[Key]
		public int MotionCompensationMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigStrokeMotionCompensationDto
	{
		[Key]
		public int StrokeMotionCompensationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigRiserAngleLimitDto
	{
		[Key]
		public int RiserAngleLimitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigHeaveMxDto
	{
		[Key]
		public int HeaveMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPitDto
	{
		[Key]
		public string Uid { get; set; }
		public string Index { get; set; }
		public string DTimInstall { get; set; }
		public string DTimRemove { get; set; }
		public RigCapMxDto CapMx { get; set; }
		public string Owner { get; set; }
		public string TypePit { get; set; }
		public string IsActive { get; set; }

	}


	public class RigIdLinerDto
	{
		[Key]
		public int IdLinerId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigLenStrokeDto
	{
		[Key]
		public int LenStrokeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RigPumpDto
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
		public RigOdRodDto OdRod { get; set; }
		public RigIdLinerDto IdLiner { get; set; }
		public string PumpAction { get; set; }
		public RigEffDto Eff { get; set; }
		public RigLenStrokeDto LenStroke { get; set; }
		public RigPresMxDto PresMx { get; set; }
		public RigPowHydMxDto PowHydMx { get; set; }
		public RigSpmMxDto SpmMx { get; set; }
		public RigDisplacementDto Displacement { get; set; }
		public RigPresDampDto PresDamp { get; set; }
		public RigVolDampDto VolDamp { get; set; }
		public RigPowMechMxDto PowMechMx { get; set; }

	}

	public class RigShakerDto
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
		public RigCapFlowDto CapFlow { get; set; }
		public string Owner { get; set; }
		public RigSizeMeshMnDto SizeMeshMn { get; set; }

	}
	public class RigDto
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
		public RigRatingDrillDepthDto RatingDrillDepth { get; set; }
		public RigRatingWaterDepthDto RatingWaterDepth { get; set; }
		public string IsOffshore { get; set; }
		public RigAirGapDto AirGap { get; set; }
		public string DTimStartOp { get; set; }
		public string DTimEndOp { get; set; }
		public RigBopDto Bop { get; set; }
		public RigPitDto Pit { get; set; }
		public RigPumpDto Pump { get; set; }
		public RigShakerDto Shaker { get; set; }
		public RigCentrifugeDto Centrifuge { get; set; }
		public RigHydrocycloneDto Hydrocyclone { get; set; }
		public RigDegasserDto Degasser { get; set; }
		public RigSurfaceEquipmentDto SurfaceEquipment { get; set; }
		public string NumDerricks { get; set; }
		public string TypeDerrick { get; set; }
		public RigRatingDerrickDto RatingDerrick { get; set; }
		public RigHtDerrickDto HtDerrick { get; set; }
		public RigRatingHkldDto RatingHkld { get; set; }
		public RigCapWindDerrickDto CapWindDerrick { get; set; }
		public RigWtBlockDto WtBlock { get; set; }
		public RigRatingBlockDto RatingBlock { get; set; }
		public string NumBlockLines { get; set; }
		public string TypeHook { get; set; }
		public RigRatingHookDto RatingHook { get; set; }
		public RigSizeDrillLineDto SizeDrillLine { get; set; }
		public string TypeDrawWorks { get; set; }
		public RigPowerDrawWorksDto PowerDrawWorks { get; set; }
		public RigRatingDrawWorksDto RatingDrawWorks { get; set; }
		public string MotorDrawWorks { get; set; }
		public string DescBrake { get; set; }
		public string TypeSwivel { get; set; }
		public RigRatingSwivelDto RatingSwivel { get; set; }
		public string RotSystem { get; set; }
		public string DescRotSystem { get; set; }
		public RigRatingTqRotSysDto RatingTqRotSys { get; set; }
		public RigRotSizeOpeningDto RotSizeOpening { get; set; }
		public RigRatingRotSystemDto RatingRotSystem { get; set; }
		public string ScrSystem { get; set; }
		public string PipeHandlingSystem { get; set; }
		public RigCapBulkMudDto CapBulkMud { get; set; }
		public RigCapLiquidMudDto CapLiquidMud { get; set; }
		public RigCapDrillWaterDto CapDrillWater { get; set; }
		public RigCapPotableWaterDto CapPotableWater { get; set; }
		public RigCapFuelDto CapFuel { get; set; }
		public RigCapBulkCementDto CapBulkCement { get; set; }
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
		public RigVarDeckLdMxDto VarDeckLdMx { get; set; }
		public RigVdlStormDto VdlStorm { get; set; }
		public string NumThrusters { get; set; }
		public string Azimuthing { get; set; }
		public RigMotionCompensationMnDto MotionCompensationMn { get; set; }
		public RigMotionCompensationMxDto MotionCompensationMx { get; set; }
		public RigStrokeMotionCompensationDto StrokeMotionCompensation { get; set; }
		public RigRiserAngleLimitDto RiserAngleLimit { get; set; }
		public RigHeaveMxDto HeaveMx { get; set; }
		public string Gantry { get; set; }
		public string Flares { get; set; }
		public RigCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class RigCommonDataDto
	{
		[Key]
		public int RigCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
 

}
