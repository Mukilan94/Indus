using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMatrixCovs
    {
        public TrajectoryMatrixCovs()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int MatrixCovId { get; set; }
        [Column("VarianceNNId")]
        public int? VarianceNnid { get; set; }
        [Column("VarianceNEId")]
        public int? VarianceNeid { get; set; }
        [Column("VarianceNVertId")]
        public int? VarianceNvertId { get; set; }
        [Column("VarianceEEId")]
        public int? VarianceEeid { get; set; }
        [Column("VarianceEVertId")]
        public int? VarianceEvertId { get; set; }
        public int? VarianceVertVertId { get; set; }
        [Column("BiasNDtId")]
        public int? BiasNdtId { get; set; }
        [Column("BiasEId")]
        public int? BiasEid { get; set; }
        public int? BiasVertId { get; set; }

        [ForeignKey(nameof(BiasEid))]
        [InverseProperty(nameof(TrajectoryBiasEs.TrajectoryMatrixCovs))]
        public virtual TrajectoryBiasEs BiasE { get; set; }
        [ForeignKey(nameof(BiasNdtId))]
        [InverseProperty(nameof(TrajectoryBiasNs.TrajectoryMatrixCovs))]
        public virtual TrajectoryBiasNs BiasNdt { get; set; }
        [ForeignKey(nameof(BiasVertId))]
        [InverseProperty(nameof(TrajectoryBiasVerts.TrajectoryMatrixCovs))]
        public virtual TrajectoryBiasVerts BiasVert { get; set; }
        [ForeignKey(nameof(VarianceEeid))]
        [InverseProperty(nameof(TrajectoryVarianceEes.TrajectoryMatrixCovs))]
        public virtual TrajectoryVarianceEes VarianceEe { get; set; }
        [ForeignKey(nameof(VarianceEvertId))]
        [InverseProperty(nameof(TrajectoryVarianceEverts.TrajectoryMatrixCovs))]
        public virtual TrajectoryVarianceEverts VarianceEvert { get; set; }
        [ForeignKey(nameof(VarianceNeid))]
        [InverseProperty(nameof(TrajectoryVarianceNes.TrajectoryMatrixCovs))]
        public virtual TrajectoryVarianceNes VarianceNe { get; set; }
        [ForeignKey(nameof(VarianceNnid))]
        [InverseProperty(nameof(TrajectoryVarianceNns.TrajectoryMatrixCovs))]
        public virtual TrajectoryVarianceNns VarianceNn { get; set; }
        [ForeignKey(nameof(VarianceNvertId))]
        [InverseProperty(nameof(TrajectoryVarianceNverts.TrajectoryMatrixCovs))]
        public virtual TrajectoryVarianceNverts VarianceNvert { get; set; }
        [ForeignKey(nameof(VarianceVertVertId))]
        [InverseProperty(nameof(TrajectoryVarianceVertVerts.TrajectoryMatrixCovs))]
        public virtual TrajectoryVarianceVertVerts VarianceVertVert { get; set; }
        [InverseProperty("MatrixCov")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
