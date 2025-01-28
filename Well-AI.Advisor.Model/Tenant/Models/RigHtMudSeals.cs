using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigHtMudSeals
    {
        public RigHtMudSeals()
        {
            RigDegassers = new HashSet<RigDegassers>();
        }

        [Key]
        public int HtMudSealId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("HtMudSeal")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
    }
}
