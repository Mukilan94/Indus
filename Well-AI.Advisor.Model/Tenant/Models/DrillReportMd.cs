using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportMd
    {
        public DrillReportMd()
        {
            DrillReportActivity = new HashSet<DrillReportActivity>();
            DrillReportEquipFailureInfo = new HashSet<DrillReportEquipFailureInfo>();
            DrillReportFluids = new HashSet<DrillReportFluids>();
            DrillReportFormTestInfo = new HashSet<DrillReportFormTestInfo>();
            DrillReportPorePressure = new HashSet<DrillReportPorePressure>();
            DrillReportStatusInfo = new HashSet<DrillReportStatusInfo>();
            DrillReportSurveyStation = new HashSet<DrillReportSurveyStation>();
        }

        [Key]
        public int MdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Md")]
        public virtual ICollection<DrillReportActivity> DrillReportActivity { get; set; }
        [InverseProperty("Md")]
        public virtual ICollection<DrillReportEquipFailureInfo> DrillReportEquipFailureInfo { get; set; }
        [InverseProperty("Md")]
        public virtual ICollection<DrillReportFluids> DrillReportFluids { get; set; }
        [InverseProperty("Md")]
        public virtual ICollection<DrillReportFormTestInfo> DrillReportFormTestInfo { get; set; }
        [InverseProperty("Md")]
        public virtual ICollection<DrillReportPorePressure> DrillReportPorePressure { get; set; }
        [InverseProperty("Md")]
        public virtual ICollection<DrillReportStatusInfo> DrillReportStatusInfo { get; set; }
        [InverseProperty("Md")]
        public virtual ICollection<DrillReportSurveyStation> DrillReportSurveyStation { get; set; }
    }
}
