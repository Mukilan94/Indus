using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimComprStren2s")]
    public partial class CementJobEtimComprStren2s
    {
        public CementJobEtimComprStren2s()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        [Column("ETimComprStren2Id")]
        public int EtimComprStren2Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimComprStren2")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
