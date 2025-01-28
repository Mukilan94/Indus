using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetDispNsOffsets
    {
        public TargetDispNsOffsets()
        {
            Targets = new HashSet<Targets>();
        }

        [Key]
        public int DispNsOffsetId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DispNsOffset")]
        public virtual ICollection<Targets> Targets { get; set; }
    }
}
