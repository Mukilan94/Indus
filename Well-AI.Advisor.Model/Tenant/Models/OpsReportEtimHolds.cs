using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimHolds")]
    public partial class OpsReportEtimHolds
    {
        public OpsReportEtimHolds()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimHoldId")]
        public int EtimHoldId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimHold")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
