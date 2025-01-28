using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportPresMaxChoke
    {
        public DrillReportPresMaxChoke()
        {
            DrillReportControlIncidentInfo = new HashSet<DrillReportControlIncidentInfo>();
        }

        [Key]
        public int PresMaxChokeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresMaxChoke")]
        public virtual ICollection<DrillReportControlIncidentInfo> DrillReportControlIncidentInfo { get; set; }
    }
}
