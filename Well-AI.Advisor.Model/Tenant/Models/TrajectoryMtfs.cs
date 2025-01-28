using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMtfs
    {
        public TrajectoryMtfs()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int MtfId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Mtf")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
