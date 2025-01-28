using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimSpuds")]
    public partial class OpsReportEtimSpuds
    {
        public OpsReportEtimSpuds()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimSpudId")]
        public int EtimSpudId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimSpud")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
