using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobMdPlugTops
    {
        public CementJobMdPlugTops()
        {
            CementJobs = new HashSet<CementJobs>();
        }

        [Key]
        public int MdPlugTopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdPlugTop")]
        public virtual ICollection<CementJobs> CementJobs { get; set; }
    }
}
