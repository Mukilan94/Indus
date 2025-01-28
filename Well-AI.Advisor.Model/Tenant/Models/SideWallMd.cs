using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallMd
    {
        public SideWallMd()
        {
            SideWallCoreSwcSample = new HashSet<SideWallCoreSwcSample>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdUomNavigation")]
        public virtual ICollection<SideWallCoreSwcSample> SideWallCoreSwcSample { get; set; }
    }
}
