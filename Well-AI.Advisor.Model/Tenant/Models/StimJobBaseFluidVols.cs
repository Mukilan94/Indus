using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobBaseFluidVols
    {
        public StimJobBaseFluidVols()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
        }

        [Key]
        public int BaseFluidVolId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BaseFluidVol")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("BaseFluidVol")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
        [InverseProperty("BaseFluidVol")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
    }
}
