using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportRecoverPc
    {
        public DrillReportRecoverPc()
        {
            DrillReportCoreInfo = new HashSet<DrillReportCoreInfo>();
        }

        [Key]
        public int RecoverPcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RecoverPc")]
        public virtual ICollection<DrillReportCoreInfo> DrillReportCoreInfo { get; set; }
    }
}
