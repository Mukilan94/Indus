using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobGel10SecStrengths
    {
        public CementJobGel10SecStrengths()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int Gel10SecStrengthId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Gel10SecStrength")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
