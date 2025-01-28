using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportGel30Mins
    {
        public OpsReportGel30Mins()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        public int Gel30MinId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Gel30Min")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
