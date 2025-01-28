using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobMdPlugBots
    {
        public CementJobMdPlugBots()
        {
            CementJobs = new HashSet<CementJobs>();
        }

        [Key]
        public int MdPlugBotId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdPlugBot")]
        public virtual ICollection<CementJobs> CementJobs { get; set; }
    }
}
