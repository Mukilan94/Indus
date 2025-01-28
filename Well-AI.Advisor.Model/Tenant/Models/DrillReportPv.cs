using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportPv
    {
        public DrillReportPv()
        {
            DrillReportFluids = new HashSet<DrillReportFluids>();
        }

        [Key]
        public int PvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Pv")]
        public virtual ICollection<DrillReportFluids> DrillReportFluids { get; set; }
    }
}
