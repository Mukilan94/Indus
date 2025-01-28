using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigOdRods
    {
        public RigOdRods()
        {
            RigPumps = new HashSet<RigPumps>();
        }

        [Key]
        public int OdRodId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OdRod")]
        public virtual ICollection<RigPumps> RigPumps { get; set; }
    }
}
