using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVolMudHoles
    {
        public OpsReportVolMudHoles()
        {
            OpsReportMudVolumes = new HashSet<OpsReportMudVolumes>();
        }

        [Key]
        public int VolMudHoleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolMudHole")]
        public virtual ICollection<OpsReportMudVolumes> OpsReportMudVolumes { get; set; }
    }
}
