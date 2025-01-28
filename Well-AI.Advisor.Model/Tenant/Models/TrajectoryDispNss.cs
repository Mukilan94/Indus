using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryDispNss
    {
        public TrajectoryDispNss()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int DispNsId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DispNs")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
