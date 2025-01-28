using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVolTotMudLostSurfs
    {
        public OpsReportVolTotMudLostSurfs()
        {
            OpsReportMudLossess = new HashSet<OpsReportMudLossess>();
        }

        [Key]
        public int VolTotMudLostSurfId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolTotMudLostSurf")]
        public virtual ICollection<OpsReportMudLossess> OpsReportMudLossess { get; set; }
    }
}
