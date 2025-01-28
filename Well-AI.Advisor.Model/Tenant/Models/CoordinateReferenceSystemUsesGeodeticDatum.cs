using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemUsesGeodeticDatum
    {
        public CoordinateReferenceSystemUsesGeodeticDatum()
        {
            CoordinateReferenceSystemGeographicCrs = new HashSet<CoordinateReferenceSystemGeographicCrs>();
            CoordinateReferenceSystemGmlGeodeticCrs = new HashSet<CoordinateReferenceSystemGmlGeodeticCrs>();
        }

        [Key]
        public int UsesGeodeticDatumId { get; set; }
        public string GeodeticDatumId { get; set; }

        [ForeignKey(nameof(GeodeticDatumId))]
        [InverseProperty(nameof(CoordinateReferenceSystemGeodeticDatum.CoordinateReferenceSystemUsesGeodeticDatum))]
        public virtual CoordinateReferenceSystemGeodeticDatum GeodeticDatum { get; set; }
        [InverseProperty("UsesGeodeticDatum")]
        public virtual ICollection<CoordinateReferenceSystemGeographicCrs> CoordinateReferenceSystemGeographicCrs { get; set; }
        [InverseProperty("UsesGeodeticDatum")]
        public virtual ICollection<CoordinateReferenceSystemGmlGeodeticCrs> CoordinateReferenceSystemGmlGeodeticCrs { get; set; }
    }
}
