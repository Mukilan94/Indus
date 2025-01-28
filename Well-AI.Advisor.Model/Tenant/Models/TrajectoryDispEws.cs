using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryDispEws
    {
        public TrajectoryDispEws()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int DispEwId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DispEw")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
