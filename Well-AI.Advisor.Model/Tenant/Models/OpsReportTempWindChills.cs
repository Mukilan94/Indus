using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTempWindChills
    {
        public OpsReportTempWindChills()
        {
            OpsReportWeathers = new HashSet<OpsReportWeathers>();
        }

        [Key]
        public int TempWindChillId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempWindChill")]
        public virtual ICollection<OpsReportWeathers> OpsReportWeathers { get; set; }
    }
}
