using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryGravTran2AccelCors
    {
        public TrajectoryGravTran2AccelCors()
        {
            TrajectoryCorUseds = new HashSet<TrajectoryCorUseds>();
        }

        [Key]
        public int GravTran2AccelCorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GravTran2AccelCor")]
        public virtual ICollection<TrajectoryCorUseds> TrajectoryCorUseds { get; set; }
    }
}
