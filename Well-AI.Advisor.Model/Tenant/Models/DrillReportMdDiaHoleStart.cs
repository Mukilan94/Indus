using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportMdDiaHoleStart
    {
        public DrillReportMdDiaHoleStart()
        {
            DrillReportStatusInfo = new HashSet<DrillReportStatusInfo>();
        }

        [Key]
        public int MdDiaHoleStartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdDiaHoleStart")]
        public virtual ICollection<DrillReportStatusInfo> DrillReportStatusInfo { get; set; }
    }
}
