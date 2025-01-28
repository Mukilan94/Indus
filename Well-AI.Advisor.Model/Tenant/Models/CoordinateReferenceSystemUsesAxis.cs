using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemUsesAxis
    {
        [Key]
        public int UsesAxisId { get; set; }
        public string CoordinateSystemAxisId { get; set; }
        [Column("CartesianCSId")]
        public string CartesianCsid { get; set; }
        [Column("EllipsoidalCSId")]
        public string EllipsoidalCsid { get; set; }

        [ForeignKey(nameof(CartesianCsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemCartesianCs.CoordinateReferenceSystemUsesAxis))]
        public virtual CoordinateReferenceSystemCartesianCs CartesianCs { get; set; }
        [ForeignKey(nameof(CoordinateSystemAxisId))]
        [InverseProperty(nameof(CoordinateReferenceSystemCoordinateSystemAxis.CoordinateReferenceSystemUsesAxis))]
        public virtual CoordinateReferenceSystemCoordinateSystemAxis CoordinateSystemAxis { get; set; }
        [ForeignKey(nameof(EllipsoidalCsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemEllipsoidalCs.CoordinateReferenceSystemUsesAxis))]
        public virtual CoordinateReferenceSystemEllipsoidalCs EllipsoidalCs { get; set; }
    }
}
