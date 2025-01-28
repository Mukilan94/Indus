using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimDrillRots")]
    public partial class OpsReportEtimDrillRots
    {
        public OpsReportEtimDrillRots()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimDrillRotId")]
        public int EtimDrillRotId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimDrillRot")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
