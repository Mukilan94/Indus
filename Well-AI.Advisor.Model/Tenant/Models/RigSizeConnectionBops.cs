using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigSizeConnectionBops
    {
        public RigSizeConnectionBops()
        {
            RigBops = new HashSet<RigBops>();
        }

        [Key]
        public int SizeConnectionBopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SizeConnectionBop")]
        public virtual ICollection<RigBops> RigBops { get; set; }
    }
}
