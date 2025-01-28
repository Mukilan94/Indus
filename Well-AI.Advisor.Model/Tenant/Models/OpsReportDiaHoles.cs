using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportDiaHoles
    {
        public OpsReportDiaHoles()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int DiaHoleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaHole")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
