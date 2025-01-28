using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularHoleOpener
    {
        public TubularHoleOpener()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int HoleOpenerId { get; set; }
        public string TypeHoleOpener { get; set; }
        public string NumCutter { get; set; }
        public string Manufacturer { get; set; }
        public int? DiaHoleOpenerId { get; set; }

        [ForeignKey(nameof(DiaHoleOpenerId))]
        [InverseProperty(nameof(TubularDiaHoleOpener.TubularHoleOpener))]
        public virtual TubularDiaHoleOpener DiaHoleOpener { get; set; }
        [InverseProperty("HoleOpener")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
