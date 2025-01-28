using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportMdTempTool
    {
        public DrillReportMdTempTool()
        {
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
        }

        [Key]
        public int MdTempToolId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdTempTool")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
    }
}
