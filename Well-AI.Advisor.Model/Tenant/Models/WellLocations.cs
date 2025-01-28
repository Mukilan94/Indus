using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellLocations
    {
        public WellLocations()
        {
            Wells = new HashSet<Wells>();
        }

        [Key]
        public int LocationId { get; set; }
        [Column("WellCRSUid")]
        public int? WellCrsuid { get; set; }
        public int? EastingId { get; set; }
        public int? NorthingId { get; set; }
        public string Uid { get; set; }
        [Column("LocalXId")]
        public int? LocalXid { get; set; }
        [Column("LocalYId")]
        public int? LocalYid { get; set; }
        public string Description { get; set; }
        public int? LatitudeId { get; set; }
        public int? LongitudeId { get; set; }
        public int? WellReferencePointReferencePointId { get; set; }

        [ForeignKey(nameof(EastingId))]
        [InverseProperty(nameof(WellEastings.WellLocations))]
        public virtual WellEastings Easting { get; set; }
        [ForeignKey(nameof(LatitudeId))]
        [InverseProperty(nameof(WellLatitudes.WellLocations))]
        public virtual WellLatitudes Latitude { get; set; }
        [ForeignKey(nameof(LocalXid))]
        [InverseProperty(nameof(WellLocalXs.WellLocations))]
        public virtual WellLocalXs LocalX { get; set; }
        [ForeignKey(nameof(LocalYid))]
        [InverseProperty(nameof(WellLocalYs.WellLocations))]
        public virtual WellLocalYs LocalY { get; set; }
        [ForeignKey(nameof(LongitudeId))]
        [InverseProperty(nameof(WellLongitudes.WellLocations))]
        public virtual WellLongitudes Longitude { get; set; }
        [ForeignKey(nameof(NorthingId))]
        [InverseProperty(nameof(WellNorthings.WellLocations))]
        public virtual WellNorthings Northing { get; set; }
        [ForeignKey(nameof(WellCrsuid))]
        [InverseProperty(nameof(WellCrss.WellLocations))]
        public virtual WellCrss WellCrsu { get; set; }
        [ForeignKey(nameof(WellReferencePointReferencePointId))]
        [InverseProperty(nameof(WellReferencePoints.WellLocations))]
        public virtual WellReferencePoints WellReferencePointReferencePoint { get; set; }
        [InverseProperty("WellLocationLocation")]
        public virtual ICollection<Wells> Wells { get; set; }
    }
}
