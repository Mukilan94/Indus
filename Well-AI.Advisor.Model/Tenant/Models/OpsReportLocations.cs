using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportLocations
    {
        [Key]
        public string Uid { get; set; }
        [Column("WellCRSId")]
        public int? WellCrsid { get; set; }
        public int? LatitudeId { get; set; }
        public int? LongitudeId { get; set; }
        [Column("ProjectedXId")]
        public int? ProjectedXid { get; set; }
        [Column("ProjectedYId")]
        public int? ProjectedYid { get; set; }
        public string OpsReportTrajectoryStationUid { get; set; }

        [ForeignKey(nameof(LatitudeId))]
        [InverseProperty(nameof(OpsReportLatitudes.OpsReportLocations))]
        public virtual OpsReportLatitudes Latitude { get; set; }
        [ForeignKey(nameof(LongitudeId))]
        [InverseProperty(nameof(OpsReportLongitudes.OpsReportLocations))]
        public virtual OpsReportLongitudes Longitude { get; set; }
        [ForeignKey(nameof(OpsReportTrajectoryStationUid))]
        [InverseProperty(nameof(OpsReportTrajectoryStations.OpsReportLocations))]
        public virtual OpsReportTrajectoryStations OpsReportTrajectoryStationU { get; set; }
        [ForeignKey(nameof(ProjectedXid))]
        [InverseProperty(nameof(OpsReportProjectedXs.OpsReportLocations))]
        public virtual OpsReportProjectedXs ProjectedX { get; set; }
        [ForeignKey(nameof(ProjectedYid))]
        [InverseProperty(nameof(OpsReportProjectedYs.OpsReportLocations))]
        public virtual OpsReportProjectedYs ProjectedY { get; set; }
        [ForeignKey(nameof(WellCrsid))]
        [InverseProperty(nameof(OpsReportWellCrss.OpsReportLocations))]
        public virtual OpsReportWellCrss WellCrs { get; set; }
    }
}
