using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobVolCircPriors
    {
        public CementJobVolCircPriors()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int VolCircPriorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolCircPrior")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
