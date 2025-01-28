using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportGravTran1AccelCors
    {
        public OpsReportGravTran1AccelCors()
        {
            OpsReportCorUseds = new HashSet<OpsReportCorUseds>();
        }

        [Key]
        public int GravTran1AccelCorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GravTran1AccelCor")]
        public virtual ICollection<OpsReportCorUseds> OpsReportCorUseds { get; set; }
    }
}
