using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemValues
    {
        public CoordinateReferenceSystemValues()
        {
            CoordinateReferenceSystemParameterValue = new HashSet<CoordinateReferenceSystemParameterValue>();
        }

        [Key]
        public int ValueId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Value")]
        public virtual ICollection<CoordinateReferenceSystemParameterValue> CoordinateReferenceSystemParameterValue { get; set; }
    }
}
