using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystem
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        [Column("GeodeticCRSCoRefGeodeticCRSId")]
        public int? GeodeticCrscoRefGeodeticCrsid { get; set; }
        [Column("ProjectedCRSId")]
        public string ProjectedCrsid { get; set; }
        [Column("VerticalCRSId")]
        public int? VerticalCrsid { get; set; }
        public string Name2 { get; set; }

        [ForeignKey(nameof(GeodeticCrscoRefGeodeticCrsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemGeodeticCrs.CoordinateReferenceSystem))]
        public virtual CoordinateReferenceSystemGeodeticCrs GeodeticCrscoRefGeodeticCrs { get; set; }
        [ForeignKey(nameof(ProjectedCrsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemProjectedCrs.CoordinateReferenceSystem))]
        public virtual CoordinateReferenceSystemProjectedCrs ProjectedCrs { get; set; }
        [ForeignKey(nameof(VerticalCrsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemVerticalCrs.CoordinateReferenceSystem))]
        public virtual CoordinateReferenceSystemVerticalCrs VerticalCrs { get; set; }
    }
}
