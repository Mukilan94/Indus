using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularNozzle
    {
        [Key]
        public int NozzleId { get; set; }
        public string Index { get; set; }
        public int? DiaNozzleId { get; set; }
        public string TypeNozzle { get; set; }
        public int? LenId { get; set; }
        public string Uid { get; set; }
        public int? TubularComponentId { get; set; }

        [ForeignKey(nameof(DiaNozzleId))]
        [InverseProperty(nameof(TubularDiaNozzle.TubularNozzle))]
        public virtual TubularDiaNozzle DiaNozzle { get; set; }
        [ForeignKey(nameof(LenId))]
        [InverseProperty(nameof(TubularLen.TubularNozzle))]
        public virtual TubularLen Len { get; set; }
        [ForeignKey(nameof(TubularComponentId))]
        [InverseProperty("TubularNozzle")]
        public virtual TubularComponent TubularComponent { get; set; }
    }
}
