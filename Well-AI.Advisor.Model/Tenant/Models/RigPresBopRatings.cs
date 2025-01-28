using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigPresBopRatings
    {
        public RigPresBopRatings()
        {
            RigBops = new HashSet<RigBops>();
        }

        [Key]
        public int PresBopRatingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresBopRating")]
        public virtual ICollection<RigBops> RigBops { get; set; }
    }
}
