using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVolTotMudStarts
    {
        public OpsReportVolTotMudStarts()
        {
            OpsReportMudVolumes = new HashSet<OpsReportMudVolumes>();
        }

        [Key]
        public int VolTotMudStartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolTotMudStart")]
        public virtual ICollection<OpsReportMudVolumes> OpsReportMudVolumes { get; set; }
    }
}
