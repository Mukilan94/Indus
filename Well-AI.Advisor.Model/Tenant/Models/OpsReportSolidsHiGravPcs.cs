using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportSolidsHiGravPcs
    {
        public OpsReportSolidsHiGravPcs()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
        }

        [Key]
        public int SolidsHiGravPcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SolidsHiGravPc")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
    }
}
