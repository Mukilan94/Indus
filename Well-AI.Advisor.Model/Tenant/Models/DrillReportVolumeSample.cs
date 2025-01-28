using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportVolumeSample
    {
        public DrillReportVolumeSample()
        {
            DrillReportFormTestInfo = new HashSet<DrillReportFormTestInfo>();
        }

        [Key]
        public int VolumeSampleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolumeSample")]
        public virtual ICollection<DrillReportFormTestInfo> DrillReportFormTestInfo { get; set; }
    }
}
