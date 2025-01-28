using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Rigs
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
        public int? RatingDrillDepthId { get; set; }
        public int? RatingWaterDepthId { get; set; }
        public string IsOffshore { get; set; }
        public int? AirGapId { get; set; }
        [Column("DTimStartOp")]
        public string DtimStartOp { get; set; }
        [Column("DTimEndOp")]
        public string DtimEndOp { get; set; }
        public int? BopId { get; set; }
        public string PitUid { get; set; }
        public string PumpUid { get; set; }
        public string ShakerUid { get; set; }
        public int? CentrifugeId { get; set; }
        public int? HydrocycloneId { get; set; }
        public int? DegasserId { get; set; }
        public int? SurfaceEquipmentId { get; set; }
        public string NumDerricks { get; set; }
        public string TypeDerrick { get; set; }
        public int? RatingDerrickId { get; set; }
        public int? HtDerrickId { get; set; }
        public int? RatingHkldId { get; set; }
        public int? CapWindDerrickId { get; set; }
        public int? WtBlockId { get; set; }
        public int? RatingBlockId { get; set; }
        public string NumBlockLines { get; set; }
        public string TypeHook { get; set; }
        public int? RatingHookId { get; set; }
        public int? SizeDrillLineId { get; set; }
        public string TypeDrawWorks { get; set; }
        public int? PowerDrawWorksId { get; set; }
        public int? RatingDrawWorksId { get; set; }
        public string MotorDrawWorks { get; set; }
        public string DescBrake { get; set; }
        public string TypeSwivel { get; set; }
        public int? RatingSwivelId { get; set; }
        public string RotSystem { get; set; }
        public string DescRotSystem { get; set; }
        public int? RatingTqRotSysId { get; set; }
        public int? RotSizeOpeningId { get; set; }
        public int? RatingRotSystemId { get; set; }
        public string ScrSystem { get; set; }
        public string PipeHandlingSystem { get; set; }
        public int? CapBulkMudId { get; set; }
        public int? CapLiquidMudId { get; set; }
        public int? CapDrillWaterId { get; set; }
        public int? CapPotableWaterId { get; set; }
        public int? CapFuelId { get; set; }
        public int? CapBulkCementId { get; set; }
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
        public int? VarDeckLdMxId { get; set; }
        public int? VdlStormId { get; set; }
        public string NumThrusters { get; set; }
        public string Azimuthing { get; set; }
        public int? MotionCompensationMnId { get; set; }
        public int? MotionCompensationMxId { get; set; }
        public int? StrokeMotionCompensationId { get; set; }
        public int? RiserAngleLimitId { get; set; }
        public int? HeaveMxId { get; set; }
        public string Gantry { get; set; }
        public string Flares { get; set; }
        public int? CommonDataRigCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(AirGapId))]
        [InverseProperty(nameof(RigAirGaps.Rigs))]
        public virtual RigAirGaps AirGap { get; set; }
        [ForeignKey(nameof(BopId))]
        [InverseProperty(nameof(RigBops.Rigs))]
        public virtual RigBops Bop { get; set; }
        [ForeignKey(nameof(CapBulkCementId))]
        [InverseProperty(nameof(RigCapBulkCements.Rigs))]
        public virtual RigCapBulkCements CapBulkCement { get; set; }
        [ForeignKey(nameof(CapBulkMudId))]
        [InverseProperty(nameof(RigCapBulkMuds.Rigs))]
        public virtual RigCapBulkMuds CapBulkMud { get; set; }
        [ForeignKey(nameof(CapDrillWaterId))]
        [InverseProperty(nameof(RigCapDrillWaters.Rigs))]
        public virtual RigCapDrillWaters CapDrillWater { get; set; }
        [ForeignKey(nameof(CapFuelId))]
        [InverseProperty(nameof(RigCapFuels.Rigs))]
        public virtual RigCapFuels CapFuel { get; set; }
        [ForeignKey(nameof(CapLiquidMudId))]
        [InverseProperty(nameof(RigCapLiquidMuds.Rigs))]
        public virtual RigCapLiquidMuds CapLiquidMud { get; set; }
        [ForeignKey(nameof(CapPotableWaterId))]
        [InverseProperty(nameof(RigCapPotableWaters.Rigs))]
        public virtual RigCapPotableWaters CapPotableWater { get; set; }
        [ForeignKey(nameof(CapWindDerrickId))]
        [InverseProperty(nameof(RigCapWindDerricks.Rigs))]
        public virtual RigCapWindDerricks CapWindDerrick { get; set; }
        [ForeignKey(nameof(CentrifugeId))]
        [InverseProperty(nameof(RigCentrifuges.Rigs))]
        public virtual RigCentrifuges Centrifuge { get; set; }
        [ForeignKey(nameof(CommonDataRigCommonDataId))]
        [InverseProperty(nameof(RigCommonDatas.Rigs))]
        public virtual RigCommonDatas CommonDataRigCommonData { get; set; }
        [ForeignKey(nameof(DegasserId))]
        [InverseProperty(nameof(RigDegassers.Rigs))]
        public virtual RigDegassers Degasser { get; set; }
        [ForeignKey(nameof(HeaveMxId))]
        [InverseProperty(nameof(RigHeaveMxs.Rigs))]
        public virtual RigHeaveMxs HeaveMx { get; set; }
        [ForeignKey(nameof(HtDerrickId))]
        [InverseProperty(nameof(RigHtDerricks.Rigs))]
        public virtual RigHtDerricks HtDerrick { get; set; }
        [ForeignKey(nameof(HydrocycloneId))]
        [InverseProperty(nameof(RigHydrocyclones.Rigs))]
        public virtual RigHydrocyclones Hydrocyclone { get; set; }
        [ForeignKey(nameof(MotionCompensationMnId))]
        [InverseProperty(nameof(RigMotionCompensationMns.Rigs))]
        public virtual RigMotionCompensationMns MotionCompensationMn { get; set; }
        [ForeignKey(nameof(MotionCompensationMxId))]
        [InverseProperty(nameof(RigMotionCompensationMxs.Rigs))]
        public virtual RigMotionCompensationMxs MotionCompensationMx { get; set; }
        [ForeignKey(nameof(PitUid))]
        [InverseProperty(nameof(RigPits.Rigs))]
        public virtual RigPits PitU { get; set; }
        [ForeignKey(nameof(PowerDrawWorksId))]
        [InverseProperty(nameof(RigPowerDrawWork.Rigs))]
        public virtual RigPowerDrawWork PowerDrawWorks { get; set; }
        [ForeignKey(nameof(PumpUid))]
        [InverseProperty(nameof(RigPumps.Rigs))]
        public virtual RigPumps PumpU { get; set; }
        [ForeignKey(nameof(RatingBlockId))]
        [InverseProperty(nameof(RigRatingBlocks.Rigs))]
        public virtual RigRatingBlocks RatingBlock { get; set; }
        [ForeignKey(nameof(RatingDerrickId))]
        [InverseProperty(nameof(RigRatingDerricks.Rigs))]
        public virtual RigRatingDerricks RatingDerrick { get; set; }
        [ForeignKey(nameof(RatingDrawWorksId))]
        [InverseProperty(nameof(RigRatingDrawWork.Rigs))]
        public virtual RigRatingDrawWork RatingDrawWorks { get; set; }
        [ForeignKey(nameof(RatingDrillDepthId))]
        [InverseProperty(nameof(RigRatingDrillDepths.Rigs))]
        public virtual RigRatingDrillDepths RatingDrillDepth { get; set; }
        [ForeignKey(nameof(RatingHkldId))]
        [InverseProperty(nameof(RigRatingHklds.Rigs))]
        public virtual RigRatingHklds RatingHkld { get; set; }
        [ForeignKey(nameof(RatingHookId))]
        [InverseProperty(nameof(RigRatingHooks.Rigs))]
        public virtual RigRatingHooks RatingHook { get; set; }
        [ForeignKey(nameof(RatingRotSystemId))]
        [InverseProperty(nameof(RigRatingRotSystems.Rigs))]
        public virtual RigRatingRotSystems RatingRotSystem { get; set; }
        [ForeignKey(nameof(RatingSwivelId))]
        [InverseProperty(nameof(RigRatingSwivels.Rigs))]
        public virtual RigRatingSwivels RatingSwivel { get; set; }
        [ForeignKey(nameof(RatingTqRotSysId))]
        [InverseProperty(nameof(RigRatingTqRotSy.Rigs))]
        public virtual RigRatingTqRotSy RatingTqRotSys { get; set; }
        [ForeignKey(nameof(RatingWaterDepthId))]
        [InverseProperty(nameof(RigRatingWaterDepths.Rigs))]
        public virtual RigRatingWaterDepths RatingWaterDepth { get; set; }
        [ForeignKey(nameof(RiserAngleLimitId))]
        [InverseProperty(nameof(RigRiserAngleLimits.Rigs))]
        public virtual RigRiserAngleLimits RiserAngleLimit { get; set; }
        [ForeignKey(nameof(RotSizeOpeningId))]
        [InverseProperty(nameof(RigRotSizeOpenings.Rigs))]
        public virtual RigRotSizeOpenings RotSizeOpening { get; set; }
        [ForeignKey(nameof(ShakerUid))]
        [InverseProperty(nameof(RigShakers.Rigs))]
        public virtual RigShakers ShakerU { get; set; }
        [ForeignKey(nameof(SizeDrillLineId))]
        [InverseProperty(nameof(RigSizeDrillLines.Rigs))]
        public virtual RigSizeDrillLines SizeDrillLine { get; set; }
        [ForeignKey(nameof(StrokeMotionCompensationId))]
        [InverseProperty(nameof(RigStrokeMotionCompensations.Rigs))]
        public virtual RigStrokeMotionCompensations StrokeMotionCompensation { get; set; }
        [ForeignKey(nameof(SurfaceEquipmentId))]
        [InverseProperty(nameof(RigSurfaceEquipments.Rigs))]
        public virtual RigSurfaceEquipments SurfaceEquipment { get; set; }
        [ForeignKey(nameof(VarDeckLdMxId))]
        [InverseProperty(nameof(RigVarDeckLdMxs.Rigs))]
        public virtual RigVarDeckLdMxs VarDeckLdMx { get; set; }
        [ForeignKey(nameof(VdlStormId))]
        [InverseProperty(nameof(RigVdlStorms.Rigs))]
        public virtual RigVdlStorms VdlStorm { get; set; }
        [ForeignKey(nameof(WtBlockId))]
        [InverseProperty(nameof(RigWtBlocks.Rigs))]
        public virtual RigWtBlocks WtBlock { get; set; }
    }
}
