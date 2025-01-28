using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DiaPlug
    {
        public DiaPlug()
        {
            SidewallCores = new HashSet<SidewallCores>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaPlugUomNavigation")]
        public virtual ICollection<SidewallCores> SidewallCores { get; set; }
    }
}
