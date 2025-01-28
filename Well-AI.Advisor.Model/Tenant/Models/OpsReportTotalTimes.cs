using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTotalTimes
    {
        public OpsReportTotalTimes()
        {
            OpsReportPersonnels = new HashSet<OpsReportPersonnels>();
        }

        [Key]
        public int TotalTimeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TotalTime")]
        public virtual ICollection<OpsReportPersonnels> OpsReportPersonnels { get; set; }
    }
}
