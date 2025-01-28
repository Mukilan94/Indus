using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigBops
    {
        public RigBops()
        {
            RigBopComponents = new HashSet<RigBopComponents>();
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public int BopId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        [Column("DTimInstall")]
        public string DtimInstall { get; set; }
        [Column("DTimRemove")]
        public string DtimRemove { get; set; }
        public string TypeConnectionBop { get; set; }
        public int? SizeConnectionBopId { get; set; }
        public int? PresBopRatingId { get; set; }
        public int? SizeBopSysId { get; set; }
        public string RotBop { get; set; }
        public int? IdBoosterLineId { get; set; }
        public int? OdBoosterLineId { get; set; }
        public int? LenBoosterLineId { get; set; }
        public int? IdSurfLineId { get; set; }
        public int? OdSurfLineId { get; set; }
        public int? LenSurfLineId { get; set; }
        public int? IdChkLineId { get; set; }
        public int? OdChkLineId { get; set; }
        public int? LenChkLineId { get; set; }
        public int? IdKillLineId { get; set; }
        public int? OdKillLineId { get; set; }
        public int? LenKillLineId { get; set; }
        public string TypeDiverter { get; set; }
        public int? DiaDiverterId { get; set; }
        public int? PresWorkDiverterId { get; set; }
        public string Accumulator { get; set; }
        public int? CapAccFluidId { get; set; }
        public int? PresAccPreChargeId { get; set; }
        public int? VolAccPreChargeId { get; set; }
        public int? PresAccOpRatingId { get; set; }
        public string TypeControlManifold { get; set; }
        public string DescControlManifold { get; set; }
        public string TypeChokeManifold { get; set; }
        public int? PresChokeManifoldId { get; set; }

        [ForeignKey(nameof(CapAccFluidId))]
        [InverseProperty(nameof(RigCapAccFluids.RigBops))]
        public virtual RigCapAccFluids CapAccFluid { get; set; }
        [ForeignKey(nameof(DiaDiverterId))]
        [InverseProperty(nameof(RigDiaDiverters.RigBops))]
        public virtual RigDiaDiverters DiaDiverter { get; set; }
        [ForeignKey(nameof(IdBoosterLineId))]
        [InverseProperty(nameof(RigIdBoosterLines.RigBops))]
        public virtual RigIdBoosterLines IdBoosterLine { get; set; }
        [ForeignKey(nameof(IdChkLineId))]
        [InverseProperty(nameof(RigIdChkLines.RigBops))]
        public virtual RigIdChkLines IdChkLine { get; set; }
        [ForeignKey(nameof(IdKillLineId))]
        [InverseProperty(nameof(RigIdKillLines.RigBops))]
        public virtual RigIdKillLines IdKillLine { get; set; }
        [ForeignKey(nameof(IdSurfLineId))]
        [InverseProperty(nameof(RigIdSurfLines.RigBops))]
        public virtual RigIdSurfLines IdSurfLine { get; set; }
        [ForeignKey(nameof(LenBoosterLineId))]
        [InverseProperty(nameof(RigLenBoosterLines.RigBops))]
        public virtual RigLenBoosterLines LenBoosterLine { get; set; }
        [ForeignKey(nameof(LenChkLineId))]
        [InverseProperty(nameof(RigLenChkLines.RigBops))]
        public virtual RigLenChkLines LenChkLine { get; set; }
        [ForeignKey(nameof(LenKillLineId))]
        [InverseProperty(nameof(RigLenKillLines.RigBops))]
        public virtual RigLenKillLines LenKillLine { get; set; }
        [ForeignKey(nameof(LenSurfLineId))]
        [InverseProperty(nameof(RigLenSurfLines.RigBops))]
        public virtual RigLenSurfLines LenSurfLine { get; set; }
        [ForeignKey(nameof(OdBoosterLineId))]
        [InverseProperty(nameof(RigOdBoosterLines.RigBops))]
        public virtual RigOdBoosterLines OdBoosterLine { get; set; }
        [ForeignKey(nameof(OdChkLineId))]
        [InverseProperty(nameof(RigOdChkLines.RigBops))]
        public virtual RigOdChkLines OdChkLine { get; set; }
        [ForeignKey(nameof(OdKillLineId))]
        [InverseProperty(nameof(RigOdKillLines.RigBops))]
        public virtual RigOdKillLines OdKillLine { get; set; }
        [ForeignKey(nameof(OdSurfLineId))]
        [InverseProperty(nameof(RigOdSurfLines.RigBops))]
        public virtual RigOdSurfLines OdSurfLine { get; set; }
        [ForeignKey(nameof(PresAccOpRatingId))]
        [InverseProperty(nameof(RigPresAccOpRatings.RigBops))]
        public virtual RigPresAccOpRatings PresAccOpRating { get; set; }
        [ForeignKey(nameof(PresAccPreChargeId))]
        [InverseProperty(nameof(RigPresAccPreCharges.RigBops))]
        public virtual RigPresAccPreCharges PresAccPreCharge { get; set; }
        [ForeignKey(nameof(PresBopRatingId))]
        [InverseProperty(nameof(RigPresBopRatings.RigBops))]
        public virtual RigPresBopRatings PresBopRating { get; set; }
        [ForeignKey(nameof(PresChokeManifoldId))]
        [InverseProperty(nameof(RigPresChokeManifolds.RigBops))]
        public virtual RigPresChokeManifolds PresChokeManifold { get; set; }
        [ForeignKey(nameof(PresWorkDiverterId))]
        [InverseProperty(nameof(RigPresWorkDiverters.RigBops))]
        public virtual RigPresWorkDiverters PresWorkDiverter { get; set; }
        [ForeignKey(nameof(SizeBopSysId))]
        [InverseProperty(nameof(RigSizeBopSyss.RigBops))]
        public virtual RigSizeBopSyss SizeBopSys { get; set; }
        [ForeignKey(nameof(SizeConnectionBopId))]
        [InverseProperty(nameof(RigSizeConnectionBops.RigBops))]
        public virtual RigSizeConnectionBops SizeConnectionBop { get; set; }
        [ForeignKey(nameof(VolAccPreChargeId))]
        [InverseProperty(nameof(RigVolAccPreCharges.RigBops))]
        public virtual RigVolAccPreCharges VolAccPreCharge { get; set; }
        [InverseProperty("RigBopBop")]
        public virtual ICollection<RigBopComponents> RigBopComponents { get; set; }
        [InverseProperty("Bop")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
