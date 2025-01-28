using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetLenRadiuss
    {
        public TargetLenRadiuss()
        {
            TargetSections = new HashSet<TargetSections>();
        }

        [Key]
        public int LenRadiusId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LenRadius")]
        public virtual ICollection<TargetSections> TargetSections { get; set; }
    }
}
