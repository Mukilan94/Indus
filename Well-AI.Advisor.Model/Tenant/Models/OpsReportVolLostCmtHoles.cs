using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVolLostCmtHoles
    {
        public OpsReportVolLostCmtHoles()
        {
            OpsReportMudLossess = new HashSet<OpsReportMudLossess>();
        }

        [Key]
        public int VolLostCmtHoleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolLostCmtHole")]
        public virtual ICollection<OpsReportMudLossess> OpsReportMudLossess { get; set; }
    }
}
