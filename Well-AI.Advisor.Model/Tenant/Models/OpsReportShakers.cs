using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportShakers
    {
        public OpsReportShakers()
        {
            OpsReportShakerOps = new HashSet<OpsReportShakerOps>();
        }

        [Key]
        public int ShakerId { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("Shaker")]
        public virtual ICollection<OpsReportShakerOps> OpsReportShakerOps { get; set; }
    }
}
