using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportCommonData
    {
        public DrillReportCommonData()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int CommonDataId { get; set; }
        public int? DefaultDatumId { get; set; }

        [ForeignKey(nameof(DefaultDatumId))]
        [InverseProperty(nameof(DrillReportDefaultDatum.DrillReportCommonData))]
        public virtual DrillReportDefaultDatum DefaultDatum { get; set; }
        [InverseProperty("CommonData")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
