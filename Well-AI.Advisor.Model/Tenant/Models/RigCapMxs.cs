using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigCapMxs
    {
        public RigCapMxs()
        {
            RigPits = new HashSet<RigPits>();
        }

        [Key]
        public int CapMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CapMx")]
        public virtual ICollection<RigPits> RigPits { get; set; }
    }
}
