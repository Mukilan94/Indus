using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemInverseFlattening
    {
        public CoordinateReferenceSystemInverseFlattening()
        {
            CoordinateReferenceSystemSecondDefiningParameter = new HashSet<CoordinateReferenceSystemSecondDefiningParameter>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("InverseFlatteningUomNavigation")]
        public virtual ICollection<CoordinateReferenceSystemSecondDefiningParameter> CoordinateReferenceSystemSecondDefiningParameter { get; set; }
    }
}
