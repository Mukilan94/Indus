using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobTimeAfterShutins
    {
        public StimJobTimeAfterShutins()
        {
            StimJobShutinPress = new HashSet<StimJobShutinPress>();
        }

        [Key]
        public int TimeAfterShutinId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TimeAfterShutin")]
        public virtual ICollection<StimJobShutinPress> StimJobShutinPress { get; set; }
    }
}
