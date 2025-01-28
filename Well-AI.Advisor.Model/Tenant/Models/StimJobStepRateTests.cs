using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobStepRateTests
    {
        public StimJobStepRateTests()
        {
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
            StimJobPresMeasurements = new HashSet<StimJobPresMeasurements>();
        }

        [Key]
        public int StepRateTestId { get; set; }
        public int? FractureExtensionPresId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(FractureExtensionPresId))]
        [InverseProperty(nameof(StimJobFractureExtensionPress.StimJobStepRateTests))]
        public virtual StimJobFractureExtensionPress FractureExtensionPres { get; set; }
        [InverseProperty("StepRateTest")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
        [InverseProperty("StepRateTest")]
        public virtual ICollection<StimJobPresMeasurements> StimJobPresMeasurements { get; set; }
    }
}
