using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("DrillReportTempBHCT")]
    public partial class DrillReportTempBhct
    {
        public DrillReportTempBhct()
        {
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
        }

        [Key]
        [Column("TempBHCTId")]
        public int TempBhctid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempBhct")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
    }
}
