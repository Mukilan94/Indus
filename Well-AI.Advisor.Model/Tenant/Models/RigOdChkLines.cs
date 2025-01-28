using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigOdChkLines
    {
        public RigOdChkLines()
        {
            RigBops = new HashSet<RigBops>();
        }

        [Key]
        public int OdChkLineId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OdChkLine")]
        public virtual ICollection<RigBops> RigBops { get; set; }
    }
}
