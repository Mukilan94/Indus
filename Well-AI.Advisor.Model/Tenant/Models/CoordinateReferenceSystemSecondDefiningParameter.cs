using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemSecondDefiningParameter
    {
        public CoordinateReferenceSystemSecondDefiningParameter()
        {
            CoordinateReferenceSystemEllipsoid = new HashSet<CoordinateReferenceSystemEllipsoid>();
        }

        [Key]
        public int SecondDefiningParameterId { get; set; }
        public string InverseFlatteningUom { get; set; }

        [ForeignKey(nameof(InverseFlatteningUom))]
        [InverseProperty(nameof(CoordinateReferenceSystemInverseFlattening.CoordinateReferenceSystemSecondDefiningParameter))]
        public virtual CoordinateReferenceSystemInverseFlattening InverseFlatteningUomNavigation { get; set; }
        [InverseProperty("SecondDefiningParameter")]
        public virtual ICollection<CoordinateReferenceSystemEllipsoid> CoordinateReferenceSystemEllipsoid { get; set; }
    }
}
