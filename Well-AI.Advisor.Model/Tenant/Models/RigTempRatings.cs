using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigTempRatings
    {
        public RigTempRatings()
        {
            RigDegassers = new HashSet<RigDegassers>();
        }

        [Key]
        public int TempRatingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempRating")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
    }
}
