using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportExtendedReport
    {
        public DrillReportExtendedReport()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int ExtendedReportId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string Text { get; set; }

        [InverseProperty("ExtendedReport")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
