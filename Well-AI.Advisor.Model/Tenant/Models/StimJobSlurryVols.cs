using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobSlurryVols
    {
        public StimJobSlurryVols()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int SlurryVolId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SlurryVol")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("SlurryVol")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
