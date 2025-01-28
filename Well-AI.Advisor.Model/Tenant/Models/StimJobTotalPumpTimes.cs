using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobTotalPumpTimes
    {
        public StimJobTotalPumpTimes()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int TotalPumpTimeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TotalPumpTime")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
        [InverseProperty("TotalPumpTime")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
