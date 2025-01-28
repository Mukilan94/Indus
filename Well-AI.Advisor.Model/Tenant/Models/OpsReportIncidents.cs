using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportIncidents
    {
        public OpsReportIncidents()
        {
            OpsReportHses = new HashSet<OpsReportHses>();
        }

        [Key]
        public int IncidentId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string Reporter { get; set; }
        public string NumMinorInjury { get; set; }
        public string NumMajorInjury { get; set; }
        public string NumFatality { get; set; }
        public string IsNearMiss { get; set; }
        public string DescLocation { get; set; }
        public string DescAccident { get; set; }
        public string RemedialActionDesc { get; set; }
        public string CauseDesc { get; set; }
        [Column("ETimLostGrossId")]
        public int? EtimLostGrossId { get; set; }
        public int? CostLostGrossId { get; set; }
        public string ResponsibleCompany { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CostLostGrossId))]
        [InverseProperty(nameof(OpsReportCostLostGrosss.OpsReportIncidents))]
        public virtual OpsReportCostLostGrosss CostLostGross { get; set; }
        [ForeignKey(nameof(EtimLostGrossId))]
        [InverseProperty(nameof(OpsReportEtimLostGrosss.OpsReportIncidents))]
        public virtual OpsReportEtimLostGrosss EtimLostGross { get; set; }
        [InverseProperty("Incident")]
        public virtual ICollection<OpsReportHses> OpsReportHses { get; set; }
    }
}
