using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemGeographicCRS")]
    public partial class CoordinateReferenceSystemGeographicCrs
    {
        public CoordinateReferenceSystemGeographicCrs()
        {
            CoordinateReferenceSystemBaseGeographicCrs = new HashSet<CoordinateReferenceSystemBaseGeographicCrs>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }
        public string Scope { get; set; }
        [Column("UsesEllipsoidalCSEllipsId")]
        public string UsesEllipsoidalCsellipsId { get; set; }
        public int? UsesGeodeticDatumId { get; set; }

        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemGeographicCrs))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemGeographicCrs))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(UsesEllipsoidalCsellipsId))]
        [InverseProperty(nameof(CoordinateReferenceSystemUsesEllipsoidalCs.CoordinateReferenceSystemGeographicCrs))]
        public virtual CoordinateReferenceSystemUsesEllipsoidalCs UsesEllipsoidalCsellips { get; set; }
        [ForeignKey(nameof(UsesGeodeticDatumId))]
        [InverseProperty(nameof(CoordinateReferenceSystemUsesGeodeticDatum.CoordinateReferenceSystemGeographicCrs))]
        public virtual CoordinateReferenceSystemUsesGeodeticDatum UsesGeodeticDatum { get; set; }
        [InverseProperty("GeographicCrs")]
        public virtual ICollection<CoordinateReferenceSystemBaseGeographicCrs> CoordinateReferenceSystemBaseGeographicCrs { get; set; }
    }
}
