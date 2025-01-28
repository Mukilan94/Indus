using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemUsesParameter
    {
        [Key]
        public int UsesParameterId { get; set; }
        public string OperationParameterId { get; set; }
        public string OperationMethodId { get; set; }

        [ForeignKey(nameof(OperationMethodId))]
        [InverseProperty(nameof(CoordinateReferenceSystemOperationMethod.CoordinateReferenceSystemUsesParameter))]
        public virtual CoordinateReferenceSystemOperationMethod OperationMethod { get; set; }
        [ForeignKey(nameof(OperationParameterId))]
        [InverseProperty(nameof(CoordinateReferenceSystemOperationParameter.CoordinateReferenceSystemUsesParameter))]
        public virtual CoordinateReferenceSystemOperationParameter OperationParameter { get; set; }
    }
}
