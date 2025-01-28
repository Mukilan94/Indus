using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("TrajectoryVarianceNEs")]
    public partial class TrajectoryVarianceNes
    {
        public TrajectoryVarianceNes()
        {
            TrajectoryMatrixCovs = new HashSet<TrajectoryMatrixCovs>();
        }

        [Key]
        [Column("VarianceNEId")]
        public int VarianceNeid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VarianceNe")]
        public virtual ICollection<TrajectoryMatrixCovs> TrajectoryMatrixCovs { get; set; }
    }
}
