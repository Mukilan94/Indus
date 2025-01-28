using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimDrills")]
    public partial class OpsReportEtimDrills
    {
        public OpsReportEtimDrills()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimDrillId")]
        public int EtimDrillId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimDrill")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
