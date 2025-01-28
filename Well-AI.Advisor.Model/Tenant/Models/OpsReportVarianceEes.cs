using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportVarianceEEs")]
    public partial class OpsReportVarianceEes
    {
        public OpsReportVarianceEes()
        {
            OpsReportMatrixCovs = new HashSet<OpsReportMatrixCovs>();
        }

        [Key]
        [Column("VarianceEEId")]
        public int VarianceEeid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VarianceEe")]
        public virtual ICollection<OpsReportMatrixCovs> OpsReportMatrixCovs { get; set; }
    }
}
