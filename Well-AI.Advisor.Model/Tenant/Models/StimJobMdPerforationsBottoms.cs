using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMdPerforationsBottoms
    {
        public StimJobMdPerforationsBottoms()
        {
            StimJobPerforationIntervals = new HashSet<StimJobPerforationIntervals>();
        }

        [Key]
        public int MdPerforationsBottomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdPerforationsBottom")]
        public virtual ICollection<StimJobPerforationIntervals> StimJobPerforationIntervals { get; set; }
    }
}
