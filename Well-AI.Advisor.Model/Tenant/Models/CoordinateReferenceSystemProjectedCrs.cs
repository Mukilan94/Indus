using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemProjectedCRS")]
    public partial class CoordinateReferenceSystemProjectedCrs
    {
        public CoordinateReferenceSystemProjectedCrs()
        {
            CoordinateReferenceSystem = new HashSet<CoordinateReferenceSystem>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }
        public string Scope { get; set; }
        public string DefinedByConversionId { get; set; }
        [Column("BaseGeographicCRSId")]
        public int? BaseGeographicCrsid { get; set; }
        [Column("UsesCartesianCSId")]
        public int? UsesCartesianCsid { get; set; }
        public string SchemaLocation { get; set; }
        public string Gml { get; set; }

        [ForeignKey(nameof(BaseGeographicCrsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemBaseGeographicCrs.CoordinateReferenceSystemProjectedCrs))]
        public virtual CoordinateReferenceSystemBaseGeographicCrs BaseGeographicCrs { get; set; }
        [ForeignKey(nameof(DefinedByConversionId))]
        [InverseProperty(nameof(CoordinateReferenceSystemDefinedByConversion.CoordinateReferenceSystemProjectedCrs))]
        public virtual CoordinateReferenceSystemDefinedByConversion DefinedByConversion { get; set; }
        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemProjectedCrs))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemProjectedCrs))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(UsesCartesianCsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemUsesCartesianCs.CoordinateReferenceSystemProjectedCrs))]
        public virtual CoordinateReferenceSystemUsesCartesianCs UsesCartesianCs { get; set; }
        [InverseProperty("ProjectedCrs")]
        public virtual ICollection<CoordinateReferenceSystem> CoordinateReferenceSystem { get; set; }
    }
}
