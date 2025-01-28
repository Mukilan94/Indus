using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportActivity
    {
        [Key]
        public int ActivityId { get; set; }
        [Column("DTimStart")]
        public string DtimStart { get; set; }
        [Column("DTimEnd")]
        public string DtimEnd { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public string Phase { get; set; }
        public string ActivityCode { get; set; }
        public string DetailActivity { get; set; }
        public string State { get; set; }
        public string StateDetailActivity { get; set; }
        public string Comments { get; set; }
        public string Uid { get; set; }
        public int? DrillReportId { get; set; }

        [ForeignKey(nameof(DrillReportId))]
        [InverseProperty(nameof(DrillReports.DrillReportActivity))]
        public virtual DrillReports DrillReport { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(DrillReportMd.DrillReportActivity))]
        public virtual DrillReportMd Md { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(DrillReportTvd.DrillReportActivity))]
        public virtual DrillReportTvd Tvd { get; set; }
    }
}
