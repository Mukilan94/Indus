using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemEllipsoidalCS")]
    public partial class CoordinateReferenceSystemEllipsoidalCs
    {
        public CoordinateReferenceSystemEllipsoidalCs()
        {
            CoordinateReferenceSystemGmlGeodeticCrs = new HashSet<CoordinateReferenceSystemGmlGeodeticCrs>();
            CoordinateReferenceSystemUsesAxis = new HashSet<CoordinateReferenceSystemUsesAxis>();
            CoordinateReferenceSystemUsesEllipsoidalCs = new HashSet<CoordinateReferenceSystemUsesEllipsoidalCs>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }

        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemEllipsoidalCs))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemEllipsoidalCs))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [InverseProperty("EllipsoidalCs")]
        public virtual ICollection<CoordinateReferenceSystemGmlGeodeticCrs> CoordinateReferenceSystemGmlGeodeticCrs { get; set; }
        [InverseProperty("EllipsoidalCs")]
        public virtual ICollection<CoordinateReferenceSystemUsesAxis> CoordinateReferenceSystemUsesAxis { get; set; }
        [InverseProperty("EllipsoidalCs")]
        public virtual ICollection<CoordinateReferenceSystemUsesEllipsoidalCs> CoordinateReferenceSystemUsesEllipsoidalCs { get; set; }
    }
}
