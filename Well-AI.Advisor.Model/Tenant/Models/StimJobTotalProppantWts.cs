using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobTotalProppantWts
    {
        public StimJobTotalProppantWts()
        {
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int TotalProppantWtId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TotalProppantWt")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
