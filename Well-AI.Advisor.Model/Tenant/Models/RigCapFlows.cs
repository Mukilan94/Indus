using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigCapFlows
    {
        public RigCapFlows()
        {
            RigCentrifuges = new HashSet<RigCentrifuges>();
            RigDegassers = new HashSet<RigDegassers>();
            RigShakers = new HashSet<RigShakers>();
        }

        [Key]
        public int CapFlowId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CapFlow")]
        public virtual ICollection<RigCentrifuges> RigCentrifuges { get; set; }
        [InverseProperty("CapFlow")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
        [InverseProperty("CapFlow")]
        public virtual ICollection<RigShakers> RigShakers { get; set; }
    }
}
