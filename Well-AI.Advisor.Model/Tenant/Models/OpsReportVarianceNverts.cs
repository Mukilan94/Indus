using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportVarianceNVerts")]
    public partial class OpsReportVarianceNverts
    {
        public OpsReportVarianceNverts()
        {
            OpsReportMatrixCovs = new HashSet<OpsReportMatrixCovs>();
        }

        [Key]
        [Column("VarianceNVertId")]
        public int VarianceNvertId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VarianceNvert")]
        public virtual ICollection<OpsReportMatrixCovs> OpsReportMatrixCovs { get; set; }
    }
}
