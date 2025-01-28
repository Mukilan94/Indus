using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemUsesMethod
    {
        public CoordinateReferenceSystemUsesMethod()
        {
            CoordinateReferenceSystemConversion = new HashSet<CoordinateReferenceSystemConversion>();
        }

        [Key]
        public string UsesMethodId { get; set; }
        public string OperationMethodId { get; set; }

        [ForeignKey(nameof(OperationMethodId))]
        [InverseProperty(nameof(CoordinateReferenceSystemOperationMethod.CoordinateReferenceSystemUsesMethod))]
        public virtual CoordinateReferenceSystemOperationMethod OperationMethod { get; set; }
        [InverseProperty("UsesMethod")]
        public virtual ICollection<CoordinateReferenceSystemConversion> CoordinateReferenceSystemConversion { get; set; }
    }
}
