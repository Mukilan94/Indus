using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CoordinateReferenceSystemAxisDirection
    {
        public CoordinateReferenceSystemAxisDirection()
        {
            CoordinateReferenceSystemCoordinateSystemAxis = new HashSet<CoordinateReferenceSystemCoordinateSystemAxis>();
        }

        [Key]
        public string CodeSpace { get; set; }
        public string Text { get; set; }

        [InverseProperty("AxisDirectionCodeSpaceNavigation")]
        public virtual ICollection<CoordinateReferenceSystemCoordinateSystemAxis> CoordinateReferenceSystemCoordinateSystemAxis { get; set; }
    }
}
