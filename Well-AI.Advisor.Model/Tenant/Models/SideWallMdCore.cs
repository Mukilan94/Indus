using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallMdCore
    {
        public SideWallMdCore()
        {
            SidewallCores = new HashSet<SidewallCores>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdCoreUomNavigation")]
        public virtual ICollection<SidewallCores> SidewallCores { get; set; }
    }
}
