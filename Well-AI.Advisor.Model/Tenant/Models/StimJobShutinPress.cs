using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobShutinPress
    {
        [Key]
        public int ShutinPresId { get; set; }
        public int? PresId { get; set; }
        public int? TimeAfterShutinId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(PresId))]
        [InverseProperty(nameof(StimJobPress.StimJobShutinPress))]
        public virtual StimJobPress Pres { get; set; }
        [ForeignKey(nameof(TimeAfterShutinId))]
        [InverseProperty(nameof(StimJobTimeAfterShutins.StimJobShutinPress))]
        public virtual StimJobTimeAfterShutins TimeAfterShutin { get; set; }
    }
}
