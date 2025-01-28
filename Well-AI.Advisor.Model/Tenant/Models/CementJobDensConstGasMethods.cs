using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobDensConstGasMethods
    {
        public CementJobDensConstGasMethods()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int DensConstGasMethodId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DensConstGasMethod")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
