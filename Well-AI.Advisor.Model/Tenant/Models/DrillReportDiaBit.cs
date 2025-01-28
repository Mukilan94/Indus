using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportDiaBit
    {
        public DrillReportDiaBit()
        {
            DrillReportBitRecord = new HashSet<DrillReportBitRecord>();
            DrillReportControlIncidentInfo = new HashSet<DrillReportControlIncidentInfo>();
        }

        [Key]
        public int DiaBitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaBit")]
        public virtual ICollection<DrillReportBitRecord> DrillReportBitRecord { get; set; }
        [InverseProperty("DiaBit")]
        public virtual ICollection<DrillReportControlIncidentInfo> DrillReportControlIncidentInfo { get; set; }
    }
}
