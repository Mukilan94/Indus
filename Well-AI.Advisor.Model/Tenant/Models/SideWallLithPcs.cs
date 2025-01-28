using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallLithPcs
    {
        public SideWallLithPcs()
        {
            SideWallCoreLithology = new HashSet<SideWallCoreLithology>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LithPcUomNavigation")]
        public virtual ICollection<SideWallCoreLithology> SideWallCoreLithology { get; set; }
    }
}
