using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimThickenings")]
    public partial class CementJobEtimThickenings
    {
        public CementJobEtimThickenings()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        [Column("ETimThickeningId")]
        public int EtimThickeningId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimThickening")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
