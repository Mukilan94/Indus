using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemPrimeMeridian
    {
        public CoordinateReferenceSystemPrimeMeridian()
        {
            CoordinateReferenceSystemUsesPrimeMeridian = new HashSet<CoordinateReferenceSystemUsesPrimeMeridian>();
        }

        [Key]
        public string Id { get; set; }
        public string IdentifierCodeSpace { get; set; }
        public string NameCodeSpace { get; set; }
        public string GreenwichLongitudeUom { get; set; }

        [ForeignKey(nameof(GreenwichLongitudeUom))]
        [InverseProperty(nameof(CoordinateReferenceSystemGreenwichLongitude.CoordinateReferenceSystemPrimeMeridian))]
        public virtual CoordinateReferenceSystemGreenwichLongitude GreenwichLongitudeUomNavigation { get; set; }
        [ForeignKey(nameof(IdentifierCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemIdentifier.CoordinateReferenceSystemPrimeMeridian))]
        public virtual CoordinateReferenceSystemIdentifier IdentifierCodeSpaceNavigation { get; set; }
        [ForeignKey(nameof(NameCodeSpace))]
        [InverseProperty(nameof(CoordinateReferenceSystemName.CoordinateReferenceSystemPrimeMeridian))]
        public virtual CoordinateReferenceSystemName NameCodeSpaceNavigation { get; set; }
        [InverseProperty("PrimeMeridian")]
        public virtual ICollection<CoordinateReferenceSystemUsesPrimeMeridian> CoordinateReferenceSystemUsesPrimeMeridian { get; set; }
    }
}
