using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportTvd
    {
        public DrillReportTvd()
        {
            DrillReportActivity = new HashSet<DrillReportActivity>();
            DrillReportFluids = new HashSet<DrillReportFluids>();
            DrillReportFormTestInfo = new HashSet<DrillReportFormTestInfo>();
            DrillReportPorePressure = new HashSet<DrillReportPorePressure>();
            DrillReportStatusInfo = new HashSet<DrillReportStatusInfo>();
            DrillReportSurveyStation = new HashSet<DrillReportSurveyStation>();
        }

        [Key]
        public int TvdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Tvd")]
        public virtual ICollection<DrillReportActivity> DrillReportActivity { get; set; }
        [InverseProperty("Tvd")]
        public virtual ICollection<DrillReportFluids> DrillReportFluids { get; set; }
        [InverseProperty("Tvd")]
        public virtual ICollection<DrillReportFormTestInfo> DrillReportFormTestInfo { get; set; }
        [InverseProperty("Tvd")]
        public virtual ICollection<DrillReportPorePressure> DrillReportPorePressure { get; set; }
        [InverseProperty("Tvd")]
        public virtual ICollection<DrillReportStatusInfo> DrillReportStatusInfo { get; set; }
        [InverseProperty("Tvd")]
        public virtual ICollection<DrillReportSurveyStation> DrillReportSurveyStation { get; set; }
    }
}
