using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportWellboreAlias
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public string NamingSystem { get; set; }
        public int? DrillReportId { get; set; }

        [ForeignKey(nameof(DrillReportId))]
        [InverseProperty(nameof(DrillReports.DrillReportWellboreAlias))]
        public virtual DrillReports DrillReport { get; set; }
    }
}
