using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMaxJobPress
    {
        public StimJobMaxJobPress()
        {
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int MaxJobPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxJobPres")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
