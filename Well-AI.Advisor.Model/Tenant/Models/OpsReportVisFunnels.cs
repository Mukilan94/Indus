using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVisFunnels
    {
        public OpsReportVisFunnels()
        {
            OpsReportFluids = new HashSet<OpsReportFluids>();
            OpsReportPitVolumes = new HashSet<OpsReportPitVolumes>();
        }

        [Key]
        public int VisFunnelId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VisFunnel")]
        public virtual ICollection<OpsReportFluids> OpsReportFluids { get; set; }
        [InverseProperty("VisFunnel")]
        public virtual ICollection<OpsReportPitVolumes> OpsReportPitVolumes { get; set; }
    }
}
