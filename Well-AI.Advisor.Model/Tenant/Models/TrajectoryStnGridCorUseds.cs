using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryStnGridCorUseds
    {
        public TrajectoryStnGridCorUseds()
        {
            TrajectoryCorUseds = new HashSet<TrajectoryCorUseds>();
        }

        [Key]
        public int StnGridCorUsedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StnGridCorUsed")]
        public virtual ICollection<TrajectoryCorUseds> TrajectoryCorUseds { get; set; }
    }
}
