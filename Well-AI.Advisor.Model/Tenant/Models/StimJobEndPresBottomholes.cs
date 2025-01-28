using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobEndPresBottomholes
    {
        public StimJobEndPresBottomholes()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int EndPresBottomholeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndPresBottomhole")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
