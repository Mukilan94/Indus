using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemUsesCartesianCS")]
    public partial class CoordinateReferenceSystemUsesCartesianCs
    {
        public CoordinateReferenceSystemUsesCartesianCs()
        {
            CoordinateReferenceSystemProjectedCrs = new HashSet<CoordinateReferenceSystemProjectedCrs>();
        }

        [Key]
        public int Id { get; set; }
        [Column("CartesianCSId")]
        public string CartesianCsid { get; set; }

        [ForeignKey(nameof(CartesianCsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemCartesianCs.CoordinateReferenceSystemUsesCartesianCs))]
        public virtual CoordinateReferenceSystemCartesianCs CartesianCs { get; set; }
        [InverseProperty("UsesCartesianCs")]
        public virtual ICollection<CoordinateReferenceSystemProjectedCrs> CoordinateReferenceSystemProjectedCrs { get; set; }
    }
}
