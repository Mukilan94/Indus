using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemName
    {
        public CoordinateReferenceSystemName()
        {
            CoordinateReferenceSystemCartesianCs = new HashSet<CoordinateReferenceSystemCartesianCs>();
            CoordinateReferenceSystemConversion = new HashSet<CoordinateReferenceSystemConversion>();
            CoordinateReferenceSystemEllipsoidalCs = new HashSet<CoordinateReferenceSystemEllipsoidalCs>();
            CoordinateReferenceSystemGeographicCrs = new HashSet<CoordinateReferenceSystemGeographicCrs>();
            CoordinateReferenceSystemGmlGeodeticCrs = new HashSet<CoordinateReferenceSystemGmlGeodeticCrs>();
            CoordinateReferenceSystemOperationMethod = new HashSet<CoordinateReferenceSystemOperationMethod>();
            CoordinateReferenceSystemOperationParameter = new HashSet<CoordinateReferenceSystemOperationParameter>();
            CoordinateReferenceSystemPrimeMeridian = new HashSet<CoordinateReferenceSystemPrimeMeridian>();
            CoordinateReferenceSystemProjectedCrs = new HashSet<CoordinateReferenceSystemProjectedCrs>();
        }

        [Key]
        public string CodeSpace { get; set; }
        public string Text { get; set; }
        public string EllipsoidId { get; set; }
        public string GeodeticDatumId { get; set; }

        [ForeignKey(nameof(EllipsoidId))]
        [InverseProperty(nameof(CoordinateReferenceSystemEllipsoid.CoordinateReferenceSystemName))]
        public virtual CoordinateReferenceSystemEllipsoid Ellipsoid { get; set; }
        [ForeignKey(nameof(GeodeticDatumId))]
        [InverseProperty(nameof(CoordinateReferenceSystemGeodeticDatum.CoordinateReferenceSystemName))]
        public virtual CoordinateReferenceSystemGeodeticDatum GeodeticDatum { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemCartesianCs> CoordinateReferenceSystemCartesianCs { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemConversion> CoordinateReferenceSystemConversion { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemEllipsoidalCs> CoordinateReferenceSystemEllipsoidalCs { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemGeographicCrs> CoordinateReferenceSystemGeographicCrs { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemGmlGeodeticCrs> CoordinateReferenceSystemGmlGeodeticCrs { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemOperationMethod> CoordinateReferenceSystemOperationMethod { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemOperationParameter> CoordinateReferenceSystemOperationParameter { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemPrimeMeridian> CoordinateReferenceSystemPrimeMeridian { get; set; }
        [InverseProperty("NameCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemProjectedCrs> CoordinateReferenceSystemProjectedCrs { get; set; }
    }
}
