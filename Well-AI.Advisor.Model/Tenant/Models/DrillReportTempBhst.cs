using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("DrillReportTempBHST")]
    public partial class DrillReportTempBhst
    {
        public DrillReportTempBhst()
        {
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
        }

        [Key]
        [Column("TempBHSTId")]
        public int TempBhstid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempBhst")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
    }
}
