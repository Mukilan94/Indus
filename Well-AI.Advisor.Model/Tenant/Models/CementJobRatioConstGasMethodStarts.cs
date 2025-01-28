using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobRatioConstGasMethodStarts
    {
        public CementJobRatioConstGasMethodStarts()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int RatioConstGasMethodStartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RatioConstGasMethodStart")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
