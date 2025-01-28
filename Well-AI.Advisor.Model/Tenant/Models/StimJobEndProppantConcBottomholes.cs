using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobEndProppantConcBottomholes
    {
        public StimJobEndProppantConcBottomholes()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int EndProppantConcBottomholeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndProppantConcBottomhole")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
