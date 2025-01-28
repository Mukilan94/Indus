using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCostDayMuds
    {
        public OpsReportCostDayMuds()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int CostDayMudId { get; set; }
        public string Currency { get; set; }
        public string Text { get; set; }

        [InverseProperty("CostDayMud")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
