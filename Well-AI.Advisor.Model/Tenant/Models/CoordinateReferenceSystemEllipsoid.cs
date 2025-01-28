using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemEllipsoid
    {
        public CoordinateReferenceSystemEllipsoid()
        {
            CoordinateReferenceSystemName = new HashSet<CoordinateReferenceSystemName>();
            CoordinateReferenceSystemUsesEllipsoid = new HashSet<CoordinateReferenceSystemUsesEllipsoid>();
        }

        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string SemiMajorAxisUom { get; set; }
        public int? SecondDefiningParameterId { get; set; }

        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemEllipsoid))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(SecondDefiningParameterId))]
        [InverseProperty(nameof(CoordinateReferenceSystemSecondDefiningParameter.CoordinateReferenceSystemEllipsoid))]
        public virtual CoordinateReferenceSystemSecondDefiningParameter SecondDefiningParameter { get; set; }
        [ForeignKey(nameof(SemiMajorAxisUom))]
        [InverseProperty(nameof(CoordinateReferenceSystemSemiMajorAxis.CoordinateReferenceSystemEllipsoid))]
        public virtual CoordinateReferenceSystemSemiMajorAxis SemiMajorAxisUomNavigation { get; set; }
        [InverseProperty("Ellipsoid")]
        public virtual ICollection<CoordinateReferenceSystemName> CoordinateReferenceSystemName { get; set; }
        [InverseProperty("Ellipsoid")]
        public virtual ICollection<CoordinateReferenceSystemUsesEllipsoid> CoordinateReferenceSystemUsesEllipsoid { get; set; }
    }
}
