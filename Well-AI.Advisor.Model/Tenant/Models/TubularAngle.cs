using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularAngle
    {
        public TubularAngle()
        {
            TubularBend = new HashSet<TubularBend>();
        }

        [Key]
        public int AngleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Angle")]
        public virtual ICollection<TubularBend> TubularBend { get; set; }
    }
}
