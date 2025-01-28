using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportIpent
    {
        public DrillReportIpent()
        {
            DrillReportGasReadingInfo = new HashSet<DrillReportGasReadingInfo>();
        }

        [Key]
        public int IpentId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Ipent")]
        public virtual ICollection<DrillReportGasReadingInfo> DrillReportGasReadingInfo { get; set; }
    }
}
