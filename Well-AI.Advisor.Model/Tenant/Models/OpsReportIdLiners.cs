using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportIdLiners
    {
        public OpsReportIdLiners()
        {
            OpsReportPumpOps = new HashSet<OpsReportPumpOps>();
        }

        [Key]
        public int IdLinerId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("IdLiner")]
        public virtual ICollection<OpsReportPumpOps> OpsReportPumpOps { get; set; }
    }
}
