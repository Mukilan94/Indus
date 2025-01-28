using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobYpMuds
    {
        public CementJobYpMuds()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int YpMudId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("YpMud")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
