using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPitVolumes
    {
        [Key]
        public int PitVolumeId { get; set; }
        public int? PitId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? VolPitId { get; set; }
        public int? DensFluidId { get; set; }
        public string DescFluid { get; set; }
        public int? VisFunnelId { get; set; }
        public string Uid { get; set; }
        public int? OpsReportId { get; set; }

        [ForeignKey(nameof(DensFluidId))]
        [InverseProperty(nameof(OpsReportDensFluids.OpsReportPitVolumes))]
        public virtual OpsReportDensFluids DensFluid { get; set; }
        [ForeignKey(nameof(OpsReportId))]
        [InverseProperty(nameof(OpsReports.OpsReportPitVolumes))]
        public virtual OpsReports OpsReport { get; set; }
        [ForeignKey(nameof(PitId))]
        [InverseProperty(nameof(OpsReportPits.OpsReportPitVolumes))]
        public virtual OpsReportPits Pit { get; set; }
        [ForeignKey(nameof(VisFunnelId))]
        [InverseProperty(nameof(OpsReportVisFunnels.OpsReportPitVolumes))]
        public virtual OpsReportVisFunnels VisFunnel { get; set; }
        [ForeignKey(nameof(VolPitId))]
        [InverseProperty(nameof(OpsReportVolPits.OpsReportPitVolumes))]
        public virtual OpsReportVolPits VolPit { get; set; }
    }
}
