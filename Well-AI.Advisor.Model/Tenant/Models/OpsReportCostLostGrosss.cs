using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCostLostGrosss
    {
        public OpsReportCostLostGrosss()
        {
            OpsReportIncidents = new HashSet<OpsReportIncidents>();
        }

        [Key]
        public int CostLostGrossId { get; set; }
        public string Currency { get; set; }
        public string Text { get; set; }

        [InverseProperty("CostLostGross")]
        public virtual ICollection<OpsReportIncidents> OpsReportIncidents { get; set; }
    }
}
