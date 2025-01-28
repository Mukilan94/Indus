using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportRateStrokes
    {
        public OpsReportRateStrokes()
        {
            OpsReportPumpOps = new HashSet<OpsReportPumpOps>();
            OpsReportScrs = new HashSet<OpsReportScrs>();
        }

        [Key]
        public int RateStrokeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RateStroke")]
        public virtual ICollection<OpsReportPumpOps> OpsReportPumpOps { get; set; }
        [InverseProperty("RateStroke")]
        public virtual ICollection<OpsReportScrs> OpsReportScrs { get; set; }
    }
}
