using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigPits
    {
        public RigPits()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public string Uid { get; set; }
        public string Index { get; set; }
        [Column("DTimInstall")]
        public string DtimInstall { get; set; }
        [Column("DTimRemove")]
        public string DtimRemove { get; set; }
        public int? CapMxId { get; set; }
        public string Owner { get; set; }
        public string TypePit { get; set; }
        public string IsActive { get; set; }

        [ForeignKey(nameof(CapMxId))]
        [InverseProperty(nameof(RigCapMxs.RigPits))]
        public virtual RigCapMxs CapMx { get; set; }
        [InverseProperty("PitU")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
