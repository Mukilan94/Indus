using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobSolidVolumeFractions
    {
        public CementJobSolidVolumeFractions()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int SolidVolumeFractionId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SolidVolumeFraction")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
