using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularJar
    {
        public TubularJar()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int JarId { get; set; }
        public int? ForUpSetId { get; set; }
        public int? ForDownSetId { get; set; }
        public int? ForUpTripId { get; set; }
        public int? ForDownTripId { get; set; }
        public int? ForPmpOpenId { get; set; }
        public int? ForSealFricId { get; set; }
        public string TypeJar { get; set; }
        public string JarAction { get; set; }

        [ForeignKey(nameof(ForDownSetId))]
        [InverseProperty(nameof(TubularForDownSet.TubularJar))]
        public virtual TubularForDownSet ForDownSet { get; set; }
        [ForeignKey(nameof(ForDownTripId))]
        [InverseProperty(nameof(TubularForDownTrip.TubularJar))]
        public virtual TubularForDownTrip ForDownTrip { get; set; }
        [ForeignKey(nameof(ForPmpOpenId))]
        [InverseProperty(nameof(TubularForPmpOpen.TubularJar))]
        public virtual TubularForPmpOpen ForPmpOpen { get; set; }
        [ForeignKey(nameof(ForSealFricId))]
        [InverseProperty(nameof(TubularForSealFric.TubularJar))]
        public virtual TubularForSealFric ForSealFric { get; set; }
        [ForeignKey(nameof(ForUpSetId))]
        [InverseProperty(nameof(TubularForUpSet.TubularJar))]
        public virtual TubularForUpSet ForUpSet { get; set; }
        [ForeignKey(nameof(ForUpTripId))]
        [InverseProperty(nameof(TubularForUpTrip.TubularJar))]
        public virtual TubularForUpTrip ForUpTrip { get; set; }
        [InverseProperty("Jar")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
