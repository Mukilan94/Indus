using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemConversion
    {
        public CoordinateReferenceSystemConversion()
        {
            CoordinateReferenceSystemDefinedByConversion = new HashSet<CoordinateReferenceSystemDefinedByConversion>();
            CoordinateReferenceSystemUsesValue = new HashSet<CoordinateReferenceSystemUsesValue>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }
        public string Scope { get; set; }
        public string UsesMethodId { get; set; }

        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemConversion))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemConversion))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(UsesMethodId))]
        [InverseProperty(nameof(CoordinateReferenceSystemUsesMethod.CoordinateReferenceSystemConversion))]
        public virtual CoordinateReferenceSystemUsesMethod UsesMethod { get; set; }
        [InverseProperty("Conversion")]
        public virtual ICollection<CoordinateReferenceSystemDefinedByConversion> CoordinateReferenceSystemDefinedByConversion { get; set; }
        [InverseProperty("Conversion")]
        public virtual ICollection<CoordinateReferenceSystemUsesValue> CoordinateReferenceSystemUsesValue { get; set; }
    }
}
