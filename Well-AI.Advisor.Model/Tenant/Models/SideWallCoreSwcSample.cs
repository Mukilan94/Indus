using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallCoreSwcSample
    {
        public SideWallCoreSwcSample()
        {
            SidewallCores = new HashSet<SidewallCores>();
        }

        [Key]
        public string Uid { get; set; }
        public string MdUom { get; set; }
        public string LithologyUid { get; set; }
        public int? ShowSideWallCoreId { get; set; }
        public string NameFormation { get; set; }
        public string Comments { get; set; }

        [ForeignKey(nameof(LithologyUid))]
        [InverseProperty(nameof(SideWallCoreLithology.SideWallCoreSwcSample))]
        public virtual SideWallCoreLithology LithologyU { get; set; }
        [ForeignKey(nameof(MdUom))]
        [InverseProperty(nameof(SideWallMd.SideWallCoreSwcSample))]
        public virtual SideWallMd MdUomNavigation { get; set; }
        [ForeignKey(nameof(ShowSideWallCoreId))]
        [InverseProperty(nameof(SideWallCoreShow.SideWallCoreSwcSample))]
        public virtual SideWallCoreShow ShowSideWallCore { get; set; }
        [InverseProperty("SwcSampleU")]
        public virtual ICollection<SidewallCores> SidewallCores { get; set; }
    }
}
