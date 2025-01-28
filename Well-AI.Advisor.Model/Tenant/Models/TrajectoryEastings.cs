using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryEastings
    {
        public TrajectoryEastings()
        {
            TrajectoryLocations = new HashSet<TrajectoryLocations>();
        }

        [Key]
        public int EastingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Easting")]
        public virtual ICollection<TrajectoryLocations> TrajectoryLocations { get; set; }
    }
}
