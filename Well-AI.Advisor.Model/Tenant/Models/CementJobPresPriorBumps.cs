using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobPresPriorBumps
    {
        public CementJobPresPriorBumps()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int PresPriorBumpId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresPriorBump")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
