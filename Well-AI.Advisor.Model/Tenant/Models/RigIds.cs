using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigIds
    {
        public RigIds()
        {
            RigDegassers = new HashSet<RigDegassers>();
        }

        [Key]
        public int UniqueId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("IdUnique")]
        public virtual ICollection<RigDegassers> RigDegassers { get; set; }
    }
}
