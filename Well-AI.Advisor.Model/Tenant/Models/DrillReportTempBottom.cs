using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportTempBottom
    {
        public DrillReportTempBottom()
        {
            DrillReportControlIncidentInfo = new HashSet<DrillReportControlIncidentInfo>();
        }

        [Key]
        public int TempBottomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempBottom")]
        public virtual ICollection<DrillReportControlIncidentInfo> DrillReportControlIncidentInfo { get; set; }
    }
}
