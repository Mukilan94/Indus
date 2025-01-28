using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportLenStrokes
    {
        public OpsReportLenStrokes()
        {
            OpsReportPumpOps = new HashSet<OpsReportPumpOps>();
        }

        [Key]
        public int LenStrokeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LenStroke")]
        public virtual ICollection<OpsReportPumpOps> OpsReportPumpOps { get; set; }
    }
}
