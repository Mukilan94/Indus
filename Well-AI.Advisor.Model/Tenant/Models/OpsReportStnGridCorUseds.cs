using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportStnGridCorUseds
    {
        public OpsReportStnGridCorUseds()
        {
            OpsReportCorUseds = new HashSet<OpsReportCorUseds>();
        }

        [Key]
        public int StnGridCorUsedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StnGridCorUsed")]
        public virtual ICollection<OpsReportCorUseds> OpsReportCorUseds { get; set; }
    }
}
