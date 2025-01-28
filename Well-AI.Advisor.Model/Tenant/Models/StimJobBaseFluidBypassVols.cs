using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobBaseFluidBypassVols
    {
        public StimJobBaseFluidBypassVols()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int BaseFluidBypassVolId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BaseFluidBypassVol")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("BaseFluidBypassVol")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
