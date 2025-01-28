using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobSteps
    {
        [Key]
        public int StepId { get; set; }
        public string Number { get; set; }
        public int? BottomholeRateId { get; set; }
        public int? PresId { get; set; }
        public int? PipeFrictionId { get; set; }
        public int? EntryFrictionId { get; set; }
        public int? PerfFrictionId { get; set; }
        public int? NearWellboreFrictionId { get; set; }
        public string Uid { get; set; }
        public int? StepDownTestId { get; set; }

        [ForeignKey(nameof(BottomholeRateId))]
        [InverseProperty(nameof(StimJobBottomholeRates.StimJobSteps))]
        public virtual StimJobBottomholeRates BottomholeRate { get; set; }
        [ForeignKey(nameof(EntryFrictionId))]
        [InverseProperty(nameof(StimJobEntryFrictions.StimJobSteps))]
        public virtual StimJobEntryFrictions EntryFriction { get; set; }
        [ForeignKey(nameof(NearWellboreFrictionId))]
        [InverseProperty(nameof(StimJobNearWellboreFrictions.StimJobSteps))]
        public virtual StimJobNearWellboreFrictions NearWellboreFriction { get; set; }
        [ForeignKey(nameof(PerfFrictionId))]
        [InverseProperty(nameof(StimJobPerfFrictions.StimJobSteps))]
        public virtual StimJobPerfFrictions PerfFriction { get; set; }
        [ForeignKey(nameof(PipeFrictionId))]
        [InverseProperty(nameof(StimJobPipeFrictions.StimJobSteps))]
        public virtual StimJobPipeFrictions PipeFriction { get; set; }
        [ForeignKey(nameof(PresId))]
        [InverseProperty(nameof(StimJobPress.StimJobSteps))]
        public virtual StimJobPress Pres { get; set; }
        [ForeignKey(nameof(StepDownTestId))]
        [InverseProperty(nameof(StimJobStepDownTests.StimJobSteps))]
        public virtual StimJobStepDownTests StepDownTest { get; set; }
    }
}
