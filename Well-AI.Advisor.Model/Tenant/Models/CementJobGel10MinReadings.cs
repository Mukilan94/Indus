using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobGel10MinReadings
    {
        public CementJobGel10MinReadings()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int Gel10MinReadingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Gel10MinReading")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
