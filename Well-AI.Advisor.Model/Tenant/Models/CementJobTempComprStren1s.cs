using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobTempComprStren1s
    {
        public CementJobTempComprStren1s()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int TempComprStren1Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempComprStren1")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
