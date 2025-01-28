using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMatrixCovs
    {
        public OpsReportMatrixCovs()
        {
            OpsReportTrajectoryStations = new HashSet<OpsReportTrajectoryStations>();
        }

        [Key]
        public int MatrixCovId { get; set; }
        [Column("VarianceNNId")]
        public int? VarianceNnid { get; set; }
        [Column("VarianceNEOpsReportsId")]
        public int? VarianceNeopsReportsId { get; set; }
        [Column("VarianceNVertId")]
        public int? VarianceNvertId { get; set; }
        [Column("VarianceEEId")]
        public int? VarianceEeid { get; set; }
        [Column("VarianceEVertId")]
        public int? VarianceEvertId { get; set; }
        public int? VarianceVertVertId { get; set; }
        [Column("BiasNId")]
        public int? BiasNid { get; set; }
        [Column("BiasEId")]
        public int? BiasEid { get; set; }
        public int? BiasVertId { get; set; }

        [ForeignKey(nameof(BiasEid))]
        [InverseProperty(nameof(OpsReportBiasEs.OpsReportMatrixCovs))]
        public virtual OpsReportBiasEs BiasE { get; set; }
        [ForeignKey(nameof(BiasNid))]
        [InverseProperty(nameof(OpsReportBiasNs.OpsReportMatrixCovs))]
        public virtual OpsReportBiasNs BiasN { get; set; }
        [ForeignKey(nameof(BiasVertId))]
        [InverseProperty(nameof(OpsReportBiasVerts.OpsReportMatrixCovs))]
        public virtual OpsReportBiasVerts BiasVert { get; set; }
        [ForeignKey(nameof(VarianceEeid))]
        [InverseProperty(nameof(OpsReportVarianceEes.OpsReportMatrixCovs))]
        public virtual OpsReportVarianceEes VarianceEe { get; set; }
        [ForeignKey(nameof(VarianceEvertId))]
        [InverseProperty(nameof(OpsReportVarianceEverts.OpsReportMatrixCovs))]
        public virtual OpsReportVarianceEverts VarianceEvert { get; set; }
        [ForeignKey(nameof(VarianceNeopsReportsId))]
        [InverseProperty(nameof(OpsReportVarianceNes.OpsReportMatrixCovs))]
        public virtual OpsReportVarianceNes VarianceNeopsReports { get; set; }
        [ForeignKey(nameof(VarianceNnid))]
        [InverseProperty(nameof(OpsReportVarianceNns.OpsReportMatrixCovs))]
        public virtual OpsReportVarianceNns VarianceNn { get; set; }
        [ForeignKey(nameof(VarianceNvertId))]
        [InverseProperty(nameof(OpsReportVarianceNverts.OpsReportMatrixCovs))]
        public virtual OpsReportVarianceNverts VarianceNvert { get; set; }
        [ForeignKey(nameof(VarianceVertVertId))]
        [InverseProperty(nameof(OpsReportVarianceVertVerts.OpsReportMatrixCovs))]
        public virtual OpsReportVarianceVertVerts VarianceVertVert { get; set; }
        [InverseProperty("MatrixCov")]
        public virtual ICollection<OpsReportTrajectoryStations> OpsReportTrajectoryStations { get; set; }
    }
}
