using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemGeodeticDatum
    {
        public CoordinateReferenceSystemGeodeticDatum()
        {
            CoordinateReferenceSystemName = new HashSet<CoordinateReferenceSystemName>();
            CoordinateReferenceSystemUsesGeodeticDatum = new HashSet<CoordinateReferenceSystemUsesGeodeticDatum>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string Scope { get; set; }
        public string AnchorPoint { get; set; }
        public int? UsesPrimeMeridianId { get; set; }
        public string UsesEllipsoidId { get; set; }

        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemGeodeticDatum))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(UsesEllipsoidId))]
        [InverseProperty(nameof(CoordinateReferenceSystemUsesEllipsoid.CoordinateReferenceSystemGeodeticDatum))]
        public virtual CoordinateReferenceSystemUsesEllipsoid UsesEllipsoid { get; set; }
        [ForeignKey(nameof(UsesPrimeMeridianId))]
        [InverseProperty(nameof(CoordinateReferenceSystemUsesPrimeMeridian.CoordinateReferenceSystemGeodeticDatum))]
        public virtual CoordinateReferenceSystemUsesPrimeMeridian UsesPrimeMeridian { get; set; }
        [InverseProperty("GeodeticDatum")]
        public virtual ICollection<CoordinateReferenceSystemName> CoordinateReferenceSystemName { get; set; }
        [InverseProperty("GeodeticDatum")]
        public virtual ICollection<CoordinateReferenceSystemUsesGeodeticDatum> CoordinateReferenceSystemUsesGeodeticDatum { get; set; }
    }
}
