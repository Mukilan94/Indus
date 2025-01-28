using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobStepDownTests
    {
        public StimJobStepDownTests()
        {
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
            StimJobSteps = new HashSet<StimJobSteps>();
        }

        [Key]
        public int StepDownTestId { get; set; }
        public int? InitialShutinPresId { get; set; }
        public int? BottomholeFluidDensityId { get; set; }
        public int? DiameterEntryHolePipeFrictionId { get; set; }
        public string PerforationCount { get; set; }
        public string DischargeCoefficient { get; set; }
        public string EffectivePerfs { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(BottomholeFluidDensityId))]
        [InverseProperty(nameof(StimJobBottomholeFluidDensitys.StimJobStepDownTests))]
        public virtual StimJobBottomholeFluidDensitys BottomholeFluidDensity { get; set; }
        [ForeignKey(nameof(DiameterEntryHolePipeFrictionId))]
        [InverseProperty(nameof(StimJobDiameterEntryHoles.StimJobStepDownTests))]
        public virtual StimJobDiameterEntryHoles DiameterEntryHolePipeFriction { get; set; }
        [ForeignKey(nameof(InitialShutinPresId))]
        [InverseProperty(nameof(StimJobInitialShutinPress.StimJobStepDownTests))]
        public virtual StimJobInitialShutinPress InitialShutinPres { get; set; }
        [InverseProperty("StepDownTest")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
        [InverseProperty("StepDownTest")]
        public virtual ICollection<StimJobSteps> StimJobSteps { get; set; }
    }
}
