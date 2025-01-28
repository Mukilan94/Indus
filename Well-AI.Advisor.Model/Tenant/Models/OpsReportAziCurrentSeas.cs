using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportAziCurrentSeas
    {
        public OpsReportAziCurrentSeas()
        {
            OpsReportWeathers = new HashSet<OpsReportWeathers>();
        }

        [Key]
        public int AziCurrentSeaId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AziCurrentSea")]
        public virtual ICollection<OpsReportWeathers> OpsReportWeathers { get; set; }
    }
}
