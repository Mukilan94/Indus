using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigPresRatings
    {
        public RigPresRatings()
        {
            RigDegassers = new HashSet<RigDegassers>();
            RigSurfaceEquipments = new HashSet<RigSurfaceEquipments>();
        }

        [Key]
        public int PresRatingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresRating")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
        [InverseProperty("PresRating")]
        public virtual ICollection<RigSurfaceEquipments> RigSurfaceEquipments { get; set; }
    }
}
