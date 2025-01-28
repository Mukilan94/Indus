using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobPresTbgEnds
    {
        public CementJobPresTbgEnds()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int PresTbgEndId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresTbgEnd")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
