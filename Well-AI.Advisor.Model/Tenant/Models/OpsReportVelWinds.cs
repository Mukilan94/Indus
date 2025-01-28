using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVelWinds
    {
        public OpsReportVelWinds()
        {
            OpsReportWeathers = new HashSet<OpsReportWeathers>();
        }

        [Key]
        public int VelWindId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VelWind")]
        public virtual ICollection<OpsReportWeathers> OpsReportWeathers { get; set; }
    }
}
