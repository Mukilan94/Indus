using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemIdentifier
    {
        public CoordinateReferenceSystemIdentifier()
        {
            CoordinateReferenceSystemCartesianCs = new HashSet<CoordinateReferenceSystemCartesianCs>();
            CoordinateReferenceSystemConversion = new HashSet<CoordinateReferenceSystemConversion>();
            CoordinateReferenceSystemCoordinateSystemAxis = new HashSet<CoordinateReferenceSystemCoordinateSystemAxis>();
            CoordinateReferenceSystemEllipsoid = new HashSet<CoordinateReferenceSystemEllipsoid>();
            CoordinateReferenceSystemEllipsoidalCs = new HashSet<CoordinateReferenceSystemEllipsoidalCs>();
            CoordinateReferenceSystemGeodeticDatum = new HashSet<CoordinateReferenceSystemGeodeticDatum>();
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

        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemCartesianCs> CoordinateReferenceSystemCartesianCs { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemConversion> CoordinateReferenceSystemConversion { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemCoordinateSystemAxis> CoordinateReferenceSystemCoordinateSystemAxis { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemEllipsoid> CoordinateReferenceSystemEllipsoid { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemEllipsoidalCs> CoordinateReferenceSystemEllipsoidalCs { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemGeodeticDatum> CoordinateReferenceSystemGeodeticDatum { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemGeographicCrs> CoordinateReferenceSystemGeographicCrs { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemGmlGeodeticCrs> CoordinateReferenceSystemGmlGeodeticCrs { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemOperationMethod> CoordinateReferenceSystemOperationMethod { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemOperationParameter> CoordinateReferenceSystemOperationParameter { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemPrimeMeridian> CoordinateReferenceSystemPrimeMeridian { get; set; }
        [InverseProperty("IdentifierCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemProjectedCrs> CoordinateReferenceSystemProjectedCrs { get; set; }
    }
}
