using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimSteerings")]
    public partial class OpsReportEtimSteerings
    {
        public OpsReportEtimSteerings()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimSteeringId")]
        public int EtimSteeringId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimSteering")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
