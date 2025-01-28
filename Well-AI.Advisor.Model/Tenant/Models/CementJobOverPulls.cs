using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobOverPulls
    {
        public CementJobOverPulls()
        {
            CementJobs = new HashSet<CementJobs>();
        }

        [Key]
        public int OverPullId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OverPull")]
        public virtual ICollection<CementJobs> CementJobs { get; set; }
    }
}
