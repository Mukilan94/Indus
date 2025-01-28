using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemGmlGeodeticCRS")]
    public partial class CoordinateReferenceSystemGmlGeodeticCrs
    {
        public CoordinateReferenceSystemGmlGeodeticCrs()
        {
            CoordinateReferenceSystemGeodeticCrs = new HashSet<CoordinateReferenceSystemGeodeticCrs>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }
        public string Scope { get; set; }
        [Column("EllipsoidalCSId")]
        public string EllipsoidalCsid { get; set; }
        public int? UsesGeodeticDatumId { get; set; }
        public string SchemaLocation { get; set; }
        public string Xsi { get; set; }
        public string Gml { get; set; }

        [ForeignKey(nameof(EllipsoidalCsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemEllipsoidalCs.CoordinateReferenceSystemGmlGeodeticCrs))]
        public virtual CoordinateReferenceSystemEllipsoidalCs EllipsoidalCs { get; set; }
        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemGmlGeodeticCrs))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemGmlGeodeticCrs))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(UsesGeodeticDatumId))]
        [InverseProperty(nameof(CoordinateReferenceSystemUsesGeodeticDatum.CoordinateReferenceSystemGmlGeodeticCrs))]
        public virtual CoordinateReferenceSystemUsesGeodeticDatum UsesGeodeticDatum { get; set; }
        [InverseProperty("GmlGeodeticCrs")]
        public virtual ICollection<CoordinateReferenceSystemGeodeticCrs> CoordinateReferenceSystemGeodeticCrs { get; set; }
    }
}
