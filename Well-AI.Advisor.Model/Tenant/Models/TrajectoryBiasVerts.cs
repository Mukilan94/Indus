using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryBiasVerts
    {
        public TrajectoryBiasVerts()
        {
            TrajectoryMatrixCovs = new HashSet<TrajectoryMatrixCovs>();
        }

        [Key]
        public int BiasVertId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BiasVert")]
        public virtual ICollection<TrajectoryMatrixCovs> TrajectoryMatrixCovs { get; set; }
    }
}
