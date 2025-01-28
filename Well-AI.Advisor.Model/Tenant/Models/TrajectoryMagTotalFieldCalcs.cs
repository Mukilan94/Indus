using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMagTotalFieldCalcs
    {
        public TrajectoryMagTotalFieldCalcs()
        {
            TrajectoryValids = new HashSet<TrajectoryValids>();
        }

        [Key]
        public int MagTotalFieldCalcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagTotalFieldCalc")]
        public virtual ICollection<TrajectoryValids> TrajectoryValids { get; set; }
    }
}
