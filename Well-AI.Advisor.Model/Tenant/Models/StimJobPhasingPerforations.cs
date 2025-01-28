using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPhasingPerforations
    {
        public StimJobPhasingPerforations()
        {
            StimJobPerforationIntervals = new HashSet<StimJobPerforationIntervals>();
        }

        [Key]
        public int PhasingPerforationId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PhasingPerforation")]
        public virtual ICollection<StimJobPerforationIntervals> StimJobPerforationIntervals { get; set; }
    }
}
