using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportAmtPrecips
    {
        public OpsReportAmtPrecips()
        {
            OpsReportWeathers = new HashSet<OpsReportWeathers>();
        }

        [Key]
        public int AmtPrecipId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AmtPrecip")]
        public virtual ICollection<OpsReportWeathers> OpsReportWeathers { get; set; }
    }
}
