using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMaxPmaxWeaklinkPress
    {
        public StimJobMaxPmaxWeaklinkPress()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int MaxPmaxWeaklinkPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxPmaxWeaklinkPres")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("MaxPmaxWeaklinkPres")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
