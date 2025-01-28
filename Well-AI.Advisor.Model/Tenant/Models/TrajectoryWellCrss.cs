using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("TrajectoryWellCRSs")]
    public partial class TrajectoryWellCrss
    {
        public TrajectoryWellCrss()
        {
            TrajectoryLocations = new HashSet<TrajectoryLocations>();
        }

        [Key]
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("WellCrsuidRefNavigation")]
        public virtual ICollection<TrajectoryLocations> TrajectoryLocations { get; set; }
    }
}
