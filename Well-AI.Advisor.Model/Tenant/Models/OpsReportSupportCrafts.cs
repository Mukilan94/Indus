using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportSupportCrafts
    {
        public OpsReportSupportCrafts()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int SupportCraftId { get; set; }
        public string Name { get; set; }
        public string TypeSuppCraft { get; set; }
        [Column("DTimArrived")]
        public string DtimArrived { get; set; }
        [Column("DTimDeparted")]
        public string DtimDeparted { get; set; }
        public string Comments { get; set; }
        public string Uid { get; set; }

        [InverseProperty("SupportCraft")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
