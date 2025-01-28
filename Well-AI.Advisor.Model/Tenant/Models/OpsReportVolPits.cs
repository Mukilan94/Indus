using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVolPits
    {
        public OpsReportVolPits()
        {
            OpsReportPitVolumes = new HashSet<OpsReportPitVolumes>();
        }

        [Key]
        public int VolPitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolPit")]
        public virtual ICollection<OpsReportPitVolumes> OpsReportPitVolumes { get; set; }
    }
}
