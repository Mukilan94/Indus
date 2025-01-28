using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallCoreCommonData
    {
        public SideWallCoreCommonData()
        {
            SidewallCores = new HashSet<SidewallCores>();
        }

        [Key]
        public int SidewallCoresCommonDataid { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataSidewallCoresCommonData")]
        public virtual ICollection<SidewallCores> SidewallCores { get; set; }
    }
}
