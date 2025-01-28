using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportDiaCsgLast
    {
        public DrillReportDiaCsgLast()
        {
            DrillReportControlIncidentInfo = new HashSet<DrillReportControlIncidentInfo>();
            DrillReportStatusInfo = new HashSet<DrillReportStatusInfo>();
        }

        [Key]
        public int DiaCsgLastId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaCsgLast")]
        public virtual ICollection<DrillReportControlIncidentInfo> DrillReportControlIncidentInfo { get; set; }
        [InverseProperty("DiaCsgLast")]
        public virtual ICollection<DrillReportStatusInfo> DrillReportStatusInfo { get; set; }
    }
}
