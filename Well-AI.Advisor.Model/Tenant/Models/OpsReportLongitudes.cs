using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportLongitudes
    {
        public OpsReportLongitudes()
        {
            OpsReportLocations = new HashSet<OpsReportLocations>();
        }

        [Key]
        public int LongitudeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Longitude")]
        public virtual ICollection<OpsReportLocations> OpsReportLocations { get; set; }
    }
}
