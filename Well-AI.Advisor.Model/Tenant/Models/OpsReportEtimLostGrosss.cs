using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimLostGrosss")]
    public partial class OpsReportEtimLostGrosss
    {
        public OpsReportEtimLostGrosss()
        {
            OpsReportIncidents = new HashSet<OpsReportIncidents>();
        }

        [Key]
        [Column("ETimLostGrossId")]
        public int EtimLostGrossId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimLostGross")]
        public virtual ICollection<OpsReportIncidents> OpsReportIncidents { get; set; }
    }
}
