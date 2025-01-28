using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetAngleArcs
    {
        public TargetAngleArcs()
        {
            TargetSections = new HashSet<TargetSections>();
        }

        [Key]
        public int AngleArcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AngleArc")]
        public virtual ICollection<TargetSections> TargetSections { get; set; }
    }
}
