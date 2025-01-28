using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobEndPumpRateBottomholes
    {
        public StimJobEndPumpRateBottomholes()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int EndPumpRateBottomholeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndPumpRateBottomhole")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
