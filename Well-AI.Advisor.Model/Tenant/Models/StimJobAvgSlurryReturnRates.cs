using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAvgSlurryReturnRates
    {
        public StimJobAvgSlurryReturnRates()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int AvgSlurryReturnRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgSlurryReturnRate")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
