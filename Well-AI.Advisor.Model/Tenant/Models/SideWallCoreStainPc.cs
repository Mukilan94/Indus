using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallCoreStainPc
    {
        public SideWallCoreStainPc()
        {
            SideWallCoreShow = new HashSet<SideWallCoreShow>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StainPcUomNavigation")]
        public virtual ICollection<SideWallCoreShow> SideWallCoreShow { get; set; }
    }
}
