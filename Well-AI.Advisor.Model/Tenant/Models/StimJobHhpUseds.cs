using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobHhpUseds
    {
        public StimJobHhpUseds()
        {
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int HhpUsedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("HhpUsed")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
