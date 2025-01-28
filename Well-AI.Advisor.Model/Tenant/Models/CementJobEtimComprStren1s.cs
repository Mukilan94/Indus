using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimComprStren1s")]
    public partial class CementJobEtimComprStren1s
    {
        public CementJobEtimComprStren1s()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        [Column("ETimComprStren1Id")]
        public int EtimComprStren1Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimComprStren1")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
