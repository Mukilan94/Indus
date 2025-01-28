using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportWellboreInfo
    {
        public DrillReportWellboreInfo()
        {
            DrillReportRigAlias = new HashSet<DrillReportRigAlias>();
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int WellboreInfoId { get; set; }
        [Column("DTimSpud")]
        public string DtimSpud { get; set; }
        [Column("DTimPreSpud")]
        public string DtimPreSpud { get; set; }
        public string Operator { get; set; }

        [InverseProperty("DrillReportWellboreInfoWellboreInfo")]
        public virtual ICollection<DrillReportRigAlias> DrillReportRigAlias { get; set; }
        [InverseProperty("WellboreInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
