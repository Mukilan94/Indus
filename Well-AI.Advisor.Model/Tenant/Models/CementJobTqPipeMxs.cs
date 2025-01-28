using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobTqPipeMxs
    {
        public CementJobTqPipeMxs()
        {
            CementJobs = new HashSet<CementJobs>();
        }

        [Key]
        public int TqPipeMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TqPipeMx")]
        public virtual ICollection<CementJobs> CementJobs { get; set; }
    }
}
