using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetRotations
    {
        public TargetRotations()
        {
            Targets = new HashSet<Targets>();
        }

        [Key]
        public int RotationId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Rotation")]
        public virtual ICollection<Targets> Targets { get; set; }
    }
}
