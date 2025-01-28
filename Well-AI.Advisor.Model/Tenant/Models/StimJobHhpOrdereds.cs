using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobHhpOrdereds
    {
        public StimJobHhpOrdereds()
        {
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int HhpOrderedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("HhpOrdered")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
