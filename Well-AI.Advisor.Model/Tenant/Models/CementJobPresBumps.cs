using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobPresBumps
    {
        public CementJobPresBumps()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int PresBumpId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresBump")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
