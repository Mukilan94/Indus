using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryAzis
    {
        public TrajectoryAzis()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int AziId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Azi")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
