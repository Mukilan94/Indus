using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetLocations
    {
        [Key]
        public int TargetLocationId { get; set; }
        [Column("WellCRSId")]
        public int? WellCrsid { get; set; }
        public int? LatitudeId { get; set; }
        public int? LongitudeId { get; set; }
        public string Uid { get; set; }
        [Column("ProjectedXId")]
        public int? ProjectedXid { get; set; }
        [Column("ProjectedYId")]
        public int? ProjectedYid { get; set; }
        public int? TargetId { get; set; }
        public int? TargetSectionId { get; set; }

        [ForeignKey(nameof(LatitudeId))]
        [InverseProperty(nameof(TargetLatitudes.TargetLocations))]
        public virtual TargetLatitudes Latitude { get; set; }
        [ForeignKey(nameof(LongitudeId))]
        [InverseProperty(nameof(TargetLongitudes.TargetLocations))]
        public virtual TargetLongitudes Longitude { get; set; }
        [ForeignKey(nameof(ProjectedXid))]
        [InverseProperty(nameof(TargetProjectedXs.TargetLocations))]
        public virtual TargetProjectedXs ProjectedX { get; set; }
        [ForeignKey(nameof(ProjectedYid))]
        [InverseProperty(nameof(TargetProjectedYs.TargetLocations))]
        public virtual TargetProjectedYs ProjectedY { get; set; }
        [ForeignKey(nameof(TargetId))]
        [InverseProperty(nameof(Targets.TargetLocations))]
        public virtual Targets Target { get; set; }
        [ForeignKey(nameof(TargetSectionId))]
        [InverseProperty(nameof(TargetSections.TargetLocations))]
        public virtual TargetSections TargetSection { get; set; }
        [ForeignKey(nameof(WellCrsid))]
        [InverseProperty(nameof(TargetWellCrss.TargetLocations))]
        public virtual TargetWellCrss WellCrs { get; set; }
    }
}
