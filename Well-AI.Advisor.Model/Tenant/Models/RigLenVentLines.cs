using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigLenVentLines
    {
        public RigLenVentLines()
        {
            RigDegassers = new HashSet<RigDegassers>();
        }

        [Key]
        public int LenVentLineId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LenVentLine")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
    }
}
