using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetThickAboves
    {
        public TargetThickAboves()
        {
            TargetSections = new HashSet<TargetSections>();
            Targets = new HashSet<Targets>();
        }

        [Key]
        public int ThickAboveId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ThickAbove")]
        public virtual ICollection<TargetSections> TargetSections { get; set; }
        [InverseProperty("ThickAbove")]
        public virtual ICollection<Targets> Targets { get; set; }
    }
}
