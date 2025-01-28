using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemParameterValue
    {
        public CoordinateReferenceSystemParameterValue()
        {
            CoordinateReferenceSystemUsesValue = new HashSet<CoordinateReferenceSystemUsesValue>();
        }

        [Key]
        public int ValueOfParameterId { get; set; }
        public int? ValueId { get; set; }
        public int? ValueOfParameterId1 { get; set; }

        [ForeignKey(nameof(ValueId))]
        [InverseProperty(nameof(CoordinateReferenceSystemValues.CoordinateReferenceSystemParameterValue))]
        public virtual CoordinateReferenceSystemValues Value { get; set; }
        [ForeignKey(nameof(ValueOfParameterId1))]
        [InverseProperty(nameof(CoordinateReferenceSystemValueOfParameter.CoordinateReferenceSystemParameterValue))]
        public virtual CoordinateReferenceSystemValueOfParameter ValueOfParameterId1Navigation { get; set; }
        [InverseProperty("ParameterValueValueOfParameter")]
        public virtual ICollection<CoordinateReferenceSystemUsesValue> CoordinateReferenceSystemUsesValue { get; set; }
    }
}
