using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryRateBuilds
    {
        public TrajectoryRateBuilds()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int RateBuildId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RateBuild")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
