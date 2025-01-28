using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimStarts")]
    public partial class OpsReportEtimStarts
    {
        public OpsReportEtimStarts()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimStartId")]
        public int EtimStartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimStart")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
