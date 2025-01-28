using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportVolGasTotal
    {
        public DrillReportVolGasTotal()
        {
            DrillReportWellTestInfo = new HashSet<DrillReportWellTestInfo>();
        }

        [Key]
        public int VolGasTotalId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolGasTotal")]
        public virtual ICollection<DrillReportWellTestInfo> DrillReportWellTestInfo { get; set; }
    }
}
