using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPresMeasurements
    {
        [Key]
        public int PresMeasurementId { get; set; }
        public int? PresId { get; set; }
        public int? BottomholeRateId { get; set; }
        public int? StepRateTestId { get; set; }

        [ForeignKey(nameof(BottomholeRateId))]
        [InverseProperty(nameof(StimJobBottomholeRates.StimJobPresMeasurements))]
        public virtual StimJobBottomholeRates BottomholeRate { get; set; }
        [ForeignKey(nameof(PresId))]
        [InverseProperty(nameof(StimJobPress.StimJobPresMeasurements))]
        public virtual StimJobPress Pres { get; set; }
        [ForeignKey(nameof(StepRateTestId))]
        [InverseProperty(nameof(StimJobStepRateTests.StimJobPresMeasurements))]
        public virtual StimJobStepRateTests StepRateTest { get; set; }
    }
}
