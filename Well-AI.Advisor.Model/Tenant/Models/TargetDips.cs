using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetDips
    {
        public TargetDips()
        {
            Targets = new HashSet<Targets>();
        }

        [Key]
        public int DipId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Dip")]
        public virtual ICollection<Targets> Targets { get; set; }
    }
}
