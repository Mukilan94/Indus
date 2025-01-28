using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportYps
    {
        public OpsReportYps()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        public int YpId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Yp")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
