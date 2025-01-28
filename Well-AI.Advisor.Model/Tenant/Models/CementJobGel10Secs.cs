using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobGel10Secs
    {
        public CementJobGel10Secs()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int Gel10SecId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Gel10Sec")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
