using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigCapBlowdowns
    {
        public RigCapBlowdowns()
        {
            RigDegassers = new HashSet<RigDegassers>();
        }

        [Key]
        public int CapBlowdownId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CapBlowdown")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
    }
}
