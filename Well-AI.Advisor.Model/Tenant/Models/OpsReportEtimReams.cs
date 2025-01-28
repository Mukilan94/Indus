using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimReams")]
    public partial class OpsReportEtimReams
    {
        public OpsReportEtimReams()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimReamId")]
        public int EtimReamId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimReam")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
