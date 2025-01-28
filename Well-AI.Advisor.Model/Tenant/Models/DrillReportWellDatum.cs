using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportWellDatum
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ElevationId { get; set; }
        public int? DrillReportId { get; set; }

        [ForeignKey(nameof(DrillReportId))]
        [InverseProperty(nameof(DrillReports.DrillReportWellDatum))]
        public virtual DrillReports DrillReport { get; set; }
        [ForeignKey(nameof(ElevationId))]
        [InverseProperty(nameof(DrillReportElevation.DrillReportWellDatum))]
        public virtual DrillReportElevation Elevation { get; set; }
    }
}
