using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellReferencePoints
    {
        public WellReferencePoints()
        {
            WellLocations = new HashSet<WellLocations>();
        }

        [Key]
        public int ReferencePointId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Uid { get; set; }
        public int? ElevationId { get; set; }
        public int? MeasuredDepthId { get; set; }
        public int? WellId { get; set; }

        [ForeignKey(nameof(ElevationId))]
        [InverseProperty(nameof(WellElevations.WellReferencePoints))]
        public virtual WellElevations Elevation { get; set; }
        [ForeignKey(nameof(MeasuredDepthId))]
        [InverseProperty(nameof(WellMeasuredDepths.WellReferencePoints))]
        public virtual WellMeasuredDepths MeasuredDepth { get; set; }
        [ForeignKey(nameof(WellId))]
        [InverseProperty(nameof(Wells.WellReferencePoints))]
        public virtual Wells Well { get; set; }
        [InverseProperty("WellReferencePointReferencePoint")]
        public virtual ICollection<WellLocations> WellLocations { get; set; }
    }
}
