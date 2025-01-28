using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMdPerforationsTops
    {
        public StimJobMdPerforationsTops()
        {
            StimJobPerforationIntervals = new HashSet<StimJobPerforationIntervals>();
        }

        [Key]
        public int MdPerforationsTopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdPerforationsTop")]
        public virtual ICollection<StimJobPerforationIntervals> StimJobPerforationIntervals { get; set; }
    }
}
