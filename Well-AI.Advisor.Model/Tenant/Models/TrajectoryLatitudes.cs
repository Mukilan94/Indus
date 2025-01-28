using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryLatitudes
    {
        public TrajectoryLatitudes()
        {
            TrajectoryLocations = new HashSet<TrajectoryLocations>();
        }

        [Key]
        public int LatitudeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Latitude")]
        public virtual ICollection<TrajectoryLocations> TrajectoryLocations { get; set; }
    }
}
