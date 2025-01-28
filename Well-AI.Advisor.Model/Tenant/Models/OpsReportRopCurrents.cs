using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportRopCurrents
    {
        public OpsReportRopCurrents()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int RopCurrentId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RopCurrent")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
