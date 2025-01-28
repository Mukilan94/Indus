using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFrictionPress
    {
        public StimJobFrictionPress()
        {
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
            StimJobPerforationIntervals = new HashSet<StimJobPerforationIntervals>();
        }

        [Key]
        public int FrictionPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FrictionPres")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
        [InverseProperty("FrictionPres")]
        public virtual ICollection<StimJobPerforationIntervals> StimJobPerforationIntervals { get; set; }
    }
}
