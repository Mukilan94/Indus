using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CoordinateReferenceSystemNameCRS")]
    public partial class CoordinateReferenceSystemNameCrs
    {
        public CoordinateReferenceSystemNameCrs()
        {
            CoordinateReferenceSystemGeodeticCrs = new HashSet<CoordinateReferenceSystemGeodeticCrs>();
            CoordinateReferenceSystemVerticalCrs = new HashSet<CoordinateReferenceSystemVerticalCrs>();
        }

        [Key]
        public string Code { get; set; }
        public string NamingSystem { get; set; }
        public string Text { get; set; }

        [InverseProperty("NameCrscodeNavigation")]
        public virtual ICollection<CoordinateReferenceSystemGeodeticCrs> CoordinateReferenceSystemGeodeticCrs { get; set; }
        [InverseProperty("NameCrscodeNavigation")]
        public virtual ICollection<CoordinateReferenceSystemVerticalCrs> CoordinateReferenceSystemVerticalCrs { get; set; }
    }
}
