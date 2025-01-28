using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("TrajectoryVarianceEVerts")]
    public partial class TrajectoryVarianceEverts
    {
        public TrajectoryVarianceEverts()
        {
            TrajectoryMatrixCovs = new HashSet<TrajectoryMatrixCovs>();
        }

        [Key]
        [Column("VarianceEVertId")]
        public int VarianceEvertId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VarianceEvert")]
        public virtual ICollection<TrajectoryMatrixCovs> TrajectoryMatrixCovs { get; set; }
    }
}
