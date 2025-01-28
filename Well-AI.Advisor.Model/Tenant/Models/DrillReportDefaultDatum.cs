using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportDefaultDatum
    {
        public DrillReportDefaultDatum()
        {
            DrillReportCommonData = new HashSet<DrillReportCommonData>();
        }

        [Key]
        public int DefaultDatumId { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("DefaultDatum")]
        public virtual ICollection<DrillReportCommonData> DrillReportCommonData { get; set; }
    }
}
