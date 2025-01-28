using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("DrillReportDensityHC")]
    public partial class DrillReportDensityHc
    {
        public DrillReportDensityHc()
        {
            DrillReportFormTestInfo = new HashSet<DrillReportFormTestInfo>();
        }

        [Key]
        [Column("DensityHCId")]
        public int DensityHcid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DensityHc")]
        public virtual ICollection<DrillReportFormTestInfo> DrillReportFormTestInfo { get; set; }
    }
}
