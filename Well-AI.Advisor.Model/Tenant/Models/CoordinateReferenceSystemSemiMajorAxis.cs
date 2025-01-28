using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemSemiMajorAxis
    {
        public CoordinateReferenceSystemSemiMajorAxis()
        {
            CoordinateReferenceSystemEllipsoid = new HashSet<CoordinateReferenceSystemEllipsoid>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SemiMajorAxisUomNavigation")]
        public virtual ICollection<CoordinateReferenceSystemEllipsoid> CoordinateReferenceSystemEllipsoid { get; set; }
    }
}
