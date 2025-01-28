using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMagAxialDrlstrCors
    {
        public TrajectoryMagAxialDrlstrCors()
        {
            TrajectoryCorUseds = new HashSet<TrajectoryCorUseds>();
        }

        [Key]
        public int MagAxialDrlstrCorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagAxialDrlstrCor")]
        public virtual ICollection<TrajectoryCorUseds> TrajectoryCorUseds { get; set; }
    }
}
