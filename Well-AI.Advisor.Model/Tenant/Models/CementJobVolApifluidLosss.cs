using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobVolAPIFluidLosss")]
    public partial class CementJobVolApifluidLosss
    {
        public CementJobVolApifluidLosss()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        [Column("VolAPIFluidLossId")]
        public int VolApifluidLossId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolApifluidLoss")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
