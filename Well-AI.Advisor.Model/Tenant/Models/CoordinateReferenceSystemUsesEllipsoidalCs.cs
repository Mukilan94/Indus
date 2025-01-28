using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemUsesEllipsoidalCS")]
    public partial class CoordinateReferenceSystemUsesEllipsoidalCs
    {
        public CoordinateReferenceSystemUsesEllipsoidalCs()
        {
            CoordinateReferenceSystemGeographicCrs = new HashSet<CoordinateReferenceSystemGeographicCrs>();
        }

        [Key]
        public string EllipsId { get; set; }
        [Column("EllipsoidalCSId")]
        public string EllipsoidalCsid { get; set; }

        [ForeignKey(nameof(EllipsoidalCsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemEllipsoidalCs.CoordinateReferenceSystemUsesEllipsoidalCs))]
        public virtual CoordinateReferenceSystemEllipsoidalCs EllipsoidalCs { get; set; }
        [InverseProperty("UsesEllipsoidalCsellips")]
        public virtual ICollection<CoordinateReferenceSystemGeographicCrs> CoordinateReferenceSystemGeographicCrs { get; set; }
    }
}
