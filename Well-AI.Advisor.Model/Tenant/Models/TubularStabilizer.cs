using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularStabilizer
    {
        public TubularStabilizer()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int StabilizerId { get; set; }
        public int? LenBladeId { get; set; }
        public int? OdBladeMxId { get; set; }
        public int? OdBladeMnId { get; set; }
        public int? DistBladeBotId { get; set; }
        public string ShapeBlade { get; set; }
        public string FactFric { get; set; }
        public string TypeBlade { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(DistBladeBotId))]
        [InverseProperty(nameof(TubularDistBladeBot.TubularStabilizer))]
        public virtual TubularDistBladeBot DistBladeBot { get; set; }
        [ForeignKey(nameof(LenBladeId))]
        [InverseProperty(nameof(TubularLenBlade.TubularStabilizer))]
        public virtual TubularLenBlade LenBlade { get; set; }
        [ForeignKey(nameof(OdBladeMnId))]
        [InverseProperty(nameof(TubularOdBladeMn.TubularStabilizer))]
        public virtual TubularOdBladeMn OdBladeMn { get; set; }
        [ForeignKey(nameof(OdBladeMxId))]
        [InverseProperty(nameof(TubularOdBladeMx.TubularStabilizer))]
        public virtual TubularOdBladeMx OdBladeMx { get; set; }
        [InverseProperty("Stabilizer")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
