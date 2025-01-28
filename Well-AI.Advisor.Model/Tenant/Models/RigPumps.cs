using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigPumps
    {
        public RigPumps()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public string Uid { get; set; }
        public string Index { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        [Column("DTimInstall")]
        public string DtimInstall { get; set; }
        [Column("DTimRemove")]
        public string DtimRemove { get; set; }
        public string Owner { get; set; }
        public string TypePump { get; set; }
        public string NumCyl { get; set; }
        public int? OdRodId { get; set; }
        public int? IdLinerId { get; set; }
        public string PumpAction { get; set; }
        public int? EffId { get; set; }
        public int? LenStrokeId { get; set; }
        public int? PresMxId { get; set; }
        public int? PowHydMxId { get; set; }
        public int? SpmMxId { get; set; }
        public int? DisplacementId { get; set; }
        public int? PresDampId { get; set; }
        public int? VolDampId { get; set; }
        public int? PowMechMxId { get; set; }

        [ForeignKey(nameof(DisplacementId))]
        [InverseProperty(nameof(RigDisplacements.RigPumps))]
        public virtual RigDisplacements Displacement { get; set; }
        [ForeignKey(nameof(EffId))]
        [InverseProperty(nameof(RigEffs.RigPumps))]
        public virtual RigEffs Eff { get; set; }
        [ForeignKey(nameof(IdLinerId))]
        [InverseProperty(nameof(RigIdLiners.RigPumps))]
        public virtual RigIdLiners IdLiner { get; set; }
        [ForeignKey(nameof(LenStrokeId))]
        [InverseProperty(nameof(RigLenStrokes.RigPumps))]
        public virtual RigLenStrokes LenStroke { get; set; }
        [ForeignKey(nameof(OdRodId))]
        [InverseProperty(nameof(RigOdRods.RigPumps))]
        public virtual RigOdRods OdRod { get; set; }
        [ForeignKey(nameof(PowHydMxId))]
        [InverseProperty(nameof(RigPowHydMxs.RigPumps))]
        public virtual RigPowHydMxs PowHydMx { get; set; }
        [ForeignKey(nameof(PowMechMxId))]
        [InverseProperty(nameof(RigPowMechMxs.RigPumps))]
        public virtual RigPowMechMxs PowMechMx { get; set; }
        [ForeignKey(nameof(PresDampId))]
        [InverseProperty(nameof(RigPresDamps.RigPumps))]
        public virtual RigPresDamps PresDamp { get; set; }
        [ForeignKey(nameof(PresMxId))]
        [InverseProperty(nameof(RigPresMxs.RigPumps))]
        public virtual RigPresMxs PresMx { get; set; }
        [ForeignKey(nameof(SpmMxId))]
        [InverseProperty(nameof(RigSpmMxs.RigPumps))]
        public virtual RigSpmMxs SpmMx { get; set; }
        [ForeignKey(nameof(VolDampId))]
        [InverseProperty(nameof(RigVolDamps.RigPumps))]
        public virtual RigVolDamps VolDamp { get; set; }
        [InverseProperty("PumpU")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
