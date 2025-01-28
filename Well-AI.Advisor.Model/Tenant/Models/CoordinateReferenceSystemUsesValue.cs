using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemUsesValue
    {
        [Key]
        public int UsesValueId { get; set; }
        public int? ParameterValueValueOfParameterId { get; set; }
        public string ConversionId { get; set; }

        [ForeignKey(nameof(ConversionId))]
        [InverseProperty(nameof(CoordinateReferenceSystemConversion.CoordinateReferenceSystemUsesValue))]
        public virtual CoordinateReferenceSystemConversion Conversion { get; set; }
        [ForeignKey(nameof(ParameterValueValueOfParameterId))]
        [InverseProperty(nameof(CoordinateReferenceSystemParameterValue.CoordinateReferenceSystemUsesValue))]
        public virtual CoordinateReferenceSystemParameterValue ParameterValueValueOfParameter { get; set; }
    }
}
