using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemGreenwichLongitude
    {
        public CoordinateReferenceSystemGreenwichLongitude()
        {
            CoordinateReferenceSystemPrimeMeridian = new HashSet<CoordinateReferenceSystemPrimeMeridian>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GreenwichLongitudeUomNavigation")]
        public virtual ICollection<CoordinateReferenceSystemPrimeMeridian> CoordinateReferenceSystemPrimeMeridian { get; set; }
    }
}
