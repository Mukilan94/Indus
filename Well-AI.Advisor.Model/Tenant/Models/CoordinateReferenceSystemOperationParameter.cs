using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemOperationParameter
    {
        public CoordinateReferenceSystemOperationParameter()
        {
            CoordinateReferenceSystemUsesParameter = new HashSet<CoordinateReferenceSystemUsesParameter>();
        }

        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }

        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemOperationParameter))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemOperationParameter))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [InverseProperty("OperationParameter")]
        public virtual ICollection<CoordinateReferenceSystemUsesParameter> CoordinateReferenceSystemUsesParameter { get; set; }
    }
}
