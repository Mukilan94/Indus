using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPumps
    {
        public OpsReportPumps()
        {
            OpsReportPumpOps = new HashSet<OpsReportPumpOps>();
            OpsReportScrs = new HashSet<OpsReportScrs>();
        }

        [Key]
        public int PumpId { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("Pump")]
        public virtual ICollection<OpsReportPumpOps> OpsReportPumpOps { get; set; }
        [InverseProperty("Pump")]
        public virtual ICollection<OpsReportScrs> OpsReportScrs { get; set; }
    }
}
