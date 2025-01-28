using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportDistDrill
    {
        public DrillReportDistDrill()
        {
            DrillReportStatusInfo = new HashSet<DrillReportStatusInfo>();
        }

        [Key]
        public int DistDrillId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DistDrill")]
        public virtual ICollection<DrillReportStatusInfo> DrillReportStatusInfo { get; set; }
    }
}
