using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemUsesPrimeMeridian
    {
        public CoordinateReferenceSystemUsesPrimeMeridian()
        {
            CoordinateReferenceSystemGeodeticDatum = new HashSet<CoordinateReferenceSystemGeodeticDatum>();
        }

        [Key]
        public int UsesPrimeMeridianId { get; set; }
        public string PrimeMeridianId { get; set; }

        [ForeignKey(nameof(PrimeMeridianId))]
        [InverseProperty(nameof(CoordinateReferenceSystemPrimeMeridian.CoordinateReferenceSystemUsesPrimeMeridian))]
        public virtual CoordinateReferenceSystemPrimeMeridian PrimeMeridian { get; set; }
        [InverseProperty("UsesPrimeMeridian")]
        public virtual ICollection<CoordinateReferenceSystemGeodeticDatum> CoordinateReferenceSystemGeodeticDatum { get; set; }
    }
}
