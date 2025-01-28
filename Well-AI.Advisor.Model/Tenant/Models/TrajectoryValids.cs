using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryValids
    {
        public TrajectoryValids()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int ValidId { get; set; }
        public int? MagTotalFieldCalcId { get; set; }
        public int? MagDipAngleCalcId { get; set; }
        public int? GravTotalFieldCalcId { get; set; }

        [ForeignKey(nameof(GravTotalFieldCalcId))]
        [InverseProperty(nameof(TrajectoryGravTotalFieldCalcs.TrajectoryValids))]
        public virtual TrajectoryGravTotalFieldCalcs GravTotalFieldCalc { get; set; }
        [ForeignKey(nameof(MagDipAngleCalcId))]
        [InverseProperty(nameof(TrajectoryMagDipAngleCalcs.TrajectoryValids))]
        public virtual TrajectoryMagDipAngleCalcs MagDipAngleCalc { get; set; }
        [ForeignKey(nameof(MagTotalFieldCalcId))]
        [InverseProperty(nameof(TrajectoryMagTotalFieldCalcs.TrajectoryValids))]
        public virtual TrajectoryMagTotalFieldCalcs MagTotalFieldCalc { get; set; }
        [InverseProperty("Valid")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
