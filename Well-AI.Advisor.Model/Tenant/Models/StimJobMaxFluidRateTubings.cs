using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMaxFluidRateTubings
    {
        public StimJobMaxFluidRateTubings()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int MaxFluidRateTubingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxFluidRateTubing")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
