using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobVolTestFluidLosss
    {
        public CementJobVolTestFluidLosss()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int VolTestFluidLossId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolTestFluidLoss")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
