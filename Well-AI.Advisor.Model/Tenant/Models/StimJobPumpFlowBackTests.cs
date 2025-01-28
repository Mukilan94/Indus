using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPumpFlowBackTests
    {
        public StimJobPumpFlowBackTests()
        {
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
        }

        [Key]
        public int PumpFlowBackTestId { get; set; }
        public int? FractureCloseDurationId { get; set; }
        public int? FractureClosePresId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(FractureCloseDurationId))]
        [InverseProperty(nameof(StimJobFractureCloseDurations.StimJobPumpFlowBackTests))]
        public virtual StimJobFractureCloseDurations FractureCloseDuration { get; set; }
        [ForeignKey(nameof(FractureClosePresId))]
        [InverseProperty(nameof(StimJobFractureClosePress.StimJobPumpFlowBackTests))]
        public virtual StimJobFractureClosePress FractureClosePres { get; set; }
        [InverseProperty("PumpFlowBackTest")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
    }
}
