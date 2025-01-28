using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetWidMinorAxiss
    {
        public TargetWidMinorAxiss()
        {
            Targets = new HashSet<Targets>();
        }

        [Key]
        public int WidMinorAxisId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("WidMinorAxis")]
        public virtual ICollection<Targets> Targets { get; set; }
    }
}
