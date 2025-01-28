using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMagTran1DrlstrCors
    {
        public OpsReportMagTran1DrlstrCors()
        {
            OpsReportCorUseds = new HashSet<OpsReportCorUseds>();
        }

        [Key]
        public int MagTran1DrlstrCorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagTran1DrlstrCor")]
        public virtual ICollection<OpsReportCorUseds> OpsReportCorUseds { get; set; }
    }
}
