using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMagDipAngleCalcs
    {
        public TrajectoryMagDipAngleCalcs()
        {
            TrajectoryValids = new HashSet<TrajectoryValids>();
        }

        [Key]
        public int MagDipAngleCalcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagDipAngleCalc")]
        public virtual ICollection<TrajectoryValids> TrajectoryValids { get; set; }
    }
}
