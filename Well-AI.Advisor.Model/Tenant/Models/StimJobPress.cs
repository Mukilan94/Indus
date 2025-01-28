using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPress
    {
        public StimJobPress()
        {
            StimJobPresMeasurements = new HashSet<StimJobPresMeasurements>();
            StimJobShutinPress = new HashSet<StimJobShutinPress>();
            StimJobSteps = new HashSet<StimJobSteps>();
        }

        [Key]
        public int PresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Pres")]
        public virtual ICollection<StimJobPresMeasurements> StimJobPresMeasurements { get; set; }
        [InverseProperty("Pres")]
        public virtual ICollection<StimJobShutinPress> StimJobShutinPress { get; set; }
        [InverseProperty("Pres")]
        public virtual ICollection<StimJobSteps> StimJobSteps { get; set; }
    }
}
