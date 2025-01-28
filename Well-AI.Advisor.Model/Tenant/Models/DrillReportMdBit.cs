using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportMdBit
    {
        public DrillReportMdBit()
        {
            DrillReportControlIncidentInfo = new HashSet<DrillReportControlIncidentInfo>();
        }

        [Key]
        public int MdBitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdBit")]
        public virtual ICollection<DrillReportControlIncidentInfo> DrillReportControlIncidentInfo { get; set; }
    }
}
