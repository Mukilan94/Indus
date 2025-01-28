using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigDegassers
    {
        public RigDegassers()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public int DegasserId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        [Column("DTimInstall")]
        public string DtimInstall { get; set; }
        [Column("DTimRemove")]
        public string DtimRemove { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public int? HeightId { get; set; }
        public int? LenId { get; set; }
        public int? IdUniqueId { get; set; }
        public int? CapFlowId { get; set; }
        public int? AreaSeparatorFlowId { get; set; }
        public int? HtMudSealId { get; set; }
        public int? IdInletId { get; set; }
        public int? IdVentLineId { get; set; }
        public int? LenVentLineId { get; set; }
        public int? CapGasSepId { get; set; }
        public int? CapBlowdownId { get; set; }
        public int? PresRatingId { get; set; }
        public int? TempRatingId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AreaSeparatorFlowId))]
        [InverseProperty(nameof(RigAreaSeparatorFlows.RigDegassers))]
        public virtual RigAreaSeparatorFlows AreaSeparatorFlow { get; set; }
        [ForeignKey(nameof(CapBlowdownId))]
        [InverseProperty(nameof(RigCapBlowdowns.RigDegassers))]
        public virtual RigCapBlowdowns CapBlowdown { get; set; }
        [ForeignKey(nameof(CapFlowId))]
        [InverseProperty(nameof(RigCapFlows.RigDegassers))]
        public virtual RigCapFlows CapFlow { get; set; }
        [ForeignKey(nameof(CapGasSepId))]
        [InverseProperty(nameof(RigCapGasSeps.RigDegassers))]
        public virtual RigCapGasSeps CapGasSep { get; set; }
        [ForeignKey(nameof(HeightId))]
        [InverseProperty(nameof(RigHeights.RigDegassers))]
        public virtual RigHeights Height { get; set; }
        [ForeignKey(nameof(HtMudSealId))]
        [InverseProperty(nameof(RigHtMudSeals.RigDegassers))]
        public virtual RigHtMudSeals HtMudSeal { get; set; }
        [ForeignKey(nameof(IdInletId))]
        [InverseProperty(nameof(RigIdInlets.RigDegassers))]
        public virtual RigIdInlets IdInlet { get; set; }
        [ForeignKey(nameof(IdUniqueId))]
        [InverseProperty(nameof(RigIds.RigDegassers))]
        public virtual RigIds IdUnique { get; set; }
        [ForeignKey(nameof(IdVentLineId))]
        [InverseProperty(nameof(RigIdVentLines.RigDegassers))]
        public virtual RigIdVentLines IdVentLine { get; set; }
        [ForeignKey(nameof(LenId))]
        [InverseProperty(nameof(RigLens.RigDegassers))]
        public virtual RigLens Len { get; set; }
        [ForeignKey(nameof(LenVentLineId))]
        [InverseProperty(nameof(RigLenVentLines.RigDegassers))]
        public virtual RigLenVentLines LenVentLine { get; set; }
        [ForeignKey(nameof(PresRatingId))]
        [InverseProperty(nameof(RigPresRatings.RigDegassers))]
        public virtual RigPresRatings PresRating { get; set; }
        [ForeignKey(nameof(TempRatingId))]
        [InverseProperty(nameof(RigTempRatings.RigDegassers))]
        public virtual RigTempRatings TempRating { get; set; }
        [InverseProperty("Degasser")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
