using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigSizeMeshMns
    {
        public RigSizeMeshMns()
        {
            RigShakers = new HashSet<RigShakers>();
        }

        [Key]
        public int SizeMeshMnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SizeMeshMn")]
        public virtual ICollection<RigShakers> RigShakers { get; set; }
    }
}
