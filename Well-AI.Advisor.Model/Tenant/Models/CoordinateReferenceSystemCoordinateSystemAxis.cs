using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemCoordinateSystemAxis
    {
        public CoordinateReferenceSystemCoordinateSystemAxis()
        {
            CoordinateReferenceSystemUsesAxis = new HashSet<CoordinateReferenceSystemUsesAxis>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string Name { get; set; }
        public string AxisAbbrev { get; set; }
        public string AxisDirectionCodeSpace { get; set; }
        public string Uom { get; set; }

        [ForeignKey(nameof(AxisDirectionCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemAxisDirection.CoordinateReferenceSystemCoordinateSystemAxis))]
        public virtual CoordinateReferenceSystemAxisDirection AxisDirectionCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemCoordinateSystemAxis))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [InverseProperty("CoordinateSystemAxis")]
        public virtual ICollection<CoordinateReferenceSystemUsesAxis> CoordinateReferenceSystemUsesAxis { get; set; }
    }
}
