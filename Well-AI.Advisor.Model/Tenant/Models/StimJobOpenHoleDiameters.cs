using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobOpenHoleDiameters
    {
        public StimJobOpenHoleDiameters()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int OpenHoleDiameterId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OpenHoleDiameter")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
