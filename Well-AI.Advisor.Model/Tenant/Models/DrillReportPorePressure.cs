using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportPorePressure
    {
        public DrillReportPorePressure()
        {
            DrillReportControlIncidentInfo = new HashSet<DrillReportControlIncidentInfo>();
        }

        [Key]
        public int PorePressureId { get; set; }
        public string ReadingKind { get; set; }
        public int? EquivalentMudWeightId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public string Uid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }
        public int? DrillReportId { get; set; }

        [ForeignKey(nameof(DrillReportId))]
        [InverseProperty(nameof(DrillReports.DrillReportPorePressure))]
        public virtual DrillReports DrillReport { get; set; }
        [ForeignKey(nameof(EquivalentMudWeightId))]
        [InverseProperty(nameof(DrillReportEquivalentMudWeight.DrillReportPorePressure))]
        public virtual DrillReportEquivalentMudWeight EquivalentMudWeight { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(DrillReportMd.DrillReportPorePressure))]
        public virtual DrillReportMd Md { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(DrillReportTvd.DrillReportPorePressure))]
        public virtual DrillReportTvd Tvd { get; set; }
        [InverseProperty("PorePressure")]
        public virtual ICollection<DrillReportControlIncidentInfo> DrillReportControlIncidentInfo { get; set; }
    }
}
