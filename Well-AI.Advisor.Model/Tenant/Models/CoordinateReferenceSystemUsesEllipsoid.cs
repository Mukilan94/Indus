using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemUsesEllipsoid
    {
        public CoordinateReferenceSystemUsesEllipsoid()
        {
            CoordinateReferenceSystemGeodeticDatum = new HashSet<CoordinateReferenceSystemGeodeticDatum>();
        }

        [Key]
        public string Id { get; set; }
        public string EllipsoidId { get; set; }

        [ForeignKey(nameof(EllipsoidId))]
        [InverseProperty(nameof(CoordinateReferenceSystemEllipsoid.CoordinateReferenceSystemUsesEllipsoid))]
        public virtual CoordinateReferenceSystemEllipsoid Ellipsoid { get; set; }
        [InverseProperty("UsesEllipsoid")]
        public virtual ICollection<CoordinateReferenceSystemGeodeticDatum> CoordinateReferenceSystemGeodeticDatum { get; set; }
    }
}
