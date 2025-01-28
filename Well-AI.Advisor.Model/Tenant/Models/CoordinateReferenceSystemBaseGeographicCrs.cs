using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemBaseGeographicCRS")]
    public partial class CoordinateReferenceSystemBaseGeographicCrs
    {
        public CoordinateReferenceSystemBaseGeographicCrs()
        {
            CoordinateReferenceSystemProjectedCrs = new HashSet<CoordinateReferenceSystemProjectedCrs>();
        }

        [Key]
        [Column("BaseGeographicCRSId")]
        public int BaseGeographicCrsid { get; set; }
        [Column("GeographicCRSId")]
        public string GeographicCrsid { get; set; }

        [ForeignKey(nameof(GeographicCrsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemGeographicCrs.CoordinateReferenceSystemBaseGeographicCrs))]
        public virtual CoordinateReferenceSystemGeographicCrs GeographicCrs { get; set; }
        [InverseProperty("BaseGeographicCrs")]
        public virtual ICollection<CoordinateReferenceSystemProjectedCrs> CoordinateReferenceSystemProjectedCrs { get; set; }
    }
}
