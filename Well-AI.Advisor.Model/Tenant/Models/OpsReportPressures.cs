using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPressures
    {
        public OpsReportPressures()
        {
            OpsReportPumpOps = new HashSet<OpsReportPumpOps>();
        }

        [Key]
        public int PressureId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Pressure")]
        public virtual ICollection<OpsReportPumpOps> OpsReportPumpOps { get; set; }
    }
}
