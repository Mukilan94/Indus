using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemVerticalCRS")]
    public partial class CoordinateReferenceSystemVerticalCrs
    {
        public CoordinateReferenceSystemVerticalCrs()
        {
            CoordinateReferenceSystem = new HashSet<CoordinateReferenceSystem>();
        }

        [Key]
        [Column("VerticalCRSId")]
        public int VerticalCrsid { get; set; }
        [Column("NameCRSCode")]
        public string NameCrscode { get; set; }

        [ForeignKey(nameof(NameCrscode))]
        [InverseProperty(nameof(CoordinateReferenceSystemNameCrs.CoordinateReferenceSystemVerticalCrs))]
        public virtual CoordinateReferenceSystemNameCrs NameCrscodeNavigation { get; set; }
        [InverseProperty("VerticalCrs")]
        public virtual ICollection<CoordinateReferenceSystem> CoordinateReferenceSystem { get; set; }
    }
}
