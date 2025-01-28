using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportPresPore
    {
        public DrillReportPresPore()
        {
            DrillReportFormTestInfo = new HashSet<DrillReportFormTestInfo>();
        }

        [Key]
        public int PresPoreId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresPore")]
        public virtual ICollection<DrillReportFormTestInfo> DrillReportFormTestInfo { get; set; }
    }
}
