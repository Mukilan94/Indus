using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCostDays
    {
        public OpsReportCostDays()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int CostDayId { get; set; }
        public string Currency { get; set; }
        public string Text { get; set; }

        [InverseProperty("CostDay")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
