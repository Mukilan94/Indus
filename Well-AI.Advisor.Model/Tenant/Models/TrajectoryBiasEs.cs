using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryBiasEs
    {
        public TrajectoryBiasEs()
        {
            TrajectoryMatrixCovs = new HashSet<TrajectoryMatrixCovs>();
        }

        [Key]
        [Column("BiasEId")]
        public int BiasEid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BiasE")]
        public virtual ICollection<TrajectoryMatrixCovs> TrajectoryMatrixCovs { get; set; }
    }
}
