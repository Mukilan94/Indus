using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularBend
    {
        public TubularBend()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int BendId { get; set; }
        public int? AngleId { get; set; }
        public int? DistBendBotId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AngleId))]
        [InverseProperty(nameof(TubularAngle.TubularBend))]
        public virtual TubularAngle Angle { get; set; }
        [ForeignKey(nameof(DistBendBotId))]
        [InverseProperty(nameof(TubularDistBendBot.TubularBend))]
        public virtual TubularDistBendBot DistBendBot { get; set; }
        [InverseProperty("Bend")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
