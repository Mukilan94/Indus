using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryGravTotalUncerts
    {
        public TrajectoryGravTotalUncerts()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int GravTotalUncertId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GravTotalUncert")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
