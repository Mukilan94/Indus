using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportGel10Mins
    {
        public OpsReportGel10Mins()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        public int Gel10MinId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Gel10Min")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
