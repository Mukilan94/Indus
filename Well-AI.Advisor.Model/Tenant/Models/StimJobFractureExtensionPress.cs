using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFractureExtensionPress
    {
        public StimJobFractureExtensionPress()
        {
            StimJobFluidEfficiencyTests = new HashSet<StimJobFluidEfficiencyTests>();
            StimJobStepRateTests = new HashSet<StimJobStepRateTests>();
        }

        [Key]
        public int FractureExtensionPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FractureExtensionPres")]
        public virtual ICollection<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
        [InverseProperty("FractureExtensionPres")]
        public virtual ICollection<StimJobStepRateTests> StimJobStepRateTests { get; set; }
    }
}
