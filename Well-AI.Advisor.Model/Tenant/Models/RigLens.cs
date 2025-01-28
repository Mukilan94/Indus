using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigLens
    {
        public RigLens()
        {
            RigDegassers = new HashSet<RigDegassers>();
        }

        [Key]
        public int LenId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Len")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
    }
}
