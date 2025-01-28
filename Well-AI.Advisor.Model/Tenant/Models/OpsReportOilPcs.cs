using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportOilPcs
    {
        public OpsReportOilPcs()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        public int OilPcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OilPc")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
