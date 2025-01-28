using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigPresAccOpRatings
    {
        public RigPresAccOpRatings()
        {
            RigBops = new HashSet<RigBops>();
        }

        [Key]
        public int PresAccOpRatingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresAccOpRating")]
        public virtual ICollection<RigBops> RigBops { get; set; }
    }
}
