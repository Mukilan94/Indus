using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPeriodWaves
    {
        public OpsReportPeriodWaves()
        {
            OpsReportWeathers = new HashSet<OpsReportWeathers>();
        }

        [Key]
        public int PeriodWaveId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PeriodWave")]
        public virtual ICollection<OpsReportWeathers> OpsReportWeathers { get; set; }
    }
}
