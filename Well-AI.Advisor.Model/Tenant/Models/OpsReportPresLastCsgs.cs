using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPresLastCsgs
    {
        public OpsReportPresLastCsgs()
        {
            OpsReportHses = new HashSet<OpsReportHses>();
        }

        [Key]
        public int PresLastCsgId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresLastCsg")]
        public virtual ICollection<OpsReportHses> OpsReportHses { get; set; }
    }
}
