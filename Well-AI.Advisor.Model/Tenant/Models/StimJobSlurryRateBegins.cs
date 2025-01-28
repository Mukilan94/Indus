using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobSlurryRateBegins
    {
        public StimJobSlurryRateBegins()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int SlurryRateBeginId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SlurryRateBegin")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
