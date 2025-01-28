using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigDiaCloseMxs
    {
        public RigDiaCloseMxs()
        {
            RigBopComponents = new HashSet<RigBopComponents>();
        }

        [Key]
        public int DiaCloseMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaCloseMx")]
        public virtual ICollection<RigBopComponents> RigBopComponents { get; set; }
    }
}
