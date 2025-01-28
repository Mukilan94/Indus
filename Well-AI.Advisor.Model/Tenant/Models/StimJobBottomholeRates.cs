using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobBottomholeRates
    {
        public StimJobBottomholeRates()
        {
            StimJobPresMeasurements = new HashSet<StimJobPresMeasurements>();
            StimJobSteps = new HashSet<StimJobSteps>();
        }

        [Key]
        public int BottomholeRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BottomholeRate")]
        public virtual ICollection<StimJobPresMeasurements> StimJobPresMeasurements { get; set; }
        [InverseProperty("BottomholeRate")]
        public virtual ICollection<StimJobSteps> StimJobSteps { get; set; }
    }
}
