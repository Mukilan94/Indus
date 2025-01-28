using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPumpOutputs
    {
        public OpsReportPumpOutputs()
        {
            OpsReportPumpOps = new HashSet<OpsReportPumpOps>();
        }

        [Key]
        public int PumpOutputId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PumpOutput")]
        public virtual ICollection<OpsReportPumpOps> OpsReportPumpOps { get; set; }
    }
}
