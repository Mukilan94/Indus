using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("TrajectoryVarianceNNs")]
    public partial class TrajectoryVarianceNns
    {
        public TrajectoryVarianceNns()
        {
            TrajectoryMatrixCovs = new HashSet<TrajectoryMatrixCovs>();
        }

        [Key]
        [Column("VarianceNNId")]
        public int VarianceNnid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VarianceNn")]
        public virtual ICollection<TrajectoryMatrixCovs> TrajectoryMatrixCovs { get; set; }
    }
}
