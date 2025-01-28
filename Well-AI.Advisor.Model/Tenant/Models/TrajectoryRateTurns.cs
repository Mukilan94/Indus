using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryRateTurns
    {
        public TrajectoryRateTurns()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int RateTurnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RateTurn")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
