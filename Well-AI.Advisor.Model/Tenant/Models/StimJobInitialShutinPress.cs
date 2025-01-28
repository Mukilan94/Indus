using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobInitialShutinPress
    {
        public StimJobInitialShutinPress()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
            StimJobStepDownTests = new HashSet<StimJobStepDownTests>();
        }

        [Key]
        public int InitialShutinPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("InitialShutinPres")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
        [InverseProperty("InitialShutinPres")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
        [InverseProperty("InitialShutinPres")]
        public virtual ICollection<StimJobStepDownTests> StimJobStepDownTests { get; set; }
    }
}
