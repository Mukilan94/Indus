using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobTvdPerforationsBottoms
    {
        public StimJobTvdPerforationsBottoms()
        {
            StimJobPerforationIntervals = new HashSet<StimJobPerforationIntervals>();
        }

        [Key]
        public int TvdPerforationsBottomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdPerforationsBottom")]
        public virtual ICollection<StimJobPerforationIntervals> StimJobPerforationIntervals { get; set; }
    }
}
