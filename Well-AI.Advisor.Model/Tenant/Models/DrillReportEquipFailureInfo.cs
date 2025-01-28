using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportEquipFailureInfo
    {
        public DrillReportEquipFailureInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int EquipFailureInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdId { get; set; }
        public string EquipClass { get; set; }
        [Column("ETimMissProductionId")]
        public int? EtimMissProductionId { get; set; }
        [Column("DTimRepair")]
        public string DtimRepair { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(EtimMissProductionId))]
        [InverseProperty(nameof(DrillReportEtimMissProduction.DrillReportEquipFailureInfo))]
        public virtual DrillReportEtimMissProduction EtimMissProduction { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(DrillReportMd.DrillReportEquipFailureInfo))]
        public virtual DrillReportMd Md { get; set; }
        [InverseProperty("EquipFailureInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
