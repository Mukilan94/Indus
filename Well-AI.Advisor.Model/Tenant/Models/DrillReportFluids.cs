using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportFluids
    {
        [Key]
        public int FluidId { get; set; }
        public string Type { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public int? PresBopRatingId { get; set; }
        public string MudClass { get; set; }
        public int? DensityId { get; set; }
        public int? TempVisId { get; set; }
        public int? PvId { get; set; }
        public string Uid { get; set; }
        public int? DrillReportId { get; set; }

        [ForeignKey(nameof(DensityId))]
        [InverseProperty(nameof(DrillReportDensity.DrillReportFluids))]
        public virtual DrillReportDensity Density { get; set; }
        [ForeignKey(nameof(DrillReportId))]
        [InverseProperty(nameof(DrillReports.DrillReportFluids))]
        public virtual DrillReports DrillReport { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(DrillReportMd.DrillReportFluids))]
        public virtual DrillReportMd Md { get; set; }
        [ForeignKey(nameof(PresBopRatingId))]
        [InverseProperty(nameof(DrillReportPresBopRating.DrillReportFluids))]
        public virtual DrillReportPresBopRating PresBopRating { get; set; }
        [ForeignKey(nameof(PvId))]
        [InverseProperty(nameof(DrillReportPv.DrillReportFluids))]
        public virtual DrillReportPv Pv { get; set; }
        [ForeignKey(nameof(TempVisId))]
        [InverseProperty(nameof(DrillReportTempVis.DrillReportFluids))]
        public virtual DrillReportTempVis TempVis { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(DrillReportTvd.DrillReportFluids))]
        public virtual DrillReportTvd Tvd { get; set; }
    }
}
