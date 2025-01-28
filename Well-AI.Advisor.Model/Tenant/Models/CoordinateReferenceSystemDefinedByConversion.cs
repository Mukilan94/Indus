using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemDefinedByConversion
    {
        public CoordinateReferenceSystemDefinedByConversion()
        {
            CoordinateReferenceSystemProjectedCrs = new HashSet<CoordinateReferenceSystemProjectedCrs>();
        }

        [Key]
        public string DefinedByConversionId { get; set; }
        public string ConversionId { get; set; }

        [ForeignKey(nameof(ConversionId))]
        [InverseProperty(nameof(CoordinateReferenceSystemConversion.CoordinateReferenceSystemDefinedByConversion))]
        public virtual CoordinateReferenceSystemConversion Conversion { get; set; }
        [InverseProperty("DefinedByConversion")]
        public virtual ICollection<CoordinateReferenceSystemProjectedCrs> CoordinateReferenceSystemProjectedCrs { get; set; }
    }
}
