using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectorySagAziCors
    {
        public TrajectorySagAziCors()
        {
            TrajectoryCorUseds = new HashSet<TrajectoryCorUseds>();
        }

        [Key]
        public int SagAziCorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SagAziCor")]
        public virtual ICollection<TrajectoryCorUseds> TrajectoryCorUseds { get; set; }
    }
}
