using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVolMudCasings
    {
        public OpsReportVolMudCasings()
        {
            OpsReportMudVolumes = new HashSet<OpsReportMudVolumes>();
        }

        [Key]
        public int VolMudCasingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolMudCasing")]
        public virtual ICollection<OpsReportMudVolumes> OpsReportMudVolumes { get; set; }
    }
}
