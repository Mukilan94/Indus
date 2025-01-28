using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigPresDamps
    {
        public RigPresDamps()
        {
            RigPumps = new HashSet<RigPumps>();
        }

        [Key]
        public int PresDampId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresDamp")]
        public virtual ICollection<RigPumps> RigPumps { get; set; }
    }
}
