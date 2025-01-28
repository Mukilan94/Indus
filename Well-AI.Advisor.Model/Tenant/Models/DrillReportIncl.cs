using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportIncl
    {
        public DrillReportIncl()
        {
            DrillReportSurveyStation = new HashSet<DrillReportSurveyStation>();
        }

        [Key]
        public int InclId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Incl")]
        public virtual ICollection<DrillReportSurveyStation> DrillReportSurveyStation { get; set; }
    }
}
