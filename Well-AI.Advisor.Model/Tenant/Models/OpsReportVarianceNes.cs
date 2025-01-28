using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportVarianceNEs")]
    public partial class OpsReportVarianceNes
    {
        public OpsReportVarianceNes()
        {
            OpsReportMatrixCovs = new HashSet<OpsReportMatrixCovs>();
        }

        [Key]
        public int OpsReportsId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VarianceNeopsReports")]
        public virtual ICollection<OpsReportMatrixCovs> OpsReportMatrixCovs { get; set; }
    }
}
