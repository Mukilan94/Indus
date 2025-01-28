using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryVertSects
    {
        public TrajectoryVertSects()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int VertSectId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VertSect")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
