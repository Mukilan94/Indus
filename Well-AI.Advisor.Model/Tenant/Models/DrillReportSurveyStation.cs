using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportSurveyStation
    {
        public DrillReportSurveyStation()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int SurveyStationId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public int? InclId { get; set; }
        public int? AziId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AziId))]
        [InverseProperty(nameof(DrillReportAzi.DrillReportSurveyStation))]
        public virtual DrillReportAzi Azi { get; set; }
        [ForeignKey(nameof(InclId))]
        [InverseProperty(nameof(DrillReportIncl.DrillReportSurveyStation))]
        public virtual DrillReportIncl Incl { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(DrillReportMd.DrillReportSurveyStation))]
        public virtual DrillReportMd Md { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(DrillReportTvd.DrillReportSurveyStation))]
        public virtual DrillReportTvd Tvd { get; set; }
        [InverseProperty("SurveyStation")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
