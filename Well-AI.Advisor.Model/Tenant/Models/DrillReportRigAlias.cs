using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportRigAlias
    {
        [Key]
        public string Uid { get; set; }
        public int RigAliasId { get; set; }
        public string Name { get; set; }
        public string NamingSystem { get; set; }
        public int? DrillReportWellboreInfoWellboreInfoId { get; set; }

        [ForeignKey(nameof(DrillReportWellboreInfoWellboreInfoId))]
        [InverseProperty(nameof(DrillReportWellboreInfo.DrillReportRigAlias))]
        public virtual DrillReportWellboreInfo DrillReportWellboreInfoWellboreInfo { get; set; }
    }
}
