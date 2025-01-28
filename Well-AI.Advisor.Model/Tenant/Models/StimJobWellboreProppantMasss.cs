using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobWellboreProppantMasss
    {
        public StimJobWellboreProppantMasss()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int WellboreProppantMassId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("WellboreProppantMass")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
