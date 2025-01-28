using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAvgJobPress
    {
        public StimJobAvgJobPress()
        {
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int AvgJobPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgJobPres")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
