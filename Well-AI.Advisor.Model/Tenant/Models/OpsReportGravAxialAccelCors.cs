using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportGravAxialAccelCors
    {
        public OpsReportGravAxialAccelCors()
        {
            OpsReportCorUseds = new HashSet<OpsReportCorUseds>();
        }

        [Key]
        public int GravAxialAccelCorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GravAxialAccelCor")]
        public virtual ICollection<OpsReportCorUseds> OpsReportCorUseds { get; set; }
    }
}
