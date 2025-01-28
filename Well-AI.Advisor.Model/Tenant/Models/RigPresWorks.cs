using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigPresWorks
    {
        public RigPresWorks()
        {
            RigBopComponents = new HashSet<RigBopComponents>();
        }

        [Key]
        public int PresWorkId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresWork")]
        public virtual ICollection<RigBopComponents> RigBopComponents { get; set; }
    }
}
