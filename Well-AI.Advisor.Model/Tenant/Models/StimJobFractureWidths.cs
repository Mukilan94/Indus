using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFractureWidths
    {
        public StimJobFractureWidths()
        {
            StimJobFluidEfficiencyTests = new HashSet<StimJobFluidEfficiencyTests>();
        }

        [Key]
        public int FractureWidthId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FractureWidth")]
        public virtual ICollection<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
    }
}
