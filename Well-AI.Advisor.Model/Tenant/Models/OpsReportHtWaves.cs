using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportHtWaves
    {
        public OpsReportHtWaves()
        {
            OpsReportWeathers = new HashSet<OpsReportWeathers>();
        }

        [Key]
        public int HtWaveId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("HtWave")]
        public virtual ICollection<OpsReportWeathers> OpsReportWeathers { get; set; }
    }
}
