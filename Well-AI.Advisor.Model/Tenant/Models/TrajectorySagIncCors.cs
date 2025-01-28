using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectorySagIncCors
    {
        public TrajectorySagIncCors()
        {
            TrajectoryCorUseds = new HashSet<TrajectoryCorUseds>();
        }

        [Key]
        public int SagIncCorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SagIncCor")]
        public virtual ICollection<TrajectoryCorUseds> TrajectoryCorUseds { get; set; }
    }
}
