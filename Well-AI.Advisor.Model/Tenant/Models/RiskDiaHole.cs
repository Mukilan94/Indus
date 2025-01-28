using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RiskDiaHole
    {
        public RiskDiaHole()
        {
            Risks = new HashSet<Risks>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaHoleUomNavigation")]
        public virtual ICollection<Risks> Risks { get; set; }
    }
}
