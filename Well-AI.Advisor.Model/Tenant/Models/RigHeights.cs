using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigHeights
    {
        public RigHeights()
        {
            RigDegassers = new HashSet<RigDegassers>();
        }

        [Key]
        public int HeightId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Height")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
    }
}
