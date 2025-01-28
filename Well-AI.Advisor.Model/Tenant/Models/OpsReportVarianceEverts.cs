using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportVarianceEVerts")]
    public partial class OpsReportVarianceEverts
    {
        public OpsReportVarianceEverts()
        {
            OpsReportMatrixCovs = new HashSet<OpsReportMatrixCovs>();
        }

        [Key]
        [Column("VarianceEVertId")]
        public int VarianceEvertId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VarianceEvert")]
        public virtual ICollection<OpsReportMatrixCovs> OpsReportMatrixCovs { get; set; }
    }
}
