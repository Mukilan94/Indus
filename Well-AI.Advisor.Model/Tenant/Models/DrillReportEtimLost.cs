using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("DrillReportETimLost")]
    public partial class DrillReportEtimLost
    {
        public DrillReportEtimLost()
        {
            DrillReportControlIncidentInfo = new HashSet<DrillReportControlIncidentInfo>();
        }

        [Key]
        [Column("ETimLostId")]
        public int EtimLostId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimLost")]
        public virtual ICollection<DrillReportControlIncidentInfo> DrillReportControlIncidentInfo { get; set; }
    }
}
