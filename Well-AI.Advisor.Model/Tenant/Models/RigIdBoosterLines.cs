using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigIdBoosterLines
    {
        public RigIdBoosterLines()
        {
            RigBops = new HashSet<RigBops>();
        }

        [Key]
        public int IdBoosterLineId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("IdBoosterLine")]
        public virtual ICollection<RigBops> RigBops { get; set; }
    }
}
