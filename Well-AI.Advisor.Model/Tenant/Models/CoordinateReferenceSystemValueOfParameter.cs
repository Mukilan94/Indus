using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemValueOfParameter
    {
        public CoordinateReferenceSystemValueOfParameter()
        {
            CoordinateReferenceSystemParameterValue = new HashSet<CoordinateReferenceSystemParameterValue>();
        }

        [Key]
        public int ValueOfParameterId { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string Xlink { get; set; }

        [InverseProperty("ValueOfParameterId1Navigation")]
        public virtual ICollection<CoordinateReferenceSystemParameterValue> CoordinateReferenceSystemParameterValue { get; set; }
    }
}
