using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportEquivalentMudWeight
    {
        public DrillReportEquivalentMudWeight()
        {
            DrillReportPorePressure = new HashSet<DrillReportPorePressure>();
        }

        [Key]
        public int EquivalentMudWeightId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EquivalentMudWeight")]
        public virtual ICollection<DrillReportPorePressure> DrillReportPorePressure { get; set; }
    }
}
