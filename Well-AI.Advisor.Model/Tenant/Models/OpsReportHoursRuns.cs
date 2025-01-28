using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportHoursRuns
    {
        public OpsReportHoursRuns()
        {
            OpsReportShakerOps = new HashSet<OpsReportShakerOps>();
        }

        [Key]
        public int HoursRunId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("HoursRun")]
        public virtual ICollection<OpsReportShakerOps> OpsReportShakerOps { get; set; }
    }
}
