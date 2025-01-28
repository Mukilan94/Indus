using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimDrillSlids")]
    public partial class OpsReportEtimDrillSlids
    {
        public OpsReportEtimDrillSlids()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimDrillSlidId")]
        public int EtimDrillSlidId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimDrillSlid")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
