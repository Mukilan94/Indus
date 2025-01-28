using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTempSurfaceMxs
    {
        public OpsReportTempSurfaceMxs()
        {
            OpsReportWeathers = new HashSet<OpsReportWeathers>();
        }

        [Key]
        public int TempSurfaceMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempSurfaceMx")]
        public virtual ICollection<OpsReportWeathers> OpsReportWeathers { get; set; }
    }
}
