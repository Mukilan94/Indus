using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemGeodeticCRS")]
    public partial class CoordinateReferenceSystemGeodeticCrs
    {
        public CoordinateReferenceSystemGeodeticCrs()
        {
            CoordinateReferenceSystem = new HashSet<CoordinateReferenceSystem>();
        }

        [Key]
        [Column("CoRefGeodeticCRSId")]
        public int CoRefGeodeticCrsid { get; set; }
        [Column("NameCRSCode")]
        public string NameCrscode { get; set; }
        [Column("GmlGeodeticCRSId")]
        public string GmlGeodeticCrsid { get; set; }

        [ForeignKey(nameof(GmlGeodeticCrsid))]
        [InverseProperty(nameof(CoordinateReferenceSystemGmlGeodeticCrs.CoordinateReferenceSystemGeodeticCrs))]
        public virtual CoordinateReferenceSystemGmlGeodeticCrs GmlGeodeticCrs { get; set; }
        [ForeignKey(nameof(NameCrscode))]
        [InverseProperty(nameof(CoordinateReferenceSystemNameCrs.CoordinateReferenceSystemGeodeticCrs))]
        public virtual CoordinateReferenceSystemNameCrs NameCrscodeNavigation { get; set; }
        [InverseProperty("GeodeticCrscoRefGeodeticCrs")]
        public virtual ICollection<CoordinateReferenceSystem> CoordinateReferenceSystem { get; set; }
    }
}
