using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCeilingClouds
    {
        public OpsReportCeilingClouds()
        {
            OpsReportWeathers = new HashSet<OpsReportWeathers>();
        }

        [Key]
        public int CeilingCloudId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CeilingCloud")]
        public virtual ICollection<OpsReportWeathers> OpsReportWeathers { get; set; }
    }
}
