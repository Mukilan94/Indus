using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("TrajectoryVarianceEEs")]
    public partial class TrajectoryVarianceEes
    {
        public TrajectoryVarianceEes()
        {
            TrajectoryMatrixCovs = new HashSet<TrajectoryMatrixCovs>();
        }

        [Key]
        [Column("VarianceEEId")]
        public int VarianceEeid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VarianceEe")]
        public virtual ICollection<TrajectoryMatrixCovs> TrajectoryMatrixCovs { get; set; }
    }
}
