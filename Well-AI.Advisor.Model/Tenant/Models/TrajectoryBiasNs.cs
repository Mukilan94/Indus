using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryBiasNs
    {
        public TrajectoryBiasNs()
        {
            TrajectoryMatrixCovs = new HashSet<TrajectoryMatrixCovs>();
        }

        [Key]
        [Column("BiasNDtId")]
        public int BiasNdtId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BiasNdt")]
        public virtual ICollection<TrajectoryMatrixCovs> TrajectoryMatrixCovs { get; set; }
    }
}
