using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryLocations
    {
        [Key]
        public string Uid { get; set; }
        [Column("WellCRSUidRef")]
        public string WellCrsuidRef { get; set; }
        public int? LatitudeId { get; set; }
        public int? LongitudeId { get; set; }
        public int? EastingId { get; set; }
        public int? NorthingId { get; set; }
        public int? TrajectoryStationId { get; set; }

        [ForeignKey(nameof(EastingId))]
        [InverseProperty(nameof(TrajectoryEastings.TrajectoryLocations))]
        public virtual TrajectoryEastings Easting { get; set; }
        [ForeignKey(nameof(LatitudeId))]
        [InverseProperty(nameof(TrajectoryLatitudes.TrajectoryLocations))]
        public virtual TrajectoryLatitudes Latitude { get; set; }
        [ForeignKey(nameof(LongitudeId))]
        [InverseProperty(nameof(TrajectoryLongitudes.TrajectoryLocations))]
        public virtual TrajectoryLongitudes Longitude { get; set; }
        [ForeignKey(nameof(NorthingId))]
        [InverseProperty(nameof(TrajectoryNorthings.TrajectoryLocations))]
        public virtual TrajectoryNorthings Northing { get; set; }
        [ForeignKey(nameof(TrajectoryStationId))]
        [InverseProperty(nameof(TrajectoryStations.TrajectoryLocations))]
        public virtual TrajectoryStations TrajectoryStation { get; set; }
        [ForeignKey(nameof(WellCrsuidRef))]
        [InverseProperty(nameof(TrajectoryWellCrss.TrajectoryLocations))]
        public virtual TrajectoryWellCrss WellCrsuidRefNavigation { get; set; }
    }
}
