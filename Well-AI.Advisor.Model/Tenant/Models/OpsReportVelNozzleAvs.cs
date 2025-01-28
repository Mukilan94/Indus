using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVelNozzleAvs
    {
        public OpsReportVelNozzleAvs()
        {
            OpsReportDrillingParams = new HashSet<OpsReportDrillingParams>();
        }

        [Key]
        public int VelNozzleAvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VelNozzleAv")]
        public virtual ICollection<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
    }
}
