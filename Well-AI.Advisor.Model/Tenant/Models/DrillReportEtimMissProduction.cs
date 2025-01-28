using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("DrillReportETimMissProduction")]
    public partial class DrillReportEtimMissProduction
    {
        public DrillReportEtimMissProduction()
        {
            DrillReportEquipFailureInfo = new HashSet<DrillReportEquipFailureInfo>();
        }

        [Key]
        [Column("ETimMissProductionId")]
        public int EtimMissProductionId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimMissProduction")]
        public virtual ICollection<DrillReportEquipFailureInfo> DrillReportEquipFailureInfo { get; set; }
    }
}
