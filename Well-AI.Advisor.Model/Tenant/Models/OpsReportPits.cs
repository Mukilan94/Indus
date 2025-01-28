using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPits
    {
        public OpsReportPits()
        {
            OpsReportPitVolumes = new HashSet<OpsReportPitVolumes>();
        }

        [Key]
        public int PitId { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("Pit")]
        public virtual ICollection<OpsReportPitVolumes> OpsReportPitVolumes { get; set; }
    }
}
