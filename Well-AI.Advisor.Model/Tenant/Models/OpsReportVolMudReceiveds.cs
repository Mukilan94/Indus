using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVolMudReceiveds
    {
        public OpsReportVolMudReceiveds()
        {
            OpsReportMudVolumes = new HashSet<OpsReportMudVolumes>();
        }

        [Key]
        public int VolMudReceivedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolMudReceived")]
        public virtual ICollection<OpsReportMudVolumes> OpsReportMudVolumes { get; set; }
    }
}
