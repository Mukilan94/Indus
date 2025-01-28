using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportRigs
    {
        public OpsReportRigs()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int OpsReportRigId { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("RigOpsReportRig")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
