using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemOperationMethod
    {
        public CoordinateReferenceSystemOperationMethod()
        {
            CoordinateReferenceSystemUsesMethod = new HashSet<CoordinateReferenceSystemUsesMethod>();
            CoordinateReferenceSystemUsesParameter = new HashSet<CoordinateReferenceSystemUsesParameter>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }
        public string MethodFormula { get; set; }
        public string SourceDimensions { get; set; }
        public string TargetDimensions { get; set; }

        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemOperationMethod))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemOperationMethod))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [InverseProperty("OperationMethod")]
        public virtual ICollection<CoordinateReferenceSystemUsesMethod> CoordinateReferenceSystemUsesMethod { get; set; }
        [InverseProperty("OperationMethod")]
        public virtual ICollection<CoordinateReferenceSystemUsesParameter> CoordinateReferenceSystemUsesParameter { get; set; }
    }
}
