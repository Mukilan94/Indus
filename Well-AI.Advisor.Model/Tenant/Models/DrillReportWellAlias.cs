using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportWellAlias
    {
        public DrillReportWellAlias()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int WellAliasId { get; set; }
        public string Name { get; set; }
        public string NamingSystem { get; set; }

        [InverseProperty("WellAlias")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
