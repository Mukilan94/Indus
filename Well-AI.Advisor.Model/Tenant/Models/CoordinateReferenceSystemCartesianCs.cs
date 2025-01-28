using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemCartesianCS")]
    public partial class CoordinateReferenceSystemCartesianCs
    {
        public CoordinateReferenceSystemCartesianCs()
        {
            CoordinateReferenceSystemUsesAxis = new HashSet<CoordinateReferenceSystemUsesAxis>();
            CoordinateReferenceSystemUsesCartesianCs = new HashSet<CoordinateReferenceSystemUsesCartesianCs>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }

        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemCartesianCs))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemCartesianCs))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [InverseProperty("CartesianCs")]
        public virtual ICollection<CoordinateReferenceSystemUsesAxis> CoordinateReferenceSystemUsesAxis { get; set; }
        [InverseProperty("CartesianCs")]
        public virtual ICollection<CoordinateReferenceSystemUsesCartesianCs> CoordinateReferenceSystemUsesCartesianCs { get; set; }
    }
}
