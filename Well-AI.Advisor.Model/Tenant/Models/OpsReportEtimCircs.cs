using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimCircs")]
    public partial class OpsReportEtimCircs
    {
        public OpsReportEtimCircs()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimCircId")]
        public int EtimCircId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimCirc")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
