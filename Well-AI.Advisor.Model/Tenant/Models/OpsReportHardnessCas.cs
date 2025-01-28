using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportHardnessCas
    {
        public OpsReportHardnessCas()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        public int HardnessCaId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("HardnessCa")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
